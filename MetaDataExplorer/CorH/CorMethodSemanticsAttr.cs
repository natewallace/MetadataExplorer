using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// MethodSemantic attr bits, used by DefineProperty, DefineEvent.
    /// </summary>
    [Flags]
    internal enum CorMethodSemanticsAttr : uint
    {
        msSetter = 0x0001,     // Setter for property
        msGetter = 0x0002,     // Getter for property
        msOther = 0x0004,     // other method for property or event
        msAddOn = 0x0008,     // AddOn method for event
        msRemoveOn = 0x0010,     // RemoveOn method for event
        msFire = 0x0020     // Fire method for event
    }
}
