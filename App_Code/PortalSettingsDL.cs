using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class PortalSettingsDL
    {
        public static void returnTable(PortalSettingsPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_PortalSettings", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.NVarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@StartTime", SqlDbType.NVarChar).Value = PL.StartTime;
                sqlCmd.Parameters.Add("@EndTime", SqlDbType.NVarChar).Value = PL.EndTime;
                sqlCmd.Parameters.Add("@DowntimeMessage", SqlDbType.NVarChar).Value = PL.DowntimeMessage;
                sqlCmd.Parameters.Add("@ExcludedEmployees", SqlDbType.NVarChar).Value = PL.ExcludedEmployees;
                sqlCmd.Parameters.Add("@Region", SqlDbType.NVarChar).Value = PL.Region;
                sqlCmd.Parameters.Add("@StartTimeOrg", SqlDbType.NVarChar).Value = PL.StartTimeOrg;
                sqlCmd.Parameters.Add("@EndTimeOrg", SqlDbType.NVarChar).Value = PL.EndTimeOrg;
                sqlCmd.Parameters.Add("@DowntimeMessageOrg", SqlDbType.NVarChar).Value = PL.DowntimeMessageOrg;
                sqlCmd.Parameters.Add("@ExcludedEmployeesOrg", SqlDbType.NVarChar).Value = PL.ExcludedEmployeesOrg;
                sqlCmd.Parameters.Add("@RegionOrg", SqlDbType.NVarChar).Value = PL.RegionOrg;
                sqlCmd.Parameters.Add("@StartTimeHrms", SqlDbType.NVarChar).Value = PL.StartTimeHrms;
                sqlCmd.Parameters.Add("@EndTimeHrms", SqlDbType.NVarChar).Value = PL.EndTimeHrms;
                sqlCmd.Parameters.Add("@ValidationPeriod", SqlDbType.NVarChar).Value = PL.ValidationPeriod;
                SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
                PL.dt = new DataTable();
                sqlAdp.Fill(PL.dt);
            }
            catch (Exception ex)
            {
                PL.isException = true;
                PL.exceptionMessage = ex.Message;
            }
        }
    }
}