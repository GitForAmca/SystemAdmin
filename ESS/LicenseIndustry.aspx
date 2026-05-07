<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="LicenseIndustry.aspx.cs" Inherits="SystemAdmin.ESS.LicenseIndustry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="License Industry"></asp:Label>
                </div>
            </div> 

            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <%--action div start--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Industry<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlIndustryFilter" OnSelectedIndexChanged="ddlIndustryFilter_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Activity<span class="required" aria-required="true"></span></label> 
                                <asp:ListBox ID="lstActivityFilter" SelectionMode="Multiple" class="form-control select2ddl reqRec" runat="server"></asp:ListBox>
                             </div>
                        </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control select2ddl">
                                <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"></span></label>
                            <div> 
                            <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
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
                    <%--action div End--%>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_LicenseIndustry" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Industry</th>
                                            <th>Activities</th>
                                            <th>Is Active</th>
                                            <th>Created By</th>
                                            <th>Created On</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("AutoId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("IndustryName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Activities")%>
                                    </td> 
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                    </td>  
                                    <td>
                                        <%# Eval("CreatedBy")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedOn")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>License Activity</th>
                                            <th>Activity Code</th>
                                            <th>Is Active</th>
                                            <th>Created By</th>
                                            <th>Created On</th>
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
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtIndustry" CssClass="form-control reqRec" runat="server" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Activity<span class="required" aria-required="true"> *</span></label> 
                                <asp:ListBox ID="lstActivity" SelectionMode="Multiple" class="form-control select2ddl reqRec" runat="server"></asp:ListBox>
                             </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 pull-right">
                            <label class="control-label">&nbsp;</label>
                            <asp:CheckBox ID="chkactive" runat="server" class="form-control" Style='border: none !important' Checked="true" Text="Is Active" />
                        </div>
                    </div>
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClick="btnsave_Click" OnClientClick="return CheckRequiredField('reqRec')" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" /> 
</asp:Content>




