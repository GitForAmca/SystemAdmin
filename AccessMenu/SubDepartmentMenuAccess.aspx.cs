using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;
namespace SystemAdmin.AccessMenu
{
    public partial class SubDepartmentMenuAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                getIndustry();
                getIndustry(ddlIndustries);
            }
        }
        void getIndustry(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Description";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
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
            PL.OpCode = 8;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlSubDepartment.DataSource = PL.dt;
            ddlSubDepartment.DataValueField = "Autoid";
            ddlSubDepartment.DataTextField = "Subdepartment";
            ddlSubDepartment.DataBind();
            ddlSubDepartment.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getSubParentMenu(int departmentid, string region)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 7;
            PL.AutoId = departmentid;
            PL.IndustryId = ddlIndustries.SelectedValue;
            PL.RegionId = region;
            DropdownDL.returnTable(PL);
            //lstSubParentMenu.DataSource = PL.dt;
            //lstSubParentMenu.DataValueField = "Autoid";
            //lstSubParentMenu.DataTextField = "SubParentMenuName";
            //lstSubParentMenu.DataBind();
            lV_SubParent.DataSource = PL.dt;
            lV_SubParent.DataBind();
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
                getSubParentMenu(Convert.ToInt32(ddlDepartment.SelectedValue), Request.Form[ddlRegion.UniqueID]);
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
            ddlRegion.Items.Clear();
            //lstSubParentMenu.Items.Clear();
        }
        void ClearField()
        {
            ddlIndustries.SelectedIndex = 0;
            ddlRegion.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = 0;
            ddlSubDepartment.SelectedIndex = 0;
            //lstSubParentMenu.SelectedIndex = -1;
        }
        void FillListView()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 11;
            PL.Industry = ddlIndustryFilter.SelectedValue;
            PL.Department = ddlDepartmentFilter.SelectedValue;
            PL.SubDepartment = ddlSubDepartmentFilter.SelectedValue;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_ParentMenu_Access.DataSource = PL.dt;
                LV_ParentMenu_Access.DataBind();
            }
            else
            {
                LV_ParentMenu_Access.DataSource = PL.dt;
                LV_ParentMenu_Access.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<IndustryId><![CDATA[" + ddlIndustries.SelectedValue + "]]></IndustryId>";
            xml += "<SubDepartmentId><![CDATA[" + ddlSubDepartment.SelectedValue + "]]></SubDepartmentId>";

            xml += "</tr>";
            xml += "</tbl>";

            MenuAccessPL PL = new MenuAccessPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 7;
                MenuAccessDL.returnTable(PL);
                hidAutoid.Value = PL.dt.Rows[0]["MainId"].ToString();
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 8;
                PL.AutoId = hidAutoid.Value;
                MenuAccessDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                saveRegion(Convert.ToInt32(hidAutoid.Value));
                saveSubParentMenu(Convert.ToInt32(hidAutoid.Value));
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
                PL.OpCode = 9;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuAccessDL.returnTable(PL);
            }
        }
        void saveSubParentMenu(int mainId)
        { 
            string XML = "";
            XML += "<tbl>";
            foreach (ListViewItem item in lV_SubParent.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                if (chkSelect.Checked == true)
                {
                    CheckBox chkIsChecked = (CheckBox)item.FindControl("chkIsChecked");
                    HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoid");

                    XML += "<tr>";
                    XML += "<MainId><![CDATA[" + mainId + "]]></MainId>";
                    XML += "<SubParentMenuId><![CDATA[" + hdnMenuId.Value + "]]></SubParentMenuId>";
                    XML += "</tr>";
                }
            }
            XML += "</tbl>";
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 10;
            PL.XML = XML;
            PL.AutoId = mainId;
            MenuAccessDL.returnTable(PL);

            //if (groupString != null)
            //{
            //    var query = from val in groupString.Split(',')
            //                select int.Parse(val);
            //    string XML = "";
            //    XML += "<tbl>";
            //    foreach (int num in query)
            //    {
            //        XML += XMLSubParentMenu(mainId, num);
            //    }
            //    XML += "</tbl>";
            //    MenuAccessPL PL = new MenuAccessPL();
            //    PL.OpCode = 10;
            //    PL.XML = XML;
            //    PL.AutoId = mainId;
            //    MenuAccessDL.returnTable(PL);
            //}
        }
        private static string XMLRegion(int MainId, int RegionId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<RegionId><![CDATA[" + RegionId + "]]></RegionId>";
            XML += "</tr>";
            return XML;
        }
        private static string XMLSubParentMenu(int MainId, int SubParentMenuId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<SubParentMenuId><![CDATA[" + SubParentMenuId + "]]></SubParentMenuId>";
            XML += "</tr>";
            return XML;
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_ParentMenu_Access.Items)
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
            PL.OpCode = 11;
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

                getSubParentMenu(Convert.ToInt32(PL.dt.Rows[0]["DepartmentId"].ToString()), PL.dt.Rows[0]["RegionIds"].ToString());
                //SetList(lstSubParentMenu, PL.dt.Rows[0]["SubParentMenuId"].ToString());
                SelectCheckDepartmentDetail(id);
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
            PL.OpCode = 12;
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
        void getIndustry()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL); 
            ddlIndustryFilter.DataSource = PL.dt;
            ddlIndustryFilter.DataValueField = "ID";
            ddlIndustryFilter.DataTextField = "Description";
            ddlIndustryFilter.DataBind();
            ddlIndustryFilter.Items.Insert(0, new ListItem("Choose an item", ""));
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
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlIndustryFilter.SelectedIndex = -1;
            ddlDepartmentFilter.SelectedIndex = -1;
            ddlSubDepartmentFilter.SelectedIndex = -1;
            FillListView();
        }
        void SelectCheckDepartmentDetail(int autoId)
        {
            foreach (ListViewItem item2 in lV_SubParent.Items)
            {
                CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsChecked");
                chkIsChecked.Checked = false;
            }

            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 35;
            PL.AutoId = autoId;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (ListViewItem item2 in lV_SubParent.Items)
                    {
                        HiddenField hdnMenuid = (HiddenField)item2.FindControl("hidautoid");

                        string hdnMenuidVlaue = hdnMenuid.Value;
                        if (hdnMenuid.Value == row["SubParentMenuId"].ToString())
                        {
                            CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsChecked");
                            chkIsChecked.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}