using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemAdmin.App_Code;

public class LoginDL
{
    public static void returnTable(LoginPL PL)
    {
        try
        {
            SQLConnectivity SC = new SQLConnectivity();
            SqlCommand sqlCmd = new SqlCommand("MST_SP_Login", SC.SqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@OpCode", SqlDbType.BigInt).Value = PL.OpCode;
            sqlCmd.Parameters.Add("@AutoId", SqlDbType.BigInt).Value = PL.AutoId;
            sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = PL.UserName;
            sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = PL.Password;
            sqlCmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = PL.IPAddress;

            sqlCmd.Parameters.Add("@isException", SqlDbType.Bit);
            sqlCmd.Parameters["@isException"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@exceptionMessage", SqlDbType.NVarChar, 500);
            sqlCmd.Parameters["@exceptionMessage"].Direction = ParameterDirection.Output;

            SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCmd);
            PL.dt = new DataTable();
            sqlCmd.CommandTimeout = 0;
            sqlAdp.Fill(PL.dt);
            PL.isException = Convert.ToBoolean(sqlCmd.Parameters["@isException"].Value);
            PL.exceptionMessage = sqlCmd.Parameters["@exceptionMessage"].Value.ToString();
        }
        catch (Exception ex)
        {
            PL.isException = true;
            PL.exceptionMessage = ex.Message;
        }
    }
}
