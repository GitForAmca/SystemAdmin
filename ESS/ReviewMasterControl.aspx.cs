using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class ReviewMasterControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup();
                BindServiceCategory();
                BindElement();
                fillList();
            }
        }
        void FillGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 43;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
        }
        public void BindElement()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 41;
            DropdownDL.returnTable(PL);
            ddlElementName.DataSource = PL.dt;
            ddlElementName.DataValueField = "ElementName";
            ddlElementName.DataTextField = "ElementName";
            ddlElementName.DataBind();
            ddlElementName.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        public void BindServiceCategory()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 42;
            DropdownDL.returnTable(PL);
            ddlServiceTypeName.DataSource = PL.dt;
            ddlServiceTypeName.DataValueField = "Autoid";
            ddlServiceTypeName.DataTextField = "ServiceTypeName";
            ddlServiceTypeName.DataBind();
            ddlServiceTypeName.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void fillList()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 16;
            PL.Type = ddlServiceTypeName.SelectedValue;
            PL.OldName = ddlElementName.SelectedValue;
            PL.GroupId = ddlGroupFilter.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Review_Control.DataSource = PL.dt;
                LV_Review_Control.DataBind();
            }
            else
            {
                LV_Review_Control.DataSource = "";
                LV_Review_Control.DataBind();
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            fillList();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlServiceTypeName.SelectedIndex = -1;
            fillList();
        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Review_Control.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidID.Value = chkSelect.Attributes["AutoId"];
                    EssPL PL = new EssPL();
                    PL.OpCode = 17;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    EssDL.returnTable(PL);
                    DataTable dt = PL.dt;
                    if (PL.dt.Rows.Count > 0)
                    {
                        if (PL.dt.Rows[0]["IsReview"].ToString() == "True")
                        {
                            Reviewyes.Checked = true;
                            Reviewno.Checked = false;
                        }
                        else
                        {
                            Reviewyes.Checked = false;
                            Reviewno.Checked = true;
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpUpdateReviewControl();", true);
                    }
                }
            }
        }
        protected void btnUpdateControl_Click(object sender, EventArgs e)
        {
            string reviewc = string.Empty;
            if (Reviewyes.Checked)
            {
                reviewc = "True";
            }
            if (Reviewno.Checked)
            {
                reviewc = "False";
            }
            EssPL PL = new EssPL();
            PL.OpCode = 18;
            PL.AutoId = Convert.ToInt32(hidID.Value);
            PL.IsActive = reviewc;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                fillList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
    }
}