using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;
using SystemAdmin.ESS;

namespace SystemAdmin.AccessControl
{
    public partial class HRMSDataAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillActiveEmployee();
                BindScope(ddlScope);
                Bindgroup(chkactionGroup);
                BindLocation(LstWorkLocation);
                BindDepartment(chkDep);
                BindHOD(Lst_HOD);
                BindHOD(LST_RM);
                BindEmployee(Lst_Employee);
                FillDataAccess();
            }
        }

        void BindIndustry(CheckBoxList LLB)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 1;
            StructureDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Name";
            LLB.DataBind();
        }


        void Bindgroup(CheckBoxList LLB)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 26;
            StructureDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Name";
            LLB.DataBind();
        }

        void BindLocation(ListBox LLB)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 25;
            StructureDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Name";
            LLB.DataBind();
        }

        void BindEmployee(ListBox LLB)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 78; 
            string selectedcompanies = string.Join(",", chkactionCompany.Items
                                            .Cast<ListItem>()
                                            .Where(i => i.Selected)
                                            .Select(i => i.Value));

            string selecteddeps = string.Join(",", chkDep.Items
                                             .Cast<ListItem>()
                                             .Where(i => i.Selected)
                                             .Select(i => i.Value));
            PL.ServiceTypeAutoid = selectedcompanies;
            PL.SubDepartmentId = selecteddeps;
            DropdownDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Name";
            LLB.DataBind();
        }


        void BindDepartment(CheckBoxList LLB)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 52;
            DropdownDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Department";
            LLB.DataBind();
        }

        void BindHOD(ListBox LLB)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 77;
            DropdownDL.returnTable(PL);
            LLB.DataSource = PL.dt;
            LLB.DataValueField = "Id";
            LLB.DataTextField = "Name";
            LLB.DataBind();
        }
        void FillActiveEmployee()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 14;
            DropdownDL.returnTable(PL);
            ddlEmployeeName.DataSource = PL.dt;
            ddlEmployeeName.DataValueField = "Autoid";
            ddlEmployeeName.DataTextField = "Name";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, new ListItem("Choose an item", ""));

            ddl_Employeesearch.DataSource = PL.dt;
            ddl_Employeesearch.DataValueField = "Autoid";
            ddl_Employeesearch.DataTextField = "Name";
            ddl_Employeesearch.DataBind();
            ddl_Employeesearch.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void BindScope(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 76;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void FillDataAccess()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 39;
            PL.EmpId = ddl_Employeesearch.SelectedValue;
            MenuAccessDL.returnTable(PL);
            if(PL.dt.Rows.Count > 0)
            {
                LV_Portal_Access_Mapping.DataSource = PL.dt;
                LV_Portal_Access_Mapping.DataBind();
            }
            else
            {
                LV_Portal_Access_Mapping.DataSource = "";
                LV_Portal_Access_Mapping.DataBind();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divEdit.Visible = true;
            divView.Visible = false;
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeName.SelectedValue != "")
            {
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;
                GetEmployeeDepartment(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                string designation = getDesignationId(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                FillListView(designation, ddlEmployeeName.SelectedValue);
                //SetForEdit(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                //upnl_Menuaccess.Update();
            }
        }

        void FillListView(string designation, string empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 40;
            PL.Designation = designation;
            PL.Type = 2;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            { 
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
            }
            else
            { 
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
            } 
        }

        void GetEmployeeDepartment(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 15;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Employee_Menu_Details.DataSource = PL.dt;
                LV_Employee_Menu_Details.DataBind();
            }
            else
            {
                LV_Employee_Menu_Details.DataSource = PL.dt;
                LV_Employee_Menu_Details.DataBind();
            }
        }

        public string getDesignationId(int id)
        {
            string msg = "";
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 16;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                msg = PL.dt.Rows[0]["Autoids"].ToString();
            }
            else
            {
                msg = "";
            }
            return msg;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.EmpId = ddlEmployeeName.SelectedValue;
            PL.CreatedBy = Session["UserAutoId"].ToString(); 
            string xml = XMLField();
            PL.XML = xml;
            PL.OpCode = 38;
            MenuAccessDL.returnTable(PL);
            if (!PL.isException)
            {
                ClearField();
                FillDataAccess();
                divView.Visible =true;
                divEdit.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        } 
        void ClearField()
        {
            ddlEmployeeName.SelectedIndex = -1;
            ddlScope.SelectedIndex = -1;
            LST_RM.SelectedIndex = -1;
            Lst_Employee.SelectedIndex = -1;
            Lst_HOD.SelectedIndex = -1; 
            chkactionIndustry_SelectedIndexChanged(chkactionIndustry, EventArgs.Empty);
        } 
        string XMLField()
        {
            string Department = "";
            string Employee = "";
            string HOD = "";
            string RM = ""; 
            string xml = "<tbl>";

            string Industry = string.Join(",", chkactionIndustry.Items
                                            .Cast<ListItem>()
                                            .Where(i => i.Selected)
                                            .Select(i => i.Value));

            string Group = string.Join(",", chkactionGroup.Items
                                           .Cast<ListItem>()
                                           .Where(i => i.Selected)
                                           .Select(i => i.Value));

            string Region = string.Join(",", chkactionRegion.Items
                                           .Cast<ListItem>()
                                           .Where(i => i.Selected)
                                           .Select(i => i.Value));

            string Organization = string.Join(",", chkactionOrg.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));

            string Company = string.Join(",", chkactionCompany.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value)); 

            string Location = Request.Form[LstWorkLocation.UniqueID]; 
            if (!string.IsNullOrEmpty(ddlScope.SelectedValue))
            {
                switch (ddlScope.SelectedValue)
                {
                    case "1": // Department
                        Department = string.Join(",", chkDep.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));
                        break;
                    case "7": // Employee
                        Employee = Request.Form[Lst_Employee.UniqueID];
                        break;
                    case "6": // All Employee
                        Employee = "All";
                        break;
                    case "2": // HOD
                        HOD  = Request.Form[Lst_HOD.UniqueID];
                        break;
                    case "3": // RM
                        RM  = Request.Form[LST_RM.UniqueID];
                        break;
                    case "4": // Department & HOD
                        Department = string.Join(",", chkDep.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));
                        HOD  = Request.Form[Lst_HOD.UniqueID];
                        break;
                    case "5": // Department & RM
                        Department = string.Join(",", chkDep.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));
                        RM  = Request.Form[LST_RM.UniqueID];
                        break;
                }
            } 
            foreach (ListViewItem item in LV_Access_Menu_Company.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                if (chkSelect != null && chkSelect.Checked == true)
                { 
                    HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoid");
                    xml += "<tr>";
                    xml += "<ScopeID><![CDATA[" + ddlScope.SelectedValue + "]]></ScopeID>";
                    xml += "<MenuID><![CDATA[" + hdnMenuId.Value + "]]></MenuID>";
                    xml += "<Industry><![CDATA[" + Industry + "]]></Industry>";
                    xml += "<Group><![CDATA[" + Group + "]]></Group>";
                    xml += "<Region><![CDATA[" + Region + "]]></Region>";
                    xml += "<Organization><![CDATA[" + Organization + "]]></Organization>";
                    xml += "<Company><![CDATA[" + Company + "]]></Company>";
                    xml += "<Location><![CDATA[" + Location + "]]></Location>";
                    xml += "<Dep><![CDATA[" + Department + "]]></Dep>";
                    xml += "<Employee><![CDATA[" + Employee + "]]></Employee>";
                    xml += "<HOD><![CDATA[" + HOD + "]]></HOD>";
                    xml += "<RM><![CDATA[" + RM + "]]></RM>";
                    xml += "<EndDate><![CDATA[" + txtEndDate.Text + "]]></EndDate>";
                    xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
                    xml += "</tr>";
                }
            }
            xml += "</tbl>";
            return xml;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divEdit.Visible = false;
            divView.Visible = true;
        } 
        protected void ddlScope_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            SetDivVisibility(div_Department, false);
            SetDivVisibility(div_Employee, false);
            SetDivVisibility(div_HOD, false);
            SetDivVisibility(div_RM, false);

            // Show based on selection
            if (!string.IsNullOrEmpty(ddlScope.SelectedValue))
            {
                switch (ddlScope.SelectedValue)
                {
                    case "1": // Department
                        SetDivVisibility(div_Department, true);
                        break;
                    case "7": // Employee
                        SetDivVisibility(div_Employee, true);
                        break;
                    case "2": // HOD
                        SetDivVisibility(div_HOD, true);
                        break;
                    case "3": // RM
                        SetDivVisibility(div_RM, true);
                        break;
                    case "4": // Department & HOD
                        SetDivVisibility(div_Department, true);
                        SetDivVisibility(div_HOD, true);
                        break;
                    case "5": // Department & RM
                        SetDivVisibility(div_Department, true);
                        SetDivVisibility(div_RM, true);
                        break;
                }
            }
        }

        
        private void SetDivVisibility(Control div, bool visible)
        {
            div.Visible = visible; 
        }

      
    

        private DataTable GetGroupByIndustry(string selectedIndustryIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 26;
            PL.IndustryId = selectedIndustryIds;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        } 

      
        private DataTable GetOrgByRegion(string GroupIds, string RegionIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 28;
            PL.Description = GroupIds;
            PL.IndustryId = RegionIds;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

      

        private DataTable GetRegionByIndustry(string GroupIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 27;
            PL.IndustryId = GroupIds;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        

        private DataTable GetCompaniesByOrgs(string OrgIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 29;
            PL.IndustryId = OrgIds;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }
         
        protected void chkSelectAllIndustry_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkSelectAllIndustry != null)
            {
                foreach (ListItem li in chkactionIndustry.Items)
                {
                    li.Selected = chkSelectAllIndustry.Checked;
                }
                chkactionIndustry_SelectedIndexChanged(chkactionIndustry, EventArgs.Empty);
            }   
        }

        protected void chkactionIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            string selectedGroupIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedRegionIds = string.Empty;
            string selectedOrgIds = string.Empty;

            foreach (ListItem industryitem in chkactionIndustry.Items)
            {
                if (industryitem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedIndustryIds))
                    {
                        selectedIndustryIds = industryitem.Value;
                    }
                    else
                    {
                        selectedIndustryIds += "," + industryitem.Value;
                    }
                }
            }
            if (selectedIndustryIds != "")
            {
                var Regions = GetRegionByIndustry(selectedIndustryIds);
                chkactionRegion.DataSource = Regions;
                chkactionRegion.DataTextField = "CountryName";
                chkactionRegion.DataValueField = "CountryCode";
                chkactionRegion.DataBind();
                chkactionRegion.Enabled = true;
                chkSelectAllRegion.Checked = false;
            }
            else
            {
                chkactionRegion.Enabled = false;
                chkactionRegion.Items.Clear();
                chkSelectAllRegion.Checked = false;
            }
            foreach (ListItem regionItem in chkactionRegion.Items)
            {
                if (regionItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedRegionIds = regionItem.Value;
                    }
                    else
                    {
                        selectedRegionIds += "," + regionItem.Value;
                    }
                }
            }

            if (selectedRegionIds != "")
            {
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds, selectedIndustryIds);
                chkactionOrg.DataSource = organization;
                chkactionOrg.DataTextField = "Name";
                chkactionOrg.DataValueField = "Autoid";
                chkactionOrg.DataBind();
                chkactionOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkactionOrg.Enabled = false;
                chkactionOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            }

            foreach (ListItem OrgItem in chkactionOrg.Items)
            {
                if (OrgItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedOrgIds = OrgItem.Value;
                    }
                    else
                    {
                        selectedOrgIds += "," + OrgItem.Value;
                    }
                }
            }

            if (selectedOrgIds != "")
            {
                var companies = GetCompaniesByOrgs(selectedOrgIds);
                chkactionCompany.DataSource = companies;
                chkactionCompany.DataTextField = "Name";
                chkactionCompany.DataValueField = "Id";
                chkactionCompany.DataBind();
                chkactionCompany.Enabled = true;
                chkselectallcompany.Checked = false;

            }
            else
            {
                chkactionCompany.Enabled = false;
                chkactionCompany.Items.Clear();
                chkselectallcompany.Checked = false;

            } 
        }

        protected void chkSelectAllGroup_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkSelectAllGroup != null)
            {
                foreach (ListItem li in chkactionGroup.Items)
                {
                    li.Selected = chkSelectAllGroup.Checked;
                }
                chkactionGroup_SelectedIndexChanged(chkactionGroup, EventArgs.Empty);
            } 
        }
        protected void chkactionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = string.Empty;
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkactionGroup.Items)
            {
                if (groupItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedGroupIds))
                    {
                        selectedGroupIds = groupItem.Value;
                    }
                    else
                    {
                        selectedGroupIds += "," + groupItem.Value;
                    }
                }
            }

            if (selectedGroupIds != "")
            {
                var groups = GetIndustryByGroup(selectedGroupIds);
                chkactionIndustry.DataSource = groups;
                chkactionIndustry.DataTextField = "Name";
                chkactionIndustry.DataValueField = "Id";
                chkactionIndustry.DataBind();
                chkactionIndustry.Enabled = true;
                chkSelectAllIndustry.Checked = false;

            }
            else
            {
                chkactionIndustry.Enabled = false;
                chkactionIndustry.Items.Clear();
                chkSelectAllIndustry.Checked = false;

            }

            foreach (ListItem industryitem in chkactionIndustry.Items)
            {
                if (industryitem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedGroupIds))
                    {
                        selectedIndustryIds = industryitem.Value;
                    }
                    else
                    {
                        selectedIndustryIds += "," + industryitem.Value;
                    }
                }
            }

            if (selectedIndustryIds != "")
            {
                var Regions = GetRegionByIndustry(selectedIndustryIds);
                chkactionRegion.DataSource = Regions;
                chkactionRegion.DataTextField = "CountryName";
                chkactionRegion.DataValueField = "CountryCode";
                chkactionRegion.DataBind();
                chkactionRegion.Enabled = true;
                chkSelectAllRegion.Checked = false;
            }
            else
            {
                chkactionRegion.Enabled = false;
                chkactionRegion.Items.Clear();
                chkSelectAllRegion.Checked = false;
            }

            foreach (ListItem regionItem in chkactionRegion.Items)
            {
                if (regionItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedRegionIds = regionItem.Value;
                    }
                    else
                    {
                        selectedRegionIds += "," + regionItem.Value;
                    }
                }
            }

            if (selectedRegionIds != "")
            {
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds, selectedIndustryIds);
                chkactionOrg.DataSource = organization;
                chkactionOrg.DataTextField = "Name";
                chkactionOrg.DataValueField = "Autoid";
                chkactionOrg.DataBind();
                chkactionOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkactionOrg.Enabled = false;
                chkactionOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            }

            foreach (ListItem OrgItem in chkactionOrg.Items)
            {
                if (OrgItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedOrgIds = OrgItem.Value;
                    }
                    else
                    {
                        selectedOrgIds += "," + OrgItem.Value;
                    }
                }
            }

            if (selectedOrgIds != "")
            {
                var companies = GetCompaniesByOrgs(selectedOrgIds);
                chkactionCompany.DataSource = companies;
                chkactionCompany.DataTextField = "Name";
                chkactionCompany.DataValueField = "Id";
                chkactionCompany.DataBind();
                chkactionCompany.Enabled = true;
                chkselectallcompany.Checked = false;

            }
            else
            {
                chkactionCompany.Enabled = false;
                chkactionCompany.Items.Clear();
                chkselectallcompany.Checked = false; 
            }
        }

        protected void chkSelectAllRegion_CheckedChanged(object sender, EventArgs e)
        { 
            if (chkSelectAllRegion != null)
            {
                foreach (ListItem li in chkactionRegion.Items)
                {
                    li.Selected = chkSelectAllRegion.Checked;
                }
                chkselectallcompany.Checked = chkSelectAllRegion.Checked;
                chkactionRegion_SelectedIndexChanged(chkactionRegion, EventArgs.Empty);
                foreach (ListItem li in chkactionCompany.Items)
                {
                    li.Selected = chkSelectAllRegion.Checked;
                }
            }
        }
        protected void chkactionRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = string.Empty;
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkactionGroup.Items)
            {
                if (groupItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedGroupIds))
                    {
                        selectedGroupIds = groupItem.Value;
                    }
                    else
                    {
                        selectedGroupIds += "," + groupItem.Value;
                    }
                }
            }

            foreach (ListItem industryitem in chkactionIndustry.Items)
            {
                if (industryitem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedGroupIds))
                    {
                        selectedIndustryIds = industryitem.Value;
                    }
                    else
                    {
                        selectedIndustryIds += "," + industryitem.Value;
                    }
                }
            }
            foreach (ListItem regionItem in chkactionRegion.Items)
            {
                if (regionItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedRegionIds = regionItem.Value;
                    }
                    else
                    {
                        selectedRegionIds += "," + regionItem.Value;
                    }
                }
            }
            if (selectedRegionIds != "")
            {
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds, selectedIndustryIds);
                chkactionOrg.DataSource = organization;
                chkactionOrg.DataTextField = "Name";
                chkactionOrg.DataValueField = "Autoid";
                chkactionOrg.DataBind();
                chkactionOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkactionOrg.Enabled = false;
                chkactionOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            }
            foreach (ListItem OrgItem in chkactionOrg.Items)
            {
                if (OrgItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedRegionIds))
                    {
                        selectedOrgIds = OrgItem.Value;
                    }
                    else
                    {
                        selectedOrgIds += "," + OrgItem.Value;
                    }
                }
            }
            if (selectedOrgIds != "")
            {
                var companies = GetCompaniesByOrgs(selectedOrgIds);
                chkactionCompany.DataSource = companies;
                chkactionCompany.DataTextField = "Name";
                chkactionCompany.DataValueField = "Id";
                chkactionCompany.DataBind();
                chkactionCompany.Enabled = true;
                chkselectallcompany.Checked = false; 
            }
            else
            {
                chkactionCompany.Enabled = false;
                chkactionCompany.Items.Clear();
                chkselectallcompany.Checked = false; 
            } 
        }

        protected void chkSelectAllOrg_CheckedChanged(object sender, EventArgs e)
        { 
            if (chkSelectAllOrg != null)
            {
                foreach (ListItem li in chkactionOrg.Items)
                {
                    li.Selected = chkSelectAllOrg.Checked;
                }
                chkactionOrg_SelectedIndexChanged(chkactionOrg, EventArgs.Empty);
            } 
        }

        protected void chkactionOrg_SelectedIndexChanged(object sender, EventArgs e)
        { 
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkactionOrg.Items)
            {
                if (groupItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedOrgIds))
                    {
                        selectedOrgIds = groupItem.Value;
                    }
                    else
                    {
                        selectedOrgIds += "," + groupItem.Value;
                    }
                }
            }
            if (selectedOrgIds != "")
            {
                var Companies = GetCompaniesByOrgs(selectedOrgIds);
                chkactionCompany.DataSource = Companies;
                chkactionCompany.DataTextField = "Name";
                chkactionCompany.DataValueField = "Id";
                chkactionCompany.DataBind();
                chkactionCompany.Enabled = true;
                chkselectallcompany.Checked = false;
            }
            else
            {
                chkactionCompany.Enabled = false;
                chkactionCompany.Items.Clear();
                chkselectallcompany.Checked = false;
            } 
        }


        protected void chkselectallcompany_CheckedChanged(object sender, EventArgs e)
        { 
            if (chkactionCompany != null)
            {
                foreach (ListItem li in chkactionCompany.Items)
                {
                    li.Selected = chkselectallcompany.Checked;
                }
                chkactionCompany_SelectedIndexChanged(chkactionCompany, EventArgs.Empty);
            } 
        }

        protected void chkactionCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployee(Lst_Employee);
        }

        

        protected void chkDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployee(Lst_Employee);
        }

        protected void chkSelectAllDep_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDep != null)
            {
                foreach (ListItem li in chkDep.Items)
                {
                    li.Selected = chkSelectAllDep.Checked;
                }
                chkDep_SelectedIndexChanged(chkDep, EventArgs.Empty);
            }
        }

        protected void btnViewAction_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (ListViewItem)btn.NamingContainer;

            hidAutoidMain.Value = ((HiddenField)item.FindControl("hidAutoId")).Value;
            setforedit();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpAction();", true);
        }

        void setforedit()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 93;
            PL.AutoId = hidAutoidMain.Value; 
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtEndDateUpdate.Text = PL.dt.Rows[0]["EndDate"].ToString();
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActiveUpdate.Checked = false;
                }
                else
                {
                    chkActiveUpdate.Checked = true;
                }
                lblIndustry.Text = PL.dt.Rows[0]["Industry"].ToString();
                lblGroup.Text= PL.dt.Rows[0]["GroupName"].ToString();
                lblRegion.Text= PL.dt.Rows[0]["region"].ToString();
                lblOrganization.Text= PL.dt.Rows[0]["Organization"].ToString();
                lblCompany.Text= PL.dt.Rows[0]["Company"].ToString();
                lblLocation.Text= PL.dt.Rows[0]["Locations"].ToString();
                lblDepartment.Text= PL.dt.Rows[0]["Department"].ToString();
                lblHOD.Text= PL.dt.Rows[0]["HOD"].ToString();
                lblRM.Text= PL.dt.Rows[0]["RM"].ToString();
                lblEmployee.Text= PL.dt.Rows[0]["Employee"].ToString();
                LV_Access_Menu_Company_Update.DataSource = PL.dt;
                LV_Access_Menu_Company_Update.DataBind(); 
            }
            else
            {
                LV_Access_Menu_Company_Update.DataSource = "";
                LV_Access_Menu_Company_Update.DataBind();
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillDataAccess();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddl_Employeesearch.SelectedIndex = -1;
        }

        protected void btnUpdateAction_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 92;
            PL.IsActive  =  chkActiveUpdate.Checked;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.AutoId = hidAutoidMain.Value;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (!PL.isException)
            {
                FillDataAccess();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Updated successfully');", true);
            }
            else
            { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }

        public DataTable GetIndustryByGroup(string GroupIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 36;
            PL.Name = GroupIds;
            StructureDL.returnTable(PL);
            return PL.dt;
        }

        private DataTable GetOrgByRegion(string GroupIds, string RegionIds, string IndustryIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 28;
            PL.Description = GroupIds;
            PL.Name = RegionIds;
            PL.IndustryId = IndustryIds;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

    }
}