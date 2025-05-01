<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Directors.aspx.cs" Inherits="SystemAdmin.Directors.Directors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="col-md-12 col-sm-12">
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Directors"></asp:Label>
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
                                        <th>Director</th>
                                        <th>Team</th>
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
                                    <%# Eval("DirectorName")%>
                                </td>     
                                <td>
                                    <%# Eval("TeamName")%>
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
                                        <th>Director</th>
                                        <th>Team</th>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Director<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlDirector" oldname="" onblur="CheckExistDirector(this);" class="form-control req" runat="server"></asp:DropDownList>  
                            </div> 
                        </div> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Team Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtTeamName" runat="server" oldname="" onblur="CheckExistTeam(this);" CssClass="form-control req"></asp:TextBox>
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
<script>
    function CheckExistDirector(args) {
        debugger
        var text = $(args).val().trim();
        $(args).val(text);
        if (text.trim() == "") {
            return;
        }
        var Data = JSON.stringify({ text: text, oldname: $(args).attr('oldname') });
        $.ajax({
            dataType: "json",
            type: "POST",
            data: Data,
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "Directors.aspx/CheckExistDirector",
            success: function (Result) {
                if (Result.d != "0") {
                    ShowWarning('Already Exist!')
                    $(args).val($(args).attr('oldname'));
                }
            },
            error: function (errMsg) {
                ShowError(errMsg);
            }
        });
    }
</script>
<script>
    function CheckExistTeam(args) {
        debugger
        var text = $(args).val().trim();
        $(args).val(text);
        if (text.trim() == "") {
            return;
        }
        var Data = JSON.stringify({ text: text, oldname: $(args).attr('oldname') });
        $.ajax({
            dataType: "json",
            type: "POST",
            data: Data,
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "Directors.aspx/CheckExistTeam",
            success: function (Result) {
                if (Result.d != "0") {
                    ShowWarning('Already Exist!')
                    $(args).val($(args).attr('oldname'));
                }
            },
            error: function (errMsg) {
                ShowError(errMsg);
            }
        });
    }
</script>
</asp:Content>
