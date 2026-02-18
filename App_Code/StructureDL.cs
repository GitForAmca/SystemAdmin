using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class StructureDL
    {
        public static void returnTable(StructurePL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_GroupStructure", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId; 
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy;
                sqlCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = PL.Name;
                sqlCmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = PL.Description;
                sqlCmd.Parameters.Add("@XML", SqlDbType.Xml).Value = PL.XML;
                sqlCmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = PL.IsActive;
                sqlCmd.Parameters.Add("@IndustryId", SqlDbType.VarChar).Value = PL.IndustryId;
                sqlCmd.Parameters.Add("@HOD", SqlDbType.VarChar).Value = PL.HOD;
                sqlCmd.Parameters.Add("@XMLData", SqlDbType.Xml).Value = PL.XMLData;
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