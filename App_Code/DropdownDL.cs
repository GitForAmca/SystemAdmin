using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class DropdownDL
    {
        public static void returnTable(DropdownPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Get_Dropdown_List", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.BigInt).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.NVarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@RegionId", SqlDbType.NVarChar).Value = PL.RegionId;
                sqlCmd.Parameters.Add("@IndustryId", SqlDbType.NVarChar).Value = PL.IndustryId;
                sqlCmd.Parameters.Add("@SubDepartmentId", SqlDbType.NVarChar).Value = PL.SubDepartmentId;
                sqlCmd.Parameters.Add("@ServiceTypeAutoid", SqlDbType.NVarChar).Value = PL.ServiceTypeAutoid;

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