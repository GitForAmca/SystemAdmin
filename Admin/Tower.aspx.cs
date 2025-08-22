using AMCAPropertiesAdmin.App_Code;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class Tower : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetCommunityList(ddlCommunityFilter);
                GetTowerFilter(ddlTowerFilter);
                GetCommunityList(ddlCommunity);  
                FillListView();
            }
        }
        void GetTowerFilter(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 21;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "TowerName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetCommunityList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 15;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "CommunityName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            ddlCommunity.SelectedIndex = -1;
            txtTower.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 

            CommunityPL PL = new CommunityPL();

            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<CommunityId><![CDATA[" + ddlCommunity.SelectedValue + "]]></CommunityId>";
            xml += "<TowerName><![CDATA[" + txtTower.Text + "]]></TowerName>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";

            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString(); 

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 24;
                CommunityDL.returnTable(PL);
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 25;
                PL.AutoId = hidTowerId.Value;
                CommunityDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                divView.Visible = true;
                divEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Save Successfully');", true);
            }

            else
            {
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 31;
            PL.CommunityId = ddlCommunityFilter.SelectedValue;
            PL.TowerId = ddlTowerFilter.SelectedValue;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Tower.DataSource = PL.dt;
                LV_Tower.DataBind();
            }
            else
            {
                LV_Tower.DataSource = "";
                LV_Tower.DataBind();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Tower.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidTowerId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidTowerId.Value)); 
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        }
        void getData(int id)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 32;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtTower.Text = PL.dt.Rows[0]["TowerName"].ToString();
                chkActive.Checked = Convert.ToBoolean(PL.dt.Rows[0]["IsActive"].ToString());


                GetCommunityList(ddlCommunity);
                ddlCommunity.SelectedIndex = ddlCommunity.Items.IndexOf(ddlCommunity.Items.FindByValue(PL.dt.Rows[0]["CommunityId"].ToString()));

            }
        }  
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
            Response.Redirect(Request.RawUrl);
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlCommunityFilter.SelectedIndex = -1; 
            ddlTowerFilter.SelectedIndex = -1; 
            FillListView();
        } 
    }
}