using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using Wallace.Nate.MetaDataExplorer.CorH;

namespace Wallace.Nate.MetaDataExplorer
{
    /// <summary>
    /// This class is used to explore the metadata for an assembly without having to use the CLR to load it into memory.
    /// Instead this class uses the unmanaged metadata API to read the assembly data from file.
    /// </summary>
    public class AssemblyExplorer : IDisposable
    {
        #region Fields

        /// <summary>
        /// Supports the Dispenser property.
        /// </summary>
        private IMetaDataDispenser _dispenser = null;

        /// <summary>
        /// Supports the Import property.
        /// </summary>
        private IMetaDataImport2 _import = null;

        /// <summary>
        /// When this object has been disposed this field is set to true.
        /// </summary>
        private bool _isDisposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">The path to the assembly file to be explored.</param>
        public AssemblyExplorer(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentException("The specified file doesn't exist: " + path, "path");

            Path = path;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The path to the assembly file that is being explored.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Used to open an assembly file for inspection.
        /// </summary>
        private IMetaDataDispenser Dispenser
        {
            get
            {
                AssertUndisposed();

                if (_dispenser == null)
                {
                    _dispenser = Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_CorMetaDataDispenser)) as IMetaDataDispenser;
                    if (_dispenser == null)
                        throw new Exception("Something went wrong with the creation of the IMetaDataDispenser object and _dispenser is null.");
                }

                return _dispenser;
            }
        }

        /// <summary>
        /// Used to inspect an assembly file.
        /// </summary>
        private IMetaDataImport2 Import
        {
            get
            {
                AssertUndisposed();

                if (_import == null)
                {
                    object ppIUnk = null;
                    Dispenser.OpenScope(
                        Path,
                        CorOpenFlags.ofReadOnly,
                        ref Guids.IID_IMetaDataImport2,
                        out ppIUnk);

                    _import = ppIUnk as IMetaDataImport2;
                    if (_import == null)
                        throw new Exception("Something went wrong with the creation of the IMetaDataImport2 object and _import is null.");
                }

                return _import;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Throws an exception if this object has been disposed.
        /// </summary>
        private void AssertUndisposed()
        {
            if (_isDisposed)
                throw new Exception("This object has been disposed.");
        }

        /// <summary>
        /// Get a list of all of the classes that implement the given interface.
        /// </summary>
        /// <param name="fullInterfaceName">The name of the interface (including namespace) to search for.</param>
        /// <returns>All classes that implement the given interface.</returns>
        public IEnumerable<string> GetClassesThatImplementInterface(string fullInterfaceName)
        {
            IntPtr phEnum = IntPtr.Zero;
            uint[] rTypeDefs = new uint[100];
            uint pcTypeDefs = (uint)rTypeDefs.Length;

            List<string> result = new List<string>();

            while (pcTypeDefs == (uint)rTypeDefs.Length)
            {
                Import.EnumTypeDefs(
                    ref phEnum,
                    rTypeDefs,
                    (uint)rTypeDefs.Length,
                    out pcTypeDefs);

                for (int i = 0; i < pcTypeDefs; i++)
                    if (GetInterfaceImplementationNames(rTypeDefs[i]).Contains(fullInterfaceName))
                        result.Add(GetFullClassName(rTypeDefs[i]));
            }

            Import.CloseEnum(phEnum);

            return result;
        }

        /// <summary>
        /// Get the full name for the given class.
        /// </summary>
        /// <param name="token">The token identifying the class to get the name for.</param>
        /// <returns>The full name for the given class.</returns>
        private string GetFullClassName(uint token)
        {
            uint typeDefOrRef = (token >> 24);

            switch (typeDefOrRef)
            {
                case 2: // typedef

                    StringBuilder szTypeDef = new StringBuilder();
                    uint pchTypeDef = (uint)szTypeDef.Capacity;
                    CorTypeAttr pdwTypeDefFlags = CorTypeAttr.tdNotPublic;
                    uint ptkExtends = 0;

                    while (pchTypeDef == (uint)szTypeDef.Capacity)
                    {
                        szTypeDef.Capacity += 100;

                        Import.GetTypeDefProps(
                            token,
                            szTypeDef,
                            (uint)szTypeDef.Capacity,
                            out pchTypeDef,
                            out pdwTypeDefFlags,
                            out ptkExtends);
                    }

                    return szTypeDef.ToString();

                case 1: // typeref

                    uint ptkResolutionScope = 0;
                    StringBuilder szName = new StringBuilder();
                    uint pchName = (uint)szName.Capacity;

                    while (pchName == (uint)szName.Capacity)
                    {
                        szName.Capacity += 100;

                        Import.GetTypeRefProps(
                            token,
                            out ptkResolutionScope,
                            szName,
                            (uint)szName.Capacity,
                            out pchName);
                    }

                    return szName.ToString();

                default:
                    throw new Exception("Unexpected TypeDefOrRef value.");
            }
        }

        /// <summary>
        /// Get the interfaces that the given class implements.
        /// </summary>
        /// <param name="token">The token identifying the class to get the implemented interfaces from.</param>
        /// <returns>The interfaces that the given class implements</returns>
        private IEnumerable<string> GetInterfaceImplementationNames(uint token)
        {
            List<string> interfaceNames = new List<string>();

            IntPtr phEnum = IntPtr.Zero;   
            uint[] rImpls = new uint[100];
            uint pcImpls = (uint)rImpls.Length;

            while (pcImpls == (uint)rImpls.Length)
            {
                Import.EnumInterfaceImpls(
                    ref phEnum,
                    token,
                    rImpls,
                    (uint)rImpls.Length,
                    out pcImpls);

                for (int i = 0; i < pcImpls; i++)
                {
                    uint pClass = 0;
                    uint ptkIface = 0;
                    
                    Import.GetInterfaceImplProps(
                        rImpls[i],
                        out  pClass,
                        out  ptkIface);

                    interfaceNames.Add(GetFullClassName(ptkIface));
                }
            }

            Import.CloseEnum(phEnum);

            return interfaceNames;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose of this class.
        /// </summary>
        public void Dispose()
        {
            if (_dispenser != null)
                Marshal.ReleaseComObject(_dispenser);

            if (_import != null)
                Marshal.ReleaseComObject(_import);
        }

        #endregion
    }
}
