using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;

namespace SystemAdmin.AccessMenu
{
    public partial class DepartmentMenuAccess : System.Web.UI.Page
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
        void getDepartment(string IndustryId,string RegionId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 5;
            PL.IndustryId = IndustryId;
            PL.RegionId = RegionId;
            DropdownDL.returnTable(PL);
            ddlDepartment.DataSource = PL.dt;
            ddlDepartment.DataValueField = "Autoid";
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        //void getParentMenu(string region, int indutryid)
        //{
        //    DropdownPL PL = new DropdownPL();
        //    PL.OpCode = 6;
        //    PL.RegionId = region;
        //    PL.IndustryId = indutryid;
        //    DropdownDL.returnTable(PL);
        //    lstParentMenu.DataSource = PL.dt;
        //    lstParentMenu.DataValueField = "Autoid";
        //    lstParentMenu.DataTextField = "ParentMenuName";
        //    lstParentMenu.DataBind();
        //}
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
                //getParentMenu(Request.Form[ddlRegion.UniqueID], Convert.ToInt32(ddlIndustries.SelectedValue));
                FillDepartmetnDetail(Request.Form[ddlRegion.UniqueID], Convert.ToInt32(ddlIndustries.SelectedValue));
            }
        }
        protected void btnGetDepartment_Click(object sender, EventArgs e)
        {
            getDepartment(ddlIndustries.SelectedValue, Request.Form[ddlRegion.UniqueID]);
            //getParentMenu(Request.Form[ddlRegion.UniqueID], Convert.ToInt32(ddlIndustries.SelectedValue));
            FillDepartmetnDetail(Request.Form[ddlRegion.UniqueID], Convert.ToInt32(ddlIndustries.SelectedValue));
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
            ddlRegion.Items.Clear();
            ddlDepartment.Items.Clear();
            //lstParentMenu.Items.Clear();
        }
        void ClearField()
        {
            ddlIndustries.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            //lstParentMenu.SelectedIndex = 0;

        }
        void FillListView()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 5;
            PL.Industry = ddlIndustryFilter.SelectedValue;
            PL.Department = ddlDepartmentFilter.SelectedValue;
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
            xml += "<DepartmentId><![CDATA[" + ddlDepartment.SelectedValue + "]]></DepartmentId>";

            xml += "</tr>";
            xml += "</tbl>";

            MenuAccessPL PL = new MenuAccessPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 1;
                MenuAccessDL.returnTable(PL);
                hidAutoid.Value = PL.dt.Rows[0]["MainId"].ToString();
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 2;
                PL.AutoId = hidAutoid.Value;
                MenuAccessDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                saveRegion(Convert.ToInt32(hidAutoid.Value));
                saveParentMenu(Convert.ToInt32(hidAutoid.Value));
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
                PL.OpCode = 3;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuAccessDL.returnTable(PL);
            }
        }
        void saveParentMenu(int mainId)
        {
            //string groupString = Request.Form[lstParentMenu.UniqueID];
            string XML = "";
            XML += "<tbl>";
            foreach (ListViewItem item in lstDEpartmentDetail.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                if (chkSelect.Checked == true)
                {
                    CheckBox chkIsChecked = (CheckBox)item.FindControl("chkIsChecked");
                    HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoid");

                    XML += "<tr>"; 
                    XML += "<MainId><![CDATA[" + mainId + "]]></MainId>";
                    XML += "<ParentMenuId><![CDATA[" + hdnMenuId.Value + "]]></ParentMenuId>"; 
                    XML += "</tr>";
                }
            }
            XML += "</tbl>";
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 4;
            PL.XML = XML;
            PL.AutoId = mainId;
            MenuAccessDL.returnTable(PL); 
        }
        private static string XMLRegion(int MainId, int RegionId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<RegionId><![CDATA[" + RegionId + "]]></RegionId>";
            XML += "</tr>";
            return XML;
        }
        private static string XMLParentMenu(int MainId, int ParentMenuId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<ParentMenuId><![CDATA[" + ParentMenuId + "]]></ParentMenuId>";
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
            PL.OpCode = 5;
            PL.AutoId = id;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ddlIndustries.SelectedIndex = ddlIndustries.Items.IndexOf(ddlIndustries.Items.FindByValue(PL.dt.Rows[0]["IndustryId"].ToString()));
                getRegion(PL.dt.Rows[0]["IndustryId"].ToString());
                SetList(ddlRegion, PL.dt.Rows[0]["RegionIds"].ToString());
                getDepartment(PL.dt.Rows[0]["IndustryId"].ToString(), PL.dt.Rows[0]["RegionIds"].ToString());
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(PL.dt.Rows[0]["DepartmentId"].ToString()));
                //getParentMenu(PL.dt.Rows[0]["RegionIds"].ToString(), Convert.ToInt32(PL.dt.Rows[0]["IndustryId"].ToString()));
                FillDepartmetnEdit(PL.dt.Rows[0]["RegionIds"].ToString(), Convert.ToInt32(PL.dt.Rows[0]["IndustryId"].ToString()));
                //SetList(lstParentMenu, PL.dt.Rows[0]["ParentMenuIds"].ToString());
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
            PL.OpCode = 6;
            PL.AutoId = Convert.ToInt32(value);
            PL.Industry = industry;
            PL.OldName = oldname;
            MenuAccessDL.returnTable(PL);
            return PL.dt.Rows[0]["count"].ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

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
        protected void ddlIndustriesFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustryFilter.SelectedValue != "")
            {
                getDepartmentFilter(Convert.ToInt32(ddlIndustryFilter.SelectedValue));
            }
        }
        protected void ddldepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlIndustryFilter.SelectedIndex = -1;
            ddlDepartmentFilter.SelectedIndex = -1;
            FillListView();
        }
        void FillDepartmetnDetail(string region, int indutryid)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 6;
            PL.RegionId = region;
            PL.IndustryId = indutryid;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;

            if (PL.dt.Rows.Count > 0)
            {
                lstDEpartmentDetail.DataSource = PL.dt;
                lstDEpartmentDetail.DataBind();  

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (Convert.ToBoolean(row["IsDefault"]))
                    {
                        ListViewItem item2 = lstDEpartmentDetail.Items[i]; // assuming row and item are aligned by index
                        CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsChecked");
                        chkIsChecked.Checked = true;
                    }
                }
            }
            else
            {
                lstDEpartmentDetail.DataSource = PL.dt;
                lstDEpartmentDetail.DataBind();
            }
        }
        void FillDepartmetnEdit(string region, int indutryid)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 6;
            PL.RegionId = region;
            PL.IndustryId = indutryid;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt; 
            if (PL.dt.Rows.Count > 0)
            {
                lstDEpartmentDetail.DataSource = PL.dt;
                lstDEpartmentDetail.DataBind(); 
            }
            else
            {
                lstDEpartmentDetail.DataSource = PL.dt;
                lstDEpartmentDetail.DataBind();
            }
        }
        void SelectCheckDepartmentDetail(int autoId)
        {
            foreach (ListViewItem item2 in lstDEpartmentDetail.Items)
            {
                CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsChecked");
                chkIsChecked.Checked = false;
            }

            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 34;
            PL.AutoId = autoId;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (ListViewItem item2 in lstDEpartmentDetail.Items)
                    {
                        HiddenField hdnMenuid = (HiddenField)item2.FindControl("hidautoid");

                        string hdnMenuidVlaue = hdnMenuid.Value;
                        if (hdnMenuid.Value == row["ParentMenuId"].ToString())
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