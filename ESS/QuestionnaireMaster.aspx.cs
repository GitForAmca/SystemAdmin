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
    public partial class QuestionnaireMaster : System.Web.UI.Page
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
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 16;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        void ClearField()
        {
            txtDocumentName.Text = "";
            txtDescription.Text = "";
            txtDocumentName.Attributes.Add("oldname", "");
            ddlDocumentType.SelectedIndex = 0;
            ddlSystemType.SelectedIndex = 0;
            ddlQuestionnaire.SelectedIndex = 0;
            chkactive.Checked = true;
            chkDefault.Checked = false;
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
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        ServiceMasterPL PL = new ServiceMasterPL();
                        PL.OpCode = 16;
                        PL.AutoId = Autoid;
                        ServiceMasterDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtDocumentName.Text = dt.Rows[0]["Questions"].ToString();
                            txtDocumentName.Attributes.Add("oldname", dt.Rows[0]["Questions"].ToString());
                            txtDescription.Text = dt.Rows[0]["Description"].ToString();
                            ddlDocumentType.SelectedIndex = ddlDocumentType.Items.IndexOf(ddlDocumentType.Items.FindByValue(dt.Rows[0]["Type"].ToString()));
                            ddlSystemType.SelectedIndex = ddlSystemType.Items.IndexOf(ddlSystemType.Items.FindByValue(dt.Rows[0]["SystemType"].ToString()));
                            ddlQuestionnaire.SelectedIndex = ddlQuestionnaire.Items.IndexOf(ddlQuestionnaire.Items.FindByValue(dt.Rows[0]["Questionnaire"].ToString()));
                            chkactive.Checked = bool.Parse(dt.Rows[0]["isActive"].ToString());
                            chkDefault.Checked = bool.Parse(dt.Rows[0]["isMandatory"].ToString());
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
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        ServiceMasterPL PL = new ServiceMasterPL();
                        PL.OpCode = 17;
                        PL.AutoId = Autoid;
                        ServiceMasterDL.returnTable(PL);
                        if (!PL.isException)
                        {
                            divView.Visible = true;
                            divAddEdit.Visible = false;
                            ClearField();
                            FillListView();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record delete successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
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
        string XMLField()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<Questions><![CDATA[" + txtDocumentName.Text + "]]></Questions>";
            xml += "<Description><![CDATA[" + txtDescription.Text + "]]></Description>";
            xml += "<Type><![CDATA[" + ddlDocumentType.SelectedValue + "]]></Type>";
            xml += "<SystemType><![CDATA[" + ddlSystemType.SelectedValue + "]]></SystemType>";
            xml += "<Questionnaire><![CDATA[" + ddlQuestionnaire.SelectedValue + "]]></Questionnaire>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "<isMandatory><![CDATA[" + (chkDefault.Checked) + "]]></isMandatory>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtDocumentName.Text.Trim() != "" && Request.Form[ddlDocumentType.UniqueID] != "")
            {
                ServiceMasterPL PL = new ServiceMasterPL();
                PL.XML1 = XMLField();
                PL.CreatedBy = Session["UserAutoId"].ToString();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 18;
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    PL.OpCode = 19;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                }
                ServiceMasterDL.returnTable(PL);
                divView.Visible = true;
                divAddEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Save Successfully');", true);
            }
        }
        [System.Web.Services.WebMethod]
        public static string CheckName(string text, string oldname)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 20;
            PL.Type = text;
            PL.OldName = oldname;
            ServiceMasterDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}