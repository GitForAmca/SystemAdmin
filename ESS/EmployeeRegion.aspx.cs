using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class EmployeeRegion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                BindAllDropdownsFromSingleDataTable();
                BindMasterRegion(ddlMasterRegion);
                BindMasterRegion(ddlMasterRegionSearch);
            }
        }
        void FillListView()
        {
            //-----------------
            EssPL PL = new EssPL();
            PL.OpCode = 85;
            PL.String1 = ddlMasterRegionSearch.SelectedValue.ToString();
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV_Employee_Region.DataSource = dt;
            LV_Employee_Region.DataBind();
        }
        protected void ddlMasterRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
        void BindMasterRegion(DropDownList ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 82;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", ""));
        }
        void BindAllDropdownsFromSingleDataTable()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 81; 
            EssDL.returnTable(PL);

            if (PL.dt != null && PL.dt.Rows.Count > 0)
            {
                BindDropdown(ddlRegion, PL.dt, "CountryName");
                BindDropdown(ddlTimeZone, PL.dt, "TimeZoneFormat");
                BindDropdown(ddlCurrency, PL.dt, "Currency");
            }
        }

        void BindDropdown(DropDownList ddl, DataTable dt, string typeFilter)
        {
            DataView view = new DataView(dt);

            ddl.DataSource = dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = typeFilter;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", ""));
        }

        void ClearField()
        {
            ddlMasterRegion.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;
            ddlTimeZone.SelectedIndex = -1;
            ddlCurrency.SelectedIndex = -1;
            chkactive.Checked = true;
            hidID.Value = "";
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            ddlMasterRegion.Enabled = true;
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Employee_Region.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        EssPL PL = new EssPL();
                        PL.OpCode = 85;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        if (dt.Rows.Count > 0)
                        {
                            ddlMasterRegion.SelectedValue = dt.Rows[0]["MasterRegionAutoid"].ToString();
                            ddlMasterRegion.Enabled = false;
                            ddlRegion.SelectedValue = dt.Rows[0]["RegionAutoid"].ToString();
                            ddlTimeZone.SelectedValue = dt.Rows[0]["TimeZoneId"].ToString();
                            ddlCurrency.SelectedValue = dt.Rows[0]["CurrencyId"].ToString();
                            chkactive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
                            ViewState["Mode"] = "Edit";
                            hidID.Value = Autoid.ToString();
                            divView.Visible = false;
                            divAddEdit.Visible = true;
                            break;
                        }
                    }
                }
            }
        }
        void SetMultiBD(ListBox ddl, string ids)
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<MasterRegionAutoid><![CDATA[" + ddlMasterRegion.SelectedValue + "]]></MasterRegionAutoid>";
            xml += "<RegionCode><![CDATA[" + ddlRegion.SelectedValue + "]]></RegionCode>";
            xml += "<TimeZone><![CDATA[" + ddlTimeZone.SelectedValue + "]]></TimeZone>";
            xml += "<Currency><![CDATA[" + ddlCurrency.SelectedValue + "]]></Currency>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked == true ? 1 : 0) + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 83;
                PL.XML = xml;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    if (PL.dt.Rows[0]["Status"].ToString() == "1")
                    {
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ClearField();
                        FillListView();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowError('Region already exist in the database.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 84;
                PL.XML = xml;
                PL.AutoId = Convert.ToInt32(hidID.Value);
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
        }
    }
}