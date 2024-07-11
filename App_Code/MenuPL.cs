using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class MenuPL : UtilityPL
    {
        public object AutoId { get; set; }
        public object CreatedBy { get; set; }
        public object ParentMenu { get; set; }
        public object SubParentMenu { get; set; }
        public object RegionId { get; set; }
        public object MenuType { get; set; }
        public object IsActive { get; set; }
        public object IsDefault { get; set; }
        public object XML { get; set; }
    }
}