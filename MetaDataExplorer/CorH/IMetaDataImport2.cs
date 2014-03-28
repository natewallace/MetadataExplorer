using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Extends the IMetaDataImport interface to provide the capability of working with generic types.
    /// </summary>
    [ComImport]
    [Guid("FCE5EFA0-8BBA-4f8e-A036-8F2022B08466")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataImport2 : IMetaDataImport
    {
        /// <summary>
        /// Gets an enumerator for an array of generic parameter tokens associated with the specified TypeDef or MethodDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="tk">The TypeDef or MethodDef token whose generic parameters are to be enumerated.</param>
        /// <param name="rGenericParams">The array of generic parameters to enumerate.</param>
        /// <param name="cMax">The requested maximum number of tokens to place in rGenericParams.</param>
        /// <param name="pcGenericParams">The returned number of tokens placed in rGenericParams.</param>
        void EnumGenericParams(
            ref IntPtr phEnum,
            uint tk,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rGenericParams,
            uint cMax,
            out uint pcGenericParams);

        /// <summary>
        /// Gets the metadata associated with the generic parameter represented by the specified token.
        /// </summary>
        /// <param name="gp">The token that represents the generic parameter for which to return metadata.</param>
        /// <param name="pulParamSeq">The ordinal position of the Type parameter in the parent constructor or method.</param>
        /// <param name="pdwParamFlags">A value of the CorGenericParamAttr enumeration that describes the Type for the generic parameter.</param>
        /// <param name="ptOwner">A TypeDef or MethodDef token that represents the owner of the parameter.</param>
        /// <param name="reserved">Reserved for future extensibility.</param>
        /// <param name="wzname">The name of the generic parameter.</param>
        /// <param name="cchName">The size of the wzName buffer.</param>
        /// <param name="pchName">The returned size of the name, in wide characters.</param>
        void GetGenericParamProps(
            uint gp,
            out uint pulParamSeq,
            out CorGenericParamAttr pdwParamFlags,
            out uint ptOwner,
            out int reserved,
            StringBuilder wzname,
            uint cchName,
            out uint pchName);

        /// <summary>
        /// Gets the metadata signature of the method referenced by the specified MethodSpec token.
        /// </summary>
        /// <param name="mi">A MethodSpec token that represents the instantiation of the method.</param>
        /// <param name="tkParent">A pointer to the MethodDef or MethodRef token that represents the method definition.</param>
        /// <param name="ppvSigBlob">A pointer to the binary metadata signature of the method.</param>
        /// <param name="pcbSigBlob">The size, in bytes, of ppvSigBlob.</param>
        void GetMethodSpecProps(
            uint mi,
            out uint tkParent,
            [MarshalAs(UnmanagedType.LPArray)] byte[] ppvSigBlob,
            out uint pcbSigBlob);

        /// <summary>
        /// Gets an enumerator for an array of generic parameter constraints associated with the generic parameter represented by the specified token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="tk">A token that represents the generic parameter whose constraints are to be enumerated.</param>
        /// <param name="rGenericParamConstraints">The array of generic parameter constraints to enumerate.</param>
        /// <param name="cMax">The requested maximum number of tokens to place in rGenericParamConstraints.</param>
        /// <param name="pcGenericParamConstraints">A pointer to the number of tokens placed in rGenericParamConstraints.</param>
        void EnumGenericParamConstraints(
            ref IntPtr phEnum,
            uint tk,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rGenericParamConstraints,
            uint cMax,
            out uint pcGenericParamConstraints);

        /// <summary>
        /// Gets the metadata associated with the generic parameter constraint represented by the specified constraint token.
        /// </summary>
        /// <param name="gpc">The token to the generic parameter constraint for which to return the metadata.</param>
        /// <param name="ptGenericParam">A pointer to the token that represents the generic parameter that is constrained.</param>
        /// <param name="ptkConstraintType">A pointer to a TypeDef, TypeRef, or TypeSpec token that represents a constraint on ptGenericParam.</param>
        void GetGenericParamConstraintProps(
            uint gpc,
            out uint ptGenericParam,
            out uint ptkConstraintType);

        /// <summary>
        /// Gets a value identifying the nature of the code in the portable executable (PE) file, typically a DLL or EXE file, that is defined in the current metadata scope.
        /// </summary>
        /// <param name="pdwPEKind">A pointer to a value of the CorPEKind enumeration that describes the PE file.</param>
        /// <param name="pdwMAchine">A pointer to a value that identifies the architecture of the machine. See the next section for possible values.</param>
        void GetPEKind(
            out CorPEKind pdwPEKind,
            out int pdwMAchine);

        /// <summary>
        /// Gets the version number of the runtime that was used to build the assembly.
        /// </summary>
        /// <param name="pwzBuf">An array to store the string that specifies the version.</param>
        /// <param name="ccBufSize">The size, in wide characters, of the pwzBuf array.</param>
        /// <param name="pccBufSize">The number of wide characters, including a null terminator, returned in the pwzBuf array.</param>
        void GetVersionString(
            StringBuilder pwzBuf,
            uint ccBufSize,
            out uint pccBufSize);

        /// <summary>
        /// Gets an enumerator for an array of MethodSpec tokens associated with the specified MethodDef or MemberRef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator for rMethodSpecs.</param>
        /// <param name="tk">The MemberRef or MethodDef token that represents the method whose MethodSpec tokens are to be enumerated. If the value of tk is 0 (zero), all MethodSpec tokens in the scope will be enumerated.</param>
        /// <param name="rMethodSpecs">The array of MethodSpec tokens to enumerate.</param>
        /// <param name="cMax">The requested maximum number of tokens to place in rMethodSpecs.</param>
        /// <param name="pcMethodSpecs">The returned number of tokens placed in rMethodSpecs.</param>
        void EnumMethodSpecs(
            ref IntPtr phEnum,
            uint tk,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethodSpecs,
            uint cMax,
            out uint pcMethodSpecs);
    }
}
