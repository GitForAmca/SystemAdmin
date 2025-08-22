<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="NearByPlaces.aspx.cs" Inherits="AMCAPropertiesAdmin.Admin.NearByPlaces" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Nearby Places"></asp:Label>
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
                                <label class="control-label">Place<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList ID="ddlPlaceFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
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
                    <asp:ListView ID="LV_Places" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Place</th> 
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
                                    <%# Eval("PlaceName") %>
                                </td>  
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Place</th> 
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
                            <label class="control-label">Place<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtPlace" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnSave" runat="server" class="btn blue" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
    <asp:HiddenField ID="hidPlaceId" runat="server" Value="" />
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

