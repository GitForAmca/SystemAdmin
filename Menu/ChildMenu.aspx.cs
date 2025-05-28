using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;
using Label = System.Web.UI.WebControls.Label;

namespace SystemAdmin.Menu
{
    public partial class ChildMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getMenuType();
                CreateActionTable();
                getParentMenu(ddlParentMenuFilter, "", "");
                getSubParentMenu(ddlSubParentMenuFilter, "", "");
                getRegion("0");
                getIndustry(ddlIndustries);
                BindStatus();
                FillListView();
            }
        }
        void getEmployeeSupport()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 4;
            DropdownDL.returnTable(PL);
            ddlPrimaryPerson.DataSource = PL.dt;
            ddlPrimaryPerson.DataValueField = "Autoid";
            ddlPrimaryPerson.DataTextField = "Name";
            ddlPrimaryPerson.DataBind();
            ddlPrimaryPerson.Items.Insert(0, new ListItem("Choose an item", ""));
            ddlSecondayPerson.DataSource = PL.dt;
            ddlSecondayPerson.DataValueField = "Autoid";
            ddlSecondayPerson.DataTextField = "Name";
            ddlSecondayPerson.DataBind();
            ddlSecondayPerson.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void BindStatus()
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.Industry = ddlIndustries.SelectedValue;
            PL.GroupId = ddlGroup.SelectedValue;
            PL.OpCode = 40;
            ServiceMasterDL.returnTable(PL);
            lstStatus.DataSource = PL.dt;
            lstStatus.DataValueField = "id";
            lstStatus.DataTextField = "StatusName";
            lstStatus.DataBind();
        }

        void getRegion(string id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 71;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlRegion.DataSource = PL.dt;
            ddlRegion.DataValueField = "ID";
            ddlRegion.DataTextField = "CountryName";
            ddlRegion.DataBind();
        }
        void getIndustry(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Description";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getGroupCompany(string id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlGroup.DataSource = PL.dt;
            ddlGroup.DataValueField = "AutoId";
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataBind();
        }
        protected void ddlIndustries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustries.SelectedValue != "")
            {
                getGroupCompany(ddlIndustries.SelectedValue);
                getRegion(ddlIndustries.SelectedValue);
                BindStatus();
            }
        }
        void getMenuType()
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 3;
            MenuDL.returnTable(PL);
            ddlMenuType.DataSource = PL.dt;
            ddlMenuType.DataValueField = "AutoId";
            ddlMenuType.DataTextField = "MenuType";
            ddlMenuType.DataBind();
            ddlMenuType.Items.Insert(0, new ListItem("Choose an item", ""));
            ddlMenuTypeFilter.DataSource = PL.dt;
            ddlMenuTypeFilter.DataValueField = "AutoId";
            ddlMenuTypeFilter.DataTextField = "MenuType";
            ddlMenuTypeFilter.DataBind();
            ddlMenuTypeFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void ddlMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenuType.SelectedValue != "")
            {
                getParentMenu(ddlParentMenu, ddlMenuType.SelectedValue, "");
                if(ddlMenuType.SelectedValue == "1")
                {
                    divIndustries.Visible = true;
                    divManualStatus.Visible = true;
                    divGroup.Visible = true;
                }
                else
                {
                    getRegion("0");
                    divIndustries.Visible = false;
                    divManualStatus.Visible = false;
                    divGroup.Visible = false;
                }
            }
        }
        protected void ddlParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParentMenu.SelectedValue != "")
            {
                getSubParentMenu(ddlSubParentMenu, ddlParentMenu.SelectedValue, "");
            }
        }
        void getParentMenu(DropDownList ddl, string MenuType, string Autoid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 5;
            PL.MenuType = MenuType;
            PL.AutoId = Autoid;
            MenuDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "ParentMenuName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getSubParentMenu(DropDownList ddl, string ParentMenuId, string Autoid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 9;
            PL.ParentMenu = ParentMenuId;
            PL.AutoId = Autoid;
            MenuDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "SubParentMenuName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            txtMenuName.Text = string.Empty;
            ddlMenuType.SelectedIndex = 0;
            ddlParentMenu.SelectedIndex = 0;
            ddlSubParentMenu.SelectedIndex = 0;
        }
        void FillListView()
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 12;
            PL.MenuType = ddlMenuTypeFilter.SelectedValue;
            PL.ParentMenu = ddlParentMenuFilter.SelectedValue;
            PL.SubParentMenu = ddlSubParentMenuFilter.SelectedValue;
            PL.IsDefault = ddlDefault.SelectedValue;
            PL.IsActive = ddlActive.SelectedValue;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_ParentMenu.DataSource = PL.dt;
                LV_ParentMenu.DataBind();
            }
            else
            {
                LV_ParentMenu.DataSource = PL.dt;
                LV_ParentMenu.DataBind();
            }
        }
        void FillActionListView(int id)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 16;
            PL.AutoId = id;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ViewState["actiondt"] = PL.dt;
                lvaction.DataSource = ViewState["actiondt"] as DataTable;
                lvaction.DataBind();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
            ddlParentMenu.Items.Clear();
            ddlSubParentMenu.Items.Clear();
            getEmployeeSupport();
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_ParentMenu.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoid.Value = chkSelect.Attributes["Autoid"];
                    getData(Autoid);
                    FillActionListView(Autoid);
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        }
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string manualStatus = Request.Form[lstStatus.UniqueID];

            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<SubParentMenuId><![CDATA[" + ddlSubParentMenu.SelectedValue + "]]></SubParentMenuId>";
            xml += "<MenuName><![CDATA[" + txtMenuName.Text + "]]></MenuName>";
            xml += "<MenuURL><![CDATA[" + txtMenuURL.Text.Trim() + "]]></MenuURL>";
            xml += "<IndustriesId><![CDATA[" + ddlIndustries.SelectedValue + "]]></IndustriesId>";
            xml += "<PrimaryPerson><![CDATA[" + ddlPrimaryPerson.SelectedValue + "]]></PrimaryPerson>";
            xml += "<SecondaryPerson><![CDATA[" + ddlSecondayPerson.SelectedValue + "]]></SecondaryPerson>";
            xml += "<IsDefault><![CDATA[" + chkDefault.Checked + "]]></IsDefault>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "<IsMasterMenu><![CDATA[" + chkMasterMenu.Checked + "]]></IsMasterMenu>";
            xml += "<StatusId><![CDATA[" + manualStatus + "]]></StatusId>";
            xml += "</tr>";
            xml += "</tbl>";

            MenuPL PL = new MenuPL();
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 10;
                MenuDL.returnTable(PL);
                hidAutoid.Value = PL.dt.Rows[0]["MainId"].ToString();
            }
            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 11;
                PL.AutoId = hidAutoid.Value;
                MenuDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                saveGroup(Convert.ToInt32(hidAutoid.Value));
                saveRegion(Convert.ToInt32(hidAutoid.Value));
                SaveAction(hidAutoid.Value);
                divView.Visible = true;
                divEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Save Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void saveGroup(int mainId)
        {
            string groupString = Request.Form[ddlGroup.UniqueID];
            if(groupString != null)
            {
                var query = from val in groupString.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLGroup(mainId, num);
                }
                XML += "</tbl>";
                MenuPL PL = new MenuPL();
                PL.OpCode = 13;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuDL.returnTable(PL);
            }
        }
        void saveRegion(int mainId)
        {
            string regionString = Request.Form[ddlRegion.UniqueID];
            if (regionString != null)
            {
                var query = from val in regionString.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLRegion(mainId, num);
                }
                XML += "</tbl>";
                MenuPL PL = new MenuPL();
                PL.OpCode = 14;
                PL.XML = XML;
                PL.AutoId = mainId;
                MenuDL.returnTable(PL);
            }
        }
        void getData(int id)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 12;
            PL.AutoId = id;
            MenuDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtMenuName.Text = PL.dt.Rows[0]["MenuName"].ToString();
                txtMenuURL.Text = PL.dt.Rows[0]["MenuURL"].ToString();
                ddlMenuType.SelectedIndex = ddlMenuType.Items.IndexOf(ddlMenuType.Items.FindByValue(PL.dt.Rows[0]["Type"].ToString()));
                if (PL.dt.Rows[0]["Type"].ToString() == "1")
                {
                    divIndustries.Visible = true;
                    divManualStatus.Visible = true;
                    divGroup.Visible = true;
                }
                else
                {
                    divIndustries.Visible = false;
                    divManualStatus.Visible = false;
                    divGroup.Visible = false;
                }
                ddlIndustries.SelectedIndex = ddlIndustries.Items.IndexOf(ddlIndustries.Items.FindByValue(PL.dt.Rows[0]["IndustriesId"].ToString()));
                getEmployeeSupport();
                ddlPrimaryPerson.SelectedIndex = ddlPrimaryPerson.Items.IndexOf(ddlPrimaryPerson.Items.FindByValue(PL.dt.Rows[0]["PrimaryPerson"].ToString()));
                ddlSecondayPerson.SelectedIndex = ddlSecondayPerson.Items.IndexOf(ddlSecondayPerson.Items.FindByValue(PL.dt.Rows[0]["SecondaryPerson"].ToString()));
                getSubParentMenu(ddlSubParentMenu, "", PL.dt.Rows[0]["SubParentMenuId"].ToString());
                ddlSubParentMenu.SelectedIndex = ddlSubParentMenu.Items.IndexOf(ddlSubParentMenu.Items.FindByValue(PL.dt.Rows[0]["SubParentMenuId"].ToString()));
                getParentMenu(ddlParentMenu, "", PL.dt.Rows[0]["ParentMenuId"].ToString());
                ddlParentMenu.SelectedIndex = ddlParentMenu.Items.IndexOf(ddlParentMenu.Items.FindByValue(PL.dt.Rows[0]["ParentMenuId"].ToString()));

                getGroupCompany(PL.dt.Rows[0]["IndustriesId"].ToString());
                SetList(ddlGroup, PL.dt.Rows[0]["GroupIds"].ToString());

                if (PL.dt.Rows[0]["Type"].ToString() == "1")
                {
                    getRegion(PL.dt.Rows[0]["IndustriesId"].ToString());
                }
                else
                {
                    getRegion("0");
                }

                SetList(ddlRegion, PL.dt.Rows[0]["RegionIds"].ToString());
                BindStatus();
                SetList(lstStatus, PL.dt.Rows[0]["StatusId"].ToString());

                if (PL.dt.Rows[0]["IsDefault"].ToString() == "True")
                {
                    chkDefault.Checked = true;
                }
                else
                {
                    chkDefault.Checked = false;
                }
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
                if (PL.dt.Rows[0]["IsMasterMenu"].ToString() == "False")
                {
                    chkMasterMenu.Checked = false;
                }
                else
                {
                    chkMasterMenu.Checked = true;
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
            ddlMenuTypeFilter.SelectedIndex = 0;
            ddlParentMenuFilter.SelectedIndex = 0;
            ddlDefault.SelectedIndex = 0;
            ddlActive.SelectedIndex = 0;
            FillListView();
        }
        void CreateActionTable()
        {
            ViewState["actiondt"] = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ActionName");
            ViewState["actiondt"] = dt;
            lvaction.DataSource = ViewState["actiondt"] as DataTable;
            lvaction.DataBind();
        }
        protected void btnactionadd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtaction.Text))
            {
                DataTable dt = ViewState["actiondt"] as DataTable;
                DataRow dr = dt.NewRow();
                dr["ActionName"] = txtaction.Text.Trim();
                dt.Rows.Add(dr);

                ViewState["actiondt"] = dt;
                lvaction.DataSource = ViewState["actiondt"] as DataTable;
                lvaction.DataBind();
                txtaction.Text = "";
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
                lvaction.DataSource = dt;
                lvaction.DataBind();
            }
        }
        private static string XMLGroup(int MainId, int GroupId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            XML += "</tr>";
            return XML;
        }
        private static string XMLRegion(int MainId, int RegionId)
        {
            string XML = "<tr>";
            XML += "<MainId><![CDATA[" + MainId + "]]></MainId>";
            XML += "<RegionId><![CDATA[" + RegionId + "]]></RegionId>";
            XML += "</tr>";
            return XML;
        }
        void SaveAction(string mainid)
        {
            if (!string.IsNullOrEmpty(mainid))
            {
                MenuPL PL = new MenuPL();
                PL.OpCode = 15;
                foreach (ListViewItem item in lvaction.Items)
                {
                    Label lblName = (Label)item.FindControl("lblActionname");
                    PL.AutoId = mainid;
                    PL.ParentMenu = lblName.Text;
                    MenuDL.returnTable(PL);
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string CheckURL(string text, string oldname)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 17;
            PL.MenuType = text;
            PL.ParentMenu = oldname;
            MenuDL.returnTable(PL);
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}