using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class StructurePL : UtilityPL
    {
        public object AutoId { get; set; }
        public object CreatedBy { get; set; }
        public object  IndustryId { get; set; }
        public object Name { get; set; }
        public object Description { get; set; } 
        public object IsActive { get; set; } 
        public object XML { get; set; }
        public object HOD { get; set; }
        public object XMLData { get; set; }
    }
}