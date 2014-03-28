using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// File attr bits, used by DefineFile.
    /// </summary>
    [Flags]
    internal enum CorFileFlags : uint
    {
        ffContainsMetaData = 0x0000,        // This is not a resource file
        ffContainsNoMetaData = 0x0001,      // This is a resource file or other non-metadata-containing file
    }
}
