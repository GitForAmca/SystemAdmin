using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AMCAPropertiesAdmin.App_Code
{
    public class CommunityDL
    {
        public static void returnTable(CommunityPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_INS_UPD_PROPERTIES", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy; 
                sqlCmd.Parameters.Add("@ElementId", SqlDbType.VarChar).Value = PL.ElementId; 
                sqlCmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = PL.Description;
                sqlCmd.Parameters.Add("@PlaceId", SqlDbType.VarChar).Value = PL.PlaceId;
                sqlCmd.Parameters.Add("@PlaceDistance", SqlDbType.VarChar).Value = PL.PlaceDistance;
                sqlCmd.Parameters.Add("@Path", SqlDbType.VarChar).Value = PL.path;
                sqlCmd.Parameters.Add("@PathType", SqlDbType.VarChar).Value = PL.pathType;
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