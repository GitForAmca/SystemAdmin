using AMCAPropertiesAdmin.App_Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class UnitsMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindGrid(Convert.ToInt32(hidUnitId.Value));
                //Search Filter DropDown
                GetCommunityList(ddlCommunityFilter);
                GetTowerlist(ddlTowerFilter);
                UnitTypeList(ddlUnitTypeFilter);
                AreaList(ddlAreaFilter);
                PropertyStatusList(ddlPropertyStatusFilter);
                PropertyPurposeList(ddlPurposeFilter);
                Classificationlist(ddlClassificationFilter);


                //Add/Edit DropDown
                CreateActionTable();
                CreateDistanceNearByTable();
                GetCommunityList(ddlCommunity);
                UnitTypeList(ddlUnitType);
                AreaList(ddlArea);
                PropertyStatusList(ddlPropertyStatus);
                PropertyPurposeList(ddlPurpose);
                Classificationlist(ddlClassification);
                BedroomTypeList(ddlBedroom);
                BathroomTypeList(ddlBathroom);
                SellerList(ddlSeller);
                UnitElements(ddlUnitElements);
                BindPlaces(ddlplaces);
                AmentiesList(lstAmenties);
                FillListView();
            }
        }
        void CreateActionTable()
        {
            ViewState["actiondt"] = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ElementsId");
            dt.Columns.Add("ElementsName");
            dt.Columns.Add("ElementsText");
            ViewState["actiondt"] = dt;
            lvElements.DataSource = ViewState["actiondt"] as DataTable;
            lvElements.DataBind();
        }
        void CreateDistanceNearByTable()
        {
            ViewState["DistanceNearBydt"] = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("PlaceId");
            dt.Columns.Add("PlaceName");
            dt.Columns.Add("PlaceDistance");
            ViewState["DistanceNearBydt"] = dt;
            LV_DistanceNearBy.DataSource = ViewState["DistanceNearBydt"] as DataTable;
            LV_DistanceNearBy.DataBind();
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
        void GetTowerList(DropDownList ddl, int communityId)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 5;
            PL.CommunityId = communityId;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "TowerName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void GetTowerlist(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 21; 
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "TowerName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void UnitTypeList(DropDownList ddl)
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
        void AreaList(DropDownList ddl)
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
        void PropertyPurposeList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 6;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Purpose";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void Classificationlist(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 7;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "ClassificationName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void BedroomTypeList(DropDownList ddl)
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
        void BathroomTypeList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 10;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Bathroom";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void SellerList(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 11;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "EmployeeName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void UnitElements(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 17;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "ElementName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void BindPlaces(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 29;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "PlaceName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void AmentiesList(ListBox ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 26;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "Amenties";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void ClearField()
        {
            ddlCommunity.SelectedIndex = -1;
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 18; 
            PL.CommunityId = ddlCommunityFilter.SelectedValue; 
            PL.TowerId = ddlTowerFilter.SelectedValue; 
            PL.UnitTypeId = ddlUnitTypeFilter.SelectedValue; 
            PL.AreaId = ddlAreaFilter.SelectedValue; 
            PL.StatusId = ddlPropertyStatusFilter.SelectedValue; 
            PL.PurposeId = ddlPurposeFilter.SelectedValue; 
            PL.ClassificationId = ddlClassificationFilter.SelectedValue; 
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Units.DataSource = PL.dt;
                LV_Units.DataBind();
            }
            else
            {
                LV_Units.DataSource = "";
                LV_Units.DataBind();
            }
        }
        void FillActionListView(int id)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 20;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ViewState["actiondt"] = PL.dt;
                lvElements.DataSource = ViewState["actiondt"] as DataTable;
                lvElements.DataBind();
            }
        } 
        void FillDistanceNearByListView(int id)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 30;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                ViewState["DistanceNearBydt"] = PL.dt;
                LV_DistanceNearBy.DataSource = ViewState["DistanceNearBydt"] as DataTable;
                LV_DistanceNearBy.DataBind();
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
            foreach (ListViewItem item in LV_Units.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidUnitId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidUnitId.Value));
                    BindGrid(Convert.ToInt32(hidUnitId.Value)); 
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
            PL.OpCode = 19;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            { 
                if(PL.dt.Rows[0]["CommunityId"].ToString() != "")
                {
                    divTower.Visible = true;

                    GetCommunityList(ddlCommunity);
                    ddlCommunity.SelectedIndex = ddlCommunity.Items.IndexOf(ddlCommunity.Items.FindByValue(PL.dt.Rows[0]["CommunityId"].ToString()));

                    GetTowerList(ddlTower, Convert.ToInt32(PL.dt.Rows[0]["CommunityId"].ToString()));
                    ddlTower.SelectedIndex = ddlTower.Items.IndexOf(ddlTower.Items.FindByValue(PL.dt.Rows[0]["TowerId"].ToString()));

                }
                //ddlCommunity.Attributes.Add("readonly", "readonly"); 

                txtPropertyNo.Text = PL.dt.Rows[0]["PropertyNo"].ToString();

                UnitTypeList(ddlUnitType);
                ddlUnitType.SelectedIndex = ddlUnitType.Items.IndexOf(ddlUnitType.Items.FindByValue(PL.dt.Rows[0]["UnitId"].ToString()));

                AreaList(ddlArea); 
                ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(PL.dt.Rows[0]["AreaId"].ToString()));

                PropertyStatusList(ddlPropertyStatus);
                ddlPropertyStatus.SelectedIndex = ddlPropertyStatus.Items.IndexOf(ddlPropertyStatus.Items.FindByValue(PL.dt.Rows[0]["PropertyId"].ToString()));

                PropertyPurposeList(ddlPurpose);
                ddlPurpose.SelectedIndex = ddlPurpose.Items.IndexOf(ddlPurpose.Items.FindByValue(PL.dt.Rows[0]["PurposeId"].ToString()));

                Classificationlist(ddlClassification);
                ddlClassification.SelectedIndex = ddlClassification.Items.IndexOf(ddlClassification.Items.FindByValue(PL.dt.Rows[0]["ClassificationId"].ToString()));

                txtMetaTitle.Text = PL.dt.Rows[0]["UnitMetaTitle"].ToString();
                txtMetaDescription.Text = PL.dt.Rows[0]["UnitMetaDescription"].ToString();

                SellerList(ddlSeller);
                ddlSeller.SelectedIndex = ddlSeller.Items.IndexOf(ddlSeller.Items.FindByValue(PL.dt.Rows[0]["SellerId"].ToString()));

                txtPrice.Text = PL.dt.Rows[0]["Price"].ToString();
                txtSize.Text = PL.dt.Rows[0]["UnitSize"].ToString();
                txtTittle.Text = PL.dt.Rows[0]["UnitTitle"].ToString();
                chkDescription.InnerText = PL.dt.Rows[0]["UnitDescription"].ToString();
                chkActive.Checked = Convert.ToBoolean(PL.dt.Rows[0]["IsActive"].ToString());

                BedroomTypeList(ddlBedroom);
                ddlBedroom.SelectedIndex = ddlBedroom.Items.IndexOf(ddlBedroom.Items.FindByValue(PL.dt.Rows[0]["bedroomId"].ToString()));

                BathroomTypeList(ddlBathroom);
                ddlBathroom.SelectedIndex = ddlBathroom.Items.IndexOf(ddlBathroom.Items.FindByValue(PL.dt.Rows[0]["BathroomsId"].ToString()));

                AmentiesList(lstAmenties);
                SetList(lstAmenties, PL.dt.Rows[0]["AmentiesId"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[0]["VideoPath"].ToString()))
                {
                    ltrVideo.Text = "<a target='_blank' class='' href='" + ResolveUrl(PL.dt.Rows[0]["VideoPath"].ToString()) + "'>View</a>";
                    //ltrVideo.Text = "<a target='_blank' class='' href='" + ResolveUrl("~/" + dt.Rows[0]["VideoPath"].ToString()) + "'>View</a>";
                    fileVideoUpload.Attributes.Add("oldpath", PL.dt.Rows[0]["VideoPath"].ToString());
                }

                FillActionListView(id);
                FillDistanceNearByListView(id);          
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
            ddlCommunityFilter.SelectedIndex = 0;
            ddlTowerFilter.SelectedIndex = 0;
            ddlAreaFilter.SelectedIndex = 0;
            ddlUnitTypeFilter.SelectedIndex = 0;
            ddlPropertyStatusFilter.SelectedIndex = 0;
            ddlPurposeFilter.SelectedIndex = 0;
            ddlClassificationFilter.SelectedIndex = 0; 
            FillListView();
        }  
        protected void ddlCommunity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCommunity.SelectedValue != "")
            {
                divTower.Visible = true;
                GetTowerList(ddlTower, Convert.ToInt32(ddlCommunity.SelectedValue));
            }
        } 
        protected void btnAddElements_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUnitElements.Text))
            {
                DataTable dt = ViewState["actiondt"] as DataTable;
                DataRow dr = dt.NewRow();
                dr["ElementsId"] = ddlUnitElements.SelectedValue;
                dr["ElementsName"] = ddlUnitElements.SelectedItem.Text;
                dr["ElementsText"] = txtUnitElements.Text.Trim();
                dt.Rows.Add(dr);

                ViewState["actiondt"] = dt;
                lvElements.DataSource = ViewState["actiondt"] as DataTable;
                lvElements.DataBind();
                ddlUnitElements.SelectedIndex = -1;
                txtUnitElements.Text = "";
            }
        }
        protected void btnPlace_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDistance.Text))
            {
                DataTable dt = ViewState["DistanceNearBydt"] as DataTable;
                DataRow dr = dt.NewRow();
                dr["PlaceId"] = ddlplaces.SelectedValue;
                dr["PlaceName"] = ddlplaces.SelectedItem.Text;
                dr["PlaceDistance"] = txtDistance.Text.Trim();
                dt.Rows.Add(dr);

                ViewState["DistanceNearBydt"] = dt;
                LV_DistanceNearBy.DataSource = ViewState["DistanceNearBydt"] as DataTable;
                LV_DistanceNearBy.DataBind();
                ddlplaces.SelectedIndex = -1;  
                txtDistance.Text = "";
            }
        }
        void SaveAction(string mainid)
        {
            if (!string.IsNullOrEmpty(mainid))
            {
                CommunityPL PL = new CommunityPL();
                PL.OpCode = 8;
                foreach (ListViewItem item in lvElements.Items)
                {
                    Label lblElementId = (Label)item.FindControl("lblElementsId");
                    Label lblElementsName = (Label)item.FindControl("lblElementsName");
                    Label lblDescription = (Label)item.FindControl("lblElementsText");
                    PL.AutoId = mainid;
                    PL.ElementId = lblElementId.Text;
                    PL.Description = lblDescription.Text;
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    CommunityDL.returnTable(PL);
                }
            }
        }
        void SaveDistanceNearBy(string mainid)
        {
            if (!string.IsNullOrEmpty(mainid))
            {
                CommunityPL PL = new CommunityPL();
                PL.OpCode = 23;
                foreach (ListViewItem item in LV_DistanceNearBy.Items)
                {
                    Label lblPlaceId = (Label)item.FindControl("lblPlaceId");
                    Label lblPlaceName = (Label)item.FindControl("lblPlaceName");
                    Label lblPlaceDistance = (Label)item.FindControl("lblPlaceDistance");
                    PL.AutoId = mainid;
                    PL.PlaceNmae = lblPlaceName.Text;
                    PL.PlaceDistance = lblPlaceDistance.Text;
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    CommunityDL.returnTable(PL);
                }
            }
        }
        void EditAction(string unitId)
        {
            if (!string.IsNullOrEmpty(unitId))
            {
                CommunityPL PL = new CommunityPL();
                PL.OpCode = 18;
                var XML = "<tbl>";
                foreach (ListViewItem item in lvElements.Items)
                {
                    Label lblElementId = (Label)item.FindControl("lblElementsId");
                    Label lblElementsName = (Label)item.FindControl("lblElementsName");
                    Label lblDescription = (Label)item.FindControl("lblElementsText"); 
                    XML += "<tr>";
                    XML += "<UnitId><![CDATA[" + unitId + "]]></UnitId>";
                    XML += "<ElementId><![CDATA[" + lblElementId.Text + "]]></ElementId>";
                    XML += "<Details><![CDATA[" + lblDescription.Text + "]]></Details>";
                    XML += "</tr>";
                }

                PL.AutoId = unitId;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                XML += "</tbl>";
                PL.XML = XML;
                CommunityDL.returnTable(PL);
            }
        }
        void EditDistanceNearBy(string unitId)
        {
            if (!string.IsNullOrEmpty(unitId))
            {
                CommunityPL PL = new CommunityPL();
                PL.OpCode = 22;
                var XML = "<tbl>";
                foreach (ListViewItem item in LV_DistanceNearBy.Items)
                {
                    Label lblPlaceId = (Label)item.FindControl("lblPlaceId");
                    Label lblPlaceName = (Label)item.FindControl("lblPlaceName");
                    Label lblPlaceDistance = (Label)item.FindControl("lblPlaceDistance"); 
                    XML += "<tr>";
                    XML += "<UnitId><![CDATA[" + unitId + "]]></UnitId>"; 
                    XML += "<PlaceNearById><![CDATA[" + lblPlaceId.Text + "]]></PlaceNearById>";
                    XML += "<DistanceMin><![CDATA[" + lblPlaceDistance.Text + "]]></DistanceMin>";
                    XML += "</tr>";
                }
                PL.AutoId = unitId;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                XML += "</tbl>";
                PL.XML = XML;
                CommunityDL.returnTable(PL);
            } 
        } 
        void SaveVideo(int unitId)
        {
            CommunityPL PL = new CommunityPL();
            PL.CreatedBy = Session["UserAutoId"].ToString();
            PL.OpCode = 10;
            PL.AutoId = unitId;
            PL.pathType = "Video";
            PL.XML = XMLVideo(unitId);
            CommunityDL.returnTable(PL);
        }
        private string XMLVideo(int unitId)
        {
            string PathType = "Video";
            var XML = "<tbl>";
            XML += "<tr>";
            XML += "<UnitId><![CDATA[" + unitId + "]]></UnitId>";
            XML += "<path><![CDATA[" + VideoUpload(fileVideoUpload , unitId) + "]]></path>";
            XML += "<PathType><![CDATA[" + PathType + "]]></PathType>";
            XML += "</tr>";
            XML += "</tbl>";
            return XML;
        }
        public string VideoUpload(FileUpload filevideo , int unitId)
        {
            string savePath = "";
            string fileName = "";
            string strfilePath = "";
            if (filevideo.HasFile)
            {
                // Check for video file type (optional but recommended)
                string extension = Path.GetExtension(filevideo.FileName).ToLower();
                string[] allowedExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" };

                if (System.IO.Directory.Exists(Server.MapPath("~/UploadedVideos/")) == false)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadedVideos/"));

                string filepath = "UploadedVideos/";

                if (allowedExtensions.Contains(extension))
                {
                    try
                    {
                        fileName = Path.GetFileName(filevideo.FileName);
                        savePath = Server.MapPath("~/UploadedVideos/") + fileName; 
                        string[] ArrFName = fileName.Split('.');
                        string strfilename = string.Empty;
                        strfilename = unitId + "." + ArrFName[ArrFName.Length - 1];
                        strfilePath = filepath + strfilename;

                        if (File.Exists(Server.MapPath("~/" + strfilePath)))
                        {
                            try
                            {
                                File.Delete(Server.MapPath("~/" + strfilePath));
                            }
                            catch (Exception ex)
                            {
                                //Do something
                            }
                        }

                        // Save the video file
                        filevideo.SaveAs(Server.MapPath("~/" + strfilePath));

                        //lblMessage.Text = "Upload successful!";
                        //lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError("+ ex.Message + ");", true);
                        //lblMessage.Text = "Error: " + ex.Message;
                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Only video files are allowed.');", true);
                    //lblMessage.Text = "Only video files are allowed.";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Please select a video file.');", true);
                // lblMessage.Text = "Please select a video file.";
            }
            return "~/" + strfilePath;
        }
        void saveAmenties(int unitId)
        {
            string AmentiesId = Request.Form[lstAmenties.UniqueID];
            if (AmentiesId != null)
            {
                var query = from val in AmentiesId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLAmenitiesType(unitId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 16;
                PL.XML = XML;
                PL.AutoId = unitId;
                CommunityDL.returnTable(PL);
            }
        }
        void EditAmenties(int unitId)
        {
            string AmentiesId = Request.Form[lstAmenties.UniqueID];
            if (AmentiesId != null)
            {
                var query = from val in AmentiesId.Split(',')
                            select int.Parse(val);
                string XML = "";
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLAmenitiesType(unitId, num);
                }
                XML += "</tbl>";
                CommunityPL PL = new CommunityPL();
                PL.CreatedBy = Session["UserAutoId"].ToString(); ;
                PL.OpCode = 17;
                PL.XML = XML;
                PL.AutoId = unitId;
                CommunityDL.returnTable(PL);
            }
        }
        private static string XMLAmenitiesType(int unitid, int amentiesId)
        {
            string XML = "<tr>";
            XML += "<UnitId><![CDATA[" + unitid + "]]></UnitId>";
            XML += "<AmentiesId><![CDATA[" + amentiesId + "]]></AmentiesId>"; 
            XML += "</tr>";
            return XML;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        { 
            CommunityPL PL = new CommunityPL();
            if (lvElements.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Kindly add atleast one Elements');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "getfocus();", true);
                ddlUnitElements.Focus();
                return;
            }
            var xml = "<tbl>";
            xml += "<tr>";

            xml += "<CommunityId><![CDATA[" + ddlCommunity.SelectedValue + "]]></CommunityId>";
            xml += "<TowerId><![CDATA[" + ddlTower.SelectedValue + "]]></TowerId>";
            xml += "<PropertyNo><![CDATA[" + txtPropertyNo.Text + "]]></PropertyNo>";
            xml += "<UnitType><![CDATA[" + ddlUnitType.SelectedValue + "]]></UnitType>";
            xml += "<Area><![CDATA[" + ddlArea.SelectedValue + "]]></Area>";
            xml += "<Status><![CDATA[" + ddlPropertyStatus.SelectedValue + "]]></Status>";


            xml += "<Purpose><![CDATA[" + ddlPurpose.SelectedValue + "]]></Purpose>";
            xml += "<Classification><![CDATA[" + ddlClassification.SelectedValue + "]]></Classification>";
            xml += "<UnitMetaTitle><![CDATA[" + txtMetaTitle.Text + "]]></UnitMetaTitle>";
            xml += "<UnitMetaDescription><![CDATA[" + txtMetaDescription.Text + "]]></UnitMetaDescription>";
            xml += "<ContactSellerId><![CDATA[" + ddlSeller.SelectedValue + "]]></ContactSellerId>";
            xml += "<Price><![CDATA[" + txtPrice.Text + "]]></Price>";
            xml += "<UnitSize><![CDATA[" + txtSize.Text + "]]></UnitSize>";
            xml += "<Bedrooms><![CDATA[" + ddlBedroom.SelectedValue + "]]></Bedrooms>";
            xml += "<Bathrooms><![CDATA[" + ddlBathroom.SelectedValue + "]]></Bathrooms>";
            xml += "<UnitTitle><![CDATA[" + txtTittle.Text + "]]></UnitTitle>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "<UnitDescription><![CDATA[" + chkDescription.InnerText + "]]></UnitDescription>";
            xml += "</tr>";
            xml += "</tbl>";

            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString();

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 7;
                CommunityDL.returnTable(PL);
                SaveAction(PL.dt.Rows[0]["UnitId"].ToString());
                SaveDistanceNearBy(PL.dt.Rows[0]["UnitId"].ToString());
                saveAmenties(Convert.ToInt32(PL.dt.Rows[0]["UnitId"].ToString()));
                uploadMultipleFile(Convert.ToInt32(PL.dt.Rows[0]["UnitId"].ToString()));
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 9;
                PL.AutoId = hidUnitId.Value;
                CommunityDL.returnTable(PL);
                EditAction(hidUnitId.Value);
                EditDistanceNearBy(hidUnitId.Value);
                EditAmenties(Convert.ToInt32(hidUnitId.Value));
                SaveVideo(Convert.ToInt32(hidUnitId.Value));
                uploadMultipleFile(Convert.ToInt32(hidUnitId.Value)); 
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
        private void BindGrid(int unitId)
        { 
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 27;
            PL.AutoId = unitId;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                GridView1.DataSource = PL.dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
            }
        } 
        private void uploadMultipleFile(int unitId)
        { 
            if (FileUpload1.HasFiles)
            {
                CommunityPL PL = new CommunityPL();
                PL.OpCode = 19;

                foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                {
                    string folderPath = Server.MapPath("~/UnitImages/");
                    if (!System.IO.Directory.Exists(folderPath))
                        System.IO.Directory.CreateDirectory(folderPath);

                    string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    string filePath = folderPath + fileName;

                    uploadedFile.SaveAs(filePath);

                    PL.AutoId = unitId;
                    PL.path = "UnitImages/" + fileName; 
                    PL.pathType = "Image"; 
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    CommunityDL.returnTable(PL); 
                }

                lblMessage.Text = "Images uploaded successfully."; 
            }
            else
            {
                lblMessage.Text = "Please select image(s).";
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);

            if (e.CommandName == "DeleteImage")
            {
                DeleteImage(id);
            }
            else if (e.CommandName == "EditImage")
            {
                hidImageId.Value = id.ToString();
                LoadImage(id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpImage();", true); 
            }
        }
        private void LoadImage(int autoId)
        { 
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 28;
            PL.AutoId = autoId;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["FilePath"].ToString()))
                {
                    ltrUpdateImage.Text = "<a target='_blank' class='' href='" + ResolveUrl("~/" + dt.Rows[0]["FilePath"].ToString()) + "'>View</a>";
                }
                //imgPreview.ImageUrl = "~/" + PL.dt.Rows[0]["FilePath"].ToString(); 
            }
            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
            }
        }
        private void DeleteImage(int autoId)
        { 
            CommunityPL PL = new CommunityPL();
            PL.OpCode = 20; 
            PL.AutoId = autoId; 
            CommunityDL.returnTable(PL);

            lblMessage.Text = "Image deleted successfully.";
            BindGrid(Convert.ToInt32(hidUnitId.Value));
        }
        protected void btnUpdateImage_Click(object sender, EventArgs e)
        {
            if (FileUploadEdit.HasFile)
            {
                string oldPath = null; 

                GetCommonPL PL = new GetCommonPL();
                PL.OpCode = 28;
                PL.AutoId = hidImageId.Value;
                GetCommonDL.returnTable(PL);
                DataTable dt = PL.dt;
                if (PL.dt.Rows.Count > 0)
                {
                    oldPath = "~/" + PL.dt.Rows[0]["FilePath"].ToString();
                }
                else
                { 

                }

                if (!string.IsNullOrEmpty(oldPath))
                {
                    string oldFile = Server.MapPath("~/" + oldPath);
                    if (File.Exists(oldFile))
                        File.Delete(oldFile);
                }

                string newFileName = Guid.NewGuid() + Path.GetExtension(FileUploadEdit.FileName);
                string folder = Server.MapPath("~/UnitImages/");
                string newFilePath = folder + newFileName;
                FileUploadEdit.SaveAs(newFilePath); 

                CommunityPL PL2 = new CommunityPL();
                PL2.OpCode = 21;
                PL2.path = "UnitImages/" + newFileName;
                PL2.AutoId = hidImageId.Value;
                CommunityDL.returnTable(PL2);

                //lblMsg.Text = "Image updated.";
                LoadImage(Convert.ToInt32(hidImageId.Value));
                BindGrid(Convert.ToInt32(hidUnitId.Value)); 
            }
        }
    }
}