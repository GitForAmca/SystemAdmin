<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="DataAccess.aspx.cs" Inherits="SystemAdmin.AccessControl.DataAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Data Access"></asp:Label>
                </div>
            </div> 
            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlGroupFilter" CssClass="form-control select2ddl"></asp:DropDownList>
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
                    <div class="col-md-1 pull-right">
                        <div class="form-group">
                            <label class="control-label">&nbsp;</label>
                            <div>
                                <div class="btn-group pull-right">
                                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                                        Action <i class="fa fa-angle-down"></i>
                                    </button>
                                    <ul class="dropdown-menu pull-right">
                                        <li>
                                            <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i> Add</asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                        </li>
                                    </ul> 
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_Portal_Access_Mapping" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Element</th>                                                    
                                            <th>Access Employee</th>                                                    
                                            <th>End Date</th>                                                 
                                            <th>Created By</th>                                                 
                                            <th>Created On</th>                                                 
                                            <th>Is Active</th>                                                 
                                            <th>Action</th>                                                 
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
                                        <asp:HiddenField ID="hidEmpId" runat="server" Value='<%# Eval("EmpId")%>' />
                                        <asp:HiddenField ID="hidElementName" runat="server" Value='<%# Eval("ElementName")%>' />
                                        <asp:HiddenField ID="hidAccessEmpId" runat="server" Value='<%# Eval("AccessEmpId")%>' />
                                        <asp:HiddenField ID="hidGroupId" runat="server" Value='<%# Eval("GroupId")%>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" EmpId='<%# Eval("EmpId")%>' ElementName='<%# Eval("ElementName")%>' AccessEmpId='<%# Eval("AccessEmpId")%>' GroupId='<%# Eval("GroupId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("EmpName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ElementName")%>
                                    </td>
                                    <td>
                                        <%# Eval("AccessEmpName")%>
                                    </td>
                                    <td>
                                        <%# Eval("EndDate")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedBy")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedOn")%>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                    </td>
                                    <td>
                                         <asp:Button ID="btnViewAction" CssClass="btn btn-xs blue" runat="server" Text="View Action" OnClick="btnViewAction_Click" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>CEM</th>
                                            <th>Element</th>                                                    
                                            <th>Access Employee</th>                                                    
                                            <th>End Date</th>                                                 
                                            <th>By</th>                                                 
                                            <th>On</th>                                                 
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
                    <div class="row" runat="server" id="divAddGroup" visible="false">
                        <div class="col-md-12">
                            <h4><strong>Company List</strong></h4>
                            <asp:CheckBoxList ID="chkGroupCompany" runat="server" RepeatDirection="Horizontal" DataTextField="Name" DataValueField="AutoId" OnDataBinding="chkGroupCompany_DataBinding" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="divUpdateGroup" visible="false">
                        <div class="col-md-3">
                            <h4><strong>Company List</strong></h4>
                            <asp:DropDownList ID="ddlUpdateGroupCompany" class="form-control requp select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlEmpName" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divEmployeeDetails">
                        <div class="col-md-12">
                            <asp:ListView ID="LV_Employee_Menu_Details" runat="server" ItemPlaceholderID="itemplaceholder">
                                <LayoutTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr style="background: #ddd;">
                                                <th>Department</th>
                                                <th>Sub Department</th>
                                                <th>Designation</th>
                                                <th>CEM</th>
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
                                        <td>
                                            <%# Eval("CEMName") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Element<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlElement" OnSelectedIndexChanged="ddlElement_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Access Name<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlAccessName" OnSelectedIndexChanged="ddlAccessName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>  
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">End Date<span class="required" aria-required="true"> *</span></label>
                                <div class="input-group date date-picker" data-date-format="dd-M-yyyy">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control req" onkeypress="return false"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                        </div>  
                        <div class="col-md-12 pull-right text-right">
                            <div class="form-group">
                                <label class="control-label">Is Active</label>
                                <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                            </div>
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
                                            <asp:HiddenField ID="hidautoid" runat="server" Value='<%# Eval("ChildMenuid")%>' />
                                            <asp:CheckBox ID="chkIsChecked" Checked="true" runat="server" CssClass="checkboxes" />
                                        </td>
                                        <td>
                                            <%# Eval("MenuName") %>
                                        </td>
                                        <td>
                                            <%# Eval("Pname") %>
                                        </td>
                                        <td>
                                            <%# Eval("PPname") %>
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
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClientClick="return CheckRequiredField('req');" OnClick="btnsave_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
                        </div>
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
                    <h4 class="modal-title" style="color: #fff;">Update Access</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">End Date<span class="required" aria-required="true"> *</span></label>
                                <div class="input-group date date-picker" data-date-format="dd-M-yyyy">
                                    <asp:TextBox ID="txtEndDateUpdate" runat="server" CssClass="form-control requp" onkeypress="return false"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                        </div>  
                        <div class="col-md-2 text-right">
                            <label class="control-label"><span class="required" aria-required="true"> </span></label>
                            <div class="form-group">
                                <asp:CheckBox ID="chkActiveUpdate" Checked="true" runat="server"></asp:CheckBox>
                                <label class="control-label">Is Active</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ListView ID="LV_Access_Menu_Company_Update" runat="server" ItemPlaceholderID="itemplaceholder">
                                <LayoutTemplate>
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr style="background: #ddd;">
                                                <th>Action</th>
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
                                            <asp:HiddenField ID="hidautoidUpdate" runat="server" Value='<%# Eval("ChildMenuid")%>' />
                                            <asp:CheckBox ID="chkIsCheckedUpdate" Checked="true" runat="server" CssClass="checkboxes" />
                                        </td>
                                        <td>
                                            <%# Eval("MenuName") %>
                                        </td>
                                        <td>
                                            <%# Eval("Pname") %>
                                        </td>
                                        <td>
                                            <%# Eval("PPname") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-md-12 text-right">
                            <asp:Button ID="btnUpdateAction" runat="server" class="btn blue" OnClick="btnUpdateAction_Click" OnClientClick="return CheckRequiredField('requp');" Text="Update" />
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidCEM" runat="server" />
    <asp:HiddenField ID="hidEmpIdMain" runat="server" />
    <asp:HiddenField ID="hidElementNameMain" runat="server" />
    <asp:HiddenField ID="hidAccessEmpIdMain" runat="server" />
    <asp:HiddenField ID="hidGroupIdMain" runat="server" />
    <script>
        function OpenPopUpAction() {
            $("#PopUpAction").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
</asp:Content>
