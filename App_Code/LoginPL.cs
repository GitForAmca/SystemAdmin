using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class LoginPL : UtilityPL
    {
        public object AutoId { get; set; }
        public object EmployeeId { get; set; }
        public object UserName { get; set; }
        public object Password { get; set; }
        public object IPAddress { get; set; }
    }
}
