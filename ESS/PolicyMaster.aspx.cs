using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;


namespace SystemAdmin.ESS
{
    public partial class PolicyMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAutoId"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            { 
                FillListView();
                BindRegion(ddlRegion);
                BindIndustry(ddl_industry);
                BindCategory(ddlCategory);
            }
        } 
        void BindRegion(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 60;
            PL.AutoId = Session["UserAutoId"].ToString(); 
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));

            ////////////////////////

            ddlRegionSearch.DataSource = PL.dt;
            ddlRegionSearch.DataValueField = "Id";
            ddlRegionSearch.DataTextField = "Name";
            ddlRegionSearch.DataBind();
            ddlRegionSearch.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        void BindIndustry(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 61;
            PL.AutoId = Session["UserAutoId"].ToString(); 
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));
            ////////////////////////

            ddlIndustrySearch.DataSource = PL.dt;
            ddlIndustrySearch.DataValueField = "Id";
            ddlIndustrySearch.DataTextField = "Name";
            ddlIndustrySearch.DataBind();
            ddlIndustrySearch.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        void BindCategory(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 62;
            PL.AutoId = Session["UserAutoId"].ToString();
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));
            ////////////////////////

            ddlCategorySearch.DataSource = PL.dt;
            ddlCategorySearch.DataValueField = "Id";
            ddlCategorySearch.DataTextField = "Name";
            ddlCategorySearch.DataBind();
            ddlCategorySearch.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        void BindAMCAGroupCompany(ListBox ddl, string RegionId, string IndustryId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 33;
            PL.Region = RegionId;
            PL.Industry = IndustryId;
            ServiceMasterDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", "")); 
        }
        void BindAMCAGroupCompanySearch(ListBox ddl, string RegionId, string IndustryId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 33;
            PL.Region = RegionId;
            PL.Industry = IndustryId;
            ServiceMasterDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        void BindDepartment(DropDownList ddl, string RegionId, string IndustryId)
        { 
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 34;
            PL.Region = RegionId;
            PL.Industry = IndustryId;
            ServiceMasterDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        void BindDepartmentSearch(DropDownList ddl, string RegionId, string IndustryId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 34;
            PL.Region = RegionId;
            PL.Industry = IndustryId;
            ServiceMasterDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item.", ""));
        }
        public void GetSubDepartment(string Depid) // Bind SubDepartment
        {
            if(Depid == "")
            {
                Depid = "0";
            }
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 35;
            PL.AutoId = Convert.ToInt32(Depid);
            ServiceMasterDL.returnTable(PL);
            ddlSubDepartment.DataSource = PL.dt;
            ddlSubDepartment.DataValueField = "Id";
            ddlSubDepartment.DataTextField = "Name";
            ddlSubDepartment.DataBind();
            ddlSubDepartment.Items.Insert(0, new ListItem("Select", ""));
        } 
        void BindHeirarchyBasedonDepartment() // Bind Department Heirarchy
        {
            GetSubDepartment(ddlDepartment.SelectedValue);  // Bind SubDepartment 
        }

        protected void chkGroupCompany_DataBinding(object sender, EventArgs e)
        {
            ((CheckBoxList)sender).DataSource = GetGroup();
        }
        public DataTable GetGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            return PL.dt;
        } 
        void FillListView()
        {
            //-----------------
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 37;
            PL.Region = ddlRegionSearch.SelectedValue; 
            PL.Industry = ddlIndustrySearch.SelectedValue; 
            PL.Category = ddlCategorySearch.SelectedValue; 
            PL.CompanyId = ddlCompanySearch.SelectedValue; 
            PL.DepartmentId = ddlDepartmentSearch.SelectedValue;  
            PL.SubDepartmentId = ddlSubDepartmentSearch.SelectedValue; 
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        } 
        void ClearField()
        {
            txteditorAMCAHandbook.InnerText = "";
            txtPolicyName.Text = "";
            ddlCategory.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;
            ddl_industry.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            ddlSubDepartment.SelectedIndex = -1;
            ddlAMCAGroupCompany.SelectedIndex = -1;
            //ddlRegionSearch.SelectedIndex = -1;
            //ddlIndustrySearch.SelectedIndex = -1;
            //ddlCategorySearch.SelectedIndex = -1;
            //ddlCompanySearch.SelectedIndex = -1;
            //ddlDepartmentSearch.SelectedIndex = -1;
            //ddlSubDepartmentSearch.SelectedIndex = -1;
            divRegion.Visible = false;
            divIndustry.Visible = false;
            divCompany.Visible = false;
            divDepartment.Visible = false;
            divSubDepartment.Visible = false;
            divPolicyName.Visible = false;
            hidID.Value = "";
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";  
        }
        protected void ddlUpdateGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
           // setForEdit(Convert.ToInt32(ddlUpdateGroupCompany.SelectedValue), Convert.ToInt32(hidID.Value));
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
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 38; 
            PL.AutoId = id;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["PolicyId"].ToString() == "1")
                {
                    txteditorAMCAHandbook.InnerText = dt.Rows[0]["Content"].ToString();
                    divAMCAEmployeeHandBook.Visible = true;
                    divDepartmentalPolicy.Visible = false;
                    divSubDepartmentalPolicy.Visible = false;
                    divRegion.Visible = true;
                    divIndustry.Visible = true;
                    divCompany.Visible = true;
                    divDepartment.Visible = false;
                    divSubDepartment.Visible = false;
                    divPolicyName.Visible = true;
                }
                else if (dt.Rows[0]["PolicyId"].ToString() == "2")
                {
                    txteditorDepartmentalPolicy.InnerText = dt.Rows[0]["Content"].ToString();
                    divAMCAEmployeeHandBook.Visible = false;
                    divDepartmentalPolicy.Visible = true;
                    divSubDepartmentalPolicy.Visible = false;
                    divRegion.Visible = true;
                    divIndustry.Visible = true;
                    divCompany.Visible = false;
                    divDepartment.Visible = true;
                    divSubDepartment.Visible = false;
                    divPolicyName.Visible = true;
                }
                else if (dt.Rows[0]["PolicyId"].ToString() == "3")
                {
                    txteditorSubDepartmentalPolicy.InnerText = dt.Rows[0]["Content"].ToString();
                    divAMCAEmployeeHandBook.Visible = false;
                    divDepartmentalPolicy.Visible = false;
                    divSubDepartmentalPolicy.Visible = true;
                    divRegion.Visible = true;
                    divIndustry.Visible = true;
                    divCompany.Visible = false;
                    divDepartment.Visible = true;
                    divSubDepartment.Visible = true;
                    divPolicyName.Visible = true;
                }
                else if (dt.Rows[0]["PolicyId"].ToString() == "4")
                {
                    divRegion.Visible = true;
                    divIndustry.Visible = true;
                    divCompany.Visible = true;
                    divPolicyName.Visible = true;
                }

                hidID.Value = dt.Rows[0]["Autoid"].ToString();
                txtPolicyName.Text = dt.Rows[0]["Name"].ToString();
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(dt.Rows[0]["PolicyId"].ToString()));
                ddlRegion.SelectedIndex = ddlRegion.Items.IndexOf(ddlRegion.Items.FindByValue(dt.Rows[0]["Region"].ToString()));
                ddl_industry.SelectedIndex = ddl_industry.Items.IndexOf(ddl_industry.Items.FindByValue(dt.Rows[0]["IndustryId"].ToString()));

                if(ddl_industry.SelectedIndex != 0)
                { 
                    BindAMCAGroupCompany(ddlAMCAGroupCompany, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
                }
                ddlAMCAGroupCompany.SelectedIndex = ddlAMCAGroupCompany.Items.IndexOf(ddlAMCAGroupCompany.Items.FindByValue(dt.Rows[0]["CompanyId"].ToString()));

                if (ddlDepartment.SelectedIndex != 0 )
                {    
                    BindDepartment(ddlDepartment, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
                } 
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(dt.Rows[0]["DepartmentId"].ToString()));

                GetSubDepartment(ddlDepartment.SelectedValue);
                ddlSubDepartment.SelectedIndex = ddlSubDepartment.Items.IndexOf(ddlSubDepartment.Items.FindByValue(dt.Rows[0]["SubDepartmentId"].ToString()));
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        string GetParentServiceXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<PolicyName><![CDATA[" + txtPolicyName.Text.Trim() + "]]></PolicyName>";
            xml += "<Content><![CDATA[" + txteditorAMCAHandbook.InnerText + "]]></Content>";
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>"; 
            xml += "<Industry><![CDATA[" + ddl_industry.SelectedValue + "]]></Industry>"; 
            xml += "<CompanyId><![CDATA[" + ddlAMCAGroupCompany.SelectedValue + "]]></CompanyId>"; 
            xml += "<Department><![CDATA[" + ddlDepartment.SelectedValue + "]]></Department>"; 
            xml += "<SubDepartment><![CDATA[" + ddlSubDepartment.SelectedValue + "]]></SubDepartment>";
            xml += "<PolicyId><![CDATA[" + ddlCategory.SelectedValue + "]]></PolicyId>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        } 
        protected void btnsave_Click(object sender, EventArgs e)
        { 
                ServiceMasterPL PL = new ServiceMasterPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 36;
                    PL.XML = GetParentServiceXml(); 
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
                    if (!PL.isException)
                    { 
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ClearField();
                        FillListView();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Policy save successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    updateEmailConentXml(hidID.Value);
                }
                ClearField(); 
                FillListView(); 
        }

        [System.Web.Services.WebMethod]
        public static string CheckName(string text, string oldname)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 14;
            PL.Type = text;
            PL.OldName = oldname;
            ServiceMasterDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
        [System.Web.Services.WebMethod]
        public static string QuestionsData(string DocumentId)
        {
            string jsondata = "";
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 15;
            PL.AutoId = Convert.ToInt32(DocumentId);
            ServiceMasterDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                jsondata = new clsGeneral().JSONfromDT(PL.dt);
            }
            return jsondata;
        }

        protected void ddlGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        } 
        void updateEmailConentXml(string mainEmailId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<PolicyName><![CDATA[" + txtPolicyName.Text.Trim() + "]]></PolicyName>";
            if (ddlCategory.SelectedValue == "1")
            {
                xml += "<Content><![CDATA[" + txteditorAMCAHandbook.InnerText + "]]></Content>";
            }
            else if (ddlCategory.SelectedValue == "2")
            {
                xml += "<Content><![CDATA[" + txteditorDepartmentalPolicy.InnerText + "]]></Content>";
            }
            else if (ddlCategory.SelectedValue == "3")
            {
                xml += "<Content><![CDATA[" + txteditorSubDepartmentalPolicy.InnerText + "]]></Content>";
            }
            else if (ddlCategory.SelectedValue == "4")
            {
                xml += "<Content><![CDATA[" + txteditorAMCAHandbook.InnerText + "]]></Content>";
            }
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>";
            xml += "<Industry><![CDATA[" + ddl_industry.SelectedValue + "]]></Industry>";
            xml += "<CompanyId><![CDATA[" + ddlAMCAGroupCompany.SelectedValue + "]]></CompanyId>";
            xml += "<Department><![CDATA[" + ddlDepartment.SelectedValue + "]]></Department>";
            xml += "<SubDepartment><![CDATA[" + ddlSubDepartment.SelectedValue + "]]></SubDepartment>";
            xml += "<PolicyId><![CDATA[" + ddlCategory.SelectedValue + "]]></PolicyId>";
            xml += "</tr>";
            xml += "</tbl>";
            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.AutoId = mainEmailId; 
            PL.OpCode = 39;
            PL.XML = xml;
            ServiceMasterDL.returnTable(PL);
            if (!PL.isException)
            {
                divView.Visible = true;
                divAddEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Policy Update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        } 
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAMCAGroupCompany(ddlAMCAGroupCompany, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
            BindDepartment(ddlDepartment, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
        }
        protected void ddl_industry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDepartment(ddlDepartment, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
            BindAMCAGroupCompany(ddlAMCAGroupCompany, ddlRegion.SelectedValue, ddl_industry.SelectedValue);
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindHeirarchyBasedonDepartment();
        }
        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindHeirarchyBasedonSubDepartment();
        }
        void BindHeirarchyBasedonSubDepartment() // Bind SubDepartment Heirarchy
        {
            //GetDesignation(ddlSubDepartment.SelectedValue, ddlRegion.SelectedValue, ddl_industry.SelectedValue); // Bind Designation
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCategory.SelectedValue == "1")
            {
                divRegion.Visible = true;
                divIndustry.Visible = true;
                divCompany.Visible = true;
                divDepartment.Visible = false;
                divSubDepartment.Visible = false;
                divPolicyName.Visible = true;
                divAMCAEmployeeHandBook.Visible = true;
                divDepartmentalPolicy.Visible = false;
                divSubDepartmentalPolicy.Visible = false;
            }
            else if (ddlCategory.SelectedValue == "2")
            {
                divRegion.Visible = true;
                divIndustry.Visible = true;
                divCompany.Visible = false;
                divDepartment.Visible = true;
                divSubDepartment.Visible = false;
                divPolicyName.Visible = true;
                divAMCAEmployeeHandBook.Visible = false;
                divSubDepartmentalPolicy.Visible = false;
                divDepartmentalPolicy.Visible = true;
            }
            else if (ddlCategory.SelectedValue == "3")
            {
                divRegion.Visible = true;
                divIndustry.Visible = true;
                divCompany.Visible = false;
                divDepartment.Visible = true;
                divSubDepartment.Visible = true;
                divPolicyName.Visible = true;
                divSubDepartmentalPolicy.Visible = true;
                divAMCAEmployeeHandBook.Visible = false;
                divDepartmentalPolicy.Visible = false;
            }
            else if (ddlCategory.SelectedValue == "4")
            {
                divRegion.Visible = true;
                divIndustry.Visible = true;
                divCompany.Visible = true;
                divPolicyName.Visible = true;
            }
            else
            {
                divRegion.Visible = false;
                divIndustry.Visible = false;
                divCompany.Visible = false;
                divPolicyName.Visible = false;
            }
        }

        protected void ddlCategorySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearField();   
            FillListView();
            if (ddlCategorySearch.SelectedValue == "1")
            {
                divRegionSearch.Visible = true;
                divIndustrySearch.Visible = true;
                divCompanySearch.Visible = true;
                divDepartmentSearch.Visible = false;
                divSubDepartmentSearch.Visible = false; 
            }
            else if (ddlCategorySearch.SelectedValue == "2")
            {
                divRegionSearch.Visible = true;
                divIndustrySearch.Visible = true;
                divCompanySearch.Visible = false;
                divDepartmentSearch.Visible = true;
                divSubDepartmentSearch.Visible = false;
            }
            else if (ddlCategorySearch.SelectedValue == "3")
            {
                divRegionSearch.Visible = true;
                divIndustrySearch.Visible = true;
                divCompanySearch.Visible = false;
                divDepartmentSearch.Visible = true;
                divSubDepartmentSearch.Visible = true;
            }
            else if (ddlCategorySearch.SelectedValue == "4")
            {
                divRegionSearch.Visible = true;
                divIndustrySearch.Visible = true;
                divCompanySearch.Visible = true;
            }
            else
            {
                divRegionSearch.Visible = false;
                divIndustrySearch.Visible = false;
                divCompanySearch.Visible = false;
            }
        }

        protected void ddlRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAMCAGroupCompanySearch(ddlCompanySearch, ddlRegionSearch.SelectedValue, ddlIndustrySearch.SelectedValue);
            BindDepartmentSearch(ddlDepartmentSearch, ddlRegionSearch.SelectedValue, ddlIndustrySearch.SelectedValue);
            FillListView();
        }

        protected void ddlIndustrySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAMCAGroupCompanySearch(ddlCompanySearch, ddlRegionSearch.SelectedValue, ddlIndustrySearch.SelectedValue);
            BindDepartmentSearch(ddlDepartmentSearch, ddlRegionSearch.SelectedValue, ddlIndustrySearch.SelectedValue);
            FillListView();
        }

        protected void ddlDepartmentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView(); 
        }

        protected void ddlSubDepartmentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
    }
}