using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class ProductsDL
    {
        public static void returnTable(ProductsPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Products", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
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