using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class PortalSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTime(ddlStartTime);
                BindTime(ddlEndTime);
                BindTime(ddlStartTimeOrg);
                BindTime(ddlEndTimeOrg);
                BindTime(ddlStartTimeHrms);
                BindTime(ddlEndTimeHrms);
                BindRegion(ddlRegion);
                BindRegion(ddlRegionOrg);
                BindEmployee(ddlExcludedEmployees);
                BindEmployee(ddlExcludedEmployeesOrg);
                FillData();
            }
        }
        void BindRegion(ListBox ddl)
        {
            PortalSettingsPL PL = new PortalSettingsPL();
            PL.OpCode = 1;
            PortalSettingsDL.returnTable(PL);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
        }
        void BindTime(DropDownList ddl)
        {
            PortalSettingsPL PL = new PortalSettingsPL();
            PL.OpCode = 2;
            PortalSettingsDL.returnTable(PL);
            ddl.DataTextField = "TimeSlot12Hour";
            ddl.DataValueField = "TimeSlot12Hour";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void BindEmployee(ListBox ddl)
        {
            PortalSettingsPL PL = new PortalSettingsPL();
            PL.OpCode = 3;
            PortalSettingsDL.returnTable(PL);
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
        }
        protected void btnSave(object sender, EventArgs e)
        {
            PortalSettingsPL PL = new PortalSettingsPL();
            PL.OpCode = ViewState["Mode"].Equals("Add") ? 4 : 5;
            PL.StartTime = ddlStartTime.SelectedValue;
            PL.AutoId = hidID?.Value;
            PL.EndTime = ddlEndTime.SelectedValue;
            PL.DowntimeMessage = txtDowntimeMessage.Text;
            PL.ExcludedEmployees = Request.Form[ddlExcludedEmployees.UniqueID];
            PL.Region = Request.Form[ddlRegion.UniqueID];

            PL.StartTimeOrg = ddlStartTimeOrg.SelectedValue;
            PL.EndTimeOrg = ddlEndTimeOrg.SelectedValue;
            PL.DowntimeMessageOrg = txtDowntimeMessageOrg.Text;
            PL.ExcludedEmployeesOrg = Request.Form[ddlExcludedEmployeesOrg.UniqueID];
            PL.RegionOrg = Request.Form[ddlRegionOrg.UniqueID];

            PL.StartTimeHrms = ddlStartTimeHrms.SelectedValue;
            PL.EndTimeHrms = ddlEndTimeHrms.SelectedValue;
            PL.ValidationPeriod = ddlValidationPeriod.SelectedValue;
            PortalSettingsDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record submitted successfully');", true);
                FormControl(false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }

        protected void btnEdit(object sender, EventArgs e)
        {
            FillData();
            FormControl(true);
        }
        protected void FillData()
        {
            PortalSettingsPL PL = new PortalSettingsPL();
            PL.OpCode = 6;
            PortalSettingsDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt.Rows.Count > 0)
            {
                ViewState["Mode"] = "Edit";
                hidID.Value = PL.dt.Rows[0]["Autoid"].ToString();
                ddlStartTime.SelectedValue = PL.dt.Rows[0]["StartTime"].ToString();
                ddlEndTime.SelectedValue = PL.dt.Rows[0]["EndTime"].ToString();
                txtDowntimeMessage.Text = PL.dt.Rows[0]["DowntimeMessage"].ToString();
                SetMultiBD(ddlRegion, PL.dt.Rows[0]["Region"].ToString());
                SetMultiBD(ddlExcludedEmployees, PL.dt.Rows[0]["ExcludedEmployees"].ToString());

                ddlStartTimeOrg.SelectedValue = PL.dt.Rows[0]["StartTimeOrg"].ToString();
                ddlEndTimeOrg.SelectedValue = PL.dt.Rows[0]["EndTimeOrg"].ToString();
                txtDowntimeMessageOrg.Text = PL.dt.Rows[0]["DowntimeMessageOrg"].ToString();
                SetMultiBD(ddlRegionOrg, PL.dt.Rows[0]["RegionOrg"].ToString());
                SetMultiBD(ddlExcludedEmployeesOrg, PL.dt.Rows[0]["ExcludedEmployeesOrg"].ToString());

                ddlStartTimeHrms.SelectedValue = PL.dt.Rows[0]["StartTimeHrms"].ToString();
                ddlEndTimeHrms.SelectedValue = PL.dt.Rows[0]["EndTimeHrms"].ToString();
                ddlValidationPeriod.SelectedValue = PL.dt.Rows[0]["ValidationPeriod"].ToString();
                FormControl(false);
            }
            else
            {
                ViewState["Mode"] = "Add";
            }
        }
        protected void FormControl(bool value)
        {
            string className = value ? "form-control select2ddl req" : "form-control select2ddl req disabled";
            ddlStartTime.Enabled = value;
            ddlEndTime.Enabled = value;
            txtDowntimeMessage.Enabled = value;
            ddlRegion.CssClass = " " + className;
            ddlExcludedEmployees.CssClass = " " + className;

            ddlStartTimeOrg.Enabled = value;
            ddlEndTimeOrg.Enabled = value;
            txtDowntimeMessageOrg.Enabled = value;
            ddlRegionOrg.CssClass = " " + className;
            ddlExcludedEmployeesOrg.CssClass = " " + className;

            ddlStartTimeHrms.Enabled = value;
            ddlEndTimeHrms.Enabled = value;
            ddlValidationPeriod.Enabled = value;
        }

        void SetMultiBD(ListBox ddl, string ids)
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
        public void ClearField()
        {
            ViewState["Mode"] = "";
            ddlStartTime.SelectedIndex = -1;
            ddlEndTime.SelectedIndex = -1;
            txtDowntimeMessage.Text = "";
            ddlExcludedEmployees.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;

            ddlStartTimeOrg.SelectedIndex = -1;
            ddlEndTimeOrg.SelectedIndex = -1;
            txtDowntimeMessageOrg.Text = "";
            ddlExcludedEmployeesOrg.SelectedIndex = -1;
            ddlRegionOrg.SelectedIndex = -1;

            ddlStartTimeHrms.SelectedIndex = -1;
            ddlEndTimeHrms.SelectedIndex = -1;
            ddlValidationPeriod.SelectedIndex = -1;
            hidID.Value = "";
        }
    }
}