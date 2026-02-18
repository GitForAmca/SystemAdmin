using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.Products
{
    public partial class ProductsAceess : System.Web.UI.Page
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
                FillEmployeeFilter(ddlEmpNameFilter);
            }
        }
        void FillListView()
        {
            //-----------------
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 4;
            PL.String1 = ddlEmpNameFilter.SelectedValue;
            ProductsDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        private void BindCheckBoxList()
        {
            chkProductList.DataSource = GetProduct();
            chkProductList.DataBind();
        }
        public DataTable GetProduct()
        {
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 1;
            ProductsDL.returnTable(PL);
            DataTable dt = PL.dt;
            return PL.dt;
        }
        void ClearField()
        {

        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            BindCheckBoxList();
            FillEmployee(ddlEmpName);
            divView.Visible = false;
            divAddEdit.Visible = true;
            divEmpDDL.Visible = true;
            divEmptxt.Visible = false;
            ViewState["Mode"] = "Add";
        }
        void FillEmployeeFilter(DropDownList ddl)
        {
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 4;
            ProductsDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "EmpName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void FillEmployee(DropDownList ddl)
        {
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 5;
            ProductsDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "EmpName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
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
            divEmpDDL.Visible = false;
            divEmptxt.Visible = true;
            ViewState["Mode"] = "Edit";
        }
        void setForEdit(int id)
        {
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 7;
            PL.AutoId = id;
            ProductsDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
                BindCheckBoxList();

                lblEmpName.Text = dt.Rows[0]["EmpName"].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    foreach(ListItem gr in chkProductList.Items)
                    {
                        if (dr["ProductId"].ToString() == gr.Value)
                        {
                            gr.Selected = true;
                        }
                    }
                }
            }
        }
        private bool IsAnyItemChecked()
        {
            foreach (ListItem item in chkProductList.Items)
            {
                if (item.Selected)
                {
                    return true;
                }
            }
            return false;
        }
        string GetXmlData()
        {
            string EmpId = "";
            if (ViewState["Mode"].ToString() == "Add")
            {
                EmpId = ddlEmpName.SelectedValue;
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                EmpId = hidID.Value;
            }
            string XML = "<tbl>";
            foreach (ListItem gr in chkProductList.Items)
            {
                if (gr.Selected)
                {
                    XML += "<tr>";
                    XML += "<EmpId>" + EmpId + "</EmpId>";
                    XML += "<ProductId>" + gr.Value + "</ProductId>";
                    XML += "</tr>";
                }
            }
            XML += "</tbl>";
            return XML;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked())
            {
                ProductsPL PL = new ProductsPL();
                PL.String1 = Session["UserAutoId"].ToString();
                PL.XML = GetXmlData();
                PL.AutoId = hidID.Value;
                PL.OpCode = 6;
                ProductsDL.returnTable(PL);
                if (!PL.isException)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Save successfully.');", true);
                    divAddEdit.Visible = false;
                    divView.Visible = true;
                    FillListView();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
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
        [WebMethod]
        public static string GetProductList(string EmpId)
        {
            string data = "";
            ProductsPL PL = new ProductsPL();
            PL.OpCode = 7;
            PL.AutoId = EmpId;
            ProductsDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                data = new clsGeneral().JSONfromDT(PL.dt);
            }
            return data;
        }
    }
}