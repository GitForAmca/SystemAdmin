using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.Directors
{
    public partial class Directors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                getDirectorList();
            }
        }
        void getDirectorList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 68;
            DropdownDL.returnTable(PL);
            ddlDirector.DataSource = PL.dt;
            ddlDirector.DataValueField = "Autoid";
            ddlDirector.DataTextField = "Name";
            ddlDirector.DataBind();
            ddlDirector.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 70;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        void ClearField()
        {
            ddlDirector.SelectedIndex = -1;
            txtTeamName.Text = "";
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidID.Value = Autoid.ToString();
                        setForEdit(Autoid);
                    }
                }
            }
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Edit";
        }
        void setForEdit(int id)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 70;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtTeamName.Text = dt.Rows[0]["TeamName"].ToString();
                ddlDirector.SelectedIndex = ddlDirector.Items.IndexOf(ddlDirector.Items.FindByValue(dt.Rows[0]["DirectorId"].ToString()));
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        string GetParentServiceXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<DirectorId><![CDATA[" + ddlDirector.SelectedValue + "]]></DirectorId>";
            xml += "<TeamName><![CDATA[" + txtTeamName.Text.Trim() + "]]></TeamName>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 71;
                PL.XML = GetParentServiceXml();
                PL.CreatedBy = Session["UserAutoId"].ToString();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 72;
                PL.XML = GetParentServiceXml();
                PL.AutoId = Convert.ToInt32(hidID.Value);
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            ClearField();
            FillListView();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        [System.Web.Services.WebMethod]
        public static string CheckExistDirector(string text, string oldname)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 73;
            PL.String1 = text;
            PL.String2 = oldname;
            EssDL.returnTable(PL);
            return PL.dt.Rows[0]["count"].ToString();
        }
        [System.Web.Services.WebMethod]
        public static string CheckExistTeam(string text, string oldname)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 74;
            PL.String1 = text;
            PL.String2 = oldname;
            EssDL.returnTable(PL);
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}