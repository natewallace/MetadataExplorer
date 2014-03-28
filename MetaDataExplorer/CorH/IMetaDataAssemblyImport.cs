using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Provides methods to access and examine the contents of an assembly manifest.
    /// </summary>
    [ComImport]
    [Guid("EE62470B-E94B-424e-9B7C-2F00C9249F93")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataAssemblyImport
    {
        /// <summary>
        /// Gets the set of properties for the assembly with the specified metadata signature.
        /// </summary>
        /// <param name="mda">. The <c>mdAssembly</c> metadata token that represents the assembly for which to get the properties. </param>
        /// <param name="ppbPublicKey">A pointer to the public key or the metadata token.</param>
        /// <param name="pcbPublicKey">The number of bytes in the returned public key.</param>
        /// <param name="pulHashAlgId">A pointer to the algorithm used to hash the files in the assembly.</param>
        /// <param name="szName">The simple name of the assembly.</param>
        /// <param name="cchName">The size, in wide chars, of <c>szName</c>.</param>
        /// <param name="pchName">The number of wide chars actually returned in <c>szName</c>.</param>
        /// <param name="pMetaData">A pointer to an <c>ASSEMBLYMETADATA</c> structure that contains the assembly metadata. </param>
        /// <param name="pdwAssemblyFlags">Flags that describe the metadata applied to an assembly. This value is a combination of one or more <c>CorAssemblyFlags</c> values.</param>
        void GetAssemblyProps(
            uint mda,
            IntPtr ppbPublicKey,
            out uint pcbPublicKey,
            out uint pulHashAlgId,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            out ASSEMBLYMETADATA pMetaData,
            out CorAssemblyFlags pdwAssemblyFlags);

        /// <summary>
        /// Gets the set of properties for the assembly reference with the specified metadata signature.
        /// </summary>
        /// <param name="mdar">The <c>mdAssemblyRef</c> metadata token that represents the assembly reference for which to get the properties. </param>
        /// <param name="ppbPublicKeyOrToken">A pointer to the public key or the metadata token.</param>
        /// <param name="pcbPublicKeyOrToken">The number of bytes in the returned public key or token.</param>
        /// <param name="szName">The simple name of the assembly.</param>
        /// <param name="cchName">The size, in wide chars, of <c>szName</c>.</param>
        /// <param name="pchName">A pointer to the number of wide chars actually returned in <c>szName</c>.</param>
        /// <param name="pMetaData">A pointer to an <c>ASSEMBLYMETADATA</c> structure that contains the assembly metadata.</param>
        /// <param name="ppbHashValue">A pointer to the hash value. This is the hash, using the SHA-1 algorithm, of the <c>PublicKey</c> property of the assembly being referenced, unless the <c>arfFullOriginator</c> flag of the <c>AssemblyRefFlags</c> enumeration is set.</param>
        /// <param name="pcbHashValue">The number of wide chars in the returned hash value.</param>
        /// <param name="pdwAssemblyFlags">A pointer to flags that describe the metadata applied to an assembly. The flags value is a combination of one or more <c>CorAssemblyFlags</c> values.</param>
        void GetAssemblyRefProps(
            uint mdar,
            IntPtr ppbPublicKeyOrToken,
            out uint pcbPublicKeyOrToken,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            out ASSEMBLYMETADATA pMetaData,
            IntPtr ppbHashValue,
            out uint pcbHashValue,
            out CorAssemblyFlags pdwAssemblyFlags);

        /// <summary>
        /// Gets the properties of the file with the specified metadata signature.
        /// </summary>
        /// <param name="mdf">The <c>mdFile</c> metadata token that represents the file for which to get the properties.</param>
        /// <param name="szName">The simple name of the file.</param>
        /// <param name="cchName">The size, in wide chars, of <c>szName</c>.</param>
        /// <param name="pchName">A pointer to the number of wide chars actually returned in <c>szName</c>.</param>
        /// <param name="ppbHashValue">A pointer to the hash value. This is the hash, using the SHA-1 algorithm, of the <c>PublicKey</c> property of the assembly being referenced, unless the <c>arfFullOriginator</c> flag of the <c>AssemblyRefFlags</c> enumeration is set.</param>
        /// <param name="pcbHashValue">The number of wide chars in the returned hash value.</param>
        /// <param name="pdwFileFlags">A pointer to the flags that describe the metadata applied to a file. The flags value is a combination of one or more <c>CorFileFlags</c> values.</param>
        void GetFileProps(
            uint mdf,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            IntPtr ppbHashValue,
            out uint pcbHashValue,
            out CorFileFlags pdwFileFlags);

        /// <summary>
        /// Gets the set of properties of the exported type with the specified metadata signature.
        /// </summary>
        /// <param name="mdct">An <c>mdExportedType</c> metadata token that represents the exported type. </param>
        /// <param name="szName">The simple name of the file.</param>
        /// <param name="cchName">The size, in wide chars, of <c>szName</c>.</param>
        /// <param name="pchName">A pointer to the number of wide chars actually returned in <c>szName</c>.</param>
        /// <param name="ptkImplementation">An <c>mdFile</c>, <c>mdAssemblyRef</c>, or <c>mdExportedType</c> metadata token that contains or allows access to the properties of the exported type.</param>
        /// <param name="ptkTypeDef">A pointer to an <c>mdTypeDe</c>f token that represents a type in the file.</param>
        /// <param name="pdwExportedTypeFlags">A pointer to the flags that describe the metadata applied to the exported type. The flags value can be one or more <c>CorTypeAttr</c> values.</param>
        void GetExportedTypeProps(
            uint mdct,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            out uint ptkImplementation,
            out uint ptkTypeDef,
            out CorTypeAttr pdwExportedTypeFlags);

        /// <summary>
        /// Gets the set of properties of the manifest resource with the specified metadata signature.
        /// </summary>
        /// <param name="mdmr">An <c>mdManifestResource</c> token that represents the resource for which to get the properties. </param>
        /// <param name="szName">The simple name of the file.</param>
        /// <param name="cchName">The size, in wide chars, of <c>szName</c>.</param>
        /// <param name="pchName">A pointer to the number of wide chars actually returned in <c>szName</c>.</param>
        /// <param name="ptkImplementation">A pointer to an <c>mdFile</c> token or an mdAssemblyRef token that represents the file or assembly, respectively, that contains the resource. </param>
        /// <param name="pdwOffset">A pointer to a value that specifies the offset to the beginning of the resource within the file.</param>
        /// <param name="pdwResourceFlags">A pointer to flags that describe the metadata applied to a resource. The flags value is a combination of one or more <c>CorManifestResourceFlags</c> values.</param>
        void GetManifestResourceProps(
            uint mdmr,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            out uint ptkImplementation,
            out uint pdwOffset,
            out CorManifestResourceFlags pdwResourceFlags);

        /// <summary>
        /// Enumerates the <c>mdAssemblyRef</c> instances that are defined in the assembly manifest. 
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be a null value when the EnumAssemblyRefs method is called for the first time.</param>
        /// <param name="rAssemblyRefs">The enumeration of mdAssemblyRef metadata tokens.</param>
        /// <param name="cMax">The maximum number of tokens that can be placed in the rAssemblyRefs array.</param>
        /// <param name="pcTokens">The number of tokens actually placed in rAssemblyRefs.</param>
        void EnumAssemblyRefs(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rAssemblyRefs,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates the files referenced in the current assembly manifest.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be a null value for the first call of this method.</param>
        /// <param name="rFiles">The array used to store the <c>mdFile</c> metadata tokens.</param>
        /// <param name="cMax">The maximum number of mdFile tokens that can be placed in <c>rFiles</c>.</param>
        /// <param name="pcTokens">The number of <c>mdFile</c> tokens actually placed in <c>rFiles</c>.</param>
        void EnumFiles(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rFiles,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates the exported types referenced in the assembly manifest in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be a null value when the <c>EnumExportedTypes</c> method is called for the first time.</param>
        /// <param name="rExportedTypes">The enumeration of mdExportedType metadata tokens.</param>
        /// <param name="cMax">The maximum number of mdExportedType tokens that can be placed in the <c>rExportedTypes</c> array.</param>
        /// <param name="pcTokens">The number of <c>mdExportedType</c> tokens actually placed in <c>rExportedTypes</c>.</param>
        void EnumExportedTypes(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rExportedTypes,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Gets a pointer to an enumerator for the resources referenced in the current assembly manifest.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be a null value when the <c>EnumManifestResources</c> method is called for the first time.</param>
        /// <param name="rManifestResources">The array used to store the <c>mdManifestResource</c> metadata tokens.</param>
        /// <param name="cMax">The maximum number of <c>mdManifestResource</c> tokens that can be placed in <c>rManifestResources</c>.</param>
        /// <param name="pcTokens">The number of <c>mdManifestResource</c> tokens actually placed in <c>rManifestResources</c>.</param>
        void EnumManifestResources(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rManifestResources,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Gets a pointer to the assembly in the current scope.
        /// </summary>
        /// <param name="ptkAssembly">A pointer to the retrieved <c>mdAssembly</c> token that identifies the assembly.</param>
        void GetAssemblyFromScope(
            out uint ptkAssembly);

        /// <summary>
        /// Gets a pointer to an exported type, given its name and enclosing type.
        /// </summary>
        /// <param name="szName">The name of the exported type.</param>
        /// <param name="mdtExportedType">The metadata token for the enclosing class of the exported type. This value is <c>mdExportedTypeNil</c> if the requested exported type is not a nested type.</param>
        /// <param name="mdExportedType">A pointer to the <c>mdExportedType</c> token that represents the exported type.</param>
        void FindExportedTypeByName(
            string szName,
            uint mdtExportedType,
            out uint mdExportedType);

        /// <summary>
        /// Gets a pointer to the manifest resource with the specified name.
        /// </summary>
        /// <param name="szName">The name of the resource.</param>
        /// <param name="ptkManifestResource">The array used to store the <c>mdManifestResource</c> metadata tokens, each of which represents a manifest resource.</param>
        void FindManifestResourceByName(
            string szName,
            out uint ptkManifestResource);

        /// <summary>
        /// Releases a reference to the specified enumeration instance.
        /// </summary>
        /// <param name="hEnum">Handle of enumeration to be closed.</param>
        [PreserveSig]
        void CloseEnum(
            IntPtr hEnum);

        /// <summary>
        /// Gets an array of assemblies with the specified <c>szAssemblyName</c> parameter, using the standard rules employed by the common language runtime (CLR) for resolving references.
        /// </summary>
        /// <param name="szAppBase">The root directory in which to search for the given assembly. If this value is set to null, <c>FindAssembliesByName</c> will look only in the global assembly cache for the assembly.</param>
        /// <param name="szPrivateBin">A list of semicolon-delimited subdirectories (for example, "bin;bin2"), under the root directory, in which to search for the assembly. These directories are probed in addition to those specified in the default probing rules.</param>
        /// <param name="szAssemblyName">The name of the assembly to find. The format of this string is defined in the class reference page for <c>AssemblyName</c>.</param>
        /// <param name="ppIUnk">An array of type <c>IUnknown</c> in which to put the <c>IIMetadataAssemblyImport</c> interface pointers. </param>
        /// <param name="cMax">The maximum number of interface pointers that can be placed in <c>IppIUnk</c>.</param>
        /// <param name="pcAssemblies">The number of interface pointers returned. That is, the number of interface pointers actually placed in <c>ppIUnk</c>.</param>
        void FindAssembliesByName(
            string szAppBase,
            string szPrivateBin,
            string szAssemblyName,
            [MarshalAs(UnmanagedType.Interface)] out object[] ppIUnk,
            uint cMax,
            out uint pcAssemblies);
    }
}
