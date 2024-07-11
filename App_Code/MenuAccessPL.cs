using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class MenuAccessPL : UtilityPL
    {
        public object AutoId { get; set; }
        public object EmpId { get; set; }
        public object Industry { get; set; }
        public object CreatedBy { get; set; }
        public object OldName { get; set; }
        public object Department { get; set; }
        public object SubDepartment { get; set; }
        public object Designation { get; set; }
        public object Type { get; set; }
        public object XML { get; set; }
    }
}