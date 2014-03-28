using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallace.Nate.MetaDataExplorer.CorH
{
    /// <summary>
    /// Guids used in cor.h
    /// </summary>
    internal class Guids
    {
        /// <summary>
        /// Class ID of the current metadata dispenser.
        /// </summary>
        public static Guid CLSID_CorMetaDataDispenser = new Guid("E5CB7A31-7512-11d2-89CE-0080C792E5D8");

        /// <summary>
        /// Guid of the IMetaDataAssemblyImport interface.
        /// </summary>
        public static Guid IID_IMetaDataAssemblyImport = new Guid("EE62470B-E94B-424e-9B7C-2F00C9249F93");

        /// <summary>
        /// Guid of the IMetaDataImport interface.
        /// </summary>
        public static Guid IID_IMetaDataImport = new Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44");

        /// <summary>
        /// Guid of the IMetaDataImport2 interface.
        /// </summary>
        public static Guid IID_IMetaDataImport2 = new Guid("FCE5EFA0-8BBA-4f8e-A036-8F2022B08466");

        /// <summary>
        /// Guid of the IMetaDataTables interface.
        /// </summary>
        public static Guid IID_IMetaDataTables = new Guid("D8F579AB-402D-4b8e-82D9-5D63B1065C68");
    }
}
