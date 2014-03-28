using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// PE file kind bits, returned by IMetaDataImport2::GetPEKind()
    /// </summary>
    [Flags]
    internal enum CorPEKind : uint
    {
        peNot = 0x00000000,             // not a PE file
        peILonly = 0x00000001,          // flag IL_ONLY is set in COR header
        pe32BitRequired = 0x00000002,   // flag 32BIT_REQUIRED is set in COR header
        pe32Plus = 0x00000004,          // PE32+ file (64 bit)
        pe32Unmanaged = 0x00000008      // PE32 without COR header
    }
}
