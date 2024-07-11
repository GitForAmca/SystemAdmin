using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.AccessMenu
{
    public partial class DesignationMenuAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                getIndustry();
            }
        }
        void getIndustry()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL);
            ddlIndustries.DataSource = PL.dt;
            ddlIndustries.DataValueField = "ID";
            ddlIndustries.DataTextField = "Description";
            ddlIndustries.DataBind();
            ddlIndustries.Items.Insert(0, new ListItem("Choose an item", ""));
            ddlIndustryFilter.DataSource = PL.dt;
            ddlIndustryFilter.DataValueField = "ID";
            ddlIndustryFilter.DataTextField = "Description";
            ddlIndustryFilter.DataBind();
            ddlIndustryFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void ddlIndustriesFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustryFilter.SelectedValue != "")
            {
                getDepartmentFilter(Convert.ToInt32(ddlIndustryFilter.SelectedValue));
            }
        }
        protected void ddldepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartmentFilter.SelectedValue != "")
            {
                getSubDepartmentFilter(Convert.ToInt32(ddlDepartmentFilter.SelectedValue));
            }
        }
        void getDepartmentFilter(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 17;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlDepartmentFilter.DataSource = PL.dt;
            ddlDepartmentFilter.DataValueField = "DesignationId";
            ddlDepartmentFilter.DataTextField = "Name";
            ddlDepartmentFilter.DataBind();
            ddlDepartmentFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getSubDepartmentFilter(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 18;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlSubDepartmentFilter.DataSource = PL.dt;
            ddlSubDepartmentFilter.DataValueField = "Autoid";
            ddlSubDepartmentFilter.DataTextField = "GroupName";
            ddlSubDepartmentFilter.DataBind();
            ddlSubDepartmentFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDepartment(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 9;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlDepartment.DataSource = PL.dt;
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataTextField = "Departmentname";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getSubDepartment(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 11;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlSubDepartment.DataSource = PL.dt;
            ddlSubDepartment.DataValueField = "SubDepartmentId";
            ddlSubDepartment.DataTextField = "SubDepartmentName";
            ddlSubDepartment.DataBind();
            ddlSubDepartment.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDesignation(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 12;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlDesignation.DataSource = PL.dt;
            ddlDesignation.DataValueField = "Autoid";
            ddlDesignation.DataTextField = "DesignationSubName";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getRegion(string id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 1;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlRegion.DataSource = PL.dt;
            ddlRegion.DataValueField = "ID";
            ddlRegion.DataTextField = "CountryName";
            ddlRegion.DataBind();
        }
        protected void ddlIndustries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustries.SelectedValue != "")
            {
                getRegion(ddlIndustries.SelectedValue);
                getDepartment(Convert.ToInt32(ddlIndustries.SelectedValue));
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartment.SelectedValue != "")
            {
                getSubDepartment(Convert.ToInt32(ddlDepartment.SelectedValue));
            }
        }
        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubDepartment.SelectedValue != "")
            {
                getDesignation(Convert.ToInt32(ddlSubDepartment.SelectedValue));
                getChildMenu(Convert.ToInt32(ddlSubDepartment.SelectedValue), Request.Form[ddlRegion.UniqueID], Convert.ToInt32(ddlIndustries.SelectedValue));
            }
        }
        void getChildMenu(int id, string region, int industries)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 13;
            PL.AutoId = id;
            PL.RegionId = region;
            PL.IndustryId = industries;
            DropdownDL.returnTable(PL);
            lstChildMenu.DataSource = PL.dt;
            lstChildMenu.DataValueField = "Autoid";
            lstChildMenu.DataTextField = "MenuName";
            lstChildMenu.DataBind();
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
            ddlRegion.Items.Clear();
            lstChildMenu.Items.Clear();
        }
        void ClearField()
        {
            ddlIndustries.SelectedIndex = 0;
            ddlRegion.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = 0;
            ddlSubDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            lstChildMenu.SelectedIndex = -1;
        }
        void FillListView()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 17;
            PL.Industry = ddlIndustryFilter.SelectedValue;
            PL.Department = ddlDepartmentFilter.SelectedValue;
            PL.SubDepartment = ddlSubDepartmentFilter.SelectedValue;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_ChildMenu_Access.DataSource = PL.dt;
                LV_ChildMenu_Access.DataBind();
            }
            else
            {
                LV_ChildMenu_Access.DataSource = PL.dt;
                LV_ChildMenu_Access.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<IndustryId><![CDATA[" + ddlIndustries.SelectedValue + "]]></IndustryId>";
            xml += "<DesignaitonId><![CDATA[" + ddlDesignation.SelectedValue + "]]></DesignaitonId>";

            xml += "</tr>";
            xml += "</tbl>";

            MenuAccessPL PL = new MenuAccessPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 13;
                MenuAccessDL.returnTable(PL);
                hidAutoid.Value = PL.dt.Rows[0]["MainId"].ToString();
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 14;
                PL.AutoId = hidAutoid.Value;
                MenuAccessDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                saveRegion(Convert.ToInt32(hidAutoid.Value));
                saveChildMenu(Convert.ToInt32(hidAutoid.Value));
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
        void saveRegion(int mainId)
        {
            string groupString = Request.Form[ddlRegion.UniqueID];
            if (groupString != null)
            {
                var query = from val in groupString.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLRegion(mainId, num);
                }
                XML += "</tbl>";
                MenuAccessPL PL = new MenuAccessPL();
                PL.OpCode = 15;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuAccessDL.returnTable(PL);
            }
        }
        void saveChildMenu(int mainId)
        {
            string groupString = Request.Form[lstChildMenu.UniqueID];
            if (groupString != null)
            {
                var query = from val in groupString.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLChildMenu(mainId, num);
                }
                XML += "</tbl>";
                MenuAccessPL PL = new MenuAccessPL();
                PL.OpCode = 16;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuAccessDL.returnTable(PL);
            }
        }
        private static string XMLRegion(int MainId, int RegionId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<RegionId><![CDATA[" + RegionId + "]]></RegionId>";
            XML += "</tr>";
            return XML;
        }
        private static string XMLChildMenu(int MainId, int ChildMenuId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<ChildMenuId><![CDATA[" + ChildMenuId + "]]></ChildMenuId>";
            XML += "</tr>";
            return XML;
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_ChildMenu_Access.Items)
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
        void getData(int id)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 17;
            PL.AutoId = id;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ddlIndustries.SelectedIndex = ddlIndustries.Items.IndexOf(ddlIndustries.Items.FindByValue(PL.dt.Rows[0]["IndustryId"].ToString()));
                getRegion(PL.dt.Rows[0]["IndustryId"].ToString());
                SetList(ddlRegion, PL.dt.Rows[0]["RegionIds"].ToString());
                getDepartment(Convert.ToInt32(PL.dt.Rows[0]["IndustryId"].ToString()));
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(PL.dt.Rows[0]["DepartmentId"].ToString()));

                getSubDepartment(Convert.ToInt32(PL.dt.Rows[0]["DepartmentId"].ToString()));
                ddlSubDepartment.SelectedIndex = ddlSubDepartment.Items.IndexOf(ddlSubDepartment.Items.FindByValue(PL.dt.Rows[0]["SubDepartmentId"].ToString()));

                getDesignation(Convert.ToInt32(PL.dt.Rows[0]["SubDepartmentId"].ToString()));
                ddlDesignation.SelectedIndex = ddlDesignation.Items.IndexOf(ddlDesignation.Items.FindByValue(PL.dt.Rows[0]["DesignaitonId"].ToString()));

                getChildMenu(Convert.ToInt32(PL.dt.Rows[0]["SubDepartmentId"].ToString()), PL.dt.Rows[0]["RegionIds"].ToString(), Convert.ToInt32(PL.dt.Rows[0]["IndustryId"].ToString()));
                SetList(lstChildMenu, PL.dt.Rows[0]["ChildMenuIds"].ToString());
            }
        }
        void SetList(ListBox ddl, string ids)
        {
            ddl.SelectedIndex = -1;
            foreach (var item in ids.Split(','))
            {
                foreach (ListItem item2 in ddl.Items)
                {
                    if (item2.Value == item)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }
        }
        [System.Web.Services.WebMethod]
        public static string CheckName(string value, string oldname, string industry)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 18;
            PL.AutoId = Convert.ToInt32(value);
            PL.Industry = industry;
            PL.OldName = oldname;
            MenuAccessDL.returnTable(PL);
            return PL.dt.Rows[0]["count"].ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
        }
    }
}