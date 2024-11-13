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
            }
        }
        void FillListView()
        {
            //-----------------
            EssPL PL = new EssPL();
            PL.OpCode = 50;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV_Risk_Questions.DataSource = dt;
            LV_Risk_Questions.DataBind();
        }
        void ClearField()
        {
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
            foreach (ListViewItem item in LV_Risk_Questions.Items)
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
            foreach (ListViewItem item in LV_Risk_Questions.Items)
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
                    PL.OpCode = 48;
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
    }
}