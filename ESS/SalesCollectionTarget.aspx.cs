using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class SalesCollectionTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup();
                BindCECCollectionTeam(ddlSalesCollectionCECTeam);
                BindBDCollectionTeam(ddlSalesCollectionBDTeam);
                getEmployee(ddlCECIndivdual);
                getParentTeamName(ddlParentTeamName);
                getTeamName(ddlTeamName);
                FillListSearch();
            }
        }
        void getEmployee(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 54;
            DropdownDL.returnTable(PL);
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "Name";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void getTeamName(DropDownList dll)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 55;
            DropdownDL.returnTable(PL);
            dll.DataValueField = "AutoId";
            dll.DataTextField = "TeamName";
            dll.DataSource = PL.dt;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void getParentTeamName(DropDownList dll)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 56;
            DropdownDL.returnTable(PL);
            dll.DataValueField = "Autoid";
            dll.DataTextField = "PTeamName";
            dll.DataSource = PL.dt;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void BindCECCollectionTeam(DropDownList dll)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 54;
            DropdownDL.returnTable(PL);
            dll.DataValueField = "Autoid";
            dll.DataTextField = "Name";
            dll.DataSource = PL.dt;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void BindBDCollectionTeam(DropDownList dll)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 57;
            DropdownDL.returnTable(PL);
            dll.DataValueField = "Autoid";
            dll.DataTextField = "Name";
            dll.DataSource = PL.dt;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem("Select Option", ""));

            ddlBDIndividual.DataValueField = "Autoid";
            ddlBDIndividual.DataTextField = "Name";
            ddlBDIndividual.DataSource = PL.dt;
            ddlBDIndividual.DataBind();
            ddlBDIndividual.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void BindddlBDIndividualTbl(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 57;
            DropdownDL.returnTable(PL);
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "Name";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void FillGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 58;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
        }
        private void BindCheckBoxList()
        {
            chkGroupCompany.DataSource = GetGroup();
            chkGroupCompany.DataBind();
        }
        protected void chkGroupCompany_DataBinding(object sender, EventArgs e)
        {
            ((CheckBoxList)sender).DataSource = GetGroup();
        }
        public DataTable GetGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            return PL.dt;
        }
        void getDDLGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "Autoid";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
            ddlUpdateGroupCompany.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListSearch();
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";
            btnAdd.Text = "Add";
            divIndivdual.Visible = false;
            divTeam.Visible = false;
            divAddGroup.Visible = true;
            divUpdateGroup.Visible = false;
            ddlUpdateGroupCompany.Enabled = true;
            BindCheckBoxList();
            ClearField();
            ddlTeamName.Items.Clear();
            ddlCECIndivdual.Items.Clear();
            ddlSalesCollectionCECTeam.Items.Clear();
            ddlSalesCollectionBDTeam.Items.Clear();
        }
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_List_Sales_Target.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        EssPL PL = new EssPL();
                        PL.OpCode = 36;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        if (!PL.isException)
                        {
                            FillListSearch();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Record(s) Deleted successfully');", true);
                            divView.Visible = true;
                            divAddEdit.Visible = false;
                        }
                    }
                }
            }
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_List_Sales_Target.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidId.Value = Autoid.ToString();
                        EssPL PL = new EssPL();
                        PL.OpCode = 37;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        if (PL.dt.Rows.Count > 0)
                        {
                            getDDLGroup();
                            txtPeriodFrom.Text = dt.Rows[0]["PeriodFrom"].ToString();
                            txtPeriodTo.Text = dt.Rows[0]["PeriodTo"].ToString();
                            ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(PL.dt.Rows[0]["TargetType"].ToString()));
                            if (PL.dt.Rows[0]["TargetType"].ToString() == "Individual")
                            {
                                ddlCECIndivdual.SelectedIndex = ddlCECIndivdual.Items.IndexOf(ddlCECIndivdual.Items.FindByValue(PL.dt.Rows[0]["CECIndId"].ToString()));
                                ddlBDIndividual.SelectedIndex = ddlBDIndividual.Items.IndexOf(ddlBDIndividual.Items.FindByValue(PL.dt.Rows[0]["BDIndId"].ToString()));
                                divIndivdual.Visible = true;
                                divTeam.Visible = false;
                            }
                            if (PL.dt.Rows[0]["TargetType"].ToString() == "Team")
                            {
                                ddlParentTeamName.SelectedIndex = ddlParentTeamName.Items.IndexOf(ddlParentTeamName.Items.FindByValue(PL.dt.Rows[0]["ParentTeam"].ToString()));
                                ddlTeamName.SelectedIndex = ddlTeamName.Items.IndexOf(ddlTeamName.Items.FindByValue(PL.dt.Rows[0]["Team"].ToString()));
                                ddlSalesCollectionCECTeam.SelectedIndex = ddlTeamName.Items.IndexOf(ddlTeamName.Items.FindByValue(PL.dt.Rows[0]["CECTeam"].ToString()));
                                ddlSalesCollectionBDTeam.SelectedIndex = ddlTeamName.Items.IndexOf(ddlTeamName.Items.FindByValue(PL.dt.Rows[0]["BDTeam"].ToString()));
                                //SetMultiple(ddlSalesCollectionCECTeam, PL.dt.Rows[0]["CECTeam"].ToString());
                                //SetMultiple(ddlSalesCollectionBDTeam, PL.dt.Rows[0]["BDTeam"].ToString());
                                divIndivdual.Visible = false;
                                divTeam.Visible = true;
                            }
                            txtTargetAmount.Text = dt.Rows[0]["TargetAmount"].ToString();
                            txtLead.Text = dt.Rows[0]["TargetLead"].ToString();
                            txtTargetEL.Text = dt.Rows[0]["TargetEL"].ToString();
                            txtTargetClient.Text = dt.Rows[0]["TargetClient"].ToString();
                            txtTargetConsultant.Text = dt.Rows[0]["TargetConsultant"].ToString();
                            txtTargetAssignment.Text = dt.Rows[0]["TargetAssignment"].ToString();
                            divView.Visible = false;
                            divAddEdit.Visible = true;
                            ViewState["Mode"] = "Edit";
                            btnAdd.Text = "Update";
                        }
                    }
                }
            }
        }

        protected void UpdateRecord_Click(object sender, EventArgs e)
        { 
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;
            int AutoId = int.Parse(((HiddenField)item.FindControl("hidautoidUpdate")).Value); 

            string xml = "<tbl><tr>";
            xml += "<CECIndName><![CDATA[" + ((DropDownList)item.FindControl("ddl_CECIndNameTbl")).SelectedValue.Trim() + "]]></CECIndName>";
            xml += "<BDIndName><![CDATA[" + ((DropDownList)item.FindControl("ddl_BDIndNameTbl")).SelectedValue.Trim() + "]]></BDIndName>";
            xml += "<ParentTeam><![CDATA[" + ((DropDownList)item.FindControl("ddl_ParentTeamNameTbl")).SelectedValue.Trim() + "]]></ParentTeam>";
            //xml += "<TeamName><![CDATA[" + ((DropDownList)item.FindControl("ddl_TeamNameTbl")).SelectedValue.Trim() + "]]></TeamName>";
            xml += "<CECCollectionTeam><![CDATA[" + ((DropDownList)item.FindControl("ddl_CECCollectionTbl")).SelectedValue.Trim() + "]]></CECCollectionTeam>";
            xml += "<BDCollectionTeam><![CDATA[" + ((DropDownList)item.FindControl("ddl_BDCollectionTbl")).SelectedValue.Trim() + "]]></BDCollectionTeam>";
            xml += "<Lead><![CDATA[" + ((TextBox)item.FindControl("txt_LeadTbl")).Text.Trim() + "]]></Lead>";
            xml += "<EL><![CDATA[" + ((TextBox)item.FindControl("txt_ElTbl")).Text.Trim() + "]]></EL>";
            xml += "<Client><![CDATA[" + ((TextBox)item.FindControl("txt_ClientTbl")).Text.Trim() + "]]></Client>";
            xml += "<Consultant><![CDATA[" + ((TextBox)item.FindControl("txt_ConsultantTbl")).Text.Trim() + "]]></Consultant>";
            xml += "<Assignment><![CDATA[" + ((TextBox)item.FindControl("txt_AssignmentTbl")).Text.Trim() + "]]></Assignment>";
            xml += "<Amount><![CDATA[" + ((TextBox)item.FindControl("txt_AmountTbl")).Text.Trim() + "]]></Amount>";
            xml += "</tr></tbl>"; 

            EssPL PL = new EssPL();
            PL.XML = xml; 
            PL.AutoId = AutoId;
            PL.OpCode = 44;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Record(s) Update Record successfully');", true);
            }

        }
        void SetMultiple(ListBox ddl, string ids)
        {
            ddl.SelectedIndex = -1;
            foreach (var item in ids.Split(','))
            {
                foreach (ListItem item2 in ddl.Items)
                {
                    if (item2.Value == item)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }
        }
        void ClearField()
        {
            txtPeriodFrom.Text = string.Empty;
            txtPeriodTo.Text = string.Empty;
            ddlType.SelectedIndex = -1;
            ddlTeamName.SelectedIndex = -1;
            ddlCECIndivdual.SelectedIndex = -1;
            ddlSalesCollectionCECTeam.SelectedIndex = -1;
            ddlSalesCollectionBDTeam.SelectedIndex = -1;
            txtTargetAmount.Text = "0";
            txtLead.Text = "0";
            txtTargetEL.Text = "0";
            txtTargetClient.Text = "0";
            txtTargetConsultant.Text = "0";
            txtTargetAssignment.Text = "0";
        }
        string XMLField(string GroupId)
        {
            string xml = "<tr>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<TargetType><![CDATA[" + ddlType.SelectedValue + "]]></TargetType>";
            xml += "<CECIndId><![CDATA[" + ddlCECIndivdual.SelectedValue + "]]></CECIndId>";
            xml += "<BDIndId><![CDATA[" + ddlBDIndividual.SelectedValue + "]]></BDIndId>";
            xml += "<ParentTeam><![CDATA[" + ddlParentTeamName.SelectedValue + "]]></ParentTeam>";
            xml += "<Team><![CDATA[" + ddlTeamName.SelectedValue + "]]></Team>";
            xml += "<CECTeam><![CDATA[" + ddlSalesCollectionCECTeam.SelectedValue + "]]></CECTeam>";
            xml += "<BDTeam><![CDATA[" + ddlSalesCollectionBDTeam.SelectedValue + "]]></BDTeam>";
            xml += "<TargetAmount><![CDATA[" + txtTargetAmount.Text.Trim() + "]]></TargetAmount>";
            xml += "<TargetLead><![CDATA[" + txtLead.Text.Trim() + "]]></TargetLead>";
            xml += "<TargetEL><![CDATA[" + txtTargetEL.Text.Trim() + "]]></TargetEL>";
            xml += "<TargetClient><![CDATA[" + txtTargetClient.Text.Trim() + "]]></TargetClient>";
            xml += "<TargetConsultant><![CDATA[" + txtTargetConsultant.Text.Trim() + "]]></TargetConsultant>";
            xml += "<TargetAssignment><![CDATA[" + txtTargetAssignment.Text.Trim() + "]]></TargetAssignment>";
            xml += "</tr>";
            return xml;
        }
        string GetChildStatusXml()
        {
            string xml = "<tbl>";
            foreach (ListItem gr in chkGroupCompany.Items)
            {
                if (gr.Selected)
                {
                    xml += XMLField(gr.Value);
                }
            }
            xml += "</tbl>";
            return xml;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                if (ddlType.SelectedValue == "Individual")
                {
                    if (ddlCECIndivdual.SelectedValue != "" && ddlBDIndividual.SelectedValue != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Kindly select either BD or CEC only.');", true);
                    }
                    else if (ddlCECIndivdual.SelectedValue == "" && ddlBDIndividual.SelectedValue == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Kindly select atleast one');", true);
                    }
                    else
                    {
                        AddData();
                    }
                }
                else
                {
                    AddData();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Kindly select Company Name.');", true);
            }
        }
        public void AddData()
        {
            EssPL PL = new EssPL();
            PL.XML = GetChildStatusXml();
            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.FromDate = "01-" + txtPeriodFrom.Text;
            PL.ToDate = "01-" + txtPeriodTo.Text;
            PL.OpCode = 38;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Record(s) add successfully');", true);
            }
        } 
        void FillListSearch()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 37;
            PL.Type = ddlSearchType.SelectedValue;
            PL.GroupId = ddlGroupFilter.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_List_Sales_Target.DataSource = PL.dt;
                LV_List_Sales_Target.DataBind();
            }
            else
            {
                LV_List_Sales_Target.DataSource = "";
                LV_List_Sales_Target.DataBind();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue != "")
            {
                if (ddlType.SelectedValue == "Individual")
                {
                    divIndivdual.Visible = true;
                    divTeam.Visible = false;
                }
                if (ddlType.SelectedValue == "Team")
                {
                    divIndivdual.Visible = false;
                    divTeam.Visible = true;
                }
                getTeamName(ddlTeamName);
                BindCECCollectionTeam(ddlSalesCollectionCECTeam);
                BindBDCollectionTeam(ddlSalesCollectionBDTeam);
                getEmployee(ddlCECIndivdual);
            }
        }
        protected void lvSales_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //DropDownList ddlType = (DropDownList)e.Item.FindControl("ddl_SearchTypeTbl");
            //ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(ddlType.Attributes["oldvalue"]));

            DropDownList ddlCECTeamName = (DropDownList)e.Item.FindControl("ddl_CECIndNameTbl");
            getEmployee(ddlCECTeamName);
            ddlCECTeamName.SelectedIndex = ddlCECTeamName.Items.IndexOf(ddlCECTeamName.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnCECIndName")).Value));

            DropDownList ddlBDIndNamee = (DropDownList)e.Item.FindControl("ddl_BDIndNameTbl");
            BindddlBDIndividualTbl(ddlBDIndNamee);
            ddlBDIndNamee.SelectedIndex = ddlBDIndNamee.Items.IndexOf(ddlBDIndNamee.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnBDIndName")).Value));

            DropDownList ddlParentTeamName = (DropDownList)e.Item.FindControl("ddl_ParentTeamNameTbl");
            getParentTeamName(ddlParentTeamName);
            ddlParentTeamName.SelectedIndex = ddlParentTeamName.Items.IndexOf(ddlParentTeamName.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnParenTeam")).Value));             

            //DropDownList ddlTeamName = (DropDownList)e.Item.FindControl("ddl_TeamNameTbl");
            //getTeamName(ddlTeamName);
            //ddlTeamName.SelectedIndex = ddlTeamName.Items.IndexOf(ddlTeamName.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnTeamName")).Value));

            DropDownList ddlCECCollection = (DropDownList)e.Item.FindControl("ddl_CECCollectionTbl");
            BindCECCollectionTeam(ddlCECCollection);
            ddlCECCollection.SelectedIndex = ddlCECCollection.Items.IndexOf(ddlCECCollection.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnCECCollectionTeam")).Value));

            DropDownList ddlBDCollection = (DropDownList)e.Item.FindControl("ddl_BDCollectionTbl");
            BindBDCollectionTeam(ddlBDCollection);
            ddlBDCollection.SelectedIndex = ddlBDCollection.Items.IndexOf(ddlBDCollection.Items.FindByValue(((HiddenField)e.Item.FindControl("hdnBDCollectionTeam")).Value));

            Panel pnlCECIndividual = (Panel)e.Item.FindControl("pnlCECIndividual");
            Panel pnlBDIndividual = (Panel)e.Item.FindControl("pnlBDIndividual");
            Panel pnlTeam = (Panel)e.Item.FindControl("pnlTeam");
            if (ddlSearchType.SelectedValue == "Individual")
            {
                pnlCECIndividual.Visible = true;
                pnlBDIndividual.Visible = true;
                pnlTeam.Visible = false;
                LV_List_Sales_Target.FindControl("thCECIndividual").Visible = true;
                LV_List_Sales_Target.FindControl("thBDIndividual").Visible = true;
                LV_List_Sales_Target.FindControl("thParentTeamName").Visible = false;
                LV_List_Sales_Target.FindControl("thTeamName").Visible = false;
                LV_List_Sales_Target.FindControl("thCECName").Visible = false;
                LV_List_Sales_Target.FindControl("thBDName").Visible = false;
            }
            if (ddlSearchType.SelectedValue == "Team")
            {
                pnlCECIndividual.Visible = false;
                pnlBDIndividual.Visible = false;
                pnlTeam.Visible = true;
                LV_List_Sales_Target.FindControl("thCECIndividual").Visible = false;
                LV_List_Sales_Target.FindControl("thBDIndividual").Visible = false;
                LV_List_Sales_Target.FindControl("thParentTeamName").Visible = true;
                LV_List_Sales_Target.FindControl("thTeamName").Visible = true;
                LV_List_Sales_Target.FindControl("thCECName").Visible = true;
                LV_List_Sales_Target.FindControl("thBDName").Visible = true;
            }
        }
        [System.Web.Services.WebMethod]
        public static string CheckName(string value, string oldname, string periodfrom, string periodto)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 39;
            PL.EmpId = Convert.ToInt32(value);
            PL.OldName = oldname;
            PL.FromDate = "01-" + periodfrom;
            PL.ToDate = "01-" + periodto;
            EssDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
        private bool IsAnyItemChecked()
        {
            foreach (ListItem item in chkGroupCompany.Items)
            {
                if (item.Selected)
                {
                    return true;
                }
            }
            return false;
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
    }
}