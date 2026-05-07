using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class LicenseIndustry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                GetIndustries(ddlIndustryFilter);
                GetActivies(lstActivityFilter);
                GetActivies(lstActivity);
            }
        }
        void FillListView()
        { 
            EssPL PL = new EssPL();
            PL.OpCode = 119;
            PL.AutoId = ddlIndustryFilter.SelectedValue;
            if(lstActivityFilter.SelectedValue != "")
            {
                PL.String1 = Request.Form[lstActivityFilter.UniqueID].ToString();
            }
            PL.IsActive = ddlIsActive.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (!PL.isException)
            {
                if (dt.Rows.Count > 0)
                {
                    LV_LicenseIndustry.DataSource = dt;
                    LV_LicenseIndustry.DataBind();
                }
                else
                {
                    LV_LicenseIndustry.DataSource = "";
                    LV_LicenseIndustry.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            } 
        }
        void GetIndustries(DropDownList ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 139;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "IndustryName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetActivies(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 116;
            PL.String1 = lstActivity.SelectedValue;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "LicenseActivityName";
            ddl.DataBind();
        }
        void GetActiviesFilter(ListBox ddl, String IndustryId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 121;
            PL.Industry = IndustryId;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ActivityId";
            ddl.DataTextField = "LicenseActivityName";
            ddl.DataBind();
        }
        void ClearField()
        {
            ddlIndustryFilter.SelectedIndex = -1;
            lstActivityFilter.SelectedIndex = -1;
            ddlIsActive.SelectedIndex = -1;

            lstActivity.SelectedIndex = -1;
            txtIndustry.Text = ""; 
            chkactive.Checked = true;
            hidID.Value = "";
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";
        } 
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_LicenseIndustry.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 120;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtIndustry.Text = dt.Rows[0]["IndustryName"].ToString();
                            GetActivies(lstActivity);
                            SetList(lstActivity, PL.dt.Rows[0]["ActivityIds"].ToString()); 
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtIndustry.Text.Trim() != "" && lstActivity.SelectedValue != "")
            {
                EssPL PL = new EssPL();

                string xml = "<tbl>";
                xml += "<tr>";
                xml += "<IndustryName><![CDATA[" + txtIndustry.Text + "]]></IndustryName>";
                xml += "<ActivityIds><![CDATA[" + Request.Form[lstActivity.UniqueID].ToString() + "]]></ActivityIds>";
                xml += "<IsActive><![CDATA[" + (chkactive.Checked == true ? 1 : 0) + "]]></IsActive>";
                xml += "</tr>";
                xml += "</tbl>";

                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 117;
                    PL.XML = xml;
                    PL.CreatedBy = Session["UserAutoId"].ToString();
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
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    PL.OpCode = 118;
                    PL.XML = xml;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    EssDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ClearField();
                        FillListView();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
            }
            else
            {

            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearField();
            FillListView();
        }
        protected void ddlIndustryFilter_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //if (ddlIndustryFilter.SelectedValue != "")
            //{
            //    GetActiviesFilter(lstActivityFilter, ddlIndustryFilter.SelectedValue);
            //}
            //else
            //{
            //    ddlIndustryFilter.Items.Clear();
            //    GetIndustries(ddlIndustryFilter);
            //}
        }
    }
}