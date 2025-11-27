<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeBehind="Group.aspx.cs" Inherits="SystemAdmin.GroupStructure.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Group"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Industry<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlIndustryFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
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
                    <label class="control-label"></label>
                    <div class="form-group">
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"></span></label>
                        <div>
                            <div class="btn-group pull-right">
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
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Group" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr class="bgprimary">
                                        <th>#</th>
                                        <th>Group</th>
                                        <th>Industry</th>
                                        <%--  <th>Region</th>--%>
                                        <th>Head of Group</th>
                                        <th>Is Active</th>
                                        <th>Created</th>
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
                                    <%# Eval("Name") %>
                                </td>
                                <td>
                                    <%# Eval("Industry") %>
                                </td>
                                <%-- <td>
                                     <%# Eval("Region") %>
                                </td>--%>
                                <td>
                                    <%# Eval("HOD") %>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>By :  <%# Eval("CreatedBy") %>
                                    <br />
                                    On : <%# Eval("CreatedOn") %> 
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
                            <label class="control-label">Group Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtGroupName" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="lstIndustry" runat="server" SelectionMode="Multiple" CssClass="form-control select2ddl req"></asp:ListBox>
                            <%--<asp:DropDownList ID="ddlIndustry" class="form-control select2ddl req" runat="server"></asp:DropDownList>--%>
                        </div>
                    </div>
                    <div id="div_region" runat="server" visible="false" class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="LstRegion" SelectionMode="Multiple" class="form-control select2ddl req" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Head of Group<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlHOD" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div  style="display:none;" class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Add Level<span class="required" aria-required="true"> *</span></label>
                            <div class="input-group add-on" style="display: flex;">
                                <asp:DropDownList ID="ddlLevel" runat="server" class="form-control select2ddl sub"></asp:DropDownList>
                                <asp:DropDownList ID="ddlEmp" runat="server" class="form-control select2ddl sub" ></asp:DropDownList>
                                <asp:Button ID="btnAddLevel" runat="server" class="btn blue" OnClick="btnAdd_Level" OnClientClick="return CheckRequiredField('sub');" Text="+" />
                            </div>
                        </div>
                    </div>
                    <div  class="col-md-1">
                        <div class="form-group" style="padding-top: 25px;">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div> 
               
                    <div class="col-md-6"  style="display:none;">
                        <asp:UpdatePanel ID="AssessorTbl" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="LV_AssessorTbl" runat="server" DataKeyNames="Autoid" OnItemCommand="LV_AssessorTbl_ItemCommand" OnItemDataBound="LV_AssessorTbl_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr style="border: none; background-color: transparent !important;">
                                                    <td colspan="8" class="text-center" style="border: none !important; font-weight: bold;">Group Heirarchy</td>
                                                </tr>
                                                <tr class="tableStatusBg">
                                                    <th>SI. No</th>
                                                    <th>Level</th>
                                                    <th>Employee</th>
                                                    <th>Is Enabled?</th>
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
                                            <td><%# Container.DataItemIndex+1 %></td>
                                            <td>
                                                <asp:HiddenField ID="hdnLevel" runat="server" Value='<%# Eval("Autoid") %>' />
                                                <div class="input-group add-on" style="display: flex;">
                                                    <%# Eval("Level") %>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnEmp" runat="server" Value='<%# Eval("EmployeeName") %>' />
                                                <div class="input-group add-on" style="display: flex;">
                                                    <%# Eval("EmployeeName") %>

                                                </div>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkOnOffPreGrace" runat="server" OnCheckedChanged="chkOnOffPreGrace_CheckedChanged" Checked='<%# Eval("isEnabled").ToString() == "True" %>' AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("Autoid") %>' CssClass="btn btn-danger btn-sm">
                                                    <i class="fa fa-trash"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="LV_AssessorTbl" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                 
                <div class="col-md-12 pull-right">
                    <div class="form-group pull-right">
                        <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div> 
             </div>
            </div> 
        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
</asp:Content>
