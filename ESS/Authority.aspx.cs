using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SystemAdmin.App_Code;
using SystemAdmin.GroupStructure;

namespace SystemAdmin.ESS
{
    public partial class Authority : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAuthourity(ddlAuthorityFilter);
                GetLicenseActivites(lstLicenseTypeFilter);
                GetSectors(lstSectorFilter); 
                GetIndustries(lstIndustryFilter);
                GetActivies(lstActivityFilter);
                GetLicenseType(lstLicenseType);
                FillListView();
            }
        }
        void FillListView()
        { 
            EssPL PL = new EssPL();
            PL.OpCode = 133;
            PL.AutoId = ddlAuthorityFilter.SelectedValue;
            if (lstLicenseTypeFilter.SelectedValue != "")
            {
                PL.String1 = Request.Form[lstLicenseTypeFilter.UniqueID].ToString();
            }
            if (lstSectorFilter.SelectedValue != "")
            {
                PL.String2 = Request.Form[lstSectorFilter.UniqueID].ToString();
            }
            if (lstIndustryFilter.SelectedValue != "")
            {
                PL.String3 = Request.Form[lstIndustryFilter.UniqueID].ToString();
            }
            if (lstActivityFilter.SelectedValue != "")
            {
                PL.String4 = Request.Form[lstActivityFilter.UniqueID].ToString();
            }
            PL.IsActive = ddlIsActive.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt; 
            if(!PL.isException)
            { 
                if (PL.dt.Rows.Count > 0)
                {
                    LV_Authority.DataSource = PL.dt;
                    LV_Authority.DataBind();
                }
                else
                {
                    LV_Authority.DataSource = "";
                    LV_Authority.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void GetAuthourity(DropDownList ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 141;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Authority";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetLicenseActivites(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 134;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "LicenseType";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetSectors(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 127;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Sector";
            ddl.DataBind();
        }
        void GetIndustries(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 122;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "IndustryName";
            ddl.DataBind();
        }
        void GetActivies(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 116;
            PL.String1 = lstActivity.SelectedValue;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "LicenseActivityName";
            ddl.DataBind();
        }
        void GetIndustries(ListBox ddl, String SectorId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 128;
            PL.String1 = SectorId;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "IndustryId";
            ddl.DataTextField = "IndustryName";
            ddl.DataBind();
        }
        void GetSectors(ListBox ddl , string LicenseTypeId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 137;
            PL.String1 = LicenseTypeId;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Sector";
            ddl.DataBind();
        }
        void GetLicenseType(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 134;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "LicenseType";
            ddl.DataBind();
        }
        void GetActivies(ListBox ddl , String IndustryId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 121;
            PL.Industry = IndustryId;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ActivityId";
            ddl.DataTextField = "LicenseActivityName";
            ddl.DataBind();
        }
        void ClearField()
        { 
            ddlAuthorityFilter.SelectedIndex = -1;
            lstLicenseTypeFilter.SelectedIndex = -1;
            lstSectorFilter.SelectedIndex = -1;
            lstIndustryFilter.SelectedIndex = -1;
            lstActivityFilter.SelectedIndex = -1;
            ddlIsActive.SelectedIndex = -1;


            lstLicenseType.SelectedIndex = -1;
            lstSector.SelectedIndex = -1;
            lstIndustry.SelectedIndex = -1;
            lstActivity.SelectedIndex = -1;
            txtAuthority.Text = ""; 
            chkactive.Checked = true;
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
            foreach (ListViewItem item in LV_Authority.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 142;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtAuthority.Text = dt.Rows[0]["Authority"].ToString();

                            GetLicenseActivites(lstLicenseType);
                            SetList(lstLicenseType, PL.dt.Rows[0]["LicenseTypeIds"].ToString());
                             
                            GetSectors(lstSector, PL.dt.Rows[0]["LicenseTypeIds"].ToString());
                            SetList(lstSector, PL.dt.Rows[0]["SectorIds"].ToString());

                            GetIndustries(lstIndustry, PL.dt.Rows[0]["SectorIds"].ToString());
                            SetList(lstIndustry, PL.dt.Rows[0]["IndustryIds"].ToString());

                            GetActivies(lstActivity, PL.dt.Rows[0]["IndustryIds"].ToString());
                            SetList(lstActivity, PL.dt.Rows[0]["ActivityIds"].ToString());

                            chkactive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();

            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<Authority><![CDATA[" + txtAuthority.Text + "]]></Authority>";
            xml += "<LicenseTypeIds><![CDATA[" + Request.Form[lstLicenseType.UniqueID].ToString() + "]]></LicenseTypeIds>";
            xml += "<SectorIds><![CDATA[" + Request.Form[lstSector.UniqueID].ToString() + "]]></SectorIds>";
            xml += "<IndustryIds><![CDATA[" + Request.Form[lstIndustry.UniqueID].ToString() + "]]></IndustryIds>";
            xml += "<ActivityIds><![CDATA[" + Request.Form[lstActivity.UniqueID].ToString() + "]]></ActivityIds>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked == true ? 1 : 0) + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 135;
                PL.XML = xml;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                EssDL.returnTable(PL);
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
                PL.OpCode = 136;
                PL.XML = xml;
                PL.AutoId = Convert.ToInt32(hidID.Value);
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
        }

        protected void lstLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLicenseType.SelectedValue != "")
            {
                GetSectors(lstSector, Request.Form[lstLicenseType.UniqueID]);
            }
            else
            {
                lstLicenseType.Items.Clear();
                GetLicenseType(lstLicenseType);
            }

        }
        protected void lstSector_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (lstSector.SelectedValue != "")
            {
                GetIndustries(lstIndustry, Request.Form[lstSector.UniqueID]);
            }
            else
            {
                lstSector.Items.Clear();
                GetActivies(lstActivity, Request.Form[lstIndustry.UniqueID]);
            }
        }
        protected void lstIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstIndustry.SelectedValue != "")
            {
                GetActivies(lstActivity, Request.Form[lstIndustry.UniqueID]);
            }
            else
            {
                lstIndustry.Items.Clear();
                GetIndustries(lstIndustry, Request.Form[lstSector.UniqueID]);
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearField();
            FillListView();
        }
    }
}