using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class ServiceMasterDL
    {
        public static void returnTable(ServiceMasterPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Service", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = PL.EmpId;
                sqlCmd.Parameters.Add("@Industry", SqlDbType.VarChar).Value = PL.Industry;
                sqlCmd.Parameters.Add("@OldName", SqlDbType.VarChar).Value = PL.OldName;
                sqlCmd.Parameters.Add("@GroupId", SqlDbType.VarChar).Value = PL.GroupId;
                sqlCmd.Parameters.Add("@Region", SqlDbType.VarChar).Value = PL.Region;
                sqlCmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = PL.Category;
                sqlCmd.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = PL.CompanyId;
                sqlCmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = PL.DepartmentId;
                sqlCmd.Parameters.Add("@SubdepartmentId", SqlDbType.VarChar).Value = PL.SubDepartmentId;
                sqlCmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PL.Type;
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy;
                sqlCmd.Parameters.Add("@XML", SqlDbType.Xml).Value = PL.XML;
                sqlCmd.Parameters.Add("@XML1", SqlDbType.Xml).Value = PL.XML1;
                sqlCmd.Parameters.Add("@XML2", SqlDbType.Xml).Value = PL.XML2;

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