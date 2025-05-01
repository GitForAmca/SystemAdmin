<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="HRMSAccessControl.aspx.cs" Inherits="SystemAdmin.AccessControl.HRMSAccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="HRMS Access Control"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
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
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxes');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
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
                                        <th>Employee Name</th>
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
                                    <%# Eval("EmpName") %>
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
                <div class="row" runat="server" id="divEmployeeAccess">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="upnl_Menuaccess" runat="server" UpdateMode="Conditional">
                            <ContentTemplate> 
                                <asp:ListView ID="LV_Access_Menu_Company" runat="server" ItemPlaceholderID="itemplaceholder2" OnItemDataBound="LV_Hid_Region_Industry_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr style="background: #ddd;">
                                                    <th>Parent</th>
                                                    <th>Sub Parent</th>
                                                    <th>Child</th>
                                                    <th>Region</th>
                                                    <th>Industries</th>
                                                    <th>Company</th>
                                                    <th>Reporting To</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead> 
                                            <tr id="itemplaceholder2" runat="server" />
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hidautoid" runat="server" Value='<%# Eval("Autoid")%>' />
                                                <asp:HiddenField ID="hidIsParentMenu" runat="server" Value='<%# Eval("IsMasterMenu")%>' />
                                                <%# Eval("ParentMenuName") %>
                                            </td>
                                            <td>
                                                <%# Eval("SubParentMenuName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MenuName") %>
                                            </td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlRegion">  
                                                <asp:CheckBox ID="chkSelectAllRegion" ForeColor="Blue" Font-Bold="true" runat="server" Text="Select All"
                                                  AutoPostBack="true" OnCheckedChanged="chkSelectAllRegion_CheckedChanged" /> 
                                                <asp:CheckBoxList ID="chkactionRegion" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' OnSelectedIndexChanged="chkactionRegion_SelectedIndexChanged" AutoPostBack="true"  runat="server" RepeatDirection="Vertical" DataTextField="CountryName" DataValueField="RegionId" DataSource='<%# GetRegionAction(Eval("Autoid").ToString()) %>' /> 
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlIndustry"> 
                                                <asp:CheckBoxList ID="chkactionIndustry" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' OnSelectedIndexChanged="chkactionIndustry_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Vertical" DataTextField="Description" DataValueField="Autoid" DataSource='<%# GetIndustryAction(Eval("RegionIds").ToString()) %>' />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlCompany"> 
                                                <asp:CheckBox ID="chkselectallcompany" ForeColor="Blue" Font-Bold="true" runat="server" Text="Select All"
                                                 AutoPostBack="true" OnCheckedChanged="chkselectallcompany_CheckedChanged" /> 
                                                <asp:CheckBoxList ID="chkactionCompany" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="Description" DataValueField="Autoid" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                               <asp:Panel runat="server" ID="pnlReporting">
                                                 <asp:CheckBox ID="chkReportingPerson" ForeColor="Blue" Font-Bold="true" runat="server" Text="Select All"
                                                    AutoPostBack="true" OnCheckedChanged="chkReportingPerson_CheckedChanged" /> 
                                                 <asp:CheckBoxList ID="chkactioreporting" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="EmpName" DataSource='<%# GetReportingperson(Eval("EmpId").ToString()) %>' DataValueField="Autoid" />
                                               </asp:Panel>
                                            </td>
                                            <td>
                                              <asp:CheckBoxList ID="chkactionMenu" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="ActionName" DataValueField="Autoid" DataSource='<%# GetAction(Eval("Autoid").ToString()) %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                             </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="LV_Access_Menu_Company" />
                            </Triggers>
                         </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-actions right">
                <div class="row">
                    <div class="col-md-12"> 
                        <asp:Button ID="btnSave" runat="server" class="btn blue" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
    </div>
<asp:HiddenField ID="hidAutoidMain" runat="server" Value="" />  
  <script type="text/javascript">
     function selectAllRegion(source) {
         debugger
         var container = source.closest(".region-container");
         if (!container) return; 
         var checkboxes = container.querySelectorAll(".checkbox-wrapper input[type='checkbox']"); 
         checkboxes.forEach(function (cb) {
             cb.checked = source.checked;
              
         }); 
     }
  </script> 
</asp:Content>
