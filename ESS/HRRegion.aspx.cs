using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class HRRegion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getRegion("0");
                getIndustry(ddlIndustries);
                BindHR();
                FillListView();
            }
        }

        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 88;
            PL.String1 = ddlRegionSearch.SelectedValue;
            PL.Industry = ddl_IndustrySearch.SelectedValue;
            EssDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                LV_ParentHR_Access.DataSource = PL.dt;
                LV_ParentHR_Access.DataBind();
            }
            else
            {
                LV_ParentHR_Access.DataSource = "";
                LV_ParentHR_Access.DataBind();
            }
        }
        void BindHR ()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 74;
            DropdownDL.returnTable(PL);
            ddl_PrimaryHR.DataSource = PL.dt;
            ddl_PrimaryHR.DataValueField = "Id";
            ddl_PrimaryHR.DataTextField = "Name";
            ddl_PrimaryHR.DataBind();
            ddl_PrimaryHR.Items.Insert(0, new ListItem("Choose an item", ""));  
            ddl_SecondaryHR.DataSource = PL.dt;
            ddl_SecondaryHR.DataValueField = "Id";
            ddl_SecondaryHR.DataTextField = "Name";
            ddl_SecondaryHR.DataBind(); 
            ddl_SecondaryHR.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDepartmentFilter(ListBox dll)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 72;
            DropdownDL.returnTable(PL);
            dll.DataSource = PL.dt;
            dll.DataValueField = "Id";
            dll.DataTextField = "Name";
            dll.DataBind();
            
        }
        void getRegion(string id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 71;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlRegion.DataSource = PL.dt;
            ddlRegion.DataValueField = "ID";
            ddlRegion.DataTextField = "CountryName";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("Choose an item", "")); 

            ddlRegionSearch.DataSource = PL.dt;
            ddlRegionSearch.DataValueField = "ID";
            ddlRegionSearch.DataTextField = "CountryName";
            ddlRegionSearch.DataBind();
            ddlRegionSearch.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getIndustry(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Description";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));

            ddl_IndustrySearch.DataSource = PL.dt;
            ddl_IndustrySearch.DataValueField = "ID";
            ddl_IndustrySearch.DataTextField = "Description";
            ddl_IndustrySearch.DataBind();
            ddl_IndustrySearch.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Add";
            divEdit.Visible = true;
            divView.Visible = false;
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_ParentHR_Access.Items)
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

        void getData(int id)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 89;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ddlIndustries.SelectedIndex = ddlIndustries.Items.IndexOf(ddlIndustries.Items.FindByValue(PL.dt.Rows[0]["IndustryId"].ToString()));
                ddlRegion.SelectedIndex = ddlRegion.Items.IndexOf(ddlRegion.Items.FindByValue(PL.dt.Rows[0]["Region"].ToString()));
                ddl_PrimaryHR.SelectedIndex = ddl_PrimaryHR.Items.IndexOf(ddl_PrimaryHR.Items.FindByValue(PL.dt.Rows[0]["PrimaryHR"].ToString()));
                ddl_SecondaryHR.SelectedIndex = ddl_SecondaryHR.Items.IndexOf(ddl_SecondaryHR.Items.FindByValue(PL.dt.Rows[0]["SecondaryHR"].ToString()));
                BindDepartment();
                SelectCheckDesignationMenu(PL.dt.Rows[0]["DepID"].ToString());
            }
        }

        void SelectCheckDesignationMenu(string depids)
        {
            if (string.IsNullOrEmpty(depids)) return;

            string[] selectedIds = depids.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (ListViewItem item in LV_department.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                HiddenField hdn = (HiddenField)item.FindControl("hdnId");

                if (chk != null && hdn != null)
                {
                    chk.Checked = selectedIds.Contains(hdn.Value);
                }
            }
        }


        protected void ddlIndustries_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearfield();
            divEdit.Visible = false;
            divView.Visible = true;
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clearfield();
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlRegion.SelectedValue != "")
            {
                BindDepartment();
            }
            else
            {
                LV_department.DataSource = "";
                LV_department.DataBind();
            }
            
        }
        void BindDepartment()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 75;
            PL.IndustryId = ddlIndustries.SelectedValue;
            DropdownDL.returnTable(PL);
            if(PL.dt.Rows.Count > 0)
            {
                LV_department.DataSource = PL.dt;
                LV_department.DataBind();
            }
            else
            {
                LV_department.DataSource = "";
                LV_department.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ViewState["Mode"].ToString() == "Add")
            { 
                EssPL PL = new EssPL();
                PL.OpCode = 86;
                PL.Industry = ddlIndustries.SelectedValue;
                PL.String1 = ddlRegion.SelectedValue;
                PL.String2 = ddl_PrimaryHR.SelectedValue;
                PL.String3 = ddl_SecondaryHR.SelectedValue;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                PL.Department = GetSelectedIds();
                EssDL.returnTable(PL);
                if(PL.dt.Rows.Count == 0 )
                {
                    PL.OpCode = 87;
                    EssDL.returnTable(PL);
                    if(!PL.isException)
                    {
                        divEdit.Visible = false;
                        divView.Visible = true;
                        Clearfield();
                        FillListView();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Saved successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('Record already exits for the selected departments');", true);
                }
            }
            else
            {
                EssPL PL = new EssPL(); 
                PL.OpCode = 90;
                PL.AutoId = hidAutoid.Value;
                PL.Industry = ddlIndustries.SelectedValue;
                PL.String1 = ddlRegion.SelectedValue;
                PL.String2 = ddl_PrimaryHR.SelectedValue;
                PL.String3 = ddl_SecondaryHR.SelectedValue;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                PL.Department = GetSelectedIds();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divEdit.Visible = false;
                    divView.Visible = true;
                    Clearfield();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
        }

        string GetSelectedIds()
        {
            string dep = "";
            foreach (ListViewItem lvItem in LV_department.Items)
            {
                CheckBox chkSelect = (CheckBox)lvItem.FindControl("chkSelect");
                HiddenField hdnId = (HiddenField)lvItem.FindControl("hdnId");
                if (chkSelect != null && chkSelect.Checked && hdnId != null)
                {
                    dep += hdnId.Value + "^";
                }
            }
            return dep;
        }

        void Clearfield()
        {
            ddl_IndustrySearch.SelectedIndex = -1;
            ddlRegionSearch.SelectedIndex = -1;
            ddlIndustries.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;
            ddl_PrimaryHR.SelectedIndex = -1;
            ddl_SecondaryHR.SelectedIndex = -1;
            BindDepartment();
        }

    }
}