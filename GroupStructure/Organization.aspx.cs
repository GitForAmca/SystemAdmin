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
                GetRegion(ddlRegion);
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
            PL.OpCode = 11;
            StructureDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtGroupName.Text = string.Empty;
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
                txtGroupName.Text = PL.dt.Rows[0]["Name"].ToString();
                txtShortName.Text = PL.dt.Rows[0]["ShortForm"].ToString();
                ddlGroup.SelectedValue = PL.dt.Rows[0]["GroupID"].ToString();
                ddlRegion.SelectedValue = PL.dt.Rows[0]["RegionId"].ToString();
                txtconnectionName.Text = PL.dt.Rows[0]["ConnectionName"].ToString();
                txtSMTP.Text = PL.dt.Rows[0]["SMTP"].ToString();
                ddlSSL.SelectedValue = PL.dt.Rows[0]["SSL"].ToString();
                txtPortNo.Text = PL.dt.Rows[0]["PortNo"].ToString();
                txtSequence.Text = PL.dt.Rows[0]["Sequence"].ToString();
                txtDBName.Text = PL.dt.Rows[0]["DbName"].ToString(); 
                //txtDBPassword.Text = PL.dt.Rows[0]["dbPassword"].ToString();
                txtDBPassword.Attributes["value"] = PL.dt.Rows[0]["dbPassword"].ToString();
                txtPrimaryColor.Value = PL.dt.Rows[0]["PrimaryColor"].ToString();
                txtSecondarColor.Value = PL.dt.Rows[0]["SecondaryColor"].ToString();
                txtlogourl.Text = PL.dt.Rows[0]["Logo"].ToString();
                txtDBUserName.Text = PL.dt.Rows[0]["dbUserName"].ToString();
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
                ddlHOD.SelectedValue = PL.dt.Rows[0]["HOD"].ToString();
            }
            else
            {
                txtGroupName.Text = "";
                txtShortName.Text = "";
                ddlGroup.SelectedValue = "";
                ddlRegion.SelectedValue = "";
                txtconnectionName.Text = "";
                txtSMTP.Text = "";
                ddlSSL.SelectedIndex = - 1;
                txtPortNo.Text = "";
                txtSequence.Text = "";
                txtDBName.Text = "";
                txtDBUserName.Text = "";
                txtDBPassword.Text = "";
                txtPrimaryColor.Value = "";
                txtSecondarColor.Value = "";
                txtlogourl.Text = "";
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
            xml += "<Region><![CDATA[" + ddlRegion.SelectedValue + "]]></Region>";
            xml += "<ConnectionName><![CDATA[" + txtconnectionName.Text.Trim() + "]]></ConnectionName>";
            xml += "<SMTP><![CDATA[" + txtSMTP.Text.Trim() + "]]></SMTP>";
            xml += "<SSL><![CDATA[" + ddlSSL.SelectedValue  + "]]></SSL>";
            xml += "<PortNo><![CDATA[" + txtPortNo.Text.Trim() + "]]></PortNo>";
            xml += "<Sequence><![CDATA[" + txtSequence.Text.Trim() + "]]></Sequence>";
            xml += "<DBName><![CDATA[" + txtDBName.Text.Trim() + "]]></DBName>";
            xml += "<DBUName><![CDATA[" + txtDBUserName.Text.Trim() + "]]></DBUName>";
            xml += "<DBPassword><![CDATA[" + txtDBPassword.Text.Trim() + "]]></DBPassword>";
            xml += "<PrimaryColor><![CDATA[" + txtPrimaryColor.Value.Trim() + "]]></PrimaryColor>";
            xml += "<SecondaryColor><![CDATA[" + txtSecondarColor.Value.Trim() + "]]></SecondaryColor>";
            xml += "<logourl><![CDATA[" + txtlogourl.Text.Trim() + "]]></logourl>";
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
    }
}