<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/MainMaster.master"  CodeBehind="Group.aspx.cs" Inherits="SystemAdmin.GroupStructure.Group" %>
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
                         <label class="control-label"> </label>
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
                                <td>
                                    <%# Eval("HOD") %>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    By :  <%# Eval("CreatedBy") %> <br />
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
                              <asp:DropDownList ID="ddlIndustry" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                         </div>
                     </div>
                     <div class="col-md-4">
                         <div class="form-group">
                             <label class="control-label">Head of Group<span class="required" aria-required="true"> *</span></label>
                               <asp:DropDownList ID="ddlHOD" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                         </div>
                     </div>
                    <div class="col-md-1">
                        <div class="form-group" style="padding-top:25px;">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
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
