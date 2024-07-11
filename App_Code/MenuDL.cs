using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SystemAdmin.App_Code
{
    public class MenuDL
    {
        public static void returnTable(MenuPL PL)
        {
            try
            {
                SQLConnectivity SC = new SQLConnectivity();
                SqlCommand sqlCmd = new SqlCommand("MST_SP_Menu", SC.SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@OpCode", SqlDbType.NVarChar).Value = PL.OpCode;
                sqlCmd.Parameters.Add("@AutoId", SqlDbType.VarChar).Value = PL.AutoId;
                sqlCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = PL.CreatedBy;
                sqlCmd.Parameters.Add("@ParentMenu", SqlDbType.VarChar).Value = PL.ParentMenu;
                sqlCmd.Parameters.Add("@SubParentMenu", SqlDbType.VarChar).Value = PL.SubParentMenu;
                sqlCmd.Parameters.Add("@RegionId", SqlDbType.VarChar).Value = PL.RegionId;
                sqlCmd.Parameters.Add("@MenuType", SqlDbType.VarChar).Value = PL.MenuType;
                sqlCmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = PL.IsActive;
                sqlCmd.Parameters.Add("@IsDefault", SqlDbType.VarChar).Value = PL.IsDefault;
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