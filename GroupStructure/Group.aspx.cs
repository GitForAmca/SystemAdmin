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
            AddNewRecordRowToHeirarchy();
            AssessorTbl.Update();
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
        private void AddNewRecordRowToHeirarchy()
        {
            if (ddlLevel.SelectedValue != "" && ddlEmp.SelectedValue != "")
            {
                if (ViewState["Assessor"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["Assessor"];
                    DataRow[] IsExixts = dtCurrentTable.Select("Autoid='" + ddlLevel.SelectedValue.ToString() + "'");
                    if (IsExixts.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('This Level already exists.Please select another');", true);
                        return;
                    }
                    DataRow drCurrentRow = null;
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["Autoid"] = ddlLevel.SelectedValue.ToString();
                            drCurrentRow["Level"] = ddlLevel.SelectedItem.ToString();
                            drCurrentRow["EmpId"] = ddlEmp.SelectedValue.ToString();
                            drCurrentRow["EmployeeName"] = ddlEmp.SelectedItem.Text.ToString().Trim();
                            drCurrentRow["isEnabled"] = false;
                        }
                        if (dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1][0].ToString() == "")
                        {
                            dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1].Delete();
                            dtCurrentTable.AcceptChanges();
                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["Assessor"] = dtCurrentTable;
                        LV_AssessorTbl.DataSource = dtCurrentTable;
                        LV_AssessorTbl.DataBind();
                    }
                }
            }
        }
        protected void LV_AssessorTbl_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlLevel = (DropDownList)e.Item.FindControl("ddlLevel");
                HiddenField hdnEmp = (HiddenField)e.Item.FindControl("hdnEmp");
               
                if (ddlEmp != null)
                {
                    GetEmployee(ddlEmp);

                    // Pre-select employee if exists in data
                    if (hdnEmp != null && !string.IsNullOrEmpty(hdnEmp.Value))
                    {
                        ListItem item = ddlEmp.Items.FindByValue(hdnEmp.Value);
                        if (item != null)
                            ddlEmp.SelectedValue = hdnEmp.Value;
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
        protected void chkOnOffPreGrace_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            ListViewItem item = (ListViewItem)chk.NamingContainer;
            int index = item.DataItemIndex;
            DropDownList ddl = (DropDownList)item.FindControl("ddlHierarchyAssessor");
            if (ViewState["Assessor"] != null)
            {
                DataTable dt = (DataTable)ViewState["Assessor"];
                try
                {
                    if (dt.Rows.Count > index)
                    {
                        dt.Rows[index]["isEnabled"] = chk.Checked;
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
            if (ddl != null)
            {
                if (!chk.Checked)
                {
                    ddl.CssClass = ddl.CssClass + "form-control select2ddl disabled";
                    ddl.SelectedIndex = -1;
                }
                else
                {
                    ddl.CssClass = ddl.CssClass.Replace("disabled", "form-control select2ddl reqAdd").Trim();
                }
            }
            AssessorTbl.Update();
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
            for (int i = 0; i < rowCount; i++)
            {
                xml += "<tr>";
                xml += "<Level><![CDATA[" + dtCurrentTable.Rows[i]["Autoid"] + "]]></Level>";
                xml += "<EmpId><![CDATA[" + dtCurrentTable.Rows[i]["EmpId"] + "]]></EmpId>";
                xml += "<IsEnabled><![CDATA[" + dtCurrentTable.Rows[i]["isEnabled"] + "]]></IsEnabled>";
                xml += "<AutoId><![CDATA[" + PL.AutoId + "]]></AutoId>";
                xml += "</tr>";
            }
            xml += "</tbl>";
            PL.XML = xml;
            //PL.Description = Request.Form[LstRegion.UniqueID];
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
    }
}