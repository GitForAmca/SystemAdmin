using AMCAPropertiesAdmin.App_Code;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class Developers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeveloperList(ddlDevelopersFilter);  
                FillListView();
            }
        }
        void DeveloperList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 23;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Developers";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        } 
        void ClearField()
        {
            txtDevelopers.Text = "";
            txtDescription.Text = "";
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 23;  
            PL.DevelopersId = ddlDevelopersFilter.SelectedValue;  
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Developers.DataSource = PL.dt;
                LV_Developers.DataBind();
            }
            else
            {
                LV_Developers.DataSource = "";
                LV_Developers.DataBind();
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
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Developers.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidDeveloperId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidDeveloperId.Value));
                    FillActionListView(Convert.ToInt32(hidDeveloperId.Value));
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
            PL.OpCode = 24;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {

                txtDevelopers.Text = PL.dt.Rows[0]["Developers"].ToString();

                chkActive.Checked = Convert.ToBoolean(PL.dt.Rows[0]["IsActive"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[0]["Imagepath"].ToString()))
                {
                    ltrDeveloperDocuments.Text = "<a target='_blank' class='' href='" + ResolveUrl("../"+dt.Rows[0]["Imagepath"].ToString()) + "'>View</a>";
                    fileDeveloperDocuments.Attributes.Add("oldpath", PL.dt.Rows[0]["Imagepath"].ToString());
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
            ddlDevelopersFilter.SelectedIndex = 0; 
            FillListView();
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
        protected void btnAdd_Click(object sender, EventArgs e)
        { 

            CommunityPL PL = new CommunityPL();

            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<Developers><![CDATA[" + txtDevelopers.Text + "]]></Developers>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "<LogoPath><![CDATA[" + FileUpload(fileDeveloperDocuments) + "]]></LogoPath>";
            xml += "</tr>";
            xml += "</tbl>";

            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 12;
                CommunityDL.returnTable(PL); 
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 13;
                PL.AutoId = hidDeveloperId.Value;
                CommunityDL.returnTable(PL); 
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
        public string FileUpload(FileUpload Ctrfile)
        {
            string strfilePath = "";
            if (System.IO.Directory.Exists(Server.MapPath("~/UploadedFile/DevelopersFile")) == false)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadedFile/DevelopersFile"));
            string filepath = "UploadedFile/DevelopersFile/";

            if (Ctrfile.HasFile)
            {
                Guid myglobalid = Guid.NewGuid();
                string filename = string.Empty;
                filename = System.IO.Path.GetFileName(Ctrfile.PostedFile.FileName);
                string[] ArrFName = filename.Split('.');
                string strfilename = string.Empty;
                strfilename = "FILE_" + myglobalid.ToString() + "." + ArrFName[ArrFName.Length - 1];
                strfilePath = filepath + strfilename;
                Ctrfile.SaveAs(Server.MapPath("~/" + strfilePath));
            }
            else
            {
                strfilePath = string.IsNullOrEmpty(Ctrfile.Attributes["oldpath"]) ? "" : Ctrfile.Attributes["oldpath"];
            }             
            return strfilePath;
        }
    }
}