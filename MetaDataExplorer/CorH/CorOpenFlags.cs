using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Open bits.
    /// </summary>
    [Flags]
    internal enum CorOpenFlags : uint
    {
        ofRead = 0x00000000,            // Open scope for read
        ofWrite = 0x00000001,           // Open scope for write.
        ofReadWriteMask = 0x00000001,   // Mask for read/write bit.

        ofCopyMemory = 0x00000002,      // Open scope with memory. Ask metadata to maintain its own copy of memory.

        ofReadOnly = 0x00000010,        // Open scope for read. Will be unable to QI for a IMetadataEmit* interface
        ofTakeOwnership = 0x00000020,   // The memory was allocated with CoTaskMemAlloc and will be freed by the metadata

        // These are obsolete and are ignored.
        // ofCacheImage     =   0x00000004,     // EE maps but does not do relocations or verify image
        // ofManifestMetadata = 0x00000008,     // Open scope on ngen image, return the manifest metadata instead of the IL metadata
        ofNoTypeLib = 0x00000080,       // Don't OpenScope on a typelib.

        // Internal bits
        ofReserved1 = 0x00000100,       // Reserved for internal use.
        ofReserved2 = 0x00000200,       // Reserved for internal use.
        ofReserved3 = 0x00000400,       // Reserved for internal use.
        ofReserved = 0xffffff40         // All the reserved bits.
    }
}
