using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// ManifestResource attr bits, used by DefineManifestResource.
    /// </summary>
    [Flags]
    internal enum CorManifestResourceFlags : uint
    {
        mrVisibilityMask = 0x0007,
        mrPublic = 0x0001,          // The Resource is exported from the Assembly.
        mrPrivate = 0x0002,         // The Resource is private to the Assembly.
    }
}
