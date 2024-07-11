using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class MenuAccessDL
    {
        public static void returnTable(MenuAccessPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Menu_Access", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = PL.EmpId;
                sqlCmd.Parameters.Add("@Industry", SqlDbType.VarChar).Value = PL.Industry;
                sqlCmd.Parameters.Add("@OldName", SqlDbType.VarChar).Value = PL.OldName;
                sqlCmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = PL.Department;
                sqlCmd.Parameters.Add("@SubDepartment", SqlDbType.VarChar).Value = PL.SubDepartment;
                sqlCmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = PL.Designation;
                sqlCmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PL.Type;
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy;
                sqlCmd.Parameters.Add("@XML", SqlDbType.Xml).Value = PL.XML;

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