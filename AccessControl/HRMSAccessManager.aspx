<%@ Page Language="C#"   MasterPageFile="~/MasterPage/MainMaster.master" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="HRMSAccessManager.aspx.cs" Inherits="SystemAdmin.AccessControl.HRMSAccessManager" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="HRMS Data Access Control"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                        <asp:DropDownList ID="ddl_Employeesearch" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <div class="btn-group pull-right" style="margin-left: 8px;">
                            <button class="btn dropdown-toggle" data-toggle="dropdown">
                                Action <i class="fa fa-angle-down"></i>
                            </button>
                            <ul class="dropdown-menu pull-right">
                                <li>
                                    <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxes1');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Portal_Access_Mapping" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr class="bgprimary">
                                        <th>#</th>
                                        <th>Employee</th>
                                        <th>Access Element</th>
                                        <th>Group</th>
                                        <th>Menus</th>
                                        <th>Start Date</th>
                                        <th>End Date</th>
                                        <th>Created</th>
                                        <th>Updated</th>
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
                                    <asp:HiddenField ID="hidAutoId" runat="server" Value='<%# Eval("Autoid")%>' />
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes1 chkselect" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <%# Eval("EmpName")%>
                                </td>
                                <td>
                                    <%# Eval("Scope")%>
                                </td>
                                <td>
                                   <%# Eval("GroupName")%> 
                                </td>
                                <td>
                                    <%# Eval("Menus")%> 
                                </td>
                                <td>
                                    <%# Eval("StartDate")%>
                                </td>
                                <td style='<%# 
                       Convert.ToDateTime(Eval("EndDate")).Date < DateTime.Now.Date ? "color:red;": Convert.ToDateTime(Eval("EndDate")).Date == DateTime.Now.Date ? "color:orange;" : 
                       "" %>'>
                                    <%# Eval("EndDate", "{0:yyyy-MM-dd}") %>
                                </td>
                                <td>
                                    <%# Eval("CreatedBy")%><br />
                                     <%# Eval("CreatedOn")%>
                                </td>
                                <td>
                                   <%# Eval("UpdatedBy")%><br />
                                   <%# Eval("UpdatedOn")%>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    
                                    <asp:LinkButton ID="lnk_view" runat="server" OnClick="lnk_view_Click" CssClass="btn btn-xs blue" ToolTip="Click here to view details" ><i class="fa fa fa-eye"></i></asp:LinkButton>
                                    <%--<asp:Button ID="btnViewAction" CssClass="btn btn-xs blue" runat="server" Text="View " OnClick="btnViewAction_Click" />--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>Employee</th>
                                        <th>Access Element</th>
                                        <th>Start Date</th>
                                        <th>End Date</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
                                        <th>Is Active</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Employee <span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlEmployeeName" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged" AutoPostBack="true" class="form-control req select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divEmployeeDetails">
                    <div class="col-md-6">
                        <asp:ListView ID="LV_Employee_Menu_Details" runat="server" ItemPlaceholderID="itemplaceholder1">
                            <LayoutTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr style="background: #ddd;">
                                            <th>Department</th>
                                            <th>Sub Department</th>
                                            <th>Designation</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder1" runat="server" />
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
                <div class="row">
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
                <div class="row"> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Access Element<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlScope"  CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> *</span></label>
                             <asp:DropDownList runat="server" ID="ddlGroup" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req select2ddl"></asp:DropDownList> 
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                            <asp:Panel runat="server" ID="pnlIndustry" CssClass="dropdown-container">
                                <button type="button" class="dropdown-button">Choose an item</button>
                                <div class="access-filters">
                                    <div class=" dropdown-menu">
                                        <input type="text" class="dropdown-search" placeholder="Search..." />
                                        <asp:CheckBox ID="chkSelectAllIndustry" ForeColor="Blue" Font-Bold="true"
                                            runat="server" Text="Select All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllIndustry_CheckedChanged" />
                                        <div class="company-items-wrapper">
                                            <asp:CheckBoxList ID="chkactionIndustry"
                                                CssClass="industry-checkboxlist req" AutoPostBack="true" OnSelectedIndexChanged="chkactionIndustry_SelectedIndexChanged"
                                                runat="server" RepeatDirection="Vertical"
                                                DataTextField="Name" DataValueField="Id" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div> 
                   <div class="col-md-3"> 
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:Panel runat="server" ID="pnlRegion" CssClass="dropdown-container">
                                <button type="button" class="dropdown-button">Choose an item</button>
                                <div class="access-filters">
                                    <div class=" dropdown-menu">
                                        <input type="text" class="dropdown-search" placeholder="Search..." />
                                        <asp:CheckBox ID="chkSelectAllRegion" ForeColor="Blue" Font-Bold="true"
                                            runat="server" Text="Select All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllRegion_CheckedChanged" />
                                        <div class="company-items-wrapper">
                                            <asp:CheckBoxList ID="chkactionRegion"
                                                CssClass="region-checkboxlist req" AutoPostBack="true" OnSelectedIndexChanged="chkactionRegion_SelectedIndexChanged"
                                                runat="server" RepeatDirection="Vertical"
                                                DataTextField="CountryName" DataValueField="CountryCode" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>  
                </div>
                <div class="row">
                    <div class="col-md-3">

                        <div class="form-group">
                            <label class="control-label">Organization<span class="required" aria-required="true"> *</span></label>
                            <asp:Panel runat="server" ID="pnlOrganization" CssClass="dropdown-container">
                                <button type="button" class="dropdown-button">Choose an item</button>
                                <div class="access-filters">
                                    <div class=" dropdown-menu">
                                        <input type="text" class="dropdown-search" placeholder="Search..." />
                                        <asp:CheckBox ID="chkSelectAllOrg" ForeColor="Blue" Font-Bold="true"
                                            runat="server" Text="Select All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllOrg_CheckedChanged" />
                                        <div class="company-items-wrapper">
                                            <asp:CheckBoxList ID="chkactionOrg"
                                                CssClass="org-checkboxlist req" AutoPostBack="true" OnSelectedIndexChanged="chkactionOrg_SelectedIndexChanged"
                                                runat="server" RepeatDirection="Vertical"
                                                DataTextField="Name" DataValueField="Autoid" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div> 
                    
                     <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Company<span class="required">*</span></label>
                            <asp:Panel runat="server" ID="pnlCompany" CssClass="dropdown-container">
                                <button type="button" class="dropdown-button">Choose an item</button>
                                <div class="access-filters">
                                    <div class="dropdown-menu">
                                        <input type="text" class="dropdown-search" placeholder="Search..." />
                                        <asp:CheckBox ID="chkselectallcompany" ForeColor="Blue" Font-Bold="true"
                                            runat="server" Text="Select All" AutoPostBack="true"
                                            OnCheckedChanged="chkselectallcompany_CheckedChanged" />
                                        <asp:Panel ID="pnlCompanyItems" runat="server" CssClass="company-items-wrapper company-checkboxlist"> 
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Work Location<span class="required">*</span></label>
                                <asp:Panel runat="server" ID="pnlWorkLocation" CssClass="dropdown-container">
                                    <button type="button" class="dropdown-button">Choose an item</button>
                                    <div class="access-filters">
                                        <div class="dropdown-menu">
                                            <input type="text" class="dropdown-search" placeholder="Search..." />
                                            <asp:CheckBox ID="chkselectallworklocation" ForeColor="Blue" Font-Bold="true"
                                                runat="server" Text="Select All" AutoPostBack="true"
                                                OnCheckedChanged="chkselectallworklocation_CheckedChanged" />
                                            <div class="company-items-wrapper">
                                                <asp:CheckBoxList ID="chkactionWkLocation"
                                                    CssClass="region-checkboxlist req" AutoPostBack="true" OnSelectedIndexChanged="chkactionWkLocation_SelectedIndexChanged"
                                                    runat="server" RepeatDirection="Vertical"
                                                    DataTextField="Name" DataValueField="Id" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel> 
                        </div>
                    </div>
                    <div class="col-md-3" runat="server"  id="div_RM">
                        <div class="form-group">
                            <label class="control-label">Reporting Manager<span class="required" aria-required="true"> *</span></label>
                             <asp:Panel runat="server" ID="PnlreportingManager" CssClass="dropdown-container">
                                 <button type="button" class="dropdown-button">Choose an item</button>
                                 <div class="access-filters">
                                     <div class="dropdown-menu">
                                         <input type="text" class="dropdown-search" placeholder="Search..." />
                                         <asp:CheckBox ID="chkSelectAllRM" ForeColor="Blue" Font-Bold="true"
                                             runat="server" Text="Select All"
                                             AutoPostBack="true" OnCheckedChanged="chkSelectAllRM_CheckedChanged" />
                                         <div class="company-items-wrapper">
                                             <asp:CheckBoxList ID="chkactionRM"
                                                 CssClass="dep-checkboxlist req"
                                                 runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="chkactionRM_SelectedIndexChanged" AutoPostBack="true"
                                                 DataTextField="Department" DataValueField="Id" />
                                         </div>
                                     </div>
                                 </div>
                             </asp:Panel>
                          <%--  <asp:ListBox runat="server" ID="LST_RM" SelectionMode="Multiple" CssClass="form-control select2ddl req"></asp:ListBox>--%>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="div_Department">
                        <div class="form-group">
                            <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                            <asp:Panel runat="server" ID="PnlDepartment" CssClass="dropdown-container">
                                <button type="button" class="dropdown-button">Choose an item</button>
                                <div class="access-filters">
                                    <div class="dropdown-menu">
                                        <input type="text" class="dropdown-search" placeholder="Search..." />
                                        <asp:CheckBox ID="chkSelectAllDep" ForeColor="Blue" Font-Bold="true"
                                            runat="server" Text="Select All"
                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllDep_CheckedChanged" />
                                        <div class="company-items-wrapper">
                                            <asp:CheckBoxList ID="chkDep"
                                                CssClass="dep-checkboxlist req"
                                                runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="chkDep_SelectedIndexChanged" AutoPostBack="true"
                                                DataTextField="Department" DataValueField="Id" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-3" runat="server" visible="false" id="div_Employee">
                        <div class="form-group">
                            <label class="control-label">Employees<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox runat="server" ID="Lst_Employee" SelectionMode="Multiple" CssClass="form-control select2ddl req"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" visible="false" id="div_HOD">
                        <div class="form-group">
                            <label class="control-label">HOD<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox runat="server" ID="Lst_HOD" SelectionMode="Multiple" CssClass="form-control select2ddl req"></asp:ListBox>
                        </div>
                    </div>
                    
                    <div class="col-md-3">
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
                    <div style="display:none;" class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Is Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                </div>
                <div class="row mb-3">
                </div>

                <div class="row" runat="server" id="divEmployeeAccess">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_Access_Menu_Company" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="background: #ddd;">
                                            <th>
                                                <asp:CheckBox ID="chkSelectAll"
                                                    runat="server"
                                                    AutoPostBack="true"
                                                    OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </th>
                                            <th>Child</th>
                                            <th>Sub Parent</th>
                                            <th>Parent</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hidautoid" runat="server" Value='<%# Eval("Autoid")%>' />
                                        <asp:CheckBox ID="chkIsChecked"  runat="server" CssClass="checkboxes" />
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
                                     <td>
                                       <asp:CheckBoxList ID="chkactionMenu" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="ActionName" DataValueField="Autoid" DataSource='<%# GetAction(Eval("Autoid").ToString()) %>' />
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
                        <asp:Button ID="btnSave" runat="server" class="btn blue" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req')  && validateAllSelections();" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>

        <div id="PopUpAction" tabindex="-1" data-width="400" class="modal fade" style="display: none">
            <div class="modal-dialog modal-self-width">
                <div class="modal-content">
                    <div class="modal-header bg-green">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title" style="color: #fff;">View Access</h4>
                    </div>
                    <div class="modal-body"  style="max-height:800px; overflow-y: auto;">
                        <div class="row">
                            <div class="card p-3 shadow-sm"> 
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Group:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblGroup" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Industry:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblIndustry" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Region:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblRegion" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Organization:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblOrganization" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Company:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblCompany" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Location:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblLocation" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Reporting Manager:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblRM" runat="server"></asp:Label></div>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-md-4"><strong>Department:</strong></div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblDepartment" runat="server"></asp:Label></div>
                                </div> 
                                <hr /> 
                            </div> 
                        </div>
                        <div class="row margin-top-20">

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">End Date<span class="required" aria-required="true"> *</span></label>
                                    <div class="input-group date date-picker" data-date-start-date="0d"  data-date-format="dd-M-yyyy">
                                        <asp:TextBox ID="txtEndDateUpdate" runat="server" CssClass="form-control requp" onkeypress="return false"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2 text-right">
                                <label class="control-label"><span class="required" aria-required="true"></span></label>
                                <div class="form-group">
                                    <asp:CheckBox ID="chkActiveUpdate" Checked="true" runat="server"></asp:CheckBox>
                                    <label class="control-label">Is Active</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:ListView ID="LV_Access_Menu_Company_Update" OnItemDataBound="LV_Access_Menu_Company_Update_ItemDataBound" runat="server" ItemPlaceholderID="itemplaceholder">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr style="background: #ddd;">
                                                    <th>Is Active</th>
                                                    <th>Child</th>
                                                    <th>Sub Parent</th>
                                                    <th>Parent</th>
                                                    <th>Action Menu</th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server" />
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hidautoidUpdate" runat="server" Value='<%# Eval("MenuID")%>' />
                                                <asp:CheckBox ID="chkIsCheckedUpdate"  Checked='<%# Eval("MenuActive") %>'  runat="server" CssClass="checkboxes" />
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
                                            <td> 
                                             <asp:CheckBoxList ID="chkactionMenu" class='<%# GetInt(Container.DataItemIndex.ToString()) %>'
                                                runat="server" RepeatDirection="Vertical" /> 
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            
                        </div>
                    </div>
                    <div class="modal-footer">
                         <asp:Button ID="btnUpdateAction" runat="server" class="btn blue" OnClick="btnUpdateAction_Click" OnClientClick="return CheckRequiredField('requp')" Text="Update" />
                        <button type="button" data-dismiss="modal" class="btn">Close</button>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoidMain" runat="server" Value="" />
    <asp:HiddenField ID="hdnIndustry" runat="server" />
    <asp:HiddenField ID="hdnGroup" runat="server" />
    <asp:HiddenField ID="hdnRegion" runat="server" />
    <asp:HiddenField ID="hdnOrganization" runat="server" />
    <asp:HiddenField ID="hdnCompany" runat="server" />
    <asp:HiddenField ID="hdnWorkLocation" runat="server" />


<script>
        $(document).ready(function () {

            $('.dropdown-button').click(function (e) {
                e.preventDefault();
                var menu = $(this).siblings('.access-filters');
                $('.access-filters').not(menu).hide(); 
                menu.toggle(); 
            });

            $(document).on('click', function () {
                $('.dropdown-container').removeClass('open');
            });

            $(document).click(function (e) {
                if (!$(e.target).closest('.dropdown-container').length) {
                    $('.access-filters').hide();
                }
            });

            $('.dropdown-search').on('keyup', function () {
                var searchText = $(this).val().toLowerCase();
                $(this).siblings('.company-items-wrapper').find('label').each(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(searchText) > -1);
                });
            });
          
        });
</script>
    <script> 
        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                updateDropdownButtonText();
            });
        }

        $(document).ready(function () {
            updateDropdownButtonText();
        }); 

        function updateDropdownButtonText() {
            $(".dropdown-container").each(function () {
                const $dropdown = $(this); 
                const selected = $dropdown.find("input[type='checkbox']:checked").map(function () { 
                    let text = $(this).next().text().trim(); 
                    if (!text) { 
                        text = $(this).parent().text().trim();
                    } 
                    if (text.toLowerCase() === "select all") return null;
                    return text;
                }).get(); 
                const btn = $dropdown.find(".dropdown-button");
                btn.text(selected.length ? selected.join(", ") : "Choose an item");
            });
        }

        function validateAllSelections() {
            debugger
            var isValid = true;

            // Helper function to check one panel
            function checkPanel(panelId, message) {
                var panel = document.getElementById(panelId);
                if (!panel) return true; // skip if not found
                var dropdownButton = panel.querySelector(".dropdown-button");
                var checkboxes = panel.querySelectorAll(".company-items-wrapper input[type='checkbox']");
                var anyChecked = Array.from(checkboxes).some(cb => cb.checked);

                if (!anyChecked) {
                    ShowWarning(message); // or alert(message)
                    if (dropdownButton) dropdownButton.classList.add("error-outline");
                    isValid = false;
                } else {
                    if (dropdownButton) dropdownButton.classList.remove("error-outline");
                }
            }

            // Validate each panel
            checkPanel('<%= pnlIndustry.ClientID %>', 'Please select at least one Industry.');
            checkPanel('<%= pnlRegion.ClientID %>', 'Please select at least one Region.');
            checkPanel('<%= pnlOrganization.ClientID %>', 'Please select at least one Organization.');
            checkPanel('<%= pnlCompany.ClientID %>', 'Please select at least one Company.');
            checkPanel('<%= pnlWorkLocation.ClientID %>', 'Please select at least one Work Location.');
            checkPanel('<%= PnlreportingManager.ClientID %>', 'Please select at least one Reporting Manager.');
            checkPanel('<%= PnlDepartment.ClientID %>', 'Please select at least one Department.'); 
            return isValid;
        }

    </script>
    <script>
        function OpenPopUpAction() {
            $("#PopUpAction").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
    <style>
          .error-outline {
      border: 1px solid red !important;
      border-radius: 4px !important;
      padding: 5px !important;
  } 

        .btn-access {
            font-weight: 600;
            letter-spacing: 0.5px;
            transition: transform 0.2s, box-shadow 0.2s;
        }

            .btn-access:hover {
                transform: translateY(-2px);
                box-shadow: 0 4px 10px rgba(0,0,0,0.2);
            }

        .dropdown-container {
            position: relative;
            display: inline-block;
            width: 100%;
            font-family: Arial, sans-serif;
        }

        .dropdown-button {
            width: 100%;
            background-color: #fff;
            border: 1px solid #ccc;
            padding: 6px 10px;
            text-align: left;
            border-radius: 6px;
            cursor: pointer;
            position: relative;
            font-size: 13px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
           
        }

            .dropdown-button::after {
                content: "▼";
                position: absolute;
                right: 10px;
                top: 6px;
                font-size: 10px;
                color: #555;
            }

        .dropdown-container.open .dropdown-button::after {
            content: "▲";
        }

        .access-filters .dropdown-menu {
            display: block;
            position: absolute;
            background-color: #fff;
            border: 1px solid #ccc;
            box-shadow: 0 2px 6px rgba(0,0,0,0.12);
            z-index: 10;
            width: 100%;
            max-height: 200px;
            overflow-y: auto;
            padding: 8px;
            border-radius: 6px;
        }

        .dropdown-container.open .access-filters .dropdown-menu {
            display: block;
        }

        .access-filters {
            display: none;
        }

            .access-filters.show {
                display: block;
            }

        .company-checkboxlist label {
            display: inline;
            padding: 3px 0;
            font-size: 13px;
        }

        .dropdown-button {
            max-width: 100%;
        }

        .access-filters .dropdown-search {
            width: 100%;
            padding: 4px 6px;
            margin-bottom: 6px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 13px;
            box-sizing: border-box;
        }

        .modal {
            z-index: 1050 !important;
        }

        .modal-backdrop {
            border: 0;
            outline: none;
            z-index: 1000 !important;
        }
        .shadow-sm
        {
            padding-left : 15px;
            
        }
        .group-heading {
    font-weight: bold;
    margin-top: 10px;
    color: #155724; /* dark green for group heading */
}
.industry-item {
    margin-left: 15px; /* indent under group */
}

    </style>
</asp:Content>
