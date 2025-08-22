using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMCAPropertiesAdmin.App_Code;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class CommunityMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetCommunityList(ddlCommunityFilter);
                EmirateList(ddlEmiratesFilter);
                AreaListFilter(ddlAreaFilter);
                DelevlopersList(ddlDevelopersFilter);
                EmirateList(ddlEmirates);
                PropertyStatusList(ddlStatusFilter);
                DelevlopersList(ddlDevelopers);
                PropertyStatusList(ddlPropertyStatus);
                BedroomTypeList(lstBedroomType);
                UnitTypeList(lstUnitType); 
                FillListView();
            }
        }
        void GetCommunityList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 15;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "CommunityName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void EmirateList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 1;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "EmiratesName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void AreaList(DropDownList ddl , int emiratesId)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 2;
            PL.EmiratesAutoid = emiratesId;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "AreaName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void AreaListFilter(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 16; 
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "AreaName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void DelevlopersList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 12;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "DevelopersId";
            ddl.DataTextField = "DevelopersName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void PropertyStatusList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 13;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Status";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void BedroomTypeList(ListBox ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 9;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Type";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void UnitTypeList(ListBox ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 8;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Type";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            //Filter fields
            ddlCommunityFilter.SelectedIndex = -1;
            ddlDevelopersFilter.SelectedIndex = -1;
            ddlEmiratesFilter.SelectedIndex = -1;
            ddlAreaFilter.SelectedIndex = -1;
            ddlStatusFilter.SelectedIndex = -1;

            // Add Edit Fields
            ddlEmirates.SelectedIndex = -1;
            ddlArea.SelectedIndex = -1;
            ddlDevelopers.SelectedIndex = -1;
            txtCommunityName.Text = "";
            ddlPropertyStatus.SelectedIndex = -1;
            lstBedroomType.SelectedIndex = -1;
            lstUnitType.SelectedIndex = -1;  
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 3; 
            PL.CommunityId = ddlCommunityFilter.SelectedValue; 
            PL.EmiratesAutoid = ddlEmiratesFilter.SelectedValue; 
            PL.AreaId = ddlAreaFilter.SelectedValue; 
            PL.DevelopersId = ddlDevelopersFilter.SelectedValue; 
            PL.StatusId = ddlStatusFilter.SelectedValue; 
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Community.DataSource = PL.dt;
                LV_Community.DataBind();
            }
            else
            {
                LV_Community.DataSource = "";
                LV_Community.DataBind();
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
            DelevlopersList(ddlDevelopers);
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Community.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                { 
                    hidCommunityId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidCommunityId.Value));
                    FillActionListView(Convert.ToInt32(hidCommunityId.Value));
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
            PL.OpCode = 14;
            PL.EmiratesAutoid = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {

                txtCommunityName.Text = PL.dt.Rows[0]["CommunityName"].ToString();
                DelevlopersList(ddlDevelopers);
                ddlDevelopers.SelectedIndex = ddlDevelopers.Items.IndexOf(ddlDevelopers.Items.FindByValue(PL.dt.Rows[0]["Developer"].ToString()));

                EmirateList(ddlEmirates);
                ddlEmirates.SelectedIndex = ddlEmirates.Items.IndexOf(ddlEmirates.Items.FindByValue(PL.dt.Rows[0]["Emiratesid"].ToString()));

                AreaList(ddlArea, Convert.ToInt32(ddlEmirates.SelectedValue));
                ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(PL.dt.Rows[0]["Area"].ToString()));

                PropertyStatusList(ddlPropertyStatus);
                ddlPropertyStatus.SelectedIndex = ddlPropertyStatus.Items.IndexOf(ddlPropertyStatus.Items.FindByValue(PL.dt.Rows[0]["Status"].ToString()));

                chkActive.Checked = Convert.ToBoolean(PL.dt.Rows[0]["IsActive"].ToString());

                BedroomTypeList(lstBedroomType);
                SetList(lstBedroomType, PL.dt.Rows[0]["Bedrooms"].ToString());

                UnitTypeList(lstUnitType);
                SetList(lstUnitType, PL.dt.Rows[0]["UnitType"].ToString());
            }
        }
        void SetList(ListBox ddl, string ids)
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
            ddlCommunityFilter.SelectedIndex = -1;
            ddlEmiratesFilter.SelectedIndex = -1;
            ddlAreaFilter.SelectedIndex = -1;
            ddlDevelopersFilter.SelectedIndex = -1;
            ddlStatusFilter.SelectedIndex = -1;
            FillListView();
        } 
        protected void btnactionadd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCommunityName.Text))
            {
                DataTable dt = ViewState["actiondt"] as DataTable;
                DataRow dr = dt.NewRow();
                dr["ActionName"] = txtCommunityName.Text.Trim();
                dt.Rows.Add(dr);

                ViewState["actiondt"] = dt; 
                txtCommunityName.Text = "";
            }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            Button btndelete = (Button)sender;
            int index = Convert.ToInt32(btndelete.CommandArgument);
            DataTable dt = ViewState["actiondt"] as DataTable;
            if (dt != null && dt.Rows.Count > index)
            {
                dt.Rows[index].Delete();
                dt.AcceptChanges();
                ViewState["actiondt"] = dt; 
            }
        }
        private static string XMLBedroomType(int MainId, int GroupId)
        {
            string XML = "<tr>";
            XML += "<CommunityId><![CDATA[" + MainId + "]]></CommunityId>";
            XML += "<Type><![CDATA[" + GroupId + "]]></Type>";
            XML += "<IsActive><![CDATA[" + 1 + "]]></IsActive>";
            XML += "</tr>";
            return XML;
        } 
        protected void ddlEmirtes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmirates.SelectedValue != "")
            {
                divddlArea.Visible = true;
                AreaList(ddlArea, Convert.ToInt32(ddlEmirates.SelectedValue));
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        { 

            CommunityPL PL = new CommunityPL();

            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<CommunityName><![CDATA[" + txtCommunityName.Text + "]]></CommunityName>";
            xml += "<Developer><![CDATA[" + ddlDevelopers.SelectedValue + "]]></Developer>";
            xml += "<Area><![CDATA[" + ddlArea.SelectedValue + "]]></Area>";
            xml += "<Status><![CDATA[" + ddlPropertyStatus.SelectedValue + "]]></Status>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";

            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 1;
                CommunityDL.returnTable(PL);
                saveBedroomType(Convert.ToInt32(PL.dt.Rows[0]["CommunityId"].ToString()));
                saveCommunityType(Convert.ToInt32(PL.dt.Rows[0]["CommunityId"].ToString())); 
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 4;
                PL.AutoId = hidCommunityId.Value;
                CommunityDL.returnTable(PL);
                editBedroomType(Convert.ToInt32(hidCommunityId.Value));
                editCommunityType(Convert.ToInt32(hidCommunityId.Value));
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
        void saveBedroomType(int communityId)
        {
            string BedroomId = Request.Form[lstBedroomType.UniqueID];
            if (BedroomId != null)
            {
                var query = from val in BedroomId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLBedroomType(communityId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 2;
                PL.XML = XML;
                PL.AutoId = communityId;
                CommunityDL.returnTable(PL);
            }
        }
        void saveCommunityType(int communityId)
        {
            string CommunityTypeId = Request.Form[lstUnitType.UniqueID];
            if (CommunityTypeId != null)
            {
                var query = from val in CommunityTypeId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLBedroomType(communityId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 3;
                PL.XML = XML;
                PL.AutoId = communityId;
                CommunityDL.returnTable(PL);
            }
        }
        void editBedroomType(int communityId)
        {
            string BedroomId = Request.Form[lstBedroomType.UniqueID];
            if (BedroomId != null)
            {
                var query = from val in BedroomId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLBedroomType(communityId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 5;
                PL.XML = XML;
                PL.AutoId = communityId;
                CommunityDL.returnTable(PL);
            }
        }
        void editCommunityType(int communityId)
        {
            string CommunityTypeId = Request.Form[lstUnitType.UniqueID];
            if (CommunityTypeId != null)
            {
                var query = from val in CommunityTypeId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLBedroomType(communityId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 6;
                PL.XML = XML;
                PL.AutoId = communityId;
                CommunityDL.returnTable(PL);
            }
        }
        //protected void ddlEmiratesFilter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlEmiratesFilter.SelectedValue != "")
        //    {
        //        ddlAreaFilter.Visible = true;
        //        AreaList(ddlAreaFilter, Convert.ToInt32(ddlEmiratesFilter.SelectedValue));
        //    }
        //}
    }
}