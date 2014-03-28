using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Provides methods for importing and manipulating existing metadata from a portable executable (PE) file or other source, such as a type library or a stand-alone, run-time metadata binary.
    /// </summary>
    [ComImport]
    [Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataImport
    {
        /// <summary>
        /// Closes the enumerator that is identified by the specified handle.
        /// </summary>
        /// <param name="hEnum">The handle for the enumerator to close.</param>
        [PreserveSig]
        void CloseEnum(
            IntPtr hEnum);

        /// <summary>
        /// Gets the number of elements in the enumeration that was retrieved by the specified enumerator.
        /// </summary>
        /// <param name="hEnum">The handle for the enumerator.</param>
        /// <param name="pulCount">The number of elements enumerated.</param>
        void CountEnum(
            IntPtr hEnum, 
            out uint pulCount);

        /// <summary>
        /// Resets the specified enumerator to the specified position.
        /// </summary>
        /// <param name="hEnum">The enumerator to reset.</param>
        /// <param name="ulPos">The new position at which to place the enumerator.</param>
        void ResetEnum(
            ref IntPtr hEnum, 
            uint ulPos);

        /// <summary>
        /// Enumerates TypeDef tokens representing all types within the current scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the new enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rTypeDefs">The array used to store the TypeDef tokens.</param>
        /// <param name="cMax">The maximum size of the rTypeDefs array.</param>
        /// <param name="pcTypeDefs">The number of TypeDef tokens returned in rTypeDefs.</param>
        void EnumTypeDefs(
            ref IntPtr phEnum, 
            [MarshalAs(UnmanagedType.LPArray)] uint[] rTypeDefs,
            uint cMax,
            out uint pcTypeDefs);

        /// <summary>
        /// Enumerates MethodDef tokens representing interface implementations.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="td">The token of the TypeDef whose MethodDef tokens representing interface implementations are to be enumerated.</param>
        /// <param name="rImpls">The array used to store the .net  tokens.</param>
        /// <param name="cMax">The maximum size of the rImpls array.</param>
        /// <param name="pcImpls">The actual number of tokens returned in rImpls.</param>
        void EnumInterfaceImpls(
            ref IntPtr phEnum, 
            uint td,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rImpls, 
            uint cMax,
            out uint pcImpls);

        /// <summary>
        /// Enumerates TypeRef tokens defined in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rTypeRefs">The array used to store the TypeRef tokens.</param>
        /// <param name="cMax">The maximum size of the rTypeRefs array.</param>
        /// <param name="pcTypeRefs">A pointer to the number of TypeRef tokens returned in rTypeRefs.</param>
        void EnumTypeRefs(
            ref IntPtr phEnum, 
            [MarshalAs(UnmanagedType.LPArray)] uint[] rTypeRefs,
            uint cMax, 
            out uint pcTypeRefs);

    
        /// <summary>
        /// Gets a pointer to the TypeDef metadata token for the Type with the specified name.
        /// </summary>
        /// <param name="szTypeDef">The name of the type for which to get the TypeDef token.</param>
        /// <param name="tkEnclosingClass">A TypeDef or TypeRef token representing the enclosing class. If the type to find is not a nested class, set this value to NULL.</param>
        /// <param name="ptd">A pointer to the matching TypeDef token.</param>
        void FindTypeDefByName(           
            string szTypeDef,              
            uint tkEnclosingClass,       
            out uint ptd);             

    
        /// <summary>
        /// Gets the name and optionally the version identifier of the assembly or module in the current metadata scope.
        /// </summary>
        /// <param name="szName">A buffer for the assembly or module name.</param>
        /// <param name="cchName">The size in wide characters of szName.</param>
        /// <param name="pchName">The number of wide characters returned in szName.</param>
        /// <param name="pmvid">[out, optional] A pointer to a GUID that uniquely identifies the version of the assembly or module.</param>
        void GetScopeProps(               
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            ref Guid pmvid);

        /// <summary>
        /// Gets a metadata token for the module referenced in the current metadata scope.
        /// </summary>
        /// <param name="pmd">A pointer to the token representing the module referenced in the current metadata scope.</param>
        void GetModuleFromScope(
            out uint pmd);
        
        /// <summary>
        /// Returns metadata information for the Type represented by the specified TypeDef token.
        /// </summary>
        /// <param name="td">The TypeDef token that represents the type to return metadata for.</param>
        /// <param name="szTypeDef">A buffer containing the type name.</param>
        /// <param name="cchTypeDef">The size in wide characters of szTypeDef.</param>
        /// <param name="pchTypeDef">The number of wide characters returned in szTypeDef.</param>
        /// <param name="pdwTypeDefFlags">A pointer to any flags that modify the type definition. This value is a bitmask from the CorTypeAttr enumeration.</param>
        /// <param name="ptkExtends">A TypeDef or TypeRef metadata token that represents the base type of the requested type.</param>
        void GetTypeDefProps(
            uint td,
            StringBuilder szTypeDef,
            uint cchTypeDef,
            out uint pchTypeDef,
            out CorTypeAttr pdwTypeDefFlags,
            out uint ptkExtends);
    
        /// <summary>
        /// Gets a pointer to the metadata tokens for the Type that implements the specified method, and for the interface that declares that method.
        /// </summary>
        /// <param name="iiImpl">The metadata token representing the method to return the class and interface tokens for.</param>
        /// <param name="pClass">The metadata token representing the class that implements the method.</param>
        /// <param name="ptkIface">The metadata token representing the interface that defines the implemented method.</param>
        void GetInterfaceImplProps(
            uint iiImpl,
            out uint pClass,
            out uint ptkIface);
            
        /// <summary>
        /// Gets the metadata associated with the Type referenced by the specified TypeRef token.
        /// </summary>
        /// <param name="tr">The TypeRef token that represents the type to return metadata for.</param>
        /// <param name="ptkResolutionScope">A pointer to the scope in which the reference is made. This value is an AssemblyRef or ModuleRef token.</param>
        /// <param name="szName">A buffer containing the type name.</param>
        /// <param name="cchName">The requested size in wide characters of szName.</param>
        /// <param name="pchName">The returned size in wide characters of szName.</param>
        void GetTypeRefProps(
            uint tr,
            out uint ptkResolutionScope,
            StringBuilder szName,
            uint cchName,
            out uint pchName);

        /// <summary>
        /// Resolves a Type reference represented by the specified TypeRef token.
        /// </summary>
        /// <param name="tr">The TypeRef metadata token to return the referenced type information for.</param>
        /// <param name="riid">The IID of the interface to return in ppIScope. Typically, this would be IID_IMetaDataImport.</param>
        /// <param name="ppIScope">An interface to the module scope in which the referenced type is defined.</param>
        /// <param name="ptd">A pointer to a TypeDef token that represents the referenced type.</param>
        void ResolveTypeRef(
            uint tr, 
            ref Guid riid, 
            [MarshalAs(UnmanagedType.IUnknown)] out object ppIScope, 
            out uint ptd);

        /// <summary>
        /// Enumerates MemberDef tokens representing members of the specified type.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="cl">A TypeDef token representing the type whose members are to be enumerated.</param>
        /// <param name="rMembers">The array used to hold the MemberDef tokens.</param>
        /// <param name="cMax">The maximum size of the rMembers array.</param>
        /// <param name="pcTokens">The actual number of MemberDef tokens returned in rMembers.</param>
        void EnumMembers(
            ref IntPtr phEnum,
            uint cl,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMembers,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates MemberDef tokens representing members of the specified type with the specified name.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="cl">A TypeDef token representing the type with members to enumerate.</param>
        /// <param name="szName">The member name that limits the scope of the enumerator.</param>
        /// <param name="rMembers">The array used to store the MemberDef tokens.</param>
        /// <param name="cMax">The maximum size of the rMembers array.</param>
        /// <param name="pcTokens">The actual number of MemberDef tokens returned in rMembers.</param>
        void EnumMembersWithName(
            ref IntPtr phEnum,
            uint   cl,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMembers,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates MethodDef tokens representing methods of the specified type.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="cl">A TypeDef token representing the type with the methods to enumerate.</param>
        /// <param name="rMethods">The array to store the MethodDef tokens.</param>
        /// <param name="cMax">The maximum size of the MethodDef rMethods array.</param>
        /// <param name="pcTokens">The number of MethodDef tokens returned in rMethods.</param>
        void EnumMethods(
            ref IntPtr phEnum,
            uint cl,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethods,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates methods that have the specified name and that are defined by the type referenced by the specified TypeDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="cl">A TypeDef token representing the type whose methods to enumerate.</param>
        /// <param name="szName">The name that limits the scope of the enumeration.</param>
        /// <param name="rMethods">The array used to store the MethodDef tokens.</param>
        /// <param name="cMax">The maximum size of the rMethods array.</param>
        /// <param name="pcTokens">The number of MethodDef tokens returned in rMethods.</param>
        void EnumMethodsWithName(
            ref IntPtr phEnum,
            uint cl,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethods,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates FieldDef tokens for the type referenced by the specified TypeDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="cl">The TypeDef token of the class whose fields are to be enumerated.</param>
        /// <param name="rFields">The list of FieldDef tokens.</param>
        /// <param name="cMax">The maximum size of the rFields array.</param>
        /// <param name="pcTokens">The actual number of FieldDef tokens returned in rFields.</param>
        void EnumFields(
            ref IntPtr phEnum,
            uint cl,
            [MarshalAs(UnmanagedType.LPArray)] uint[]  rFields,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates FieldDef tokens of the specified type with the specified name.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="cl">The token of the type whose fields are to be enumerated.</param>
        /// <param name="szName">The field name that limits the scope of the enumeration.</param>
        /// <param name="rFields">Array used to store the FieldDef tokens.</param>
        /// <param name="cMax">The maximum size of the rFields array.</param>
        /// <param name="pcTokens">The actual number of FieldDef tokens returned in rFields.</param>
        void EnumFieldsWithName(
            ref IntPtr phEnum,
            uint cl,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rFields,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates ParamDef tokens representing the parameters of the method referenced by the specified MethodDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="mb">A MethodDef token representing the method with the parameters to enumerate.</param>
        /// <param name="rParams">The array used to store the ParamDef tokens.</param>
        /// <param name="cMax">The maximum size of the rParams array.</param>
        /// <param name="pcTokens">The number of ParamDef tokens returned in rParams.</param>
        void EnumParams(
            ref IntPtr phEnum,
            uint mb,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rParams,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates MemberRef tokens representing members of the specified type.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="tkParent">A TypeDef, TypeRef, MethodDef, or ModuleRef token for the type whose members are to be enumerated.</param>
        /// <param name="rMemberRefs">The array used to store MemberRef tokens.</param>
        /// <param name="cMax">The maximum size of the rMemberRefs array.</param>
        /// <param name="pcTokens">The actual number of MemberRef tokens returned in rMemberRefs.</param>
        void EnumMemberRefs(
            ref IntPtr phEnum,
            uint tkParent,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMemberRefs,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates MethodBody and MethodDeclaration tokens representing methods of the specified type.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="td">A TypeDef token for the type whose method implementations to enumerate.</param>
        /// <param name="rMethodBody">The array to store the MethodBody tokens.</param>
        /// <param name="rMethodDecl">The array to store the MethodDeclaration tokens.</param>
        /// <param name="cMax">The maximum size of the rMethodBody and rMethodDecl arrays.</param>
        /// <param name="pcTokens">The actual number of methods returned in rMethodBody and rMethodDecl.</param>
        void EnumMethodImpls(
            ref IntPtr phEnum,
            uint td,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethodBody,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethodDecl,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Enumerates permissions for the objects in a specified metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="tk">A metadata token that limits the scope of the search, or NULL to search the widest scope possible.</param>
        /// <param name="dwActions">Flags representing the SecurityAction values to include in rPermission, or zero to return all actions.</param>
        /// <param name="rPermission">The array used to store the Permission tokens.</param>
        /// <param name="cMax">The maximum size of the rPermission array.</param>
        /// <param name="pcTokens">The number of Permission tokens returned in rPermission.</param>
        void EnumPermissionSets(
            ref IntPtr phEnum,
            uint tk,
            int dwActions,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rPermission,
            uint cMax,
            uint pcTokens);

        /// <summary>
        /// Gets a pointer to the MemberDef token for field or method that is enclosed by the specified Type and that has the specified name and metadata signature.
        /// </summary>
        /// <param name="td">The TypeDef token for the class or interface that encloses the member to search for. If this value is mdTokenNil, the lookup is done for a global-variable or global-function.</param>
        /// <param name="szName">The name of the member to search for.</param>
        /// <param name="pvSigBlob">A pointer to the binary metadata signature of the member.</param>
        /// <param name="cbSigBlob">The size in bytes of pvSigBlob.</param>
        /// <param name="pmb">A pointer to the matching MemberDef token.</param>
        void FindMember(  
            uint td,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pvSigBlob,
            uint cbSigBlob,
            out uint pmb);

        /// <summary>
        /// Gets a pointer to the MethodDef token for the method that is enclosed by the specified Type and that has the specified name and metadata signature.
        /// </summary>
        /// <param name="td">The mdTypeDef token for the type (a class or interface) that encloses the member to search for. If this value is mdTokenNil, then the lookup is done for a global function.</param>
        /// <param name="szName">The name of the method to search for.</param>
        /// <param name="pvSigBlob">A pointer to the binary metadata signature of the method.</param>
        /// <param name="cbSigBlob">The size in bytes of pvSigBlob.</param>
        /// <param name="pmb">A pointer to the matching MethodDef token.</param>
        void FindMethod(  
            uint td,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pvSigBlob,
            uint cbSigBlob,
            out uint pmb);

        /// <summary>
        /// Gets a pointer to the FieldDef token for the field that is enclosed by the specified Type and that has the specified name and metadata signature.
        /// </summary>
        /// <param name="td">The TypeDef token for the class or interface that encloses the field to search for. If this value is mdTokenNil, the lookup is done for a global variable.</param>
        /// <param name="szName">The name of the field to search for.</param>
        /// <param name="pvSigBlob">A pointer to the binary metadata signature of the field.</param>
        /// <param name="cbSigBlob">The size in bytes of pvSigBlob.</param>
        /// <param name="pmb">A pointer to the matching FieldDef token.</param>
        int FindField(   
            uint td,   
            [MarshalAs(UnmanagedType.LPTStr)] string szName, 
            [MarshalAs(UnmanagedType.LPArray)] byte[] pvSigBlobs,
            uint cbSigBlob,
            out uint pmb);

        /// <summary>
        /// Gets a pointer to the MemberRef token for the member reference that is enclosed by the specified Type and that has the specified name and metadata signature.
        /// </summary>
        /// <param name="td">The TypeRef token for the class or interface that encloses the member reference to search for. If this value is mdTokenNil, the lookup is done for a global variable or a global-function reference.</param>
        /// <param name="szName">The name of the member reference to search for.</param>
        /// <param name="pvSigBlob">A pointer to the binary metadata signature of the member reference.</param>
        /// <param name="cbSigBlob">The size in bytes of pvSigBlob.</param>
        /// <param name="pmr">A pointer to the matching MemberRef token.</param>
        void FindMemberRef(   
            uint td,
            string  szName,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pvSigBlob,
            uint cbSigBlob,
            uint pmr);

        /// <summary>
        /// Gets the metadata associated with the method referenced by the specified MethodDef token.
        /// </summary>
        /// <param name="mb">The MethodDef token that represents the method to return metadata for.</param>
        /// <param name="pClass">A Pointer to a TypeDef token that represents the type that implements the method.</param>
        /// <param name="szMethod">A Pointer to a buffer that has the method's name.</param>
        /// <param name="cchMethod">The requested size of szMethod.</param>
        /// <param name="pchMethod">A Pointer to the size in wide characters of szMethod, or in the case of truncation, the actual number of wide characters in the method name.</param>
        /// <param name="pdwAttr">A pointer to any flags associated with the method.</param>
        /// <param name="ppvSigBlob">A pointer to the binary metadata signature of the method.</param>
        /// <param name="pcbSigBlob">A Pointer to the size in bytes of ppvSigBlob.</param>
        /// <param name="pulCodeRVA">A pointer to the relative virtual address of the method.</param>
        /// <param name="pdwImplFlags">A pointer to any implementation flags for the method.</param>
        void GetMethodProps( 
            uint mb,
            out uint pClass,
            StringBuilder szMethod,
            uint cchMethod,
            out uint pchMethod,
            out CorMethodAttr pdwAttr,
            out IntPtr ppvSigBlob,
            out uint pcbSigBlob,
            out uint pulCodeRVA,
            out int pdwImplFlags);

        /// <summary>
        /// Gets metadata associated with the member referenced by the specified token.
        /// </summary>
        /// <param name="mr">The MemberRef token to return associated metadata for.</param>
        /// <param name="ptk">A TypeDef or TypeRef, or TypeSpec token that represents the class that declares the member, or a ModuleRef token that represents the module class that declares the member, or a MethodDef that represents the member.</param>
        /// <param name="szMember">A string buffer for the member's name.</param>
        /// <param name="cchMember">The requested size in wide characters of szMember.</param>
        /// <param name="pchMember">The returned size in wide characters of szMember.</param>
        /// <param name="ppvSigBlob">A pointer to the binary metadata signature for the member.</param>
        /// <param name="pbSig">The size in bytes of ppvSigBlob.</param>
        void GetMemberRefProps(
            uint mr,
            uint ptk,
            StringBuilder szMember,
            uint cchMember,
            out uint pchMember,
            out IntPtr ppvSigBlob,
            out uint pbSig);

        /// <summary>
        /// Enumerates PropertyDef tokens representing the properties of the type referenced by the specified TypeDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="td">A TypeDef token representing the type with properties to enumerate.</param>
        /// <param name="rProperties">The array used to store the PropertyDef tokens.</param>
        /// <param name="cMax">The maximum size of the rProperties array.</param>
        /// <param name="pcProperties">The number of PropertyDef tokens returned in rProperties.</param>
        void EnumProperties(
            ref IntPtr phEnum,
            uint td,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rProperties,  
            uint cMax,
            uint pcProperties);

        /// <summary>
        /// Enumerates event definition tokens for the specified TypeDef token.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator.</param>
        /// <param name="td">The TypeDef token whose event definitions are to be enumerated.</param>
        /// <param name="rEvents">The array of returned events.</param>
        /// <param name="cMax">The maximum size of the rEvents array.</param>
        /// <param name="pcEvents">The actual number of events returned in rEvents.</param>
        void EnumEvents(
            ref IntPtr phEnum,
            uint td,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rEvents,
            uint cMax,
            out uint pcEvents);

        /// <summary>
        /// Gets metadata information for the event represented by the specified event token, including the declaring type, the add and remove methods for delegates, and any flags and other associated data.
        /// </summary>
        /// <param name="ev">The event metadata token representing the event to get metadata for.</param>
        /// <param name="pClass">A pointer to the TypeDef token representing the class that declares the event.</param>
        /// <param name="szEvent">The name of the event referenced by ev.</param>
        /// <param name="cchEvent">The requested length in wide characters of szEvent.</param>
        /// <param name="pchEvent">The returned length in wide characters of szEvent.</param>
        /// <param name="pdwEventFlags"></param>
        /// <param name="ptkEventType">A pointer to a TypeRef or TypeDef metadata token representing the Delegate type of the event.</param>
        /// <param name="pmdAddOn">A pointer to the metadata token representing the method that adds handlers for the event.</param>
        /// <param name="pmdRemoveOn">A pointer to the metadata token representing the method that removes handlers for the event.</param>
        /// <param name="pmdFire">A pointer to the metadata token representing the method that raises the event.</param>
        /// <param name="rmdOtherMethod">An array of token pointers to other methods associated with the event.</param>
        /// <param name="cMax">The maximum size of the rmdOtherMethod array.</param>
        /// <param name="pcOtherMethod">The number of tokens returned in rmdOtherMethod.</param>
        void GetEventProps(
            uint ev,
            out uint pClass,
            StringBuilder szEvent,
            uint cchEvent,
            out uint pchEvent,
            out int pdwEventFlags,
            out uint ptkEventType,
            out uint pmdAddOn,
            out uint pmdRemoveOn,
            out uint pmdFire,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rmdOtherMethod,
            uint cMax,
            out uint pcOtherMethod);

        /// <summary>
        /// Enumerates the properties and the property-change events to which the specified method is related.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="mb">A MethodDef token that limits the scope of the enumeration.</param>
        /// <param name="rEventProp">The array used to store the events or properties.</param>
        /// <param name="cMax">The maximum size of the rEventProp array.</param>
        /// <param name="pcEventProp">The number of events or properties returned in rEventProp.</param>
        void EnumMethodSemantics(
            ref IntPtr phEnum,
            uint mb,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rEventProp,
            uint cMax,
            out uint pcEventProp);

        /// <summary>
        /// Gets flags indicating the relationship between the method referenced by the specified MethodDef token and the paired property and event referenced by the specified EventProp token.
        /// </summary>
        /// <param name="mb">A MethodDef token representing the method to get the semantic role information for.</param>
        /// <param name="tkEventProp">A token representing the paired property and event for which to get the method's role.</param>
        /// <param name="pdwSemanticsFlags">A pointer to the associated semantics flags. This value is a bitmask from the CorMethodSemanticsAttr enumeration.</param>
        void GetMethodSemantics(
            uint mb,
            uint tkEventProp,
            out CorMethodSemanticsAttr pdwSemanticsFlags);

        /// <summary>
        /// Gets layout information for the class referenced by the specified TypeDef token.
        /// </summary>
        /// <param name="td">The TypeDef token for the class with the layout to return.</param>
        /// <param name="pdwPackSize">One of the values 1, 2, 4, 8, or 16, representing the pack size of the class.</param>
        /// <param name="rFieldOffset">An array of COR_FIELD_OFFSET values.</param>
        /// <param name="cMax">The maximum size of the rFieldOffset array.</param>
        /// <param name="pcFieldOffset">The number of elements returned in rFieldOffset.</param>
        /// <param name="pulClassSize">The size in bytes of the class represented by td.</param>
        void GetClassLayout( 
            uint td,
            out int pdwPackSize,
            [MarshalAs(UnmanagedType.LPArray)] COR_FIELD_OFFSET[] rFieldOffset,
            uint cMax,
            out uint pcFieldOffset,
            out uint pulClassSize);

        /// <summary>
        /// Gets a pointer to the native, unmanaged type of the field represented by the specified field metadata token.
        /// </summary>
        /// <param name="tk">The metadata token that represents the field to get interop marshaling information for.</param>
        /// <param name="ppvNativeType">A pointer to the metadata signature of the field's native type.</param>
        /// <param name="pcbNativeType">The size in bytes of ppvNativeType.</param>
        void GetFieldMarshal(
            uint tk,
            out IntPtr ppvNativeType,
            out uint pcbNativeType);

        /// <summary>
        /// Gets the relative virtual address (RVA) and the implementation flags of the method or field represented by the specified token.
        /// </summary>
        /// <param name="tk">A MethodDef or FieldDef metadata token that represents the code object to return the RVA for. If the token is a FieldDef, the field must be a global variable.</param>
        /// <param name="pulCodeRVA">A pointer to the relative virtual address of the code object represented by the token.</param>
        /// <param name="pdwImplFlags">A pointer to the implementation flags for the method. This value is a bitmask from the CorMethodImpl enumeration. The value of pdwImplFlags is valid only if tk is a MethodDef token.</param>
        void GetRVA(
            uint tk,
            out uint pulCodeRVA,
            out CorMethodImpl pdwImplFlags);

        /// <summary>
        /// Gets the metadata associated with the System.Security.PermissionSet represented by the specified Permission token.
        /// </summary>
        /// <param name="pm">The Permission metadata token that represents the permission set to get the metadata properties for.</param>
        /// <param name="pdwAction">A pointer to the permission set.</param>
        /// <param name="ppvPermission">A pointer to the binary metadata signature of the permission set.</param>
        /// <param name="pcbPermission">The size in bytes of ppvPermission.</param>
        void GetPermissionSetProps(  
            uint pm,
            out int pdwAction,
            out IntPtr ppvPermission,
            out uint pcbPermission);

        /// <summary>
        /// Gets the binary metadata signature associated with the specified token.
        /// </summary>
        /// <param name="mdSig">The token to return the binary metadata signature for.</param>
        /// <param name="ppvSig">A pointer to the returned metadata signature.</param>
        /// <param name="pcbSig">The size in bytes of the binary metadata signature.</param>
        void GetSigFromToken(
            uint mdSig,
            out IntPtr ppvSig,
            out uint pcbSig);

        /// <summary>
        /// Gets the name of the module referenced by the specified metadata token.
        /// </summary>
        /// <param name="mur">The ModuleRef metadata token that references the module to get metadata information for.</param>
        /// <param name="szName">A buffer to hold the module name.</param>
        /// <param name="cchName">The requested size of szName in wide characters.</param>
        /// <param name="pchName">The returned size of szName in wide characters.</param>
        void GetModuleRefProps(
            uint mur,
            StringBuilder szName,
            uint cchName,
            out uint pchName);

        /// <summary>
        /// Enumerates ModuleRef tokens that represent imported modules.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rModuleRefs">The array used to store the ModuleRef tokens.</param>
        /// <param name="cmax">The maximum size of the rModuleRefs array.</param>
        /// <param name="pcModuleRefs">The number of ModuleRef tokens returned in rModuleRefs.</param>
        void EnumModuleRefs(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rModuleRefs,
            uint cmax,
            out uint pcModuleRefs);

        /// <summary>
        /// Gets the binary metadata signature of the type specification represented by the specified token.
        /// </summary>
        /// <param name="typespec">The TypeSpec token associated with the requested metadata signature.</param>
        /// <param name="ppvSig">A pointer to the binary metadata signature.</param>
        /// <param name="pcbSig">The size, in bytes, of the metadata signature.</param>
        void GetTypeSpecFromToken(
            uint typespec,
            out IntPtr ppvSig,
            out uint pcbSig);

        /// <summary>
        /// Gets the UTF-8 name of the object referenced by the specified metadata token. This method is obsolete.
        /// </summary>
        /// <param name="tk">The token representing the object to return the name for.</param>
        /// <param name="pszUtf8NamePtr">A pointer to the UTF-8 object name in the heap.</param>
        void GetNameFromToken(
            uint tk,
            string pszUtf8NamePtr);

        /// <summary>
        /// Enumerates MemberDef tokens representing the unresolved methods in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rMethods">The array used to store the MemberDef tokens.</param>
        /// <param name="cMax">The maximum size of the rMethods array.</param>
        /// <param name="pcTokens">The number of MemberDef tokens returned in rMethods.</param>
        void EnumUnresolvedMethods(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rMethods,
            uint cMax,
            out uint pcTokens);

        /// <summary>
        /// Gets the literal string represented by the specified metadata token.
        /// </summary>
        /// <param name="stk">The String token to return the associated string for.</param>
        /// <param name="szString">A copy of the requested string.</param>
        /// <param name="cchString">The maximum size in wide characters of the requested szString.</param>
        /// <param name="pchString">The size in wide characters of the returned szString.</param>
        void GetUserString(
            uint stk,
            StringBuilder szString,
            uint cchString,
            out uint pchString);

        /// <summary>
        /// Gets a ModuleRef token to represent the target assembly of a PInvoke call.
        /// </summary>
        /// <param name="tk">A FieldDef or MethodDef token to get the PInvoke mapping metadata for.</param>
        /// <param name="pdwMappingFlags">A pointer to flags used for mapping. This value is a bitmask from the CorPinvokeMap enumeration.</param>
        /// <param name="szImportName">The name of the unmanaged target DLL.</param>
        /// <param name="cchImportName">The size in wide characters of szImportName.</param>
        /// <param name="pchImportName">The number of wide characters returned in szImportName.</param>
        /// <param name="pmrImportDLL">A pointer to a ModuleRef token that represents the unmanaged target object library.</param>
        void GetPinvokeMap(
            uint tk,
            out CorPinvokeMap pdwMappingFlags,
            StringBuilder szImportName,
            uint cchImportName,
            out uint pchImportName,
            out uint pmrImportDLL);

        /// <summary>
        /// Enumerates Signature tokens representing stand-alone signatures in the current scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rSignatures">The array used to store the Signature tokens.</param>
        /// <param name="cmax">The maximum size of the rSignatures array.</param>
        /// <param name="pcSignatures">The number of Signature tokens returned in rSignatures.</param>
        void EnumSignatures(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rSignatures,
            uint cmax,
            out uint pcSignatures);

        /// <summary>
        /// Enumerates TypeSpec tokens defined in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This value must be NULL for the first call of this method.</param>
        /// <param name="rTypeSpecs">The array used to store the TypeSpec tokens.</param>
        /// <param name="cmax">The maximum size of the rTypeSpecs array.</param>
        /// <param name="pcTypeSpecs">The number of TypeSpec tokens returned in rTypeSpecs.</param>
        void EnumTypeSpecs(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rTypeSpecs,
            uint cmax,
            out uint pcTypeSpecs);
        
        /// <summary>
        /// Enumerates String tokens representing hard-coded strings in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">A pointer to the enumerator. This must be NULL for the first call of this method.</param>
        /// <param name="rStrings">The array used to store the String tokens.</param>
        /// <param name="cmax">The maximum size of the rStrings array.</param>
        /// <param name="pcStrings">The number of String tokens returned in rStrings.</param>
        void EnumUserStrings(
            ref IntPtr phEnum,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rStrings,
            uint cmax,
            out uint pcStrings);
        
        /// <summary>
        /// Gets the token that represents a specified parameter of the method represented by the specified MethodDef token.
        /// </summary>
        /// <param name="md">A token that represents the method to return the parameter token for.</param>
        /// <param name="ulParamSeq">The ordinal position in the parameter list where the requested parameter occurs. Parameters are numbered starting from one, with the method's return value in position zero.</param>
        /// <param name="ppd">A pointer to a ParamDef token that represents the requested parameter.</param>
        void GetParamForMethodIndex(
            uint md,
            uint ulParamSeq,
            out uint ppd);
        
        /// <summary>
        /// Enumerates custom attribute-definition tokens associated with the specified type or member.
        /// </summary>
        /// <param name="phEnum">A pointer to the returned enumerator.</param>
        /// <param name="tk">A token for the scope of the enumeration, or zero for all custom attributes.</param>
        /// <param name="tkType">A token for the type of the attributes to be enumerated, or zero for all types.</param>
        /// <param name="rCustomAttributes">An array of custom attribute tokens.</param>
        /// <param name="cMax">The maximum size of the rCustomAttributes array.</param>
        /// <param name="pcCustomAttributes">[out, optional] The actual number of token values returned in rCustomAttributes.</param>
        void EnumCustomAttributes(
            ref IntPtr phEnum,
            uint tk,
            uint tkType,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rCustomAttributes,
            uint cMax,
            out uint pcCustomAttributes);
        
        /// <summary>
        /// Gets the value of the custom attribute, given its metadata token.
        /// </summary>
        /// <param name="cv">A metadata token that represents the custom attribute to be retrieved.</param>
        /// <param name="ptkObj">[out, optional] A metadata token representing the object that the custom attribute modifies. This value can be any type of metadata token except mdCustomAttribute. See Metadata Tokens for more information about the token types.</param>
        /// <param name="ptkType">[out, optional] An mdMethodDef or mdMemberRef metadata token representing the Type of the returned custom attribute.</param>
        /// <param name="ppBlob">[out, optional] A pointer to an array of data that is the value of the custom attribute.</param>
        /// <param name="pcbSize">[out, optional] The size in bytes of the data returned in *ppBlob.</param>
        void GetCustomAttributeProps(
            uint cv,
            out uint ptkObj,
            out uint ptkType,
            [MarshalAs(UnmanagedType.LPArray)] byte[] ppBlob,
            out uint pcbSize);
        
        /// <summary>
        /// Gets a pointer to the TypeRef token for the Type reference that is in the specified scope and that has the specified name.
        /// </summary>
        /// <param name="tkResolutionScope">A ModuleRef, AssemblyRef, or TypeRef token that specifies the module, assembly, or type, respectively, in which the type reference is defined.</param>
        /// <param name="szName">The name of the type reference to search for.</param>
        /// <param name="ptr">A pointer to the matching TypeRef token.</param>
        void FindTypeRef(   
            uint tkResolutionScope,
            string szName,
            out uint ptr);
        
        /// <summary>
        /// Gets metadata information, including the name, binary signature, and relative virtual address, of the Type member referenced by the specified metadata token.
        /// </summary>
        /// <param name="mb">The token that references the member to get the associated metadata for.</param>
        /// <param name="pClass">A pointer to the metadata token that represents the class of the member.</param>
        /// <param name="szMember">The name of the member.</param>
        /// <param name="cchMember">The size in wide characters of the szMember buffer.</param>
        /// <param name="pchMember">The size in wide characters of the returned name.</param>
        /// <param name="pdwAttr">Any flag values applied to the member.</param>
        /// <param name="ppvSigBlob">A pointer to the binary metadata signature of the member.</param>
        /// <param name="pcbSigBlob">The size in bytes of ppvSigBlob.</param>
        /// <param name="pulCodeRVA">A pointer to the relative virtual address of the member.</param>
        /// <param name="pdwImplFlags">Any method implementation flags associated with the member.</param>
        /// <param name="pdwCPlusTypeFlag">A flag that marks a ValueType.</param>
        /// <param name="ppValue">A constant string value returned by this member.</param>
        /// <param name="pcchValue">The size in characters of ppValue, or zero if ppValue does not hold a string.</param>
        void GetMemberProps(  
            uint mb,
            out uint pClass,
            StringBuilder szMember,
            uint cchMember,
            out uint pchMember,
            out int pdwAttr,
            out IntPtr ppvSigBlob,
            out uint pcbSigBlob,
            out uint pulCodeRVA,
            out int pdwImplFlags,
            out CorElementType pdwCPlusTypeFlag,
            StringBuilder ppValue,
            out uint pcchValue);
        
        /// <summary>
        /// Gets metadata associated with the field referenced by the specified FieldDef token.
        /// </summary>
        /// <param name="mb">A FieldDef token that represents the field to get associated metadata for.</param>
        /// <param name="pClass">A pointer to a TypeDef token that represents the type of the class that the field belongs to.</param>
        /// <param name="szField">The name of the field.</param>
        /// <param name="cchField">The size in wide characters of the buffer for szField.</param>
        /// <param name="pchField">The actual size of the returned buffer.</param>
        /// <param name="pdwAttr">Flags associated with the field's metadata.</param>
        /// <param name="ppvSigBlob">A pointer to the binary metadata value that describes the field.</param>
        /// <param name="pcbSigBlob">The size in bytes of ppvSigBlob.</param>
        /// <param name="pdwCPlusTypeFlag">A flag that specifies the value type of the field.</param>
        /// <param name="ppValue">A constant value for the field.</param>
        /// <param name="pcchValue">The size in chars of ppValue, or zero if no string exists.</param>
        void GetFieldProps(  
            uint  mb,
            out uint pClass,
            StringBuilder szField,
            uint cchField,
            out uint pchField,
            out CorFieldAttr pdwAttr,
            out IntPtr ppvSigBlob,
            out uint pcbSigBlob,
            out CorElementType pdwCPlusTypeFlag,
            StringBuilder ppValue,
            out uint pcchValue);
        
        /// <summary>
        /// Gets the metadata for the property represented by the specified token.
        /// </summary>
        /// <param name="prop">A token that represents the property to return metadata for.</param>
        /// <param name="pClass">A pointer to the TypeDef token that represents the type that implements the property.</param>
        /// <param name="szProperty">A buffer to hold the property name.</param>
        /// <param name="cchProperty">The size in wide characters of szProperty.</param>
        /// <param name="pchProperty">The number of wide characters returned in szProperty.</param>
        /// <param name="pdwPropFlags">A pointer to any attribute flags applied to the property. This value is a bitmask from the CorPropertyAttr enumeration.</param>
        /// <param name="ppvSig">A pointer to the metadata signature of the property.</param>
        /// <param name="pbSig">The number of bytes returned in ppvSig.</param>
        /// <param name="pdwCPlusTypeFlag">A flag specifying the type of the constant that is the default value of the property. This value is from the CorElementType enumeration.</param>
        /// <param name="ppDefaultValue">A pointer to the bytes that store the default value for this property.</param>
        /// <param name="pcchDefaultValue">The size in wide characters of ppDefaultValue, if pdwCPlusTypeFlag is ELEMENT_TYPE_STRING; otherwise, this value is not relevant. In that case, the length of ppDefaultValue is inferred from the type that is specified by pdwCPlusTypeFlag.</param>
        /// <param name="pmdSetter">A pointer to the MethodDef token that represents the set accessor method for the property.</param>
        /// <param name="pmdGetter">A pointer to the MethodDef token that represents the get accessor method for the property.</param>
        /// <param name="rmdOtherMethod">An array of MethodDef tokens that represent other methods associated with the property.</param>
        /// <param name="cMax">The maximum size of the rmdOtherMethod array. If you do not provide an array large enough to hold all the methods, they are skipped without warning.</param>
        /// <param name="pcOtherMethod">The number of MethodDef tokens returned in rmdOtherMethod.</param>
        void GetPropertyProps(
            uint prop,
            out uint pClass,
            StringBuilder szProperty,
            uint cchProperty,
            out uint pchProperty,
            out CorPropertyAttr pdwPropFlags,
            out IntPtr ppvSig,
            out uint pbSig,
            out CorElementType pdwCPlusTypeFlag,
            out IntPtr ppDefaultValue,
            out uint pcchDefaultValue,
            out uint pmdSetter,
            out uint pmdGetter,
            [MarshalAs(UnmanagedType.LPArray)] uint[] rmdOtherMethod,
            uint cMax,
            out uint pcOtherMethod);
        
        /// <summary>
        /// Gets metadata values for the parameter referenced by the specified ParamDef token.
        /// </summary>
        /// <param name="tk">A ParamDef token that represents the parameter to return metadata for.</param>
        /// <param name="pmd">A pointer to a MethodDef token representing the method that takes the parameter.</param>
        /// <param name="pulSequence">The ordinal position of the parameter in the method argument list.</param>
        /// <param name="szName">A buffer to hold the name of the parameter.</param>
        /// <param name="cchName">The requested size in wide characters of szName.</param>
        /// <param name="pchName">The returned size in wide characters of szName.</param>
        /// <param name="pdwAttr">A pointer to any attribute flags associated with the parameter.</param>
        /// <param name="pdwCPlusTypeFlag">A pointer to a flag specifying that the parameter is a ValueType.</param>
        /// <param name="ppValue">A pointer to a constant string returned by the parameter.</param>
        /// <param name="pcchValue">The size of ppValue in wide characters, or zero if ppValue does not hold a string.</param>
        void GetParamProps(
            uint tk,
            out uint pmd,
            out uint pulSequence,
            StringBuilder szName,
            uint cchName,
            out uint pchName,
            out CorParamAttr pdwAttr,
            out CorElementType pdwCPlusTypeFlag,
            out IntPtr ppValue,
            out uint pcchValue);
        
        /// <summary>
        /// Gets the custom attribute, given its name and owner.
        /// </summary>
        /// <param name="tkObj">A metadata token representing the object that owns the custom attribute.</param>
        /// <param name="szName">The name of the custom attribute.</param>
        /// <param name="ppData">A pointer to an array of data that is the value of the custom attribute.</param>
        /// <param name="pcbData">The size in bytes of the data returned in *ppData.</param>
        void GetCustomAttributeByName(
            uint tkObj,
            string szName,
            [MarshalAs(UnmanagedType.LPArray)] byte[] ppData,
            out uint pcbData);
        
        /// <summary>
        /// Gets a value indicating whether the specified token holds a valid reference to a code object.
        /// </summary>
        /// <param name="tk">The token to check the reference validity for.</param>
        /// <returns>true if tk is a valid metadata token within the current scope. Otherwise, false.</returns>
        [PreserveSig]
        bool IsValidToken(
            uint tk);
        
        /// <summary>
        /// Gets the TypeDef token for the parent Type of the specified nested type.
        /// </summary>
        /// <param name="tdNestedClass">A TypeDef token representing the Type to return the parent class token for.</param>
        /// <param name="ptdEnclosingClass">A pointer to the TypeDef token for the Type that tdNestedClass is nested in.</param>
        void GetNestedClassProps(
            uint tdNestedClass,
            out uint ptdEnclosingClass);
        
        /// <summary>
        /// Gets the native calling convention for the method that is represented by the specified signature pointer.
        /// </summary>
        /// <param name="pvSig">A pointer to the metadata signature of the method to return the calling convention for.</param>
        /// <param name="cbSig">The size in bytes of pvSig.</param>
        /// <param name="pCallConv">A pointer to the native calling convention.</param>
        void GetNativeCallConvFromSig(
            uint pvSig,
            uint cbSig,
            out uint pCallConv);
        
        /// <summary>
        /// Gets a value indicating whether the field, method, or type represented by the specified metadata token has global scope.
        /// </summary>
        /// <param name="pd">A metadata token that represents a type, field, or method.</param>
        /// <param name="pbGlobal">1 if the object has global scope; otherwise, 0 (zero).</param>
        void IsGlobal(
            uint pd,
            out int pbGlobal);
    }
}
