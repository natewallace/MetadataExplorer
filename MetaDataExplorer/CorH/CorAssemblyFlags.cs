using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    [Flags]
    internal enum CorAssemblyFlags : uint
    {
        afPublicKey = 0x0001,               // The assembly ref holds the full (unhashed) public key.

        afPA_None = 0x0000,                 // Processor Architecture unspecified
        afPA_MSIL = 0x0010,                 // Processor Architecture: neutral (PE32)
        afPA_x86 = 0x0020,                  // Processor Architecture: x86 (PE32)
        afPA_IA64 = 0x0030,                 // Processor Architecture: Itanium (PE32+)
        afPA_AMD64 = 0x0040,                // Processor Architecture: AMD X64 (PE32+)
        afPA_NoPlatform = 0x0070,           // applies to any platform but cannot run on any (e.g. reference assembly), should not have "specified" set
        afPA_Specified = 0x0080,            // Propagate PA flags to AssemblyRef record
        afPA_Mask = 0x0070,                 // Bits describing the processor architecture
        afPA_FullMask = 0x00F0,             // Bits describing the PA incl. Specified
        afPA_Shift = 0x0004,                // NOT A FLAG, shift count in PA flags <--> index conversion

        afEnableJITcompileTracking = 0x8000,    // From "DebuggableAttribute".
        afDisableJITcompileOptimizer = 0x4000,  // From "DebuggableAttribute".

        afRetargetable = 0x0100,            // The assembly can be retargeted (at runtime) to an
                                            //  assembly from a different publisher.    
    }
}
