<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="CEC.aspx.cs" Inherits="SystemAdmin.AutomaticDataAssign.CEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="CEC"></asp:Label>
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
                            <label class="control-label">Name<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlNameFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">CEM<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlCEMFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Status<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlStatusFilter" CssClass="form-control select2ddl">
                                <asp:ListItem Text="Select an option" Value=""></asp:ListItem>
                                <asp:ListItem Text="Assign" Value="Assign"></asp:ListItem>
                                <asp:ListItem Text="Next Assign" Value="NextAssign"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Assigned<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlIsAssignedFilter" CssClass="form-control select2ddl">
                                <asp:ListItem Text="Select an option" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
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
                        <asp:ListView ID="LV_CEC" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>CEM</th>                                          
                                            <th>Status</th>                                                     
                                            <th>Organization</th>                                              
                                            <th>Last Active Time</th>                                                
                                            <th>IsAssigned</th>                                                
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
                                        <asp:HiddenField ID="hdnCEM" Value='<%#Eval("CEM") %>' runat="server" />
                                        <asp:HiddenField ID="hdnCompanyId" Value='<%#Eval("CompanyId") %>' runat="server" />
                                        <asp:HiddenField ID="hdnAutoId" Value='<%#Eval("Autoid") %>' runat="server" />
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("CECName")%>
                                    </td>
                                    <td>
                                        <%# Eval("CEMName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Status")%>
                                    </td>
                                    <td>
                                        <%# Eval("CompanyName")%>
                                    </td>
                                    <td>
                                        <%# Eval("LastActiveTime")%>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsLeadAssign").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsLeadAssign").ToString())==true?"Yes":"No"%></span>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btn_next_assigned" Text="Set as Next Assign" OnClick="btn_next_assigned_Click" CssClass="btn btn-xs bgBlue" OnClientClick="statusAert(this);return false;" runat="server" />
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
                                            <th>Status</th>                                                 
                                            <th>IsAssigned</th>                                                 
                                            <th>Organization</th>   
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
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlAddEmpCEC" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div> 
                        <div class="col-md-12 pull-right text-right">
                            <div class="form-group">
                                <label class="control-label">Is Assigned</label>
                                <asp:CheckBox ID="chkIsAssigned" Checked="true" runat="server"></asp:CheckBox>
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
    <script>
        function statusAert(args) {
            Myconfirm('Do you want to set as Next Assign!', args);
        }
    </script>
</asp:Content>
