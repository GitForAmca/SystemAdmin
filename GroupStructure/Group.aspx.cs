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
    public partial class Group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetIndustryAll(ddlIndustryFilter);
                GetIndustry(lstIndustry);
                GetRegion(LstRegion);
                GetHOD(ddlHOD);
                GetLevel(ddlLevel);
                GetEmployee(ddlEmp);
                FillListView();
            }
        }
        protected void btnAdd_Level(object sender, EventArgs e)
        {

            AddNewRecordRowToHeirarchyAdd();
            AssessorTbl.Update();
        }
        private void AddNewRecordRowToHeirarchyAdd()
        {
            if (!string.IsNullOrEmpty(ddlLevel.SelectedValue) && !string.IsNullOrEmpty(ddlEmp.SelectedValue))
            {
                DataTable dtCurrentTable;

                // If ViewState already has table, get it; else create new structure
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

                // Check if Level already exists
                DataRow[] existingRows = dtCurrentTable.Select("Autoid = '" + ddlLevel.SelectedValue + "'");
                if (existingRows.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('This Level already exists. Please select another.');", true);
                    return;
                }

                // Add new row
                DataRow drNew = dtCurrentTable.NewRow();
                drNew["Autoid"] = ddlLevel.SelectedValue;
                drNew["Level"] = ddlLevel.SelectedItem.Text;
                drNew["EmpId"] = ddlEmp.SelectedValue;
                drNew["DesignationId"] = "0";
                drNew["EmployeeName"] = ddlEmp.SelectedItem.Text.Trim();
                drNew["IsMain"] = false;
                dtCurrentTable.Rows.Add(drNew);

                // Store and bind
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
            PL.OpCode = 42;
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
     
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSender = (DropDownList)sender;
            ListViewItem lvItem = (ListViewItem)ddlSender.NamingContainer;
            int index = lvItem.DataItemIndex;
            if (ViewState["Assessor"] != null)
            {
                DataTable dt = (DataTable)ViewState["Assessor"];
                try
                {
                    if (dt.Rows.Count > index)
                    {
                        dt.Rows[index]["EmpId"] = ddlSender.SelectedValue;
                        dt.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {

                }
                ViewState["Assessor"] = dt;
                LV_AssessorTbl.DataSource = dt;
                LV_AssessorTbl.DataBind();
            }
            AssessorTbl.Update();
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
        void GetIndustry(ListBox ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 1;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind(); 
        }

        void GetIndustryAll(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 1;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetRegion(ListBox ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 11;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind(); 
        }
        void ClearField()
        {
            LV_AssessorTbl.DataSource = null;      
            LV_AssessorTbl.DataBind();
            ViewState["Assessor"] = null; 
            LV_AssessorTbl.SelectedIndex = -1;
            ddlHOD.SelectedIndex = -1;
            ddlEmp.SelectedIndex = -1;
            txtGroupName.Text = string.Empty;
            ddlIndustryFilter.SelectedIndex = -1;
            lstIndustry.SelectedIndex = -1;
            ddlLevel.SelectedIndex = -1;
        }
        void FillListView()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 6;
            PL.Name = ddlIndustryFilter.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Group.DataSource = PL.dt;
                LV_Group.DataBind();
            }
            else
            {
                LV_Group.DataSource = PL.dt;
                LV_Group.DataBind();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Group.Items)
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
            PL.OpCode = 9;
            PL.AutoId = Autoid;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtGroupName.Text = PL.dt.Rows[0]["Name"].ToString();
                SetList(lstIndustry, PL.dt.Rows[0]["IndustryId"].ToString());
                //ddlIndustry.SelectedValue = PL.dt.Rows[0]["IndustryId"].ToString();
                ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
                //SetList(LstRegion, PL.dt.Rows[0]["Region"].ToString());
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
            }
            else
            {
                txtGroupName.Text = "";
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

      
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL(); 
            PL.Name = txtGroupName.Text.Trim();
            PL.IndustryId = Request.Form[lstIndustry.UniqueID];
            PL.IsActive = chkActive.Checked;
            PL.HOD = ddlHOD.SelectedValue;
            
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 7;
            }
            else
            {
                PL.OpCode = 8;
                PL.AutoId = Convert.ToInt32(hidAutoid.Value);
            }
            DataTable dtCurrentTable = (DataTable)ViewState["Assessor"];
            int rowCount = dtCurrentTable.Rows.Count;
            var xml = "<tbl>"; 
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
                xml += "<tr>";
                xml += $"<Level><![CDATA[{level}]]></Level>";
                xml += $"<EmpId><![CDATA[{selectedExecutive}]]></EmpId>";
                xml += $"<DesignationId><![CDATA[{selectedDesignation}]]></DesignationId>";
                xml += $"<IsMain><![CDATA[{IsMain}]]></IsMain>";
                xml += $"<AutoId><![CDATA[{PL.AutoId}]]></AutoId>";
                xml += "</tr>";
            }

            xml += "</tbl>";
            PL.XML = xml;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagWarning", $"ShowError('An Group already exists with the same name');", true);
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
            ddlIndustryFilter.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }

        protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}