<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="DirectorElementAccess.aspx.cs" Inherits="SystemAdmin.Directors.DirectorElementAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="col-md-12 col-sm-12">
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Director Element Access"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body">
            <div class="row">

                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label">Director<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlDirectorSearch" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label">Element<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList runat="server" ID="ddlElementSearch" OnSelectedIndexChanged="ddlElementSearch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2ddl"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label">Role<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList runat="server" ID="ddlRoleSearch" CssClass="form-control select2ddl"></asp:DropDownList>
                    </div>                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label">Employee<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList runat="server" ID="ddlEmployeeSearch" CssClass="form-control select2ddl"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label">Sub Department<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList runat="server" ID="ddlSubDepartmentSearch" OnSelectedIndexChanged="ddlSubDepartmentSearch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2ddl"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" id="divGroupSearch" runat="server" visible="false">
                    <div class="form-group">
                        <label class="control-label">Group<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlGroupSearch" runat="server" class="form-control select2ddl">
                            <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                            <asp:ListItem Value="B" Text="B"></asp:ListItem>
                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label"></label>
                        <div style="margin-top: 8px;">
                            <asp:Button ID="btnGet" runat="server" CssClass="btn green" Text="Get" OnClick="btnGet_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-2 pull-right">
                    <div class="form-group">
                        <div>
                            <div class="btn-group pull-right">
                                <button class="btn dropdown-toggle" data-toggle="dropdown">
                                    Action <i class="fa fa-angle-down"></i>
                                </button>
                                <ul class="dropdown-menu pull-right">
                                    <li>
                                        <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead class="dtTheme">
                                    <tr>
                                        <th>#</th>
                                        <th>Employee</th>
                                        <th>Element</th>
                                        <th>Role</th>
                                        <th>Sub Department</th>
                                        <th>Director</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
                                        <th>Is Active</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <div id="itemplaceholder" runat="server"></div>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <%# Eval("EmpName")%>
                                </td>
                                <td>
                                    <%# Eval("ElementName")%>
                                </td>
                                <td>
                                    <%# Eval("EARole")%>
                                </td>
                                <td>
                                    <%# Eval("SubDepartment")%>
                                </td>
                                <td>
                                    <%# Eval("DirectorName")%>
                                </td>
                                <td>
                                    <%# Eval("CreatedByName")%>
                                </td>
                                <td>
                                    <%# Eval("CreatedOn")%>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Director</th>
                                        <th>Element</th>
                                        <th>Access To</th>
                                        <th>Sub Department</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
                                        <th>Is Active</th>
                                    </tr>
                                </thead>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divAddEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">                    
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Director<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlDirector" class="form-control req select2ddl" runat="server"></asp:DropDownList>  
                        </div> 
                    </div> 
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Element<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlElement" OnSelectedIndexChanged="ddlElement_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Role<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlRole" CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Employee<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Sub Department<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlSubDepartment" OnSelectedIndexChanged="ddlSubDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div>  
                    <div class="col-md-4" id="divGroup" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlGroup" runat="server" class="form-control req select2ddl">
                                <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                <asp:ListItem Value="C" Text="C"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Is Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClientClick="return CheckRequiredField('req')" OnClick="btnsave_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hidID" runat="server" />
</asp:Content>
