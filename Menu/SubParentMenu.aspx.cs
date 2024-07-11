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
    public partial class SubParentMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getMenuType();
                getParentMenu(ddlParentMenuFilter, "", "");
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
        protected void ddlMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenuType.SelectedValue != "")
            {
                getParentMenu(ddlParentMenu, ddlMenuType.SelectedValue,"");
            }
        }
        void getParentMenu(DropDownList ddl, string MenuType, string Autoid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 5;
            PL.MenuType = MenuType;
            PL.AutoId = Autoid;
            MenuDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "ParentMenuName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtSubParentMenuName.Text = string.Empty;
            ddlMenuType.SelectedIndex = 0;
            ddlParentMenu.SelectedIndex = 0;
        }
        void FillListView()
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 8;
            PL.MenuType = ddlMenuTypeFilter.SelectedValue;
            PL.ParentMenu = ddlParentMenuFilter.SelectedValue;
            PL.IsDefault = ddlDefault.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
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
            ddlParentMenu.Items.Clear();
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

            xml += "<ParentMenuId><![CDATA[" + ddlParentMenu.SelectedValue + "]]></ParentMenuId>";
            xml += "<SubParentMenuName><![CDATA[" + txtSubParentMenuName.Text + "]]></SubParentMenuName>";
            xml += "<IsDefault><![CDATA[" + chkDefault.Checked + "]]></IsDefault>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";

            xml += "</tr>";
            xml += "</tbl>";

            MenuPL PL = new MenuPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 6;
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 7;
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
            PL.OpCode = 8;
            PL.AutoId = id;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtSubParentMenuName.Text = PL.dt.Rows[0]["SubParentMenuName"].ToString();
                ddlMenuType.SelectedIndex = ddlMenuType.Items.IndexOf(ddlMenuType.Items.FindByValue(PL.dt.Rows[0]["Type"].ToString()));
                getParentMenu(ddlParentMenu, "" ,PL.dt.Rows[0]["ParentMenuId"].ToString());
                ddlParentMenu.SelectedIndex = ddlParentMenu.Items.IndexOf(ddlParentMenu.Items.FindByValue(PL.dt.Rows[0]["ParentMenuId"].ToString()));
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
            ddlParentMenuFilter.SelectedIndex = 0;
            ddlDefault.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }
    }
}