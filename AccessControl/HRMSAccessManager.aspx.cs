using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;
using SystemAdmin.ESS;
using SystemAdmin.GroupStructure;

namespace SystemAdmin.AccessControl
{
    public partial class HRMSAccessManager : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["SelectedOrgIds"] != null)
            {
                string SelectedOrgIds = ViewState["SelectedOrgIds"].ToString();
                BindCompanyDropdown(SelectedOrgIds);
            } 
            if (!Page.IsPostBack)
            {
                ViewState["Mode"] = "Add";
                FillActiveEmployee();
                BindScope(ddlScope);
                Bindgroup(ddlGroup); 
                BindDepartment(chkDep);
                BindHOD(chkactionRM); 
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
        void Bindgroup(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 26;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        } 
        protected string GetSelectedCompanies()
        {
            List<string> GetSelectedCompanies = new List<string>();

            foreach (Control ctrl in pnlCompanyItems.Controls)
            {
                if (ctrl is CheckBox cb && cb.Checked)
                {
                    GetSelectedCompanies.Add(cb.ID);  
                }
            }

            return string.Join(",", GetSelectedCompanies);
        }  
        void BindEmployee(ListBox LLB)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 78;
            
            string selectedcompanies = GetSelectedCompanies(); 
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
        void BindHOD(CheckBoxList LLB)
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
            if (PL.dt.Rows.Count > 0)
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
            ddlGroup.Enabled = true;
            ddlScope.Enabled = true; 
            ddlEmployeeName.Enabled = true;
            ClearField(); 
            ViewState["Mode"]  = "Add";
            divEdit.Visible = true;
            divView.Visible = false;
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in LV_Portal_Access_Mapping.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    { 
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        EditAll(Autoid);
                    }
                }
            }
            
        }

        void EditAll(int Autoid)
        {
            ddlGroup.Enabled = false;
            ddlScope.Enabled = false;
            ddlEmployeeName.Enabled = false;
            ViewState["Mode"]  = "Edit";
            hidAutoidMain.Value = Autoid.ToString(); 
            string selectedIndustryIds = string.Empty;
            string selectedRegionIds = string.Empty;
            string selectedOrgIds = string.Empty;
            string selectedComIds = string.Empty;
            string selectedWkIds = string.Empty;
            string selectedRmIds = string.Empty;
            string selectedDepIds = string.Empty;
            divView.Visible = false;
            divEdit.Visible = true;
            EssPL PL = new EssPL();
            PL.OpCode = 96;
            PL.AutoId = Autoid;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                try
                {
                    if (PL.dt.Rows[0]["EmpID"] != DBNull.Value && PL.dt.Rows[0]["EmpID"].ToString() != "0" && !string.IsNullOrEmpty(PL.dt.Rows[0]["EmpID"].ToString()))
                    {
                        ddlEmployeeName.SelectedValue = PL.dt.Rows[0]["EmpID"].ToString();
                    }
                    ddlEmployeeName_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {

                }
                ddlScope.SelectedValue = PL.dt.Rows[0]["DataAccessID"].ToString();
                ddlGroup.SelectedValue = PL.dt.Rows[0]["GroupID"].ToString();
                ddlGroup_SelectedIndexChanged(null, null); 
                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["Industry"].ToString()))
                { 
                    string[] action = PL.dt.Rows[0]["Industry"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();  
                        foreach (ListItem li in chkactionIndustry.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedIndustryIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                chkactionIndustry_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["region"].ToString()))
                {
                    string[] action = PL.dt.Rows[0]["region"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();
                        foreach (ListItem li in chkactionRegion.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedRegionIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                chkactionRegion_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["Organization"].ToString()))
                {
                    string[] action = PL.dt.Rows[0]["Organization"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();
                        foreach (ListItem li in chkactionOrg.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedOrgIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                chkactionOrg_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["Company"].ToString()))
                {
                    string[] selectedCompanies = PL.dt.Rows[0]["Company"].ToString().Split(',');

                    foreach (string compId in selectedCompanies)
                    {
                        string trimmedId = compId.Trim();

                        foreach (Control ctrl in pnlCompanyItems.Controls)
                        {
                            if (ctrl is CheckBox cb && cb.ID == trimmedId)
                            {
                                cb.Checked = true;
                                break;
                            }
                        }
                    }
                }

                chkactionCompany_SelectedIndexChanged(null, null);
                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["Locations"].ToString()))
                {
                    string[] action = PL.dt.Rows[0]["Locations"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();
                        foreach (ListItem li in chkactionWkLocation.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedWkIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                chkactionWkLocation_SelectedIndexChanged(null, null);

                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["RM"].ToString()))
                {
                    string[] action = PL.dt.Rows[0]["RM"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();
                        foreach (ListItem li in chkactionRM.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedRmIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                chkactionRM_SelectedIndexChanged(null, null);

                if (!string.IsNullOrEmpty(PL.dt.Rows[0]["Department"].ToString()))
                {
                    string[] action = PL.dt.Rows[0]["Department"].ToString().Split(',');
                    foreach (string str in action)
                    {
                        string trimmedStr = str.Trim();
                        foreach (ListItem li in chkDep.Items)
                        {
                            if (li.Value == trimmedStr)
                            {
                                selectedDepIds += "," + li.Value;
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
               
            }


            txtEndDate.Text = PL.dt.Rows[0]["EndDate"].ToString();
            if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
            {
                chkActive.Checked = false;
            }
            else
            {
                chkActive.Checked = true;
            }
            BindSelectedMenus(Autoid);
        }

        void BindSelectedMenus(int Autoid)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 97;
            PL.AutoId = Autoid;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;

            if (dt.Rows.Count > 0)
            {
                // Store all ActionIDs in a HashSet for quick lookup
                HashSet<string> selectedActionIds = new HashSet<string>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ActionID"] != DBNull.Value)
                    {
                        // Split comma-separated ActionIDs
                        string[] actions = row["ActionID"].ToString().Split(',');
                        foreach (string action in actions)
                        {
                            if (!string.IsNullOrWhiteSpace(action))
                                selectedActionIds.Add(action.Trim());
                        }
                    }
                }

                foreach (ListViewItem item in LV_Access_Menu_Company.Items)
                {
                    // First handle main checkbox
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                    HiddenField hdnMenuID = (HiddenField)item.FindControl("hidautoid");

                    chkSelect.Checked = dt.AsEnumerable()
                                           .Any(r => r["MenuID"].ToString() == hdnMenuID.Value);

                    // Now handle CheckBoxList for actions
                    CheckBoxList cbl = (CheckBoxList)item.FindControl("chkactionMenu");
                    if (cbl != null)
                    {
                        foreach (ListItem li in cbl.Items)
                        {
                            li.Selected = selectedActionIds.Contains(li.Value);
                        }
                    }
                }
            }
            else
            {
                // No data: uncheck everything
                foreach (ListViewItem item in LV_Access_Menu_Company.Items)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                    chkSelect.Checked = false;

                    CheckBoxList cbl = (CheckBoxList)item.FindControl("chkactionMenu");
                    if (cbl != null)
                    {
                        foreach (ListItem li in cbl.Items)
                            li.Selected = false;
                    }
                }
            }
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
            }
            else
            {
                LV_Employee_Menu_Details.DataSource = "";
                LV_Employee_Menu_Details.DataBind();
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
            PL.AutoId = hidAutoidMain.Value;
            
            PL.CreatedBy = Session["UserAutoId"].ToString();
            string xml = XMLField();
            if (xml == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('Select select menu items');", true);
                return;
            }
            PL.XML = xml;
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 41;
            }
            else
            {
                PL.OpCode = 38;
            }
           
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
            ddlEmployeeName_SelectedIndexChanged(null, null);
            LV_Access_Menu_Company.DataSource = "";
            LV_Access_Menu_Company.DataBind();
            ddlScope.SelectedIndex = -1;
            ddlGroup.SelectedIndex = -1;
            chkactionRM.ClearSelection();
            chkSelectAllRM.Checked = false;
            chkDep.ClearSelection();
            chkSelectAllDep.Checked = false;
            ddlGroup_SelectedIndexChanged(null, null);
            chkactionIndustry_SelectedIndexChanged(null, null);
            chkactionRegion_SelectedIndexChanged(null, null);
            chkactionOrg_SelectedIndexChanged   (null, null);
            chkactionCompany_SelectedIndexChanged (null, null);
            chkactionRM_SelectedIndexChanged(null, null);
            chkDep_SelectedIndexChanged (null, null); 
            Lst_Employee.SelectedIndex = -1;
            Lst_HOD.SelectedIndex = -1;
            LV_Access_Menu_Company.DataSource = "";
            LV_Access_Menu_Company.DataBind();
            txtEndDate.Text = "";
            hidAutoidMain.Value = "";
        }
        string XMLField()
        {
            string xml = "";
            xml += "<tbl>";
            string Action = ""; 
            string Group = ddlGroup.SelectedValue;
            bool isAnySelected = false;

            string Industry = string.Join(",", chkactionIndustry.Items
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

            string Company = GetSelectedCompanies();  

            string Location = string.Join(",", chkactionWkLocation.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));
            string RM = string.Join(",", chkactionRM.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value));

            string Department = string.Join(",", chkDep.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value)); 

            foreach (ListViewItem item in LV_Access_Menu_Company.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                CheckBoxList chkactionMenu = (CheckBoxList)item.FindControl("chkactionMenu");  // FIXED
                Action = "";
                if (chkSelect != null && chkSelect.Checked == true)
                {
                    isAnySelected = true;
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
                    xml += "<RM><![CDATA[" + RM + "]]></RM>";
                    xml += "<Dep><![CDATA[" + Department + "]]></Dep>"; 
                    xml += "<EndDate><![CDATA[" + txtEndDate.Text + "]]></EndDate>";
                    xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
                    foreach (ListItem act in chkactionMenu.Items)
                    {
                        if (act.Selected)
                        {
                            Action += act.Value + ",";
                        }
                    }
                    xml += "<Action><![CDATA[" + Action + "]]></Action>";
                    xml += "</tr>";
                }
            }
            if (!isAnySelected)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('Select atleast 1 menu item');", true);
                return "";    
            }
            else
            {
                xml += "</tbl>";
                return xml;
            } 
        } 
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divEdit.Visible = false;
            divView.Visible = true;
        } 
        private void SetDivVisibility(Control div, bool visible)
        {
            div.Visible = visible;
        } 
        public string GetInt(string a)
        {
            return "clschkaction" + a;
        } 
        public DataTable GetAction(string linkid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 18;
            PL.AutoId = linkid;
            MenuDL.returnTable(PL);
            return PL.dt;
        }

        public DataTable GetActionSaved(string linkid, string MenuID)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 94;
            PL.String1 = MenuID;
            PL.String2 = linkid;
            EssDL.returnTable(PL);
            return PL.dt;
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
                chkactionIndustry_SelectedIndexChanged(chkactionRegion, EventArgs.Empty); 
            }
        } 
        protected void chkactionIndustry_SelectedIndexChanged(object sender, EventArgs e)
        { 
            string selectedIndustryIds = string.Empty; 
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
        } 
        protected void chkSelectAllRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllRegion != null)
            {
                foreach (ListItem li in chkactionRegion.Items)
                {
                    li.Selected = chkSelectAllRegion.Checked;
                } 
                chkactionRegion_SelectedIndexChanged(chkactionRegion, EventArgs.Empty); 
            }
        }
        protected void chkactionRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = ddlGroup.SelectedValue; 
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
            ViewState["SelectedOrgIds"] = selectedOrgIds;
            if (selectedOrgIds != "")
            {
                BindCompanyDropdown(selectedOrgIds);
            }
            else
            {
                
            }
        } 
        protected void chkselectallcompany_CheckedChanged(object sender, EventArgs e)
        {
            bool checkAll = chkselectallcompany.Checked;

            foreach (Control ctrl in pnlCompanyItems.Controls)
            {
                if (ctrl is CheckBox cb)
                {
                    cb.Checked = checkAll;
                }
            }
            chkactionCompany_SelectedIndexChanged(chkselectallcompany,e);
        } 
        protected void chkactionCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = ddlGroup.SelectedValue;
            string selectedOrgIds = string.Empty;
            string selectedComIds = string.Empty;

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
            ViewState["SelectedOrgIds"] = selectedOrgIds; 
            selectedComIds = GetSelectedCompanies();
            if (selectedComIds != "")
            {
                var WorkLocation = GetWorkLocation(selectedComIds);
                chkactionWkLocation.DataSource = WorkLocation;
                chkactionWkLocation.DataTextField = "Name";
                chkactionWkLocation.DataValueField = "Id";
                chkactionWkLocation.DataBind();
                chkactionWkLocation.Enabled = true;
                chkselectallworklocation.Checked = false;
            }
            else
            {
                chkactionWkLocation.Enabled = false;
                chkactionWkLocation.Items.Clear();
                chkselectallworklocation.Checked = false;
            }
        } 
        protected void chkDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepIds = string.Empty; 

            foreach (ListItem depitem in chkDep.Items)
            {
                if (depitem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedDepIds))
                    {
                        selectedDepIds = depitem.Value;
                    }
                    else
                    {
                        selectedDepIds += "," + depitem.Value;
                    }
                }
            }  
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

         
        void setforedit()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 95;
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
                lblGroup.Text= PL.dt.Rows[0]["GroupName"].ToString();
                lblIndustry.Text = PL.dt.Rows[0]["Industry"].ToString(); 
                lblRegion.Text= PL.dt.Rows[0]["region"].ToString();
                lblOrganization.Text= PL.dt.Rows[0]["Organization"].ToString();
                lblCompany.Text= PL.dt.Rows[0]["Company"].ToString();
                lblLocation.Text= PL.dt.Rows[0]["Locations"].ToString();
                lblDepartment.Text= PL.dt.Rows[0]["Department"].ToString(); 
                lblRM.Text= PL.dt.Rows[0]["RM"].ToString();  
            }
            else
            {
                lblGroup.Text = "";
                lblIndustry.Text = "";
                lblRegion.Text = "";
                lblOrganization.Text = "";
                lblCompany.Text = "";
                lblLocation.Text = "";
                lblDepartment.Text = "";
                lblRM.Text = "";
            }
            fillMenus();
        }

        void fillMenus()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 93;
            PL.AutoId = hidAutoidMain.Value;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
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
            string Action = "";
            int isActive =  1 ;
            string xml = "";
            xml += "<tbl>";
            EssPL PL = new EssPL();
            PL.OpCode = 92;
            PL.IsActive  =  chkActiveUpdate.Checked;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.AutoId = hidAutoidMain.Value;
            PL.ToDate = txtEndDateUpdate.Text;
            foreach (ListViewItem item in LV_Access_Menu_Company_Update.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsCheckedUpdate");
                CheckBoxList chkactionMenu = (CheckBoxList)item.FindControl("chkactionMenu");  // FIXED
                Action = "";
                if (chkSelect != null && chkSelect.Checked == true)
                {
                    isActive =  1 ;
                }
                else
                {
                    isActive =  0 ;
                }
                HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoidUpdate");
                xml += "<tr>"; 
                xml += "<MenuID><![CDATA[" + hdnMenuId.Value + "]]></MenuID>";
                xml += "<IsActive><![CDATA[" + isActive + "]]></IsActive>";
                foreach (ListItem act in chkactionMenu.Items)
                {
                    if (act.Selected)
                    {
                        Action += act.Value + ",";
                    }
                }
                xml += "<Action><![CDATA[" + Action + "]]></Action>";
                xml += "</tr>";
               
            }
            xml += "</tbl>";
            PL.XML = xml;
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

        protected void BindCompanyDropdown(string selectedOrgIds)
        {
            DataTable dt = GetCompaniesByOrgs(selectedOrgIds);
            pnlCompanyItems.Controls.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                var groups = dt.AsEnumerable().Select(r => r.Field<string>("Organization")).Distinct();

                foreach (var group in groups)
                {
                    pnlCompanyItems.Controls.Add(new Literal { Text = $"<b>{group}</b><br/>" });

                    var industries = dt.AsEnumerable().Where(r => r.Field<string>("Organization") == group);

                    foreach (var row in industries)
                    {
                        string chkId =  row["Id"].ToString();

                        CheckBox cb = new CheckBox
                        {
                            ID = chkId,
                            Text = row["Name"].ToString(),
                            CssClass = "industry-item",
                            AutoPostBack = true
                        };
                        cb.CheckedChanged += chkactionCompany_SelectedIndexChanged;

                        pnlCompanyItems.Controls.Add(cb);
                        pnlCompanyItems.Controls.Add(new Literal { Text = "<br/>" });
                    }

                    pnlCompanyItems.Controls.Add(new Literal { Text = "<br/>" });
                }
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
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ViewState["Mode"].ToString() == "Add" )
            {
                string msg = CheckExists();
                if (msg == "1")
                {
                    ddlGroup.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowError('Record already exists with the same Element');", true);
                    return;

                }
            }
            
            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = ddlGroup.SelectedValue.ToString();
            string selectedOrgIds = string.Empty; 
            string selectedComIds = string.Empty;


            if (ddlGroup.SelectedValue.ToString() != "")
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
            
        } 
        protected void chkselectallworklocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkselectallworklocation != null)
            {
                foreach (ListItem li in chkactionWkLocation.Items)
                {
                    li.Selected = chkselectallworklocation.Checked;
                }
                chkactionWkLocation_SelectedIndexChanged(chkselectallworklocation, EventArgs.Empty);
            }
        } 
        protected void chkactionWkLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        } 
        private DataTable GetWorkLocation(string ComIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 44;
            PL.Description = ComIds; 
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
        private DataTable GetSubDeps(string DepIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 45;
            PL.Description = DepIds;
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
        protected void chkSelectAllRM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllRM != null)
            {
                foreach (ListItem li in chkactionRM.Items)
                {
                    li.Selected = chkSelectAllRM.Checked;
                }
                chkactionRM_SelectedIndexChanged(chkselectallworklocation, EventArgs.Empty);
            }
        } 
        protected void chkactionRM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }  
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;

            foreach (ListViewItem item in LV_Access_Menu_Company.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkIsChecked");
                if (chk != null)
                {
                    chk.Checked = chkSelectAll.Checked;
                }
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;

            hidAutoidMain.Value = ((HiddenField)item.FindControl("hidAutoId")).Value;
            setforedit();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpAction();", true);
        } 
         
        protected void LV_Access_Menu_Company_Update_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView drv = (DataRowView)dataItem.DataItem;

                CheckBoxList cbl = (CheckBoxList)e.Item.FindControl("chkactionMenu");
                if (cbl != null)
                {
                    DataTable dtActions = GetActionSaved(drv["Autoid"].ToString(), drv["MenuID"].ToString());

                    cbl.DataSource = dtActions;
                    cbl.DataTextField = "ActionName";
                    cbl.DataValueField = "Autoid";
                    cbl.DataBind();

                    // Pre-check items where isActive = 1
                    foreach (ListItem li in cbl.Items)
                    {
                        DataRow[] rows = dtActions.Select("Autoid = " + li.Value);
                        if (rows.Length > 0 && Convert.ToBoolean(rows[0]["isActive"]))
                        {
                            li.Selected = true;
                        }
                    }
                }
            }
        }


        string CheckExists()
        {
            string msg = "0";
            EssPL PL = new EssPL();
            PL.OpCode = 98;
            PL.EmpId = ddlEmployeeName.SelectedValue.ToString();
            PL.GroupId = ddlGroup.SelectedValue.ToString();
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                msg= "1";
            }
            else
            {
                msg= "0";
            }
            return msg;
       }


    }
}