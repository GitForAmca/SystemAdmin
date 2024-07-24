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
    public partial class ELFormat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GroupCompany();
            }
        }
        public void BindServiceCategory()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 46;
            PL.AutoId = ddlUpdateGroupCompany.SelectedValue;
            DropdownDL.returnTable(PL);
            ddlServiceTypeName.DataSource = PL.dt;
            ddlServiceTypeName.DataValueField = "Autoid";
            ddlServiceTypeName.DataTextField = "ServiceTypeName";
            ddlServiceTypeName.DataBind();
            ddlServiceTypeName.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        public void GroupCompany()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 45;
            DropdownDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "GroupId";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
            ddlUpdateGroupCompany.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void ddlUpdateGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlUpdateGroupCompany.SelectedValue != "")
            {
                BindServiceCategory();
            }
        }
        protected void ddlServiceTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 47;
            PL.AutoId = ddlUpdateGroupCompany.SelectedValue;
            PL.ServiceTypeAutoid = ddlServiceTypeName.SelectedValue;
            DropdownDL.returnTable(PL);
            ddlAssignment.DataSource = PL.dt;
            ddlAssignment.DataValueField = "Autoid";
            ddlAssignment.DataTextField = "ServiceName";
            ddlAssignment.DataBind();
            ddlAssignment.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 24;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ckObjectives.InnerText = dt.Rows[0]["Objectives"].ToString();
                ckScopeofWork.InnerText = dt.Rows[0]["ScopeOfWork"].ToString();
                ckFirsPartR.InnerText = dt.Rows[0]["FirstPartyResponsibility"].ToString();
                ckSecondPartR.InnerText = dt.Rows[0]["SecondPartyResponsibility"].ToString();
                ckLimitations.InnerText = dt.Rows[0]["Limitations"].ToString();
                ckReport.InnerText = dt.Rows[0]["Report"].ToString();
                ckOtherMatters.InnerText = dt.Rows[0]["OtherMatters"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveObjectives_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 25;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckObjectives.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Objectives update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveScopeofWork_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 26;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckScopeofWork.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Scope of Work update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveFirsPartR_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 27;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckFirsPartR.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('First Party Responsibility update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveSecond_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 28;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckSecondPartR.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Second Party Responsibility update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveLimitations_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 29;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckLimitations.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Limitations update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveReport_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 30;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckReport.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Report update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnsaveOtherMatters_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 31;
            PL.AutoId = ddlAssignment.SelectedValue;
            PL.GroupId = ddlUpdateGroupCompany.SelectedValue;
            PL.Remarks = ckOtherMatters.InnerText;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Other Matters update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
    }
}