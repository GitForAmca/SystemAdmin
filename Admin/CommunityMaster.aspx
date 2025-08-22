<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="CommunityMaster.aspx.cs" Inherits="AMCAPropertiesAdmin.Admin.CommunityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Community Master"></asp:Label>
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
                                <label class="control-label">Developers<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList ID="ddlDevelopersFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Emirates<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList ID="ddlEmiratesFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Area<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList ID="ddlAreaFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Status</label>
                                <asp:DropDownList ID="ddlStatusFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
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
                    <asp:ListView ID="LV_Community" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Community Name</th>
                                        <th>Developers</th>
                                        <th>Emirates</th>
                                        <th>Area</th>
                                        <th>UnitType</th>
                                        <th>Bedrooms</th>
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
                                    <%# Eval("Developers") %>
                                </td>
                                <td>
                                    <%# Eval("EmiratesName") %>
                                </td>
                                <td>
                                    <%# Eval("Area") %>
                                </td>
                                <td>
                                    <%# Eval("UnitType") %>
                                </td>
                                <td>
                                    <%# Eval("Bedrooms") %>
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
                                        <th>Developers</th>
                                        <th>Emirates</th>
                                        <th>Area</th>
                                        <th>UnitType</th>
                                        <th>Bedrooms</th>
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
                            <label class="control-label">Emirates<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlEmirates" OnSelectedIndexChanged="ddlEmirtes_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divddlArea" visible="false">
                        <div class="form-group">
                            <label class="control-label">Area<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlArea" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Developer<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlDevelopers" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Community Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtCommunityName" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Status<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlPropertyStatus" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group"> 
                            <label class="control-label">Bedroom Type<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="lstBedroomType" SelectionMode="Multiple" class="form-control multiselectddl req" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group"> 
                            <label class="control-label">Unit Type<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="lstUnitType" SelectionMode="Multiple" class="form-control multiselectddl req" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 30px;">
                        <div class="form-group">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
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
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
    <asp:HiddenField ID="hidCommunityId" runat="server" Value="" /> 
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

