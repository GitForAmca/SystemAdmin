using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class PortalSettingsPL : UtilityPL
    {
        public object AutoId { get; set; }
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public object DowntimeMessage { get; set; }
        public object ExcludedEmployees { get; set; }
        public object Region { get; set; }
        public object StartTimeOrg { get; set; }
        public object EndTimeOrg { get; set; }
        public object DowntimeMessageOrg { get; set; }
        public object ExcludedEmployeesOrg { get; set; }
        public object RegionOrg { get; set; }
        public object StartTimeHrms { get; set; }
        public object EndTimeHrms { get; set; }
        public object ValidationPeriod { get; set; }
    }
}