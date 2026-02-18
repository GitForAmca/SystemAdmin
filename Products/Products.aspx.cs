using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;

namespace SystemAdmin.Products
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAutoId"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                FillListView();
            }
        }
        void FillListView()
        {
            //-----------------
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 1;
            ProductsDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        void ClearField()
        {
            txtProductName.Text = string.Empty;
            txtConnectionName.Text = string.Empty;
            ltrProductIcon.Text = string.Empty;
            fileIcons.CssClass = "req";
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
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidID.Value = Autoid.ToString();
                        setForEdit(Autoid);
                    }
                }
            }
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Edit";
        }
        void setForEdit(int id)
        {
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 1;
            PL.AutoId = id;
            ProductsDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtProductName.Text = dt.Rows[0]["ProductsName"].ToString();
                txtConnectionName.Text = dt.Rows[0]["ConnectionName"].ToString();
                fileIcons.Attributes.Add("oldpath", dt.Rows[0]["ProductIcon"].ToString());
                if (dt.Rows[0]["ProductIcon"].ToString() != "")
                {
                    fileIcons.CssClass = "";
                }
                else
                {
                    fileIcons.CssClass = "req";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ProductIcon"].ToString()))
                {
                    ltrProductIcon.Text = "<a target='_blank' class='' href='" + ResolveUrl("~/" + dt.Rows[0]["ProductIcon"].ToString()) + "'>View</a>";
                }
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        string GetXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<ProductsName><![CDATA[" + txtProductName.Text.Trim() + "]]></ProductsName>";
            xml += "<ProductIcon><![CDATA[" + new clsGeneral().FileUploadAll(fileIcons, "UploadedFile/ProductsIcon/") + "]]></ProductIcon>";
            xml += "<ConnectionName><![CDATA[" + txtConnectionName.Text.Trim() + "]]></ConnectionName>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            ProductsPL PL = new ProductsPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 2;
                PL.AutoId = Session["UserAutoId"].ToString();
                PL.XML = GetXml();
                ProductsDL.returnTable(PL);
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
                PL.OpCode = 3;
                PL.XML = GetXml();
                PL.AutoId = Convert.ToInt32(hidID.Value);
                ProductsDL.returnTable(PL);
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
            ClearField();
            FillListView();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}