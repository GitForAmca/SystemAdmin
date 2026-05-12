using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class Request_Box : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getRequestList();
                getDepartment(lstDepartmentFilter);
                getDepartment(lstDepartment);
                getMenuList(lstMenu);
                getEmployeeList();
                FillListView();
            }
        }
        void getDepartment(ListBox lst)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 52;
            DropdownDL.returnTable(PL);
            lst.DataSource = PL.dt;
            lst.DataValueField = "Id";
            lst.DataTextField = "Department";
            lst.DataBind();
            lst.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getMenuList(ListBox ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 60;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }
        void getEmployeeList()
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
        void getRequestList()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 147;
            EssDL.returnTable(PL);
            ddlRequestFilter.DataSource = PL.dt;
            ddlRequestFilter.DataValueField = "Id";
            ddlRequestFilter.DataTextField = "Name";
            ddlRequestFilter.DataBind();
            ddlRequestFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 145;
            PL.AutoId = ddlRequestFilter.SelectedValue;
            PL.Department = lstDepartmentFilter.SelectedValue; 
            PL.Type = ddlTypeFilter.SelectedValue;
            PL.String1 = ddlRecieverTypeFilter.SelectedValue;
            PL.String2 = ddlRequestActionFilter.SelectedValue;
            PL.IsActive = ddlIsActiveFilter.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (!PL.isException)
            {
                if (dt.Rows.Count > 0)
                {
                    LV_LicenseIndustry.DataSource = dt;
                    LV_LicenseIndustry.DataBind();
                }
                else
                {
                    LV_LicenseIndustry.DataSource = "";
                    LV_LicenseIndustry.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void ClearField()
        {
            ddlRequestFilter.SelectedIndex = -1; 
            lstDepartmentFilter.SelectedIndex = -1;
            ddlTypeFilter.SelectedIndex = -1;
            ddlRecieverTypeFilter.SelectedIndex = -1;
            ddlRequestActionFilter.SelectedIndex = -1;
            ddlIsActiveFilter.SelectedIndex = -1;
             
            txtRequestName.Text = ""; 
            lstDepartment.SelectedIndex = -1;
            ddlType.SelectedIndex = -1;
            ddlPrimaryPerson.SelectedIndex = -1;
            ddlSecondayPerson.SelectedIndex = -1;
            ddlRecieverType.SelectedIndex = -1;
            ddlRequestAction.SelectedIndex = -1;
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
            foreach (ListViewItem item in LV_LicenseIndustry.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 144;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtRequestName.Text = dt.Rows[0]["RequestName"].ToString();

                            getDepartment(lstDepartment);
                            SetList(lstDepartment, PL.dt.Rows[0]["DepartmentIds"].ToString());
                             
                            ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(PL.dt.Rows[0]["Type"].ToString()));

                            if(PL.dt.Rows[0]["PrimaryAssignedUser"].ToString() != "" && PL.dt.Rows[0]["PrimaryAssignedUser"].ToString() != "")
                            {
                                getEmployeeList();
                                divPrimaryOrSecondaryAssigner.Visible = true;
                                divRequestAction.Visible = false;
                                ddlPrimaryPerson.SelectedIndex = ddlPrimaryPerson.Items.IndexOf(ddlPrimaryPerson.Items.FindByValue(PL.dt.Rows[0]["PrimaryAssignedUser"].ToString()));
                                ddlSecondayPerson.SelectedIndex = ddlSecondayPerson.Items.IndexOf(ddlSecondayPerson.Items.FindByValue(PL.dt.Rows[0]["SecondaryAssignedUser"].ToString()));
                            }

                            if (PL.dt.Rows[0]["RequestActionType"].ToString() != "" && PL.dt.Rows[0]["ReceiverType"].ToString() != "")
                            {
                                getEmployeeList();
                                divPrimaryOrSecondaryAssigner.Visible = false;
                                divRequestAction.Visible = true;
                                ddlRequestAction.SelectedIndex = ddlRequestAction.Items.IndexOf(ddlRequestAction.Items.FindByValue(PL.dt.Rows[0]["RequestActionType"].ToString()));
                                ddlRecieverType.SelectedIndex = ddlRecieverType.Items.IndexOf(ddlRecieverType.Items.FindByValue(PL.dt.Rows[0]["ReceiverType"].ToString()));
                            }

                            getMenuList(lstMenu);
                            SetList(lstMenu, PL.dt.Rows[0]["MenuIds"].ToString());

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

            string menuIds = Request.Form[lstMenu.UniqueID];

            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<RequestName><![CDATA[" + txtRequestName.Text + "]]></RequestName>";
            xml += "<DepartmentIds><![CDATA[" + Request.Form[lstDepartment.UniqueID].ToString() + "]]></DepartmentIds>"; 
            xml += "<Type><![CDATA[" + ddlType.SelectedValue + "]]></Type>";
            xml += "<RequestAction><![CDATA[" + ddlRequestAction.SelectedValue + "]]></RequestAction>";
            xml += "<RecieverType><![CDATA[" + ddlRecieverType.SelectedValue + "]]></RecieverType>";
            xml += "<PrimaryAssignerId><![CDATA[" + ddlPrimaryPerson.SelectedValue + "]]></PrimaryAssignerId>";
            xml += "<SecondaryAssignerId><![CDATA[" + ddlSecondayPerson.SelectedValue + "]]></SecondaryAssignerId>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked == true ? 1 : 0) + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 143;
                PL.XML = xml;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    if (PL.dt.Rows.Count > 0)
                    {
                        saveMenu(Convert.ToInt32(PL.dt.Rows[0]["AutoId"].ToString()), menuIds); 
                    }
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
                PL.OpCode = 146;
                PL.XML = xml;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                PL.AutoId = Convert.ToInt32(hidID.Value);
                EssDL.returnTable(PL);
                if (!PL.isException)
                {
                    if(menuIds != "" && menuIds != null && menuIds != "0")
                    { 
                        deleteMenuids(Convert.ToInt32(hidID.Value));
                        saveMenu(Convert.ToInt32(hidID.Value), menuIds);
                    }
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
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearField();
            FillListView();
        }
        public void saveMenu(int autoId , string MenuIds)
        { 
            string[] arr = MenuIds.Split(',');
            EssPL PL = new EssPL();

            foreach (string id in arr)
            { 
                PL.OpCode = 149; 
                PL.AutoId = autoId;
                PL.String1 = id;
                EssDL.returnTable(PL);
            }
        }
        public void deleteMenuids(int autoId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 150;
            PL.AutoId = autoId; 
            EssDL.returnTable(PL);
        }
        protected void ddlIndustryFilter_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //if (ddlIndustryFilter.SelectedValue != "")
            //{
            //    GetActiviesFilter(lstActivityFilter, ddlIndustryFilter.SelectedValue);
            //}
            //else
            //{
            //    ddlIndustryFilter.Items.Clear();
            //    GetIndustries(ddlIndustryFilter);
            //}
        }
        protected void LV_LicenseIndustry_ItemDataBound(object sender, ListViewItemEventArgs e)
        { 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HtmlTableCell tdElement = e.Item.FindControl("td_approvers") as HtmlTableCell;
                if (tdElement != null)
                {
                    string innerHtml = tdElement.InnerHtml.Replace("appr", "<span>");
                    string innerHtml1 = tdElement.InnerHtml.Replace("appre", "</span>");
                    tdElement.InnerHtml = Server.HtmlDecode(innerHtml);
                    tdElement.InnerHtml = Server.HtmlDecode(innerHtml1);
                }


                HtmlTableCell tdElement1 = e.Item.FindControl("td_approvers1") as HtmlTableCell;
                if (tdElement1 != null)
                {
                    string innerHtml = tdElement1.InnerHtml.Replace("appr", "<span>");
                    string innerHtml1 = tdElement1.InnerHtml.Replace("appre", "</span>");
                    tdElement1.InnerHtml = Server.HtmlDecode(innerHtml);
                    tdElement1.InnerHtml = Server.HtmlDecode(innerHtml1);
                }
            }
        }
        protected void ddlRecieverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRecieverType.SelectedValue != "") 
            {
                if(ddlRecieverType.SelectedValue == "Static")
                {
                    divPrimaryOrSecondaryAssigner.Visible = true;
                    divRequestAction.Visible = false;
                    ddlRequestAction.SelectedValue = null;
                }
                else
                { 
                    divPrimaryOrSecondaryAssigner.Visible = false;
                    divRequestAction.Visible = true;
                    ddlPrimaryPerson.SelectedValue = null;
                    ddlSecondayPerson.SelectedValue = null;
                }
            }
            else
            {

            }
        }
        [WebMethod]
        public static string checkRequestType(string reuqestName)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 148;
            PL.String1 = reuqestName;
            EssDL.returnTable(PL);
            return PL.dt.Rows[0]["RequestName"].ToString();
        }
    }
}