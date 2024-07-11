using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;

namespace SystemAdmin.Menu
{
    public partial class ParentMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getMenuType();
                FillListView();
            }
        }
        void getMenuType()
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 3;
            MenuDL.returnTable(PL);
            ddlMenuType.DataSource = PL.dt;
            ddlMenuType.DataValueField = "AutoId";
            ddlMenuType.DataTextField = "MenuType";
            ddlMenuType.DataBind();
            ddlMenuType.Items.Insert(0, new ListItem("Choose an item", ""));
            ddlMenuTypeFilter.DataSource = PL.dt;
            ddlMenuTypeFilter.DataValueField = "AutoId";
            ddlMenuTypeFilter.DataTextField = "MenuType";
            ddlMenuTypeFilter.DataBind();
            ddlMenuTypeFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtParentMenuName.Text = string.Empty;
            ddlMenuType.SelectedIndex = 0;
        }
        void FillListView()
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 4;
            PL.MenuType = ddlMenuTypeFilter.SelectedValue;
            PL.IsDefault = ddlDefault.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if(PL.dt.Rows.Count > 0)
            {
                LV_ParentMenu.DataSource = PL.dt;
                LV_ParentMenu.DataBind();
            }
            else
            {
                LV_ParentMenu.DataSource = PL.dt;
                LV_ParentMenu.DataBind();
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
            foreach (ListViewItem item in LV_ParentMenu.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoid.Value = chkSelect.Attributes["Autoid"];
                    getData(Autoid);
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        }
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<ParentMenuName><![CDATA[" + txtParentMenuName.Text + "]]></ParentMenuName>";
            xml += "<Type><![CDATA[" + ddlMenuType.SelectedValue + "]]></Type>";
            xml += "<IsDefault><![CDATA[" + chkDefault.Checked + "]]></IsDefault>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";

            xml += "</tr>";
            xml += "</tbl>";

            MenuPL PL = new MenuPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 1;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 2;
                PL.AutoId = hidAutoid.Value;
            }
            MenuDL.returnTable(PL);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void getData(int id)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 4;
            PL.AutoId = id;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtParentMenuName.Text = PL.dt.Rows[0]["ParentMenuName"].ToString();
                ddlMenuType.SelectedIndex = ddlMenuType.Items.IndexOf(ddlMenuType.Items.FindByValue(PL.dt.Rows[0]["Type"].ToString()));
                if (PL.dt.Rows[0]["IsDefault"].ToString() == "True")
                {
                    chkDefault.Checked = true;
                }
                else
                {
                    chkDefault.Checked = false;
                }
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlMenuTypeFilter.SelectedIndex = 0;
            ddlDefault.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }
    }
}