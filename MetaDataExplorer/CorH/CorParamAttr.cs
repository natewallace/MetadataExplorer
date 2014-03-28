using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Param attr bits, used by DefineParam.
    /// </summary>
    [Flags]
    internal enum CorParamAttr : uint
    {
        pdIn = 0x0001,                  // Param is [In]
        pdOut = 0x0002,                 // Param is [out]
        pdOptional = 0x0010,            // Param is optional

        // Reserved flags for Runtime use only.
        pdReservedMask = 0xf000,
        pdHasDefault = 0x1000,          // Param has default value.
        pdHasFieldMarshal = 0x2000,     // Param has FieldMarshal.

        pdUnused = 0xcfe0
    }
}
