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
    public partial class Organization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetGroup(ddlGroupFilter);
                GetGroup(ddlGroup);
                //GetRegion(ddlRegion);
                GetHOD(ddlHOD);
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
        void GetRegion(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 32;
            PL.Name = ddlIndustry.SelectedValue;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }

        void ClearField()
        {
            txtGroupName.Text = "";
            txtShortName.Text = "";
            ddlGroup.SelectedIndex = -1;
            ddlIndustry.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1; 
            txtSequence.Text = ""; 
            txtPrimaryColor.Value = "";
            txtSecondarColor.Value = ""; 
            ddlGroupFilter.SelectedIndex = -1;
            ddlGroup.SelectedIndex = -1;
            
        }
        void FillListView()
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 12;
            PL.Name = ddlGroupFilter.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Organization.DataSource = PL.dt;
                LV_Organization.DataBind();
            }
            else
            {
                LV_Organization.DataSource = PL.dt;
                LV_Organization.DataBind();
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
            foreach (ListViewItem item in LV_Organization.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoid.Value = chkSelect.Attributes["Autoid"];
                    getData(Autoid);
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        }

        void getData(int Autoid)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 15;
            PL.AutoId = Autoid;
            StructureDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                try
                { 
                    txtGroupName.Text = PL.dt.Rows[0]["Name"].ToString();
                    txtShortName.Text = PL.dt.Rows[0]["ShortForm"].ToString();  
                    txtSequence.Text = PL.dt.Rows[0]["Sequence"].ToString(); 
                    txtPrimaryColor.Value = PL.dt.Rows[0]["PrimaryColor"].ToString();
                    txtSecondarColor.Value = PL.dt.Rows[0]["SecondaryColor"].ToString(); 
                    if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                    {
                        chkActive.Checked = false;
                    }
                    else
                    {
                        chkActive.Checked = true;
                    }
                    ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
                    ddlGroup.SelectedValue = PL.dt.Rows[0]["GroupID"].ToString();
                    ddlGroup_SelectedIndexChanged(ddlGroup, EventArgs.Empty);
                    ddlIndustry.SelectedValue =  PL.dt.Rows[0]["IndustryID"].ToString();
                    ddlIndustry_SelectedIndexChanged(ddlIndustry, EventArgs.Empty);
                    ddlRegion.SelectedValue = PL.dt.Rows[0]["RegionId"].ToString();
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                txtGroupName.Text = "";
                txtShortName.Text = "";
                ddlGroup.SelectedValue = "";
                ddlRegion.SelectedValue = ""; 
                txtSequence.Text = ""; 
                txtPrimaryColor.Value = "";
                txtSecondarColor.Value = "";
              
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StructurePL PL = new StructurePL();
            PL.Name = txtGroupName.Text.Trim();
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<OrgName><![CDATA[" + txtGroupName.Text.Trim() + "]]></OrgName>";
            xml += "<ShortName><![CDATA[" + txtShortName.Text.Trim() + "]]></ShortName>";
            xml += "<GroupId><![CDATA[" + ddlGroup.SelectedValue + "]]></GroupId>";
            xml += "<Industry><![CDATA[" + ddlIndustry.SelectedValue + "]]></Industry>";
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>"; 
            xml += "<Sequence><![CDATA[" + txtSequence.Text.Trim() + "]]></Sequence>"; 
            xml += "<PrimaryColor><![CDATA[" + txtPrimaryColor.Value.Trim() + "]]></PrimaryColor>";
            xml += "<SecondaryColor><![CDATA[" + txtSecondarColor.Value.Trim() + "]]></SecondaryColor>"; 
            xml += "</tr>";
            xml += "</tbl>";
            PL.IsActive = chkActive.Checked;
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 13;
            }
            else
            {
                PL.OpCode = 14;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagWarning", $"ShowError('An Organization already exists with the same name');", true);
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
            ddlActive.SelectedIndex = 0;
            FillListView();
        }

        protected void ddlIndustry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRegion(ddlRegion);
        }

      

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGIndustry(ddlIndustry);
        }


        void GetGIndustry(DropDownList ddl)
        {
            StructurePL PL = new StructurePL();
            PL.OpCode = 36;
            PL.Name = ddlGroup.SelectedValue;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
    }
}