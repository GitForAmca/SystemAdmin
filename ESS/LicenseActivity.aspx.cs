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
    public partial class LicenseActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                GetActivies(ddlLicenseActivity);
            }
        }
        void GetActivies(DropDownList ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 138;  
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "LicenseActivityName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void FillListView()
        { 
            EssPL PL = new EssPL();
            PL.OpCode = 50;
            PL.AutoId = ddlLicenseActivity.SelectedValue;
            PL.IsActive = ddlIsActive.SelectedValue;
            EssDL.returnTable(PL); 
            if (!PL.isException)
            {
                if (PL.dt.Rows.Count > 0)
                {
                    LV_LicenseActivity.DataSource = PL.dt;
                    LV_LicenseActivity.DataBind();
                    
                    // Find label from ListView
                    Label lblTotalRecords = (Label)LV_LicenseActivity.FindControl("lblTotalRecords");

                    if (lblTotalRecords != null)
                    {
                        lblTotalRecords.Text = PL.dt.Rows.Count.ToString();
                    }
                }
                else
                {
                    LV_LicenseActivity.DataSource = PL.dt;
                    LV_LicenseActivity.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void ClearField()
        {
            ddlLicenseActivity.SelectedIndex = -1;
            ddlIsActive.SelectedIndex = -1;

            txtActivityCode.Text = "";
            txtLicenseActivity.Text = ""; 
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
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_LicenseActivity.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 51;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        if (!PL.isException)
                        {
                            FillListView();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Deleted Successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                        }
                    }
                }
            }
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_LicenseActivity.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 50;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtLicenseActivity.Text = dt.Rows[0]["LicenseActivityName"].ToString();
                            txtActivityCode.Text = dt.Rows[0]["ActivityCode"].ToString();
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtActivityCode.Text.Trim() != "" && txtLicenseActivity.Text != "")
            {
                EssPL PL = new EssPL();
                string xml = "<tbl>";
                xml += "<tr>";
                xml += "<LicenseActivity><![CDATA[" + txtLicenseActivity.Text + "]]></LicenseActivity>";
                xml += "<ActivityCode><![CDATA[" + txtActivityCode.Text.Trim() + "]]></ActivityCode>";
                xml += "<IsActive><![CDATA[" + (chkactive.Checked == true ? 1 : 0) + "]]></IsActive>";
                xml += "</tr>";
                xml += "</tbl>";
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 52;
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
                    PL.OpCode = 49;
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
        protected void LV_LicenseActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager pager = (DataPager)LV_LicenseActivity.FindControl("dpPager");

            pager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            FillListView();
        }
    }
}