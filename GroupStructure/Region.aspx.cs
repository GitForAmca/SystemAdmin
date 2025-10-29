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
    public partial class Region : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetGroup(ddlGroupFilter);
                 GetRegion(ddlRegion);
                GetGIndustry(ddlIndustry);
                GetHOD(ddlHOD);
                GetGroup(ddlGroup);
                FillListView();
            }
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
        void GetIndustry(DropDownList ddl)
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
        void ClearField()
        {
            ddlGroup.SelectedValue = string.Empty;
            ddlGroupFilter.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;
            ddlHOD.SelectedIndex = -1;
        }
        void FillListView()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 33;
            PL.Name = ddlGroupFilter.SelectedValue;
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
        }
        void getData(int Autoid)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 34;
            PL.AutoId = Autoid;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                try
                { 
                    ddlGroup.SelectedValue = PL.dt.Rows[0]["GroupId"].ToString();
                    ddlGroup_SelectedIndexChanged(ddlGroup,EventArgs.Empty);
                    ddlIndustry.SelectedValue = PL.dt.Rows[0]["IndustryId"].ToString();
                    ddlIndustry_SelectedIndexChanged(ddlGroup, EventArgs.Empty);
                    ddlRegion.SelectedValue = PL.dt.Rows[0]["RegionId"].ToString();
                    ddlHOD.SelectedValue = PL.dt.Rows[0]["HODID"].ToString(); 
                    if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                    {
                        chkActive.Checked = false;
                    }
                    else
                    {
                        chkActive.Checked = true;
                    }
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                ddlGroup.SelectedIndex = -1;
            }
        } 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL();
            PL.Name = ddlGroup.SelectedValue;
            PL.Description = ddlRegion.SelectedValue;
            PL.IsActive = chkActive.Checked;
            PL.HOD = ddlHOD.SelectedValue;
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 30;
            }
            else
            {
                PL.OpCode = 31;
                PL.AutoId = Convert.ToInt32(hidAutoid.Value);
            } 
            PL.IndustryId = ddlIndustry.SelectedValue;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagWarning", $"ShowError('Already exists');", true);
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

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGIndustry(ddlIndustry);
        }

        protected void ddlIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRegion(ddlRegion);
        }
    }
}