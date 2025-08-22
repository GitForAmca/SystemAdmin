using AMCAPropertiesAdmin.App_Code;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class Elements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ElementsList(ddlElementsFilter);  
                FillListView();
            }
        }
        void ElementsList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 17;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "ElementName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        } 
        void ClearField()
        {
            txtElements.Text = ""; 
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 25;   
            PL.ElementsId = ddlElementsFilter.SelectedValue;   
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Elements.DataSource = PL.dt;
                LV_Elements.DataBind();
            }
            else
            {
                LV_Elements.DataSource = "";
                LV_Elements.DataBind();
            }
        }
        void FillActionListView(int id)
        {
            CommunityPL PL = new CommunityPL();
            PL.OpCode = 16;
            PL.AutoId = id;
            CommunityDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ViewState["actiondt"] = PL.dt; 
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
            foreach (ListViewItem item in LV_Elements.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidElementsId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidElementsId.Value));
                    FillActionListView(Convert.ToInt32(hidElementsId.Value));
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        } 
        void getData(int id)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 17;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtElements.Text = PL.dt.Rows[0]["ElementName"].ToString(); 
            }
        } 
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
            Response.Redirect(Request.RawUrl);
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlElementsFilter.SelectedIndex = 0; 
            FillListView();
        }   
        protected void btnAdd_Click(object sender, EventArgs e)
        { 

            CommunityPL PL = new CommunityPL();

            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<ElementName><![CDATA[" + txtElements.Text + "]]></ElementName>"; 
            xml += "</tr>";
            xml += "</tbl>";

            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 14;
                CommunityDL.returnTable(PL); 
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 15;
                PL.AutoId = hidElementsId.Value;
                CommunityDL.returnTable(PL); 
            }
            if (!PL.isException)
            { 
                divView.Visible = true;
                divEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Save Successfully');", true);
            }

            else
            {
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        } 
    }
}