using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.GroupStructure
{
    public partial class Organization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetGroup(ddlGroupFilter);
                GetGroup(ddlGroup);
                GetLevel(ddlLevel);
                GetEmployee(ddlEmp);
                //GetRegion(ddlRegion);
                GetHOD(ddlHOD);
                FillListView();
            }
        }
        void GetHOD(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 24;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetLevel(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 40;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetGroup(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 10;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetRegion(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 32;
            PL.Name = ddlIndustry.SelectedValue;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetEmployee(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 41;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtGroupName.Text = "";
            txtShortName.Text = "";
            ddlGroup.SelectedIndex = -1;
            ddlIndustry.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1; 
            txtSequence.Text = ""; 
            txtPrimaryColor.Value = "";
            txtSecondarColor.Value = ""; 
            ddlGroupFilter.SelectedIndex = -1;
            ddlGroup.SelectedIndex = -1;
            
        }
        void FillListView()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 12;
            PL.Name = ddlGroupFilter.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Organization.DataSource = PL.dt;
                LV_Organization.DataBind();
            }
            else
            {
                LV_Organization.DataSource = PL.dt;
                LV_Organization.DataBind();
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
            foreach (ListViewItem item in LV_Organization.Items)
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
            BindLevelHeirarchy();
        }

        void getData(int Autoid)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 15;
            PL.AutoId = Autoid;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                try
                { 
                    txtGroupName.Text = PL.dt.Rows[0]["Name"].ToString();
                    txtShortName.Text = PL.dt.Rows[0]["ShortForm"].ToString();  
                    txtSequence.Text = PL.dt.Rows[0]["Sequence"].ToString(); 
                    txtPrimaryColor.Value = PL.dt.Rows[0]["PrimaryColor"].ToString();
                    txtSecondarColor.Value = PL.dt.Rows[0]["SecondaryColor"].ToString(); 
                    if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                    {
                        chkActive.Checked = false;
                    }
                    else
                    {
                        chkActive.Checked = true;
                    }
                    ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
                    ddlGroup.SelectedValue = PL.dt.Rows[0]["GroupID"].ToString();
                    ddlGroup_SelectedIndexChanged(ddlGroup, EventArgs.Empty);
                    ddlIndustry.SelectedValue =  PL.dt.Rows[0]["IndustryID"].ToString();
                    ddlIndustry_SelectedIndexChanged(ddlIndustry, EventArgs.Empty);
                    ddlRegion.SelectedValue = PL.dt.Rows[0]["RegionId"].ToString();
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                txtGroupName.Text = "";
                txtShortName.Text = "";
                ddlGroup.SelectedValue = "";
                ddlRegion.SelectedValue = ""; 
                txtSequence.Text = ""; 
                txtPrimaryColor.Value = "";
                txtSecondarColor.Value = "";
              
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL();
            PL.Name = txtGroupName.Text.Trim();
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<OrgName><![CDATA[" + txtGroupName.Text.Trim() + "]]></OrgName>";
            xml += "<ShortName><![CDATA[" + txtShortName.Text.Trim() + "]]></ShortName>";
            xml += "<GroupId><![CDATA[" + ddlGroup.SelectedValue + "]]></GroupId>";
            xml += "<Industry><![CDATA[" + ddlIndustry.SelectedValue + "]]></Industry>";
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>"; 
            xml += "<Sequence><![CDATA[" + txtSequence.Text.Trim() + "]]></Sequence>"; 
            xml += "<PrimaryColor><![CDATA[" + txtPrimaryColor.Value.Trim() + "]]></PrimaryColor>";
            xml += "<SecondaryColor><![CDATA[" + txtSecondarColor.Value.Trim() + "]]></SecondaryColor>"; 
            xml += "</tr>";
            xml += "</tbl>";
            PL.IsActive = chkActive.Checked;
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 13;
            }
            else
            {
                PL.OpCode = 14;
                PL.AutoId = Convert.ToInt32(hidAutoid.Value);
            }
            PL.HOD = ddlHOD.SelectedValue;
            PL.XML = xml;
            DataTable dtCurrentTable = (DataTable)ViewState["Assessor"];
            int rowCount = dtCurrentTable.Rows.Count;
            var xml1 = "<tbl>";
            foreach (ListViewItem item in LV_AssessorTbl.Items)
            {

                HiddenField hdnLevel = (HiddenField)item.FindControl("hdnLevel");
                HiddenField hdnEmp = (HiddenField)item.FindControl("hdnEmp");
                HiddenField hdnDesignationId = (HiddenField)item.FindControl("hdnDesignationId");
                string level = hdnLevel?.Value ?? "";
                string empId = hdnEmp?.Value ?? "";
                string designationId = hdnDesignationId?.Value ?? "";

                DropDownList ddlExecutive = (DropDownList)item.FindControl("ddlExecutive");
                DropDownList ddlDesignation = (DropDownList)item.FindControl("ddlDesignation");
                CheckBox chkOnOffPreGrace = (CheckBox)item.FindControl("chkOnOffPreGrace");
                bool IsMain = chkOnOffPreGrace?.Checked ?? false;

                string selectedExecutive = ddlExecutive?.SelectedValue ?? "";
                string selectedDesignation = ddlDesignation?.SelectedValue ?? "";
                xml1 += "<tr>";
                xml1 += $"<Level><![CDATA[{level}]]></Level>";
                xml1 += $"<EmpId><![CDATA[{selectedExecutive}]]></EmpId>";
                xml1 += $"<DesignationId><![CDATA[{selectedDesignation}]]></DesignationId>";
                xml1 += $"<IsMain><![CDATA[{IsMain}]]></IsMain>";
                xml1 += $"<AutoId><![CDATA[{PL.AutoId}]]></AutoId>";
                xml1 += "</tr>";
            }

            xml1 += "</tbl>";
            PL.XMLData = xml1;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            StructureDL.returnTable(PL);
            if (!PL.isException)
            {
                int result = Convert.ToInt32(PL.dt.Rows[0]["Result"]);
                if (result == 0)
                {
                    ClearField();
                    FillListView();
                    divView.Visible = true;
                    divEdit.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Saved Successfully');", true);
                }

                else if (result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagWarning", $"ShowError('An Organization already exists with the same name');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
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
            ddlGroupFilter.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }

        protected void ddlIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRegion(ddlRegion);
        }

      

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGIndustry(ddlIndustry);
        }


        void GetGIndustry(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 36;
            PL.Name = ddlGroup.SelectedValue;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetDesigantion(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 46;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        protected void LV_AssessorTbl_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                DropDownList ddlExecutive = (DropDownList)e.Item.FindControl("ddlExecutive");
                DropDownList ddlDesignation = (DropDownList)e.Item.FindControl("ddlDesignation");
                HiddenField hdnEmp = (HiddenField)e.Item.FindControl("hdnEmp");
                HiddenField hdnDesignationId = (HiddenField)e.Item.FindControl("hdnDesignationId");

                if (ddlExecutive != null)
                {
                    GetEmployee(ddlExecutive);
                    if (hdnEmp != null && !string.IsNullOrEmpty(hdnEmp.Value))
                    {
                        ListItem item = ddlExecutive.Items.FindByValue(hdnEmp.Value);
                        if (item != null)
                            ddlExecutive.SelectedValue = hdnEmp.Value;
                    }

                }

                if (ddlDesignation != null)
                {
                    GetDesigantion(ddlDesignation);
                    if (hdnDesignationId != null && !string.IsNullOrEmpty(hdnDesignationId.Value))
                    {
                        ListItem item = ddlDesignation.Items.FindByValue(hdnDesignationId.Value);
                        if (item != null)
                            ddlDesignation.SelectedValue = hdnDesignationId.Value;
                    }

                }

            }
        }

        protected void LV_AssessorTbl_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                string autoid = e.CommandArgument.ToString();

                if (ViewState["Assessor"] != null)
                {
                    DataTable dt = (DataTable)ViewState["Assessor"];
                    DataRow[] rows = dt.Select("Autoid='" + autoid + "'");
                    if (rows.Length > 0)
                    {
                        dt.Rows.Remove(rows[0]);
                        dt.AcceptChanges();
                    }
                    ViewState["Assessor"] = dt;
                    LV_AssessorTbl.DataSource = dt;
                    LV_AssessorTbl.DataBind();
                }
            }
        }

        protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddLevel_Click(object sender, EventArgs e)
        {
            AddNewRecordRowToHeirarchyAdd();
            AssessorTbl.Update();
        }

        private void AddNewRecordRowToHeirarchyAdd()
        {
            if (!string.IsNullOrEmpty(ddlLevel.SelectedValue) && !string.IsNullOrEmpty(ddlEmp.SelectedValue))
            {
                DataTable dtCurrentTable; 
                if (ViewState["Assessor"] != null)
                {
                    dtCurrentTable = (DataTable)ViewState["Assessor"];
                }
                else
                {
                    dtCurrentTable = new DataTable();
                    dtCurrentTable.Columns.Add("Autoid");
                    dtCurrentTable.Columns.Add("Level");
                    dtCurrentTable.Columns.Add("EmpId");
                    dtCurrentTable.Columns.Add("EmployeeName");
                    dtCurrentTable.Columns.Add("DesignationId");
                    dtCurrentTable.Columns.Add("DesignationName");
                    dtCurrentTable.Columns.Add("IsMain");
                } 
                DataRow[] existingRows = dtCurrentTable.Select("Autoid = '" + ddlLevel.SelectedValue + "'");
                if (existingRows.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('This Level already exists. Please select another.');", true);
                    return;
                } 
                DataRow drNew = dtCurrentTable.NewRow();
                drNew["Autoid"] = ddlLevel.SelectedValue;
                drNew["Level"] = ddlLevel.SelectedItem.Text;
                drNew["EmpId"] = ddlEmp.SelectedValue;
                drNew["DesignationId"] = "0";
                drNew["EmployeeName"] = ddlEmp.SelectedItem.Text.Trim();
                drNew["IsMain"] = false;
                dtCurrentTable.Rows.Add(drNew); 
                ViewState["Assessor"] = dtCurrentTable;
                LV_AssessorTbl.DataSource = dtCurrentTable;
                LV_AssessorTbl.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('Please select both Level and Employee.');", true);
            }
        }

        void BindLevelHeirarchy()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 47;
            PL.AutoId = hidAutoid.Value;
            StructureDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                LV_AssessorTbl.DataSource = PL.dt;
                LV_AssessorTbl.DataBind();
            }
            else
            {
                LV_AssessorTbl.DataSource = "";
                LV_AssessorTbl.DataBind();
            }
            ViewState["Assessor"] = PL.dt;
            AssessorTbl.Update();
        }
    }
}