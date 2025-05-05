<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="OrganizationalAccessControl.aspx.cs" Inherits="SystemAdmin.AccessControl.OrganizationalAccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Organizational Access Control"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Employee Name<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlEmployeeFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Department<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlDepartmentFilter" OnSelectedIndexChanged="ddldepartmentFilter_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Sub Department<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlSubDepartmentFilter"  OnSelectedIndexChanged="ddlSubDepartmentFilter_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Designation<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlDesignationFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <div class="btn-group pull-right" style="margin-left: 8px;"> 
                            <button class="btn dropdown-toggle" data-toggle="dropdown">
                                Action <i class="fa fa-angle-down"></i>
                            </button> 
                            <ul class="dropdown-menu pull-right"> 
                                <li> 
                                    <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxesmain');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                </li> 
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Employee_Access" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr> 
                                        <th>#</th>
                                        <th>Group</th>
                                        <th>Employee</th>
                                        <th>View Action</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hidEmpid" runat="server" Value='<%# Eval("Autoid")%>' />
                                    <asp:HiddenField ID="hidGroupId" runat="server" Value='<%# Eval("GroupId")%>' />
                                    <asp:CheckBox ID="chkSelect" class="checkboxesmain" runat="server" Autoid='<%# Eval("Autoid")%>' GroupId='<%# Eval("GroupId")%>' />
                                </td>
                                <td>
                                    <%# Eval("GroupName") %>
                                </td>
                                <td>
                                    <%# Eval("EmpName") %>
                                </td>
                                <td>
                                     <asp:Button ID="btnViewAction" CssClass="btn btn-xs blue" runat="server" Text="View Action" OnClick="btnViewAction_Click" />
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlEmployeeName" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged" AutoPostBack="true" class="form-control req select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divEmployeeDetails">
                    <div class="col-md-6">
                        <asp:ListView ID="LV_Employee_Menu_Details" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr style="background: #ddd;">
                                            <th>Department</th>
                                            <th>Sub Department</th>
                                            <th>Designation</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("DepartmentName") %>
                                    </td>
                                    <td>
                                        <%# Eval("SubDepartmentName") %>
                                    </td>
                                    <td>
                                        <%# Eval("DesignationName") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row" runat="server" id="divAddGroup" visible="false">
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <div class="col-md-12">
                        <h4><strong>Company List</strong></h4>
                        <asp:CheckBoxList ID="chkGroupCompany" runat="server" RepeatDirection="Horizontal" DataTextField="Name" DataValueField="CompanyId" OnDataBinding="chkGroupCompany_DataBinding" OnSelectedIndexChanged="chkGroupCompany_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="row" runat="server" id="divUpdateGroup" visible="false">
                    <div class="col-md-12">
                        <hr /> 
                    </div>
                    <div class="col-md-3">
                        <h4><strong>Company List</strong></h4>
                        <asp:DropDownList ID="ddlUpdateGroupCompany" OnSelectedIndexChanged="ddlUpdateGroupCompany_SelectedIndexChanged" AutoPostBack="true" class="form-control requp select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
                <div class="row" runat="server" id="divEmployeeAccess">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_Access_Menu_Company" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="background: #ddd;">
                                            <th>Action</th>
                                            <th>Full Page Access</th>
                                            <th>Child</th>
                                            <th>Sub Parent</th>
                                            <th>Parent</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hidautoid" runat="server" Value='<%# Eval("Autoid")%>' />
                                        <asp:CheckBoxList ID="chkaction" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="ActionName" DataValueField="Autoid" DataSource='<%# GetAction(Eval("Autoid").ToString()) %>' />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsFullAccess" runat="server" CssClass="checkboxes" />
                                    </td>
                                    <td>
                                        <%# Eval("MenuName") %>
                                    </td>
                                    <td>
                                        <%# Eval("SubParentMenuName") %>
                                    </td>
                                    <td>
                                        <%# Eval("ParentMenuName") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div class="form-actions right">
                <div class="row">
                    <div class="col-md-12"> 
                        <asp:Button ID="btnSave" runat="server" class="btn blue" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnUpdateAccess" runat="server" class="btn blue" OnClick="btnUpdateAccess_Click" OnClientClick="return CheckRequiredField('requp');" Text="Update" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="PopUpAction" tabindex="-1" data-width="400" class="modal fade" style="display: none">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" style="color: #fff;">Update Action</h4>
                </div>
                <div class="modal-body">
                    <div class="form form-horizontal">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ListView ID="LV_Access_Menu_Company_Update" runat="server" ItemPlaceholderID="itemplaceholder">
                                        <LayoutTemplate>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr style="background: #ddd;">
                                                        <th>Action</th>
                                                        <th>Full Page Access</th>
                                                        <th>Child</th>
                                                        <th>Sub Parent</th>
                                                        <th>Parent</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemplaceholder" runat="server" />
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="hidautoidUpdate" runat="server" Value='<%# Eval("Autoid")%>' />
                                                    <asp:CheckBoxList ID="chkactionUpdate" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="ActionName" DataValueField="Autoid" DataSource='<%# GetAction(Eval("Autoid").ToString()) %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsFullAccess" runat="server" CssClass="checkboxes" />
                                                </td>
                                                <td>
                                                    <%# Eval("MenuName") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SubParentMenuName") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ParentMenuName") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-md-12">
                                    <asp:Button ID="btnUpdateAction" runat="server" class="btn blue" OnClick="btnUpdateAction_Click" Text="Update" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoidMain" runat="server" Value="" />
    <asp:HiddenField ID="hidEmpidMain" runat="server" Value="" />
    <asp:HiddenField ID="hidGroupIdMain" runat="server" Value="" />
    <script>
        function OpenPopUpAction() {
            $("#PopUpAction").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
</asp:Content>
