<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false"  CodeBehind="HRRegion.aspx.cs" Inherits="SystemAdmin.ESS.HRRegion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Regional HR"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row"> 
              <div class="col-md-3">
                   <div class="form-group">
                      <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                      <asp:DropDownList ID="ddl_IndustrySearch" class="form-control select2ddl" runat="server"></asp:DropDownList>
                   </div>
              </div>
              <div class="col-md-3">
                   <div class="form-group">
                      <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                      <asp:DropDownList ID="ddlRegionSearch" class="form-control select2ddl" runat="server"></asp:DropDownList>
                   </div>
              </div>
              <div class="col-md-1">
                    <div class="form-group">
                       <label class="control-label"><span class="required" aria-required="true"></span></label>
                       <div>
                           <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="btn blue" OnClick="btnGet_Click" />
                           <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn resetbtn" OnClick="btnReset_Click" />
                       </div>
                    </div>
              </div>
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
                    <asp:ListView ID="LV_ParentHR_Access" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Region</th>
                                        <th>Industry</th>
                                        <th>Department</th>
                                        <th>Primary HR</th>
                                        <th>Secondary HR</th>
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
                                    <%# Eval("Region") %>
                                </td>
                                <td>
                                    <%# Eval("Industry") %>
                                </td>
                                <td style="width:500px !important;">
                                    <%# Eval("Department") %>
                                </td>
                                <td>
                                    <%# Eval("PrimaryHR") %>
                                </td>
                                <td>
                                    <%# Eval("SecondaryHR") %>
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Industries<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlIndustries" class="form-control select2ddl req" OnSelectedIndexChanged="ddlIndustries_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6" style="display: flex;">
                        <div class="form-group" style="width: 100%">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                             <asp:DropDownList ID="ddlRegion" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl req" runat="server"></asp:DropDownList> 
                        </div> 
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Primary HR<span class="required" aria-required="true"> *</span></label>
                           <asp:DropDownList ID="ddl_PrimaryHR" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                         <div class="form-group">
                             <label class="control-label">Secondary HR<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddl_SecondaryHR" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                         </div>
                    </div> 
                    <div class="col-md-12">
                        <asp:ListView ID="LV_department" runat="server" DataKeyNames="Id" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Department</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                         <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:CheckBox ID="chkSelect" class="" runat="server"  InputAttributes-data-id='<%# Eval("Id") %>'  Autoid='<%# Eval("Id")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("Department") %>
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
                        <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
           </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" /> 
</asp:Content>
