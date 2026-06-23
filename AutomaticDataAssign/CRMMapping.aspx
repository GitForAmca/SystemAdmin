<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="CRMMapping.aspx.cs" Inherits="SystemAdmin.AutomaticDataAssign.CRMMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="CRM Mapping"></asp:Label>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">From CRM<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlFromCRMFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">To CRM<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlToCRMFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Assigned Type<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlAssignedTypeFilter" CssClass="form-control select2ddl">
                                <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                <asp:ListItem Value="Pre-Sales" Text="Pre-Sales"></asp:ListItem>
                                <asp:ListItem Value="Post-Sales" Text="Post-Sales"></asp:ListItem>
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
                        <asp:ListView ID="LV_CRM_Mapping" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>From CRM</th>
                                            <th>To CRM</th>                                          
                                            <th>Assigned Type</th>                                                     
                                            <th>Organization</th>                                              
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
                                        <asp:HiddenField ID="hdnCompanyId" Value='<%#Eval("GroupId") %>' runat="server" />
                                        <asp:HiddenField ID="hdnAutoId" Value='<%#Eval("Autoid") %>' runat="server" />
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("FromCRMName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ToCRMName")%>
                                    </td>
                                    <td>
                                        <%# Eval("AssignedType")%>
                                    </td>
                                    <td>
                                        <%# Eval("CompanyName")%>
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
                                            <th>From CRM</th>
                                            <th>To CRM</th>                                          
                                            <th>Assigned Type</th>                                                     
                                            <th>Organization</th>                                              
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
                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlAssignedType" CssClass="form-control req select2ddl">
                                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                    <asp:ListItem Value="Pre-Sales" Text="Pre-Sales"></asp:ListItem>
                                    <asp:ListItem Value="Post-Sales" Text="Post-Sales"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">From<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlFromCRM" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div> 
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">To<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlToCRM" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div> 
                        <div class="col-md-12 pull-right text-right">
                            <div class="form-group">
                                <label class="control-label">Is Active</label>
                                <asp:CheckBox ID="chkIsActive" Checked="true" runat="server"></asp:CheckBox>
                            </div>
                        </div> 
                    </div>
                    <div class="row"> 
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
    <asp:HiddenField ID="hidID" runat="server" />
</asp:Content>
