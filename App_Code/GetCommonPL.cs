using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMCAPropertiesAdmin.App_Code
{
    public class GetCommonPL : UtilityPL
    {
        public object EmiratesAutoid { get; set; } 
        public object AutoId { get; set; } 
        public object DevelopersId { get; set; } 
        public object StatusId { get; set; } 
        public object CommunityId { get; set; }
        public object AreaId { get; set; }
        public object TowerId { get; set; }
        public object UnitTypeId { get; set; }
        public object PurposeId { get; set; }
        public object ElementsId { get; set; }
        public object ClassificationId { get; set; }
    }
}