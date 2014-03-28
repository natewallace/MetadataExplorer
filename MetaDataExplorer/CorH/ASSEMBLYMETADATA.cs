using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ASSEMBLYMETADATA
    {
        public ushort usMajorVersion;       // Major Version.
        public ushort usMinorVersion;       // Minor Version.
        public ushort usBuildNumber;        // Build Number.
        public ushort usRevisionNumber;     // Revision Number.
        public string szLocale;             // Locale.
        public uint cbLocale;               // [IN/OUT] Size of the buffer in wide chars/Actual size.
        public int rProcessor;              // Processor ID array.
        public uint ulProcessor;            // [IN/OUT] Size of the Processor ID array/Actual # of entries filled in.
        public int rOS;                     // OSINFO array.
        public uint ulOS;                   // [IN/OUT]Size of the OSINFO array/Actual # of entries filled in.
    }
}
