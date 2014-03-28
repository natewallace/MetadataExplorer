using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct COR_FIELD_OFFSET
    {
        uint ridOfField;
        uint ulOffset;
    }
}
