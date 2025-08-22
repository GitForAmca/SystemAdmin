<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="UnitsMaster.aspx.cs" Inherits="AMCAPropertiesAdmin.Admin.UnitsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Units Master"></asp:Label>
            </div>
        </div>

        <div id="divView" runat="server" class="portlet-body form-body">

            <div class="col-md-12">
                <div class="pull-right">
                    <div class="btn-group">
                        <button class="btn dropdown-toggle" id="toggleFilter" data-toggle="dropdown">
                            <i class="fa fa-filter" aria-hidden="true"></i>Filter <i class="fa fa-angle-down"></i>
                        </button>
                    </div>
                    <div class="btn-group pull-right" style="margin-left: 8px;">
                        <button class="btn dropdown-toggle" data-toggle="dropdown">
                            Action <i class="fa fa-angle-down"></i>
                        </button>
                        <ul class="dropdown-menu pull-right">
                            <li>
                                <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxes');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                            </li> 
                        </ul>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12" id="divFilter" style="display: none;">
                    <div class="row filterpopup"> 
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Community<span class="required" aria-required="true"> </span></label>
                                    <asp:DropDownList ID="ddlCommunityFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Tower</label>
                                    <asp:DropDownList ID="ddlTowerFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Area</label>
                                    <asp:DropDownList ID="ddlAreaFilter" class="form-control multiselectddl" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Unit Type<span class="required" aria-required="true"> </span></label>
                                    <asp:DropDownList ID="ddlUnitTypeFilter" class="form-control multiselectddl req" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Status<span class="required" aria-required="true"> </span></label>
                                    <asp:DropDownList ID="ddlPropertyStatusFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Purpose<span class="required" aria-required="true"> </span></label>
                                    <asp:DropDownList ID="ddlPurposeFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label">Classification<span class="required" aria-required="true"> </span></label>
                                    <asp:DropDownList ID="ddlClassificationFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                                </div>
                            </div>  
                            <div class="col-md-2 pull-right text-right">
                                <label class="control-label"><span class="required" aria-required="true"></span></label>
                                <div>
                                    <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                                </div>
                            </div> 
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Units" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Community Name</th>
                                        <th>Tower</th>
                                        <th>Area</th>
                                        <th>Unit Type</th>
                                        <th>Property No</th>
                                        <th>Status</th>
                                        <th>IsActive</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" class="checkboxes" runat="server" Autoid='<%# Eval("AutoId")%>' />
                                </td>
                                <td>
                                    <%# Eval("CommunityName") %>
                                </td>
                                <td>
                                    <%# Eval("TowerName") %>
                                </td>
                                <td>
                                    <%# Eval("Area") %>
                                </td>
                                <td>
                                    <%# Eval("UnitType") %>
                                </td>
                                <td>
                                    <%# Eval("PropertyNo") %>
                                </td>
                                <td>
                                    <%# Eval("Status") %>
                                </td>
                                <td style="width: 5%">
                                    <span class='<%# Eval("IsActive").ToString() =="True"?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%#  Eval("IsActive").ToString() =="False"?"InActive":"Active"%></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Community Name</th>
                                        <th>Tower</th>
                                        <th>Area</th>
                                        <th>Unit Type</th>
                                        <th>Property No</th>
                                        <th>Status</th>
                                        <th>IsActive</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>

        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Community</label>
                            <asp:DropDownList ID="ddlCommunity" OnSelectedIndexChanged="ddlCommunity_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divTower" visible="false">
                        <div class="form-group">
                            <label class="control-label">Tower<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlTower" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Property No<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtPropertyNo" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Unit Type<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlUnitType" class="form-control multiselectddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Area</label>
                            <asp:DropDownList ID="ddlArea" class="form-control multiselectddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Status<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlPropertyStatus" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Purpose<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlPurpose" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Classification<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlClassification" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Meta Tittle<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtMetaTitle" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Meta Description<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtMetaDescription" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Contact Seller<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlSeller" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Price<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtPrice" class="form-control mobile restrictZero req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Size<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtSize" class="form-control mobile restrictZero  req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Bedroom<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlBedroom" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Bathroom<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlBathroom" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Tittle<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtTittle" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Amenties<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="lstAmenties" SelectionMode="Multiple" class="form-control multiselectddl req" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 30px;">
                        <div class="form-group">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>

                    <div class="col-md-12">
                        <h3 class="section-title">Description</h3>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <textarea class="ckeditor" rows="20" runat="server" id="chkDescription"></textarea>
                        </div>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>

                    <div class="col-md-12">
                        <h3 class="section-title">Property Details</h3>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Unit Elements<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList ID="ddlUnitElements" class="form-control select2ddl req2" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label" id="lblUnitElements" runat="server">Description</label>
                            <asp:TextBox ID="txtUnitElements" class="form-control req2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1 text-right">
                        <label class="control-label">&nbsp;</label>
                        <div>
                            <asp:Button ID="btnAddElements" CssClass="btn blue" runat="server" Text="Add Elements" OnClientClick="return CheckRequiredField('req2');" OnClick="btnAddElements_Click" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:ListView ID="lvElements" runat="server" ItemPlaceholderID="plchldr">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="background: #f2f2f2">
                                            <th>Elements Name</th>
                                            <th>Description</th>
                                        </tr>
                                    </thead>
                                    <tr id="plchldr" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="display: none;">
                                        <asp:Label ID="lblElementsId" runat="server" Text='<%# Eval("ElementsId") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblElementsName" runat="server" Text='<%# Eval("ElementsName") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblElementsText" runat="server" Text='<%# Eval("ElementsText") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>

                    <div class="col-md-12">
                        <h3 class="section-title">Distance from Near By Places</h3>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Places<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList ID="ddlplaces" class="form-control select2ddl req3" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label" id="Label1" runat="server">Distance</label>
                            <asp:TextBox ID="txtDistance" class="form-control req3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1 text-right">
                        <label class="control-label">&nbsp;</label>
                        <div>
                            <asp:Button ID="btnPlace" CssClass="btn blue" runat="server" Text="Add Place" OnClientClick="return CheckRequiredField('req3');" OnClick="btnPlace_Click" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:ListView ID="LV_DistanceNearBy" runat="server" ItemPlaceholderID="plchldr">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="background: #f2f2f2">
                                            <th>Places</th>
                                            <th>Distance</th>
                                        </tr>
                                    </thead>
                                    <tr id="plchldr" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="display: none;">
                                        <asp:Label ID="lblPlaceId" runat="server" Text='<%# Eval("PlaceId") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPlaceName" runat="server" Text='<%# Eval("PlaceName") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPlaceDistance" runat="server" Text='<%# Eval("PlaceDistance") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>

                    <div class="col-md-12">
                        <h3 class="section-title">Upload Images</h3>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                    <div class="input-group input-small">
                                        <div class="form-control uneditable-input input-fixed input-small" data-trigger="fileinput">
                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                        </div>
                                        <span class="input-group-addon btn btn-xs default btn-file">
                                            <span class="fileinput-new">Select </span>
                                            <span class="fileinput-exists">Change</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" onchange="ValidatePngorJpg(this);" />
                                        </span>
                                        <a href="javascript:;" class="input-group-addon btn btn-xs red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <%-- <asp:GridView class="table table-bordered table-hover" style="background:#FFFFFF"  ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"><HeaderStyle BackColor="#f2f2f2" ForeColor="White" />--%>
                            <asp:GridView class="table table-bordered table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:BoundField Visible="false" DataField="Id" HeaderText="ID" />
                                    <asp:ImageField DataImageUrlField="FilePath" HeaderText="Image" ControlStyle-Width="100px" />
                                    <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" /> 
                                    <asp:ButtonField HeaderText="Edit" CommandName="EditImage" Text="Edit" ButtonType="Button" />
                                    <asp:ButtonField HeaderText="Delete" CommandName="DeleteImage" Text="Delete" ButtonType="Button" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                        </div>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>

                    <div class="col-md-12">
                        <h3 class="section-title">Upload Video</h3>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Videos</label>
                        <div class="form-group">
                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                <div class="input-group input-small">
                                    <div class="form-control uneditable-input input-fixed input-small" data-trigger="fileinput">
                                        <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                    </div>
                                    <span class="input-group-addon btn btn-xs default btn-file">
                                        <span class="fileinput-new">Select </span>
                                        <span class="fileinput-exists">Change</span>
                                        <asp:FileUpload ID="fileVideoUpload" CssClass="reqpop2" runat="server" onchange="ValidateOnlyVideo(this);" />
                                    </span>
                                    <a href="javascript:;" class="input-group-addon btn btn-xs red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                </div>
                            </div>
                        </div>
                        <asp:Literal ID="ltrVideo" runat="server"></asp:Literal>
                    </div>

                    <%--------------------------------------------------------------------------------------------------------------------------------%>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div id="PopUpImage" tabindex="-1" data-width="400" class="modal fade" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" style="color: #fff;">Update Image</h4>
                </div>
                <div class="modal-body">
                    <div class="form form-horizontal">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%-- <asp:Image ID="imgPreview" runat="server" Width="150px" /><br />--%>
                                    <br />
                                    <div class="form-group">
                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                            <div class="input-group input-small">
                                                <div class="form-control uneditable-input input-fixed input-small" data-trigger="fileinput">
                                                    <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-xs default btn-file">
                                                    <span class="fileinput-new">Select </span>
                                                    <span class="fileinput-exists">Change</span>
                                                    <asp:FileUpload ID="FileUploadEdit" CssClass="reqpop2" runat="server" onchange="ValidatePngorJpg(this);" />
                                                </span>
                                                <a href="javascript:;" class="input-group-addon btn btn-xs red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                            </div>
                                        </div>
                                        <asp:Literal ID="ltrUpdateImage" runat="server"></asp:Literal>
                                    </div>
                                    <%-- <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />--%>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group pull-right">
                                        <asp:Button ID="btnUpdateImage" runat="server" class="btn blue" OnClick="btnUpdateImage_Click" Text="Update" />
                                        <button type="button" data-dismiss="modal" class="btn">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
    <asp:HiddenField ID="hidUnitId" runat="server" Value="" />
    <asp:HiddenField ID="hidImageId" runat="server" Value="" />
    <script>
        function OpenPopUpImage() {
            $("#PopUpImage").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
    <script>
        function ValidatePngorJpg(input) {
            var allowedExtensions = /(\.png|\.jpg)$/i;
            if (!allowedExtensions.exec(input.value)) {
                ShowError('Please upload only .png/.jpg only.');
                input.value = '';
                return false;
            }
        }
        function ValidateOnlyVideo(input) {
            var allowedExtensions = /(\.mp4|\.mov)$/i;
            if (!allowedExtensions.exec(input.value)) {
                ShowError('Please upload only .mp4/.mov only.');
                input.value = '';
                return false;
            }
        }
        function getfocus() {
            var ddl = document.getElementById('<%= ddlUnitElements.ClientID %>');
            if (!ddl.value) {  // if value is empty
                ddl.focus();
                return false;  // prevent postback if you want
            }
            return true; // allow postback if needed
        }
    </script>
    <script>
        $("#toggleFilter").click(function () {
            $("#divFilter").slideToggle(5);
        });
        $("#toggleAnalytics").click(function () {
            $("#divAnalytics").slideToggle(5);
        });
    </script>
    <style>
        .filterpopup {
            display: block;
            position: absolute;
            background: rgb(255, 255, 255);
            z-index: 9;
            width: 98.4%;
            border: 1px solid rgb(221, 221, 221);
            padding: 13px 0px;
            top: 4px;
            left: 30px;
            box-shadow: 4px 2px 24px -3px #ccc;
            border-radius: 5px;
        }
    </style>
</asp:Content>

