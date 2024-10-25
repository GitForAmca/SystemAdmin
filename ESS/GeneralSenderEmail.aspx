<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="GeneralSenderEmail.aspx.cs" Inherits="SystemAdmin.ESS.GeneralSenderEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="General Sender Email"></asp:Label>
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
                        <asp:ListView ID="LV_Operation_Mapping" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Group</th>
                                            <th>Sender Email</th>
                                            <th>Sender Password</th>                                     
                                            <th>On</th>                                                 
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("GroupName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SenderEmail")%>
                                    </td>
                                    <td>
                                         <%# Eval("SenderPassword")%>
                                    </td> 
                                    <td>
                                        <span style='display: inline-block; width: 100px; background: #ddd; padding: 0px 4px; margin-top: 2px'>Created</span> <%# Eval("CreatedOn")%> 
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
                                            <th>Group</th>
                                            <th>Sender Email</th>
                                            <th>Sender Password</th>                                     
                                            <th>On</th>                                                 
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Sender Email<span class="required" aria-required="true"> *</span></label> 
                                <asp:TextBox ID="txtSenderEmail" class="form-control emailid req" runat="server" ></asp:TextBox>
                            </div>
                        </div> 
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Sender Password<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtSenderPassword" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>  
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
       <%-- function CheckTaskList(args) {
            debugger
            var value = $(args).val();
            $(args).val(value);
            if (value == "") {
                return;
            }
            var Data = JSON.stringify({ value: value, oldname: $(args).attr('oldname') });
            $.ajax({
                dataType: "json",
                type: "POST",
                data: Data,
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "TaskSchedule.aspx/CheckTask",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, Record Already exist')
                        $(args).val($(args).attr('oldname'));
                        $('#<%= ddlTaskList.ClientID%>').val('');
                        $('#select2-chosen-2').html('');
                    }
                },
                error: function (errMsg) {
                    ShowError(errMsg);
                }
            });
        }--%>
    </script>
</asp:Content>