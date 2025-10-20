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
                GetIndustry(ddlIndustryFilter);
                GetIndustry(ddlIndustry);
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
        void ClearField()
        {
            txtGroupName.Text = string.Empty;
            ddlIndustryFilter.SelectedIndex = -1;
            ddlIndustry.SelectedIndex = -1;
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
                ddlIndustry.SelectedValue = PL.dt.Rows[0]["IndustryId"].ToString();
                ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL(); 
            PL.Name = txtGroupName.Text.Trim();
            PL.IndustryId = ddlIndustry.SelectedValue;
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