<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Products.aspx.cs" Inherits="SystemAdmin.Products.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Products"></asp:Label>
                </div>
            </div>
            <div id="divView" runat="server" class="portlet-body">
                <div class="row">  
                    <div class="col-md-2 pull-right">
                        <div class="form-group">
                            <label class="control-label"><span class="required" aria-required="true"></span></label>
                            <div>
                                <div class="btn-group pull-right">
                                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                                        Action <i class="fa fa-angle-down"></i>
                                    </button>
                                    <ul class="dropdown-menu pull-right">
                                        <li>
                                            <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
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
                        <asp:ListView ID="LV" runat="server" ItemPlaceholderID="itemplaceholder">
                            <layouttemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead class="dtTheme">
                                        <tr>
                                            <th>#</th>
                                            <th>Product Name</th>
                                            <th>Connection Name</th> 
                                            <th>Icons</th> 
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <div id="itemplaceholder" runat="server"></div>
                                    </tbody>
                                </table>
                            </layouttemplate>
                            <itemtemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                    </td>
                                    <td>
                                        <%#Eval("ProductsName")%>
                                    </td>
                                    <td>
                                        <%#Eval("ConnectionName")%>
                                    </td> 
                                    <td>
                                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# "~/" + Eval("ProductIcon") %>' Width="50px" Height="50px" />
                                    </td>                  
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Product Name</th>
                                            <th>Connection Name</th> 
                                            <th>Icons</th> 
                                        </tr>
                                    </thead>
                                </table>
                            </emptydatatemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div id="divAddEdit" runat="server" class="portlet-body form" visible="false">
                <div class="form-body">                    
                    <div class="row"> 
                        <div class="col-md-4">
                           <div class="form-group">
                               <label class="control-label">Product Name<span class="required" aria-required="true">*</span></label>
                               <asp:TextBox ID="txtProductName" class="form-control req" runat="server"></asp:TextBox>
                           </div>
                        </div> 
                        <div class="col-md-4">
                           <div class="form-group">
                               <label class="control-label">Connection Name<span class="required" aria-required="true">*</span></label>
                               <asp:TextBox ID="txtConnectionName" class="form-control req" runat="server"></asp:TextBox>
                           </div>
                        </div>
                        <div class="col-md-4"> 
                            <label class="control-label">Icons<span style="color: red"> *</span></label>
                            <div class="form-group">
                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                    <div class="input-group">
                                        <div class="form-control uneditable-input input-fixed" data-trigger="fileinput">
                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                        </div>
                                        <span class="input-group-addon btn btn-xs default btn-file">
                                            <span class="fileinput-new">Select </span>
                                            <span class="fileinput-exists">Change</span>
                                            <asp:FileUpload ID="fileIcons" CssClass="req" runat="server" onchange="ValidateFileUpload(this);" />
                                        </span>
                                        <a href="javascript:;"  class="input-group-addon btn btn-xs red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                    </div>
                                </div>
                            </div>
                            <asp:Literal ID="ltrProductIcon" runat="server"></asp:Literal>
                        </div>
                    </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClientClick="return CheckRequiredField('req')" OnClick="btnsave_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
 </div>
    <asp:HiddenField ID="hidID" runat="server" />
</asp:Content>
