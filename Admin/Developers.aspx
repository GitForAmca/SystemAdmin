<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="Developers.aspx.cs" Inherits="AMCAPropertiesAdmin.Admin.Developers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Developers Master"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Developers<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlDevelopersFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div> 
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
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
                    <asp:ListView ID="LV_Developers" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th> 
                                        <th>Developers</th>
                                        <th>Logo</th> 
                                        <th>IsActive</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" class="checkboxes" runat="server" Autoid='<%# Eval("AutoId")%>' />
                                </td> 
                                <td>
                                    <%# Eval("Developers") %>
                                </td> 
                                <td>
                                    <img src='<%# Eval("Imagepath") %>' width="100" />
                                </td>

                                <td style="width: 5%"><span class='<%# Eval("IsActive").ToString() =="True"?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%#  Eval("IsActive").ToString() =="False"?"InActive":"Active"%></span></td>


                                <td>
                                    <%# Eval("CreatedBy") %>
                                </td>
                                <td>
                                    <%# Eval("CreatedOn") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th> 
                                        <th>Developers</th>
                                        <th>Logo</th> 
                                        <th>IsActive</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Developers<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtDevelopers" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Image</label>
                        <div class="form-group">
                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                <div class="input-group input-small">
                                    <div class="form-control uneditable-input input-fixed input-small" data-trigger="fileinput">
                                        <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                    </div>
                                    <span class="input-group-addon btn btn-xs default btn-file">
                                        <span class="fileinput-new">Select </span>
                                        <span class="fileinput-exists">Change</span>
                                        <asp:FileUpload ID="fileDeveloperDocuments" CssClass="reqpop2" runat="server"  onchange="ValidateFileUpload(this);" />
                                    </span>
                                    <a href="javascript:;" class="input-group-addon btn btn-xs red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                </div>
                            </div>
                        </div>
                        <asp:Literal ID="ltrDeveloperDocuments" runat="server"></asp:Literal>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Description<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtDescription" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" style="margin-top: 30px;">
                        <div class="form-group">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
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
    <asp:HiddenField ID="hidDeveloperId" runat="server" Value="" /> 
</asp:Content>

