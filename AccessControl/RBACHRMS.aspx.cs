using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;
using SystemAdmin.ESS;

namespace SystemAdmin.AccessControl
{
    public partial class RBACHRMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillEmpAccess();
                BindIndustry(LstIndustry);
                BindLocation(LstWorkLocation);
            }
        }

        void BindIndustry(ListBox LLB)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 1;
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
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            FillActiveEmployee();
            divEmployeeDetails.Visible = false;
            divEmployeeAccess.Visible = false;
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Employee_Access.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoidMain.Value = chkSelect.Attributes["Autoid"];
                    SetForEdit(int.Parse(chkSelect.Attributes["Autoid"].ToString()));
                    divView.Visible = false;
                    divEdit.Visible = true;
                    upnl_Menuaccess.Update();
                    break;
                }
            }
        }
        void SetForEdit(int empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 27;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                FillActiveEmployee();
                GetEmployeeDepartment(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                ddlEmployeeName.SelectedIndex = ddlEmployeeName.Items.IndexOf(ddlEmployeeName.Items.FindByValue(PL.dt.Rows[0]["EmpId"].ToString()));
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;
                string designation = getDesignationId(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                FillListView(designation, PL.dt.Rows[0]["EmpId"].ToString());
                foreach (DataRow dr in PL.dt.Rows)
                {
                    foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
                    {

                        string selectedIndustryIds = string.Empty;
                        string selectedGroupIds = string.Empty; 
                        string selectedRegionIds = string.Empty;
                        string selectedOrgIds = string.Empty; 
                        string selectedComIds = string.Empty;
                        string selectedLocationIds = string.Empty;

                        CheckBoxList chkIndustry = (CheckBoxList)lvItem.FindControl("chkactionIndustry");
                        CheckBoxList chkGroup = (CheckBoxList)lvItem.FindControl("chkactionGroup");
                        CheckBoxList chkRegion = (CheckBoxList)lvItem.FindControl("chkactionRegion");
                        CheckBoxList chkOrg = (CheckBoxList)lvItem.FindControl("chkactionOrg");
                        CheckBoxList chkCompany = (CheckBoxList)lvItem.FindControl("chkactionCompany");
                        CheckBoxList chkLocation = (CheckBoxList)lvItem.FindControl("chkLocation");
                        HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                       

                        if (dr[0].ToString() == hidAutoId.Value.ToString())
                        {
                            if (!string.IsNullOrEmpty(dr["IndustryId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionIndustry");
                                string[] action = dr["IndustryId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            selectedIndustryIds += "," + li.Value;
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            } 

                            if (selectedIndustryIds != "")
                            {
                                var Groups = GetGroupByIndustry(selectedIndustryIds);
                                chkGroup.DataSource = Groups;
                                chkGroup.DataTextField = "Name";
                                chkGroup.DataValueField = "Id";
                                chkGroup.DataBind();
                                chkGroup.Enabled = true;
                            }
                            else
                            {
                                chkGroup.Enabled = false;
                                chkGroup.Items.Clear();
                            }

                            if (!string.IsNullOrEmpty(dr["GroupId"].ToString()))
                            {
                                CheckBoxList chkactionGroup = (CheckBoxList)lvItem.FindControl("chkactionGroup");
                                string[] action = dr["GroupId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkactionGroup.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            selectedGroupIds += "," + li.Value;
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (selectedGroupIds != "")
                            {
                                var Regions = GetRegionByIndustry(selectedGroupIds);
                                chkRegion.DataSource = Regions;
                                chkRegion.DataTextField = "CountryName";
                                chkRegion.DataValueField = "CountryCode";
                                chkRegion.DataBind();
                                chkRegion.Enabled = true;
                            }
                            else
                            {
                                chkRegion.Enabled = false;
                                chkRegion.Items.Clear();
                            }

                            if (!string.IsNullOrEmpty(dr["RegionId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionRegion");
                                string[] action = dr["RegionId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            selectedRegionIds += "," + li.Value;
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (selectedRegionIds != "")
                            {
                                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds);
                                chkOrg.DataSource = organization;
                                chkOrg.DataTextField = "Name";
                                chkOrg.DataValueField = "Autoid";
                                chkOrg.DataBind();
                                chkOrg.Enabled = true; 
                            }
                            else
                            {
                                chkOrg.Enabled = false;
                                chkOrg.Items.Clear(); 
                            }

                            if (!string.IsNullOrEmpty(dr["OrgId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionOrg");
                                string[] action = dr["OrgId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            selectedOrgIds += "," + li.Value;
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (selectedOrgIds != "")
                            {
                                var companies = GetCompaniesByOrgs(selectedOrgIds);
                                chkCompany.DataSource = companies;
                                chkCompany.DataTextField = "Name";
                                chkCompany.DataValueField = "Id";
                                chkCompany.DataBind();
                                chkCompany.Enabled = true;
                            }
                            else
                            {
                                chkCompany.Enabled = false;
                                chkCompany.Items.Clear();
                            }

                            if (!string.IsNullOrEmpty(dr["CompanyId"].ToString()))
                            {
                                CheckBoxList chkcompany = (CheckBoxList)lvItem.FindControl("chkactionCompany");
                                string[] action = dr["CompanyId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkcompany.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(dr["ReportingPersonId"].ToString()))
                            {
                                CheckBoxList chkreporting = (CheckBoxList)lvItem.FindControl("chkactioreporting");
                                string[] action = dr["ReportingPersonId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkreporting.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(dr["WkLocationId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkLocation");
                                string[] action = dr["WkLocationId"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            selectedLocationIds += "," + li.Value;
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(dr["Action"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionMenu");
                                string[] action = dr["Action"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }

                           
                            
                            break;
                        }
                    }
                }
            }
        }
        void FillEmpAccess()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 25;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Employee_Access.DataSource = PL.dt;
                LV_Employee_Access.DataBind();
            }
            else
            {
                LV_Employee_Access.DataSource = PL.dt;
                LV_Employee_Access.DataBind();
            }
        }
        void FillListView(string designation, string empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 24;
            PL.Designation = designation;
            PL.Type = 2;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                btnApplyAllAccess.Visible = true;
                btnRevokeAllAccess.Visible = true;
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
            }
            else
            {
                btnApplyAllAccess.Visible = false;
                btnRevokeAllAccess.Visible = false;
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
            }
            upnl_Menuaccess.Update();
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
        public DataTable GetRegionAction(string linkid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 19;
            PL.AutoId = linkid;
            MenuDL.returnTable(PL);
            return PL.dt;
        }
        public DataTable GetIndustryAction()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 1;   
            StructureDL.returnTable(PL);
            return PL.dt;
        }

        public DataTable GetLocationn()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 25;
            StructureDL.returnTable(PL);
            return PL.dt;
        }

        public DataTable GetReportingperson(string linkid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 33;
            PL.EmpId = linkid;
            MenuAccessDL.returnTable(PL);
            return PL.dt;
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
        protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeName.SelectedValue != "")
            {
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;
                GetEmployeeDepartment(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                string designation = getDesignationId(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                FillListView(designation, ddlEmployeeName.SelectedValue);
                SetForEdit(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                upnl_Menuaccess.Update();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.EmpId = ddlEmployeeName.SelectedValue;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            string xml = GetXmlData();
            if (xml.Contains("MenuId"))
            {
                PL.OpCode = 26;
                PL.XML = xml;
                PL.EmpId = ddlEmployeeName.SelectedValue;
                MenuAccessDL.returnTable(PL);
                if (!PL.isException)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Save successfully.');", true);
                    divEdit.Visible = false;
                    divView.Visible = true;
                    FillEmpAccess();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Parental Menu should be select');", true);
            }
        }
        string GetXmlData()
        {
            string XML = "<tbl>";
            foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
            {
                string Industry = "";
                string Group = "";
                string Region = "";
                string Org = "";
                string CompanyId = "";
                string Location = "";
                string Action = "";
                string Reporting = "";
                HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                CheckBoxList chkactionIndustry = (CheckBoxList)lvItem.FindControl("chkactionIndustry");
                CheckBoxList chkactionGroup = (CheckBoxList)lvItem.FindControl("chkactionGroup");
                CheckBoxList chkactionRegion = (CheckBoxList)lvItem.FindControl("chkactionRegion");
                CheckBoxList chkactionOrg = (CheckBoxList)lvItem.FindControl("chkactionOrg"); 
                CheckBoxList chkactionCompany = (CheckBoxList)lvItem.FindControl("chkactionCompany");
                CheckBoxList chkLocation = (CheckBoxList)lvItem.FindControl("chkLocation");
                CheckBoxList chkactionMenu = (CheckBoxList)lvItem.FindControl("chkactionMenu");
                
                CheckBoxList chkactioreporting = (CheckBoxList)lvItem.FindControl("chkactioreporting");


                foreach (ListItem ind in chkactionIndustry.Items)
                {
                    if(chkactionIndustry.Visible == true)
                    {
                        if (ind.Selected)
                        {
                            Industry += ind.Value + "^";
                        }
                    }
                    else
                    {
                        Industry  = "";
                    }
                    
                }

                foreach (ListItem grp in chkactionGroup.Items)
                {
                    if (chkactionGroup.Visible == true)
                    {
                        if (grp.Selected)
                        {
                            Group += grp.Value + "^";
                        }
                    }
                    else
                    {
                        Group = "";
                    }
                }

                foreach (ListItem reg in chkactionRegion.Items)
                {
                    if (chkactionRegion.Visible == true)
                    {
                        if (reg.Selected)
                        {
                            Region += reg.Value + "^";
                        }
                    }
                    else
                    {
                        Region = "";
                    }
                }

                foreach (ListItem org in chkactionOrg.Items)
                {
                    if (chkactionOrg.Visible == true)
                    {
                        if (org.Selected)
                        {
                            Org += org.Value + "^";
                        }
                    }
                    else
                    {
                        Org = "";
                    }
                }

                
                foreach (ListItem act in chkactionCompany.Items)
                {
                    if (chkactionCompany.Visible == true)
                    {
                        if (act.Selected)
                        {
                            CompanyId += act.Value + "^";
                        }
                    }
                    else
                    {
                        CompanyId = "";
                    }
                }

                foreach (ListItem loc in chkLocation.Items)
                {
                    if (chkLocation.Visible == true)
                    {
                        if (loc.Selected)
                        {
                            Location += loc.Value + "^";
                        }
                    }
                    else
                    {
                        Location = "";
                    }
                }

                foreach (ListItem act in chkactioreporting.Items)
                {
                    if (act.Selected)
                    {
                        Reporting += act.Value + "^";
                    }
                }


                foreach (ListItem act in chkactionMenu.Items)
                {
                    if (act.Selected)
                    {
                        Action += act.Value + "^";
                    }
                }
                
                XML += "<tr>";
                XML += "<EmpId>" + ddlEmployeeName.SelectedValue + "</EmpId>";
                XML += "<IndustryId>" + Industry + "</IndustryId>";
                XML += "<Group>" + Group + "</Group>";
                XML += "<RegionId>" + Region + "</RegionId>";
                XML += "<OrgId>" + Org + "</OrgId>";
                XML += "<Company>" + CompanyId + "</Company>";
                XML += "<Location>" + Location + "</Location>";
                XML += "<Action>" + Action + "</Action>"; 
                XML += "<Reporting>" + Reporting + "</Reporting>";
                XML += "<MenuId>" + hidAutoId.Value + "</MenuId>";
                XML += "</tr>";
            }
            XML += "</tbl>";
            return XML;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divEdit.Visible = false;
            divView.Visible = true;
        }
        protected void LV_Hid_Region_Industry_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Panel pnlRegion = (Panel)e.Item.FindControl("pnlRegion");
                Panel pnlIndustry = (Panel)e.Item.FindControl("pnlIndustry");
                Panel pnlCompany = (Panel)e.Item.FindControl("pnlCompany");
                Panel pnlReporting = (Panel)e.Item.FindControl("pnlReporting");
                Panel pnlGroup = (Panel)e.Item.FindControl("PnlGroup");
                Panel pnlOrganization = (Panel)e.Item.FindControl("pnlOrganization");
                HiddenField hdnMastermenu = (HiddenField)e.Item.FindControl("hidIsParentMenu");
                CheckBoxList chkLocation = (CheckBoxList)e.Item.FindControl("chkLocation");
                if (hdnMastermenu.Value == "True")
                {
                    pnlRegion.Visible = true;
                    pnlIndustry.Visible = true;
                    pnlCompany.Visible = true;
                    pnlReporting.Visible = true;
                    pnlGroup.Visible = true;
                    pnlOrganization.Visible = true;
                    chkLocation.Visible = true;
                }
                else
                {
                    pnlRegion.Visible = false;
                    pnlIndustry.Visible = false;
                    pnlCompany.Visible = false;
                    pnlReporting.Visible = false;
                    pnlGroup.Visible = false;
                    pnlOrganization.Visible = false;
                    chkLocation.Visible = false;
                }

               
                //Panel pnlAction = (Panel)e.Item.FindControl("pnlAction");
                //CheckBoxList chkactionMenu = (CheckBoxList)e.Item.FindControl("chkactionMenu");
                //HiddenField hdnAutoId = (HiddenField)e.Item.FindControl("hidautoid"); // must exist in template

                //if (pnlAction != null && chkactionMenu != null && hdnAutoId != null)
                //{
                //    DataTable actions = GetAction(hdnAutoId.Value);

                //    if (actions != null && actions.Rows.Count > 0)
                //    {
                //        chkactionMenu.DataSource = actions;
                //        chkactionMenu.DataTextField = "ActionName";
                //        chkactionMenu.DataValueField = "Autoid";
                //        chkactionMenu.DataBind();

                //        pnlAction.Visible = true;  // show dropdown
                //    }
                //    else
                //    {
                //        pnlAction.Visible = false; // hide if no actions
                //    }
                //}
            }
        } 
        private int GetHeaderOffset(Table tbl)
        {
            int headerCount = 0;
            foreach (TableRow r in tbl.Rows)
            {
                if (r.Cells.Count == 1 && r.Cells[0].Text.Contains("&nbsp;&nbsp;<b>"))
                    headerCount++;
            }
            return headerCount;
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

         private DataTable GetCompaniesByRegions(string selectedRegionIds, string selectedIndustryIds)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 32;
            PL.Type = selectedRegionIds;
            PL.Industry = selectedIndustryIds;
            MenuAccessDL.returnTable(PL);
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

        protected void chkReportingPerson_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBoxList chkactioreporting = (CheckBoxList)item.FindControl("chkactioreporting");
            if (chkactioreporting != null)
            {
                foreach (ListItem li in chkactioreporting.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
            }
            upnl_Menuaccess.Update();
        }  
        protected void chkSelectAllIndustry_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBoxList chkRegion = (CheckBoxList)item.FindControl("chkactionRegion");
            CheckBoxList chkIndustry = (CheckBoxList)item.FindControl("chkactionIndustry");
            CheckBoxList chkComapny = (CheckBoxList)item.FindControl("chkactionCompany");
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");
            CheckBox chkSelectAllIndustry = (CheckBox)item.FindControl("chkSelectAllIndustry");
            if (chkSelectAllIndustry != null)
            {
                foreach (ListItem li in chkIndustry.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
                chkactionIndustry_SelectedIndexChanged(chkIndustry, EventArgs.Empty);
            }
            upnl_Menuaccess.Update();
        } 
        protected void chkactionIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkIndustry = (CheckBoxList)sender;
            ListViewItem item = (ListViewItem)chkIndustry.NamingContainer;
            CheckBoxList chkGroup = (CheckBoxList)item.FindControl("chkactionGroup");
            CheckBox chkSelectAllGroup = (CheckBox)item.FindControl("chkSelectAllGroup");
            CheckBoxList chkRegion = (CheckBoxList)item.FindControl("chkactionRegion");
            CheckBox chkSelectAllRegion = (CheckBox)item.FindControl("chkSelectAllRegion");
            CheckBoxList chkOrg = (CheckBoxList)item.FindControl("chkactionOrg");
            CheckBox chkSelectAllOrg = (CheckBox)item.FindControl("chkSelectAllOrg");
            CheckBoxList chkCompany = (CheckBoxList)item.FindControl("chkactionCompany");
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");

            string selectedRegionIds = string.Empty;
            string selectedIndustryIds = string.Empty;
            string selectedGroupIds = string.Empty;
            string selectedOrgIds = string.Empty;

            foreach (ListItem IndustryItem in chkIndustry.Items)
            {
                if (IndustryItem.Selected)
                {
                    if (string.IsNullOrEmpty(selectedIndustryIds))
                    {
                        selectedIndustryIds = IndustryItem.Value;
                    }
                    else
                    {
                        selectedIndustryIds += "," + IndustryItem.Value;
                    }
                }
            }

            if (selectedIndustryIds != "")
            {
                var groups = GetGroupByIndustry(selectedIndustryIds);
                chkGroup.DataSource = groups;
                chkGroup.DataTextField = "Name";
                chkGroup.DataValueField = "Id";
                chkGroup.DataBind();
                chkGroup.Enabled = true;
                chkSelectAllGroup.Checked = false;

            }
            else
            {
                chkGroup.Enabled = false;
                chkGroup.Items.Clear();
                chkSelectAllGroup.Checked = false;

            }

            foreach (ListItem groupItem in chkGroup.Items)
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
                var Regions = GetRegionByIndustry(selectedGroupIds);
                chkRegion.DataSource = Regions;
                chkRegion.DataTextField = "CountryName";
                chkRegion.DataValueField = "CountryCode";
                chkRegion.DataBind();
                chkRegion.Enabled = true;
                chkSelectAllGroup.Checked = false;
            }
            else
            {
                chkRegion.Enabled = false;
                chkRegion.Items.Clear();
                chkSelectAllGroup.Checked = false;
            }

            foreach (ListItem regionItem in chkRegion.Items)
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
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds);
                chkOrg.DataSource = organization;
                chkOrg.DataTextField = "Name";
                chkOrg.DataValueField = "Autoid";
                chkOrg.DataBind();
                chkOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkOrg.Enabled = false;
                chkOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            }

            foreach (ListItem OrgItem in chkOrg.Items)
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
                chkCompany.DataSource = companies;
                chkCompany.DataTextField = "Name";
                chkCompany.DataValueField = "Id";
                chkCompany.DataBind();
                chkCompany.Enabled = true;
                chkselectallcompany.Checked = false;

            }
            else
            {
                chkCompany.Enabled = false;
                chkCompany.Items.Clear();
                chkselectallcompany.Checked = false;

            }
            upnl_Menuaccess.Update();
        } 
        protected void chkSelectAllGroup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBox chkSelectAllGroup = (CheckBox)item.FindControl("chkSelectAllGroup");
            CheckBoxList chkGroup = (CheckBoxList)item.FindControl("chkactionGroup");
            if (chkSelectAllGroup != null)
            {
                foreach (ListItem li in chkGroup.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
                chkactionGroup_SelectedIndexChanged(chkGroup, EventArgs.Empty);
            }
            upnl_Menuaccess.Update();
        } 
        protected void chkactionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkGroup = (CheckBoxList)sender;
            ListViewItem item = (ListViewItem)chkGroup.NamingContainer;
            CheckBox chkSelectAllGroup = (CheckBox)item.FindControl("chkSelectAllGroup");
            CheckBoxList chkRegion = (CheckBoxList)item.FindControl("chkactionRegion");
            CheckBox chkSelectAllRegion = (CheckBox)item.FindControl("chkSelectAllRegion");
            CheckBoxList chkOrg = (CheckBoxList)item.FindControl("chkactionOrg");
            CheckBox chkSelectAllOrg = (CheckBox)item.FindControl("chkSelectAllOrg");
            CheckBoxList chkCompany = (CheckBoxList)item.FindControl("chkactionCompany");
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");
            
            string selectedGroupIds = string.Empty;
            string selectedRegionIds = string.Empty;  
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkGroup.Items)
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
                var Regions = GetRegionByIndustry(selectedGroupIds);
                chkRegion.DataSource = Regions;
                chkRegion.DataTextField = "CountryName";
                chkRegion.DataValueField = "CountryCode";
                chkRegion.DataBind();
                chkRegion.Enabled = true;
                
            }
            else
            {
                chkRegion.Enabled = false;
                chkRegion.Items.Clear();
                chkSelectAllRegion.Checked = false;
            }
            foreach (ListItem regionItem in chkRegion.Items)
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
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds);
                chkOrg.DataSource = organization;
                chkOrg.DataTextField = "Name";
                chkOrg.DataValueField = "Autoid";
                chkOrg.DataBind();
                chkOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkOrg.Enabled = false;
                chkOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            }

            foreach (ListItem OrgItem in chkOrg.Items)
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
                chkCompany.DataSource = companies;
                chkCompany.DataTextField = "Name";
                chkCompany.DataValueField = "Id";
                chkCompany.DataBind();
                chkCompany.Enabled = true;
                chkselectallcompany.Checked = false;

            }
            else
            {
                chkCompany.Enabled = false;
                chkCompany.Items.Clear();
                chkselectallcompany.Checked = false;

            }
            upnl_Menuaccess.Update();
        } 
        protected void chkSelectAllRegion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBoxList chkRegion = (CheckBoxList)item.FindControl("chkactionRegion");
            CheckBoxList chkIndustry = (CheckBoxList)item.FindControl("chkactionIndustry");
            CheckBoxList chkComapny = (CheckBoxList)item.FindControl("chkactionCompany");
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");
            if (chkRegion != null)
            {
                foreach (ListItem li in chkRegion.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
                chkselectallcompany.Checked = chkSelectAll.Checked;
                chkactionRegion_SelectedIndexChanged(chkRegion, EventArgs.Empty);
                foreach (ListItem li in chkComapny.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
            }
            upnl_Menuaccess.Update();
        } 
        protected void chkactionRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkRegion = (CheckBoxList)sender;
            ListViewItem item = (ListViewItem)chkRegion.NamingContainer; 
            CheckBox chkSelectAllRegion = (CheckBox)item.FindControl("chkSelectAllRegion");
            CheckBoxList chkOrg = (CheckBoxList)item.FindControl("chkactionOrg");
            CheckBox chkSelectAllOrg = (CheckBox)item.FindControl("chkSelectAllOrg");
            CheckBoxList chkCompany = (CheckBoxList)item.FindControl("chkactionCompany");
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");
            CheckBoxList chkGroup = (CheckBoxList)item.FindControl("chkactionGroup");

            string selectedRegionIds = string.Empty;
            string selectedGroupIds = string.Empty;
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkGroup.Items)
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
            foreach (ListItem regionItem in chkRegion.Items)
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
                var organization = GetOrgByRegion(selectedGroupIds, selectedRegionIds);
                chkOrg.DataSource = organization;
                chkOrg.DataTextField = "Name";
                chkOrg.DataValueField = "Autoid";
                chkOrg.DataBind();
                chkOrg.Enabled = true;
                chkSelectAllOrg.Checked = false;
            }
            else
            {
                chkOrg.Enabled = false;
                chkOrg.Items.Clear();
                chkSelectAllOrg.Checked = false;
            } 
            foreach (ListItem OrgItem in chkOrg.Items)
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
                chkCompany.DataSource = companies;
                chkCompany.DataTextField = "Name";
                chkCompany.DataValueField = "Id";
                chkCompany.DataBind();
                chkCompany.Enabled = true;
                chkselectallcompany.Checked = false;

            }
            else
            {
                chkCompany.Enabled = false;
                chkCompany.Items.Clear();
                chkselectallcompany.Checked = false;

            }
            upnl_Menuaccess.Update();
        } 
        protected void chkSelectAllOrg_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBox chkSelectAllOrg = (CheckBox)item.FindControl("chkSelectAllOrg");
            CheckBoxList chkOrg = (CheckBoxList)item.FindControl("chkactionOrg");
            if (chkSelectAllOrg != null)
            {
                foreach (ListItem li in chkOrg.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
                chkactionOrg_SelectedIndexChanged(chkOrg, EventArgs.Empty);
            }
            upnl_Menuaccess.Update();
        } 
        protected void chkactionOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkaction = (CheckBoxList)sender;
            ListViewItem item = (ListViewItem)chkaction.NamingContainer;
           
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");

            CheckBoxList chkCompany = (CheckBoxList)item.FindControl("chkactionCompany");
            string selectedOrgIds = string.Empty;

            foreach (ListItem groupItem in chkaction.Items)
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
                chkCompany.DataSource = Companies;
                chkCompany.DataTextField = "Name";
                chkCompany.DataValueField = "Id";
                chkCompany.DataBind();
                chkCompany.Enabled = true;
                chkselectallcompany.Checked = false;
            }
            else
            {
                chkCompany.Enabled = false;
                chkCompany.Items.Clear();
                chkselectallcompany.Checked = false;
            }
            upnl_Menuaccess.Update();
        }
        protected void chkselectallcompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chkSelectAll.NamingContainer;
            CheckBox chkselectallcompany = (CheckBox)item.FindControl("chkselectallcompany");
            CheckBoxList chkComapny = (CheckBoxList)item.FindControl("chkactionCompany");
            if (chkComapny != null)
            {
                foreach (ListItem li in chkComapny.Items)
                {
                    li.Selected = chkSelectAll.Checked;
                }
                chkactionCompany_SelectedIndexChanged(chkComapny, EventArgs.Empty);
            }
            upnl_Menuaccess.Update();
        }
        protected void chkactionCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }  
        
        protected void btnaddbulkaccess_Click(object sender, EventArgs e)
        {
            // Read selected values from hidden fields
            string[] selectedIndustries = string.IsNullOrEmpty(hdnIndustry.Value) ? new string[0] : hdnIndustry.Value.Split(',');
            string[] selectedGroups = string.IsNullOrEmpty(hdnGroup.Value) ? new string[0] : hdnGroup.Value.Split(',');
            string[] selectedRegions = string.IsNullOrEmpty(hdnRegion.Value) ? new string[0] : hdnRegion.Value.Split(',');
            string[] selectedOrgs = string.IsNullOrEmpty(hdnOrganization.Value) ? new string[0] : hdnOrganization.Value.Split(',');
            string[] selectedCompanies = string.IsNullOrEmpty(hdnCompany.Value) ? new string[0] : hdnCompany.Value.Split(',');
            string[] selectedLocations = string.IsNullOrEmpty(hdnWorkLocation.Value) ? new string[0] : hdnWorkLocation.Value.Split(',');

            foreach (ListViewDataItem row in LV_Access_Menu_Company.Items)
            {
                // Industry
                CheckBoxList chkactionIndustry = (CheckBoxList)row.FindControl("chkactionIndustry");
                if (chkactionIndustry != null)
                {
                    foreach (ListItem item in chkactionIndustry.Items)
                    {
                        item.Selected = selectedIndustries.Contains(item.Value);
                    }
                    chkactionIndustry_SelectedIndexChanged(chkactionIndustry, e);
                }

                // Group
                CheckBoxList chkactionGroup = (CheckBoxList)row.FindControl("chkactionGroup");
                if (chkactionGroup != null)
                {
                    foreach (ListItem item in chkactionGroup.Items)
                    {
                        item.Selected = selectedGroups.Contains(item.Value);
                    }
                    chkactionGroup_SelectedIndexChanged(chkactionGroup, e);
                }

                // Region
                CheckBoxList chkactionRegion = (CheckBoxList)row.FindControl("chkactionRegion");
                if (chkactionRegion != null)
                {
                    foreach (ListItem item in chkactionRegion.Items)
                    {
                        item.Selected = selectedRegions.Contains(item.Value);
                    }
                    chkactionRegion_SelectedIndexChanged(chkactionRegion, e);
                }

                // Organization
                CheckBoxList chkactionOrg = (CheckBoxList)row.FindControl("chkactionOrg");
                if (chkactionOrg != null)
                {
                    foreach (ListItem item in chkactionOrg.Items)
                    {
                        item.Selected = selectedOrgs.Contains(item.Value);
                    }
                    chkactionOrg_SelectedIndexChanged(chkactionOrg, e);
                }

                // Company
                CheckBoxList chkactionCompany = (CheckBoxList)row.FindControl("chkactionCompany");
                if (chkactionCompany != null)
                {
                    foreach (ListItem item in chkactionCompany.Items)
                    {
                        item.Selected = selectedCompanies.Contains(item.Value);
                    }
                    chkactionCompany_SelectedIndexChanged(chkactionCompany, e);
                }

                // Work Location
                CheckBoxList chkLocation = (CheckBoxList)row.FindControl("chkLocation");
                if (chkLocation != null)
                {
                    foreach (ListItem item in chkLocation.Items)
                    {
                        item.Selected = selectedLocations.Contains(item.Value);
                    }
                }
            }

            LstIndustry.SelectedIndex = -1;
            // Update dropdown text and close modal
            ScriptManager.RegisterStartupScript(this, GetType(), "updateDropdown", "updateDropdownButtonText();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "$('#PopUpBulkAccess').modal('hide');", true);
        }



        [WebMethod]
        public static List<Dictionary<string, string>> GetGroups(string industryIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 26;
            PL.IndustryId = industryIds;
            StructureDL.returnTable(PL);

            return PL.dt.AsEnumerable()
                .Select(dr => new Dictionary<string, string>
                {
            { "Id", dr["Id"].ToString() },
            { "Name", dr["Name"].ToString() }
                }).ToList();
        }

        [WebMethod]
        public static List<Dictionary<string, string>> GetRegions(string groupIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 27;
            PL.IndustryId = groupIds;
            StructureDL.returnTable(PL);

            return PL.dt.AsEnumerable()
                .Select(dr => new Dictionary<string, string>
                {
            { "CountryCode", dr["CountryCode"].ToString() },
            { "CountryName", dr["CountryName"].ToString() }
                }).ToList();
        }

        [WebMethod]
        public static List<Dictionary<string, string>> GetOrganizations(string regionIds, string groupIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 28; 
            PL.Description = groupIds;
            PL.IndustryId = regionIds;
            StructureDL.returnTable(PL);

            return PL.dt.AsEnumerable()
                .Select(dr => new Dictionary<string, string>
                {
            { "Autoid", dr["Autoid"].ToString() },
            { "Name", dr["Name"].ToString() }
                }).ToList();
        }

        [WebMethod]
        public static List<Dictionary<string, string>> GetCompanies(string orgIds)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 29;
            PL.IndustryId = orgIds;
            StructureDL.returnTable(PL);

            return PL.dt.AsEnumerable()
                .Select(dr => new Dictionary<string, string>
                {
            { "Id", dr["Id"].ToString() },
            { "Name", dr["Name"].ToString() }
                }).ToList();
        }

        protected void btnRevokeAllAccess_Click(object sender, EventArgs e)
        {
            foreach (ListViewDataItem row in LV_Access_Menu_Company.Items)
            {
                ClearCheckBoxList(row, "chkactionIndustry");
                ClearCheckBoxList(row, "chkactionGroup");
                ClearCheckBoxList(row, "chkactionRegion");
                ClearCheckBoxList(row, "chkactionOrg");
                ClearCheckBoxList(row, "chkactionCompany");
                ClearCheckBoxList(row, "chkLocation");
            } 
            LstIndustry.ClearSelection();
            LstGroup.ClearSelection();
            LstRegion.ClearSelection();
            LstOrgnization.ClearSelection();
            LstCompany.ClearSelection();
            LstWorkLocation.ClearSelection();

            ScriptManager.RegisterStartupScript(this, GetType(), "updateDropdown", "updateDropdownButtonText();", true);
        }

        
        private void ClearCheckBoxList(ListViewDataItem row, string controlId)
        {
            CheckBoxList chk = row.FindControl(controlId) as CheckBoxList;
            if (chk != null)
            {
                foreach (ListItem item in chk.Items)
                {
                    item.Selected = false;
                } 
                if (controlId == "chkactionIndustry") chkactionIndustry_SelectedIndexChanged(chk, EventArgs.Empty);
                if (controlId == "chkactionGroup") chkactionGroup_SelectedIndexChanged(chk, EventArgs.Empty);
                if (controlId == "chkactionRegion") chkactionRegion_SelectedIndexChanged(chk, EventArgs.Empty);
                if (controlId == "chkactionOrg") chkactionOrg_SelectedIndexChanged(chk, EventArgs.Empty);
                if (controlId == "chkactionCompany") chkactionCompany_SelectedIndexChanged(chk, EventArgs.Empty);
            }
        }

    }
}