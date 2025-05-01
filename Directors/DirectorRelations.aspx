<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="DirectorRelations.aspx.cs" Inherits="SystemAdmin.Directors.DirectorRelations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="col-md-12 col-sm-12">
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Director Relations"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body">
            <div class="row">
                <div class="col-md-2 pull-right">
                    <div class="form-group">
                        <label class="control-label">&nbsp;</label>
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
                                        <th>From Director</th>
                                        <th>To Director</th>
                                        <th>Is Active</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
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
                                    <%# Eval("FromDirectorName")%>
                                </td>  
                                <td>
                                    <%# Eval("ToDirectorName")%>
                                </td>  
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>  
                                <td>
                                    <%# Eval("CreatedByName")%>
                                </td>    
                                <td>
                                    <%# Eval("CreatedOn")%>
                                </td>                         
                            </tr>
                        </itemtemplate>
                        <emptydatatemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>From Director</th>
                                        <th>To Director</th>
                                        <th>Is Active</th>
                                        <th>Created By</th>
                                        <th>Created On</th>
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">From<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlFromDirector" onchange="CheckSameddl();" class="form-control req" runat="server"></asp:DropDownList>  
                        </div> 
                    </div> 
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">To<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlToDirector" onchange="CheckSameddl();" class="form-control req" runat="server"></asp:DropDownList>  
                        </div> 
                    </div>   
                    <div class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Is Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
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
    <script type="text/javascript">
        function CheckSameddl() {
            debugger
            var fromDirector = document.getElementById('<%= ddlFromDirector.ClientID %>').value;
            var toDirector = document.getElementById('<%= ddlToDirector.ClientID %>').value;

            if (fromDirector === toDirector) {
                ShowError('You cannot select the same name in both dropdowns. Please choose different names.');
                document.getElementById('<%= ddlFromDirector.ClientID %>').value = '';
                document.getElementById('<%= ddlToDirector.ClientID %>').value = '';
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
