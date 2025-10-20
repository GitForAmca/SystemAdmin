using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.GroupStructure
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetGroup(ddlGroupFilter);
                GetOrganization(ddlOrganization);
                GetOrganization(ddlOrganizationSearch);
                GetDomain(ddlDomain);
                GetHOD(ddlHOD);
                GetWorkLocations(lstworklocations);
                FillListView();
            }
        }

        void GetHOD(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 24;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetWorkLocations(ListBox llb)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 25;
            StructureDL.returnTable(PL);
            llb.DataSource = PL.dt;
            llb.DataValueField = "Id";
            llb.DataTextField = "Name";
            llb.DataBind(); 
        }
        void GetGroup(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 10;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetOrganization(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 17;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetDomain(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 20;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetRegion(DropDownList ddl , int Autoid)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 18;
            PL.AutoId = Autoid;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void GetStates(DropDownList ddl, string Region)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 19;
            PL.Name = Region;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtCompanyName.Text = "";
            ddlOrganization.SelectedIndex = -1;
            txtUnitNo.Text = "";
            txtTower.Text = "";
            txtArea.Text = "";
            ddlRegion.SelectedIndex = -1;
            ddlStates.SelectedIndex = -1;
            txtTRN.Text= "";
            txtStampURL.Text = "";
            txtGroupAddress.Text = "";
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtTelNo.Text = "";
            ddlDomain.SelectedIndex = -1;
            txtWebsite.Text = "";
            txtLocationURL.Text = "";
        }
        void FillListView()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 16;
            PL.Name = ddlGroupFilter.SelectedValue;
            PL.Description = ddlOrganizationSearch.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Company.DataSource = PL.dt;
                LV_Company.DataBind();
            }
            else
            {
                LV_Company.DataSource = PL.dt;
                LV_Company.DataBind();
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
            foreach (ListViewItem item in LV_Company.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoid.Value = chkSelect.Attributes["Autoid"];
                    StructurePL PL = new StructurePL();
                    PL.OpCode = 23;
                    PL.AutoId = hidAutoid.Value;
                    StructureDL.returnTable(PL);
                    DataTable dt = PL.dt;
                    if (PL.dt.Rows.Count > 0)
                    {
                        txtCompanyName.Text = PL.dt.Rows[0]["CompanyName"].ToString();
                        ddlOrganization.SelectedValue = PL.dt.Rows[0]["OrganizationID"].ToString();
                        txtUnitNo.Text = PL.dt.Rows[0]["UnitNo"].ToString();
                        txtTower.Text = PL.dt.Rows[0]["Tower"].ToString();
                        txtArea.Text = PL.dt.Rows[0]["Area"].ToString();
                        try
                        {
                            ddlOrganization_SelectedIndexChanged(sender, e);
                            ddlRegion.SelectedValue = PL.dt.Rows[0]["Region"].ToString();
                            ddlRegion_SelectedIndexChanged(sender,e);
                            ddlStates.SelectedIndex = ddlStates.Items.IndexOf(ddlStates.Items.FindByText(dt.Rows[0]["Emirates"].ToString()));
                            ddlDomain.SelectedValue =  PL.dt.Rows[0]["WebsiteDomainId"].ToString();
                        }
                        catch (Exception ex)
                        {

                        }

                        txtTRN.Text = PL.dt.Rows[0]["TRN"].ToString();
                        txtStampURL.Text = PL.dt.Rows[0]["Stamp"].ToString();
                        txtGroupAddress.Text = PL.dt.Rows[0]["GroupAddress"].ToString();
                        txtEmail.Text = PL.dt.Rows[0]["Email"].ToString();
                        txtMobileNo.Text = PL.dt.Rows[0]["MobileNo"].ToString();
                        txtTelNo.Text = PL.dt.Rows[0]["TelNo"].ToString(); 
                        txtWebsite.Text = PL.dt.Rows[0]["Website"].ToString();
                        txtLocationURL.Text = PL.dt.Rows[0]["LocationUrl"].ToString();
                        txtLogourl.Text =  PL.dt.Rows[0]["companylogo"].ToString();
                        if (PL.dt.Rows[0]["IsCompanyActive"].ToString() == "False")
                        {
                            chkActive.Checked = false;
                        }
                        else
                        {
                            chkActive.Checked = true;
                        }
                        ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
                        SetList(lstworklocations, PL.dt.Rows[0]["WorkLocations"].ToString());
                    }
                    else
                    {
                        txtCompanyName.Text = "";
                        ddlOrganization.SelectedIndex = -1;
                        txtUnitNo.Text = "";
                        txtTower.Text = "";
                        txtArea.Text = "";
                        ddlRegion.SelectedIndex = -1;
                        ddlStates.SelectedIndex = -1;
                        txtTRN.Text= "";
                        txtStampURL.Text = "";
                        txtGroupAddress.Text = "";
                        txtEmail.Text = "";
                        txtMobileNo.Text = "";
                        txtTelNo.Text = "";
                        ddlDomain.SelectedIndex = -1;
                        txtWebsite.Text = "";
                        txtLocationURL.Text = "";
                    }
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL();
            PL.Name = txtCompanyName.Text.Trim();
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<CompanyName><![CDATA[" + txtCompanyName.Text.Trim() + "]]></CompanyName>";
            xml += "<Organization><![CDATA[" + ddlOrganization.SelectedValue + "]]></Organization>";
            xml += "<UnitNo><![CDATA[" + txtUnitNo.Text.Trim() + "]]></UnitNo>";
            xml += "<Tower><![CDATA[" + txtTower.Text.Trim() + "]]></Tower>";
            xml += "<Area><![CDATA[" + txtArea.Text.Trim() + "]]></Area>";
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>";
            xml += "<States><![CDATA[" + ddlStates.SelectedItem.ToString()  + "]]></States>";
            xml += "<TRN><![CDATA[" + txtTRN.Text.Trim() + "]]></TRN>";
            xml += "<StampURL><![CDATA[" + txtStampURL.Text.Trim() + "]]></StampURL>";
            xml += "<GroupAddress><![CDATA[" + txtGroupAddress.Text.Trim() + "]]></GroupAddress>";
            xml += "<Email><![CDATA[" + txtEmail.Text.Trim() + "]]></Email>";
            xml += "<MobileNo><![CDATA[" + txtMobileNo.Text.Trim() + "]]></MobileNo>";
            xml += "<TelNo><![CDATA[" + txtTelNo.Text.Trim()+ "]]></TelNo>";
            xml += "<DomainID><![CDATA[" + ddlDomain.SelectedValue + "]]></DomainID>";
            xml += "<Domain><![CDATA[" + ddlDomain.SelectedItem.ToString() + "]]></Domain>";
            xml += "<Website><![CDATA[" + txtWebsite.Text.Trim() + "]]></Website>";
            xml += "<LocationURL><![CDATA[" + txtLocationURL.Text.Trim() + "]]></LocationURL>";
            xml += "<Logourl><![CDATA[" + txtLogourl.Text.Trim() + "]]></Logourl>";
            xml += "<WorkLocations><![CDATA[" + Request.Form[lstworklocations.UniqueID]  + "]]></WorkLocations>";
            xml += "</tr>";
            xml += "</tbl>";
            PL.IsActive = chkActive.Checked;
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 21;
            }
            else
            {
                PL.OpCode = 22;
                PL.AutoId = Convert.ToInt32(hidAutoid.Value);
            }
            PL.HOD = ddlHOD.SelectedValue;
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            StructureDL.returnTable(PL);
            if (!PL.isException)
            {
                int result = Convert.ToInt32(PL.dt.Rows[0]["Result"]);
                if (result == 0)
                {
                    ClearField();
                    FillListView();
                    divView.Visible = true;
                    divEdit.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Saved Successfully');", true);
                }
                else if (result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagWarning", $"ShowError('A Company already exists with the same name');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlGroupFilter.SelectedIndex = 0;
            ddlOrganizationSearch.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }

        protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlOrganization.SelectedValue.ToString() != "")
            {
                GetRegion(ddlRegion,Convert.ToInt32(ddlOrganization.SelectedValue));
            }
            ddlRegion_SelectedIndexChanged(sender, e);
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue.ToString() != "")
            {
                GetStates(ddlStates,  ddlRegion.SelectedValue.ToString());
            }
        }

        protected void ddlDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDomain.SelectedValue.ToString() != "")
            {
                txtWebsite.Text = "www." + ddlDomain.SelectedItem.ToString();
            }
        }
    }
}