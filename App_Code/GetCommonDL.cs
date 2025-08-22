using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AMCAPropertiesAdmin.App_Code
{
    public class GetCommonDL
    {
        public static void returnTable(GetCommonPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Get_Common", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.BigInt).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.NVarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@EmiratesAutoid", SqlDbType.NVarChar).Value = PL.EmiratesAutoid; 
                sqlCmd.Parameters.Add("@AreaId", SqlDbType.NVarChar).Value = PL.AreaId; 
                sqlCmd.Parameters.Add("@DevelopersId", SqlDbType.NVarChar).Value = PL.DevelopersId; 
                sqlCmd.Parameters.Add("@StatusId", SqlDbType.NVarChar).Value = PL.StatusId; 
                sqlCmd.Parameters.Add("@CommunityId", SqlDbType.NVarChar).Value = PL.CommunityId;
                sqlCmd.Parameters.Add("@TowerId", SqlDbType.NVarChar).Value = PL.TowerId;
                sqlCmd.Parameters.Add("@UnitTypeId", SqlDbType.NVarChar).Value = PL.UnitTypeId;
                sqlCmd.Parameters.Add("@PurposeId", SqlDbType.NVarChar).Value = PL.PurposeId;
                sqlCmd.Parameters.Add("@ElementsId", SqlDbType.NVarChar).Value = PL.ElementsId;
                sqlCmd.Parameters.Add("@ClassificationId", SqlDbType.NVarChar).Value = PL.ClassificationId;

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