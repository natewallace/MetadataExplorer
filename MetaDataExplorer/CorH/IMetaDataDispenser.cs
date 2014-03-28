using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Provides methods to create a new metadata scope, or open an existing one.
    /// </summary>
    [ComImport]
    [Guid("809C652E-7396-11D2-9771-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataDispenser
    {
        /// <summary>
        /// Creates a new area in memory in which you can create new metadata.
        /// </summary>
        /// <param name="rclsid">The CLSID of the version of metadata structures to be created. This value must be CLSID_CorMetaDataRuntime.</param>
        /// <param name="dwCreateFlags">Flags that specify options. This value must be zero.</param>
        /// <param name="riid">
        /// The IID of the desired metadata interface to be returned; the caller will use the interface to create the new metadata.
        /// The value of riid must specify one of the "emit" interfaces. Valid values are IID_IMetaDataEmit, IID_IMetaDataAssemblyEmit, or IID_IMetaDataEmit2. 
        /// </param>
        /// <param name="ppIUnk">The pointer to the returned interface.</param>
        void DefineScope(
            ref Guid rclsid,
            uint dwCreateFlags,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

        /// <summary>
        /// Opens an existing, on-disk file and maps its metadata into memory.
        /// </summary>
        /// <param name="szScope">The name of the file to be opened. The file must contain common language runtime (CLR) metadata.</param>
        /// <param name="dwOpenFlags">A value of the <c>CorOpenFlags</c> enumeration to specify the mode (read, write, and so on) for opening. </param>
        /// <param name="riid">
        /// The IID of the desired metadata interface to be returned; the caller will use the interface to import (read) or emit (write) metadata. 
        /// The value of riid must specify one of the "import" or "emit" interfaces. Valid values are IID_IMetaDataEmit, IID_IMetaDataImport, IID_IMetaDataAssemblyEmit, IID_IMetaDataAssemblyImport, IID_IMetaDataEmit2, or IID_IMetaDataImport2. 
        /// </param>
        /// <param name="ppIUnk">The pointer to the returned interface.</param>
        void OpenScope(
            string szScope,
            CorOpenFlags dwOpenFlags,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppIUnk);

        /// <summary>
        /// Opens an area of memory that contains existing metadata. That is, this method opens a specified area of memory in which the existing data is treated as metadata.
        /// </summary>
        /// <param name="pData">A pointer that specifies the starting address of the memory area.</param>
        /// <param name="cbData">The size of the memory area, in bytes.</param>
        /// <param name="dwOpenFlags">A value of the <c>CorOpenFlags</c> enumeration to specify the mode (read, write, and so on) for opening.</param>
        /// <param name="riid">
        /// The IID of the desired metadata interface to be returned; the caller will use the interface to import (read) or emit (write) metadata. 
        /// The value of riid must specify one of the "import" or "emit" interfaces. Valid values are IID_IMetaDataEmit, IID_IMetaDataImport, IID_IMetaDataAssemblyEmit, IID_IMetaDataAssemblyImport, IID_IMetaDataEmit2, or IID_IMetaDataImport2. 
        /// </param>
        /// <param name="ppIUnk">The pointer to the returned interface.</param>
        void OpenScopeOnMemory(
            IntPtr pData,
            uint cbData,
            CorOpenFlags dwOpenFlags,
            ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppIUnk);                     
    }
}
