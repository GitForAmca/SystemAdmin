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
    public partial class BankingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetCompany();
                FillListView();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divAddEdit.Visible = true;
            ddlCompanyAdd.Enabled = true;
            ClearField();
            ViewState["Mode"] = "Add";
        }
        void GetCompany()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 100;
            EssDL.returnTable(PL);
            ddlCompanyFilter.DataSource = PL.dt;
            ddlCompanyFilter.DataValueField = "CompanyId";
            ddlCompanyFilter.DataTextField = "CompanyName";
            ddlCompanyFilter.DataBind();
            ddlCompanyFilter.Items.Insert(0, new ListItem("Choose an item", ""));
            ddlCompanyAdd.DataSource = PL.dt;
            ddlCompanyAdd.DataValueField = "CompanyId";
            ddlCompanyAdd.DataTextField = "CompanyName";
            ddlCompanyAdd.DataBind();
            ddlCompanyAdd.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Bank_Details.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ViewState["Mode"] = "Edit";
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidID.Value = Autoid.ToString();
                        //-----------------
                        setForEdit(Autoid);
                    }
                }
            }
        }
        void ClearField()
        {
            ddlCompanyAdd.SelectedIndex = -1;
            txtAccountName.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtIBAN.Text = string.Empty;
            txtSwiftCode.Text = string.Empty;
            txtBankAddress.Text = string.Empty;
            ddlCurrency.SelectedIndex = -1;
            chckIsActive.Checked = true;
        }
        void setForEdit(int id)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 99;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtAccountName.Text = dt.Rows[0]["AccountName"].ToString();
                txtBankName.Text = dt.Rows[0]["BankName"].ToString();
                txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
                txtIBAN.Text = dt.Rows[0]["IBAN"].ToString();
                txtSwiftCode.Text = dt.Rows[0]["BranchSwiftCode"].ToString();
                txtBankAddress.Text = dt.Rows[0]["BankAddress"].ToString();
                ddlCompanyAdd.SelectedIndex = ddlCompanyAdd.Items.IndexOf(ddlCompanyAdd.Items.FindByValue(PL.dt.Rows[0]["CompanyId"].ToString()));
                ddlCurrency.SelectedIndex = ddlCurrency.Items.IndexOf(ddlCurrency.Items.FindByValue(PL.dt.Rows[0]["Currency"].ToString()));
                chckIsActive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
                ddlCompanyAdd.Enabled = false;
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
        }
        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 99;
            PL.String1 = ddlCompanyFilter.SelectedValue;
            PL.IsActive = ddlIsActive.SelectedValue;
            PL.String2 = ddlIsDefault.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Bank_Details.DataSource = PL.dt;
                LV_Bank_Details.DataBind();
            }
            else
            {
                LV_Bank_Details.DataSource = "";
                LV_Bank_Details.DataBind();
            }
        }
        string AddXml()
        {
            int IsDefault = 0;

            EssPL PL = new EssPL();
            PL.OpCode = 104;
            PL.AutoId = ddlCompanyAdd.SelectedValue;
            EssDL.returnTable(PL);
            if (PL.dt.Rows[0]["CountEntry"].ToString() == "0")
            {
                IsDefault = 1;
            }

            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<CompanyId><![CDATA[" + ddlCompanyAdd.SelectedValue + "]]></CompanyId>";
            xml += "<BankName><![CDATA[" + txtBankName.Text.Trim() + "]]></BankName>";
            xml += "<AccountNo><![CDATA[" + txtAccountNo.Text.Trim() + "]]></AccountNo>";
            xml += "<IBAN><![CDATA[" + txtIBAN.Text.Trim() + "]]></IBAN>";
            xml += "<BranchSwiftCode><![CDATA[" + txtSwiftCode.Text.Trim() + "]]></BranchSwiftCode>";
            xml += "<AccountName><![CDATA[" + txtAccountName.Text.Trim() + "]]></AccountName>";
            xml += "<BankAddress><![CDATA[" + txtBankAddress.Text.Trim() + "]]></BankAddress>";
            xml += "<Currency><![CDATA[" + ddlCurrency.SelectedValue + "]]></Currency>";
            xml += "<IsActive><![CDATA[" + (chckIsActive.Checked) + "]]></IsActive>";
            xml += "<IsDefault><![CDATA[" + IsDefault + "]]></IsDefault>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        string UpdateXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<CompanyId><![CDATA[" + ddlCompanyAdd.SelectedValue + "]]></CompanyId>";
            xml += "<BankName><![CDATA[" + txtBankName.Text.Trim() + "]]></BankName>";
            xml += "<AccountNo><![CDATA[" + txtAccountNo.Text.Trim() + "]]></AccountNo>";
            xml += "<IBAN><![CDATA[" + txtIBAN.Text.Trim() + "]]></IBAN>";
            xml += "<BranchSwiftCode><![CDATA[" + txtSwiftCode.Text.Trim() + "]]></BranchSwiftCode>";
            xml += "<AccountName><![CDATA[" + txtAccountName.Text.Trim() + "]]></AccountName>";
            xml += "<BankAddress><![CDATA[" + txtBankAddress.Text.Trim() + "]]></BankAddress>";
            xml += "<Currency><![CDATA[" + ddlCurrency.SelectedValue + "]]></Currency>";
            xml += "<IsActive><![CDATA[" + (chckIsActive.Checked) + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 101;
                PL.XML = AddXml();
                PL.CreatedBy = Session["UserAutoId"].ToString();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
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
                PL.OpCode = 102;
                PL.AutoId = Convert.ToInt32(hidID.Value);
                PL.XML = UpdateXml();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            ClearField();
            FillListView();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void btn_default_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;

            EssPL PL = new EssPL();
            PL.AutoId = Convert.ToInt32(((HiddenField)item.FindControl("hdnId")).Value);
            PL.String1 = Convert.ToInt32(((HiddenField)item.FindControl("hdnCompanyId")).Value);
            PL.OpCode = 103;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowDone('Record(s) updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
    }
}