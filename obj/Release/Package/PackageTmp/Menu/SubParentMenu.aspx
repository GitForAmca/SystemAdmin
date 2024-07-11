<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="SubParentMenu.aspx.cs" Inherits="SystemAdmin.Menu.SubParentMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Sub Parent Menu"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Type<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlMenuTypeFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Parent Menu<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlParentMenuFilter" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2ddl">
                            <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Is Default<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlDefault" runat="server" CssClass="form-control select2ddl">
                            <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 pull-right text-right">
                    <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
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
                            <li>
                                <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnDelete_Click"><i class="fa fa-trash"></i>Delete</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_ParentMenu" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Sub Parent Name</th>
                                        <th>Parent Name</th>
                                        <th>Type</th>
                                        <th>Is Default</th>
                                        <th>Is Active</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" class="checkboxes" runat="server" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <%# Eval("SubParentMenuName") %>
                                </td>
                                <td>
                                    <%# Eval("ParentMenuName") %>
                                </td>
                                <td>
                                    <%# Eval("MenuType") %>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsDefault").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsDefault").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlMenuType" class="form-control select2ddl req" OnSelectedIndexChanged="ddlMenuType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Parent Menu<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlParentMenu" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtSubParentMenuName" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="col-md-1 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Default</label>
                            <asp:CheckBox ID="chkDefault" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-actions right">
                <div class="row">
                    <div class="col-md-12"> 
                        <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Add" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
</asp:Content>
 