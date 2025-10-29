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