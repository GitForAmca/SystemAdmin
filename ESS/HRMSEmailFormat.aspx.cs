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
    public partial class HRMSEmailFormat : System.Web.UI.Page
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
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 44;
            PL.Type = ddlTypeSearch.SelectedValue;
            PL.Category = chkactive.Checked ? 1 : 0;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        
        void ClearField()
        {
            txtFunctionName.Text = "";
            txtname.Text = "";
            txtDescription.Text = "";
            ckObjectives.InnerText = "";
            txtFunctionName.Attributes.Add("oldname", "");
            ddlType.SelectedIndex = -1;
            chkactive.Checked = true;
            hidID.Value = "";
            txtSubject.Text = "";
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
                        setForEdit( Autoid);
                    }
                }
            }
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Edit";
             
        }
        void setForEdit( int id)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 45;
            PL.AutoId = id;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtFunctionName.Text = dt.Rows[0]["Name"].ToString();
                txtname.Text = dt.Rows[0]["EmailName"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString(); 
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(dt.Rows[0]["Type"].ToString()));
                ckObjectives.InnerText = dt.Rows[0]["Body"].ToString();
                chkactive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
                txtSubject.Text = dt.Rows[0]["Subject"].ToString();
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        string GetParentServiceXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<Name><![CDATA[" + txtFunctionName.Text.Trim() + "]]></Name>";
            xml += "<EmailName><![CDATA[" + txtname.Text.Trim() + "]]></EmailName>";
            xml += "<Description><![CDATA[" + txtDescription.Text.Trim() + "]]></Description>";
            xml += "<Type><![CDATA[" + ddlType.SelectedValue + "]]></Type>";
            xml += "<Subject><![CDATA[" + txtSubject.Text.Trim() + "]]></Subject>";
            xml += "<Content><![CDATA[" + ckObjectives.InnerText + "]]></Content>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
       
        protected void btnsave_Click(object sender, EventArgs e)
        { 
                ServiceMasterPL PL = new ServiceMasterPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 41;
                    PL.XML = GetParentServiceXml(); 
                    PL.CreatedBy = Session["UserAutoId"].ToString(); 
                     
                    ServiceMasterDL.returnTable(PL);
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
                    PL.OpCode = 42;
                    PL.XML = GetParentServiceXml();
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
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

        [System.Web.Services.WebMethod]
        public static string CheckName(string text, string oldname)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 43;
            PL.Type = text;
            PL.OldName = oldname;
            ServiceMasterDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
        
        protected void ddlGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
        void insertEmailConentXml(string mainEmailId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
             
                    string xml = "<tbl>";
                    xml += setEmailContentXML(mainEmailId, "");
                    xml += "</tbl>";
                    PL.OpCode = 23;
                    PL.XML1 = xml;
                    ServiceMasterDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Email Format Add successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
                    }
                 
        }
        void updateEmailConentXml(string mainEmailId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            string xml = "<tr>";
            xml += "<mainEmailId><![CDATA[" + mainEmailId + "]]></mainEmailId>";
            xml += "<Content><![CDATA[" + ckObjectives.InnerText + "]]></Content>";
            xml += "<Description><![CDATA[" + txtDescription.Text + "]]></Description>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "</tr>";

            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.AutoId = mainEmailId; 
            PL.OpCode = 26;
            PL.XML1 = xml;
            ServiceMasterDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Email Format Add successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        string setEmailContentXML(string mainid, string GroupId)
        {
            string xml = "<tr>";
            xml += "<mainEmailId><![CDATA[" + mainid + "]]></mainEmailId>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<Content><![CDATA[" + ckObjectives.InnerText + "]]></Content>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "</tr>";
            return xml;
        }

        protected void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void ddlIsActiveSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
    }
 
}