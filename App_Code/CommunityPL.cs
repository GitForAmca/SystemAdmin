using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMCAPropertiesAdmin.App_Code
{
    public class CommunityPL : UtilityPL
    {
        public object AutoId { get; set; }
        public object PlaceId { get; set; }
        public object CreatedBy { get; set; } 
        public object ElementId { get; set; } 
        public object PlaceNmae { get; set; } 
        public object PlaceDistance { get; set; } 
        public object Description { get; set; }
        public object path { get; set; }
        public object pathType { get; set; }
        public object XML { get; set; }
    }
}