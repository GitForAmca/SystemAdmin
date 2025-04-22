using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class EssDL
    {
        public static void returnTable(EssPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_ESS", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = PL.EmpId;
                sqlCmd.Parameters.Add("@Industry", SqlDbType.VarChar).Value = PL.Industry;
                sqlCmd.Parameters.Add("@OldName", SqlDbType.VarChar).Value = PL.OldName;
                sqlCmd.Parameters.Add("@GroupId", SqlDbType.VarChar).Value = PL.GroupId;
                sqlCmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = PL.Type;
                sqlCmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = PL.Department;
                sqlCmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = PL.Action;
                sqlCmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = PL.Category;
                sqlCmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = PL.FromDate;
                sqlCmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = PL.ToDate;
                sqlCmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = PL.IsActive;
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy;
                sqlCmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = PL.Remarks;
                sqlCmd.Parameters.Add("@String1", SqlDbType.VarChar).Value = PL.String1;
                sqlCmd.Parameters.Add("@String2", SqlDbType.VarChar).Value = PL.String2;
                sqlCmd.Parameters.Add("@String3", SqlDbType.VarChar).Value = PL.String3;
                sqlCmd.Parameters.Add("@String4", SqlDbType.VarChar).Value = PL.String4;
                sqlCmd.Parameters.Add("@String5", SqlDbType.VarChar).Value = PL.String5;
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