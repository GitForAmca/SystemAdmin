<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="RiskQuestions.aspx.cs" Inherits="SystemAdmin.ESS.RiskQuestions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Risk Questions"></asp:Label>
                </div>
            </div> 

            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <%--action div start--%>
                    <div class="col-md-12">
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
                                        <li>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnDelete_Click" Text="Edit" OnClientClick="DeleteQuestions(this);return false;"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
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
                        <asp:ListView ID="LV_Risk_Questions" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Main Risk</th>
                                            <th>Risk Questions</th>
                                            <th>Added By</th>
                                            <th>Added On</th>
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
                                        <%# Eval("MainRisk")%>
                                    </td>
                                    <td>
                                        <%# Eval("RiskQuestions")%>
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
                                            <th>Main Risk</th>
                                            <th>Risk Questions</th>
                                            <th>Added By</th>
                                            <th>Added On</th>
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
                                <label class="control-label">Main Risk<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlMainRisk" runat="server" class="form-control reqRec">
                                    <asp:ListItem Value="" Text="Select Option" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Customer Risk" Text="Customer Risk"></asp:ListItem>
                                    <asp:ListItem Value="Service/Transaction Risk" Text="Service/Transaction Risk"></asp:ListItem>
                                    <asp:ListItem Value="Geographical Risks" Text="Geographical Risks"></asp:ListItem>
                                    <asp:ListItem Value="Delivery Channel Business Risk" Text="Delivery Channel Business Risk"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Risk Question<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtRiskQuestion" CssClass="form-control reqRec" TextMode="MultiLine" Rows="5" runat="server" placeholder="input here"></asp:TextBox>
                                <%--<asp:TextBox ID="txtRiskQuestion" oldname="" onblur="CheckName(this);" CssClass="form-control reqRec" TextMode="MultiLine" Rows="5" runat="server" placeholder="input here"></asp:TextBox>--%>
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
    <script>
        function CheckName(args) {
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
                url: "RiskQuestions.aspx/CheckName",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] already exist')
                        $(args).val($(args).attr('oldname'));
                    }
                },
                error: function (errMsg) {
                    ShowError(errMsg);
                }
            });
        }
        function DeleteQuestions(args) {
            if (CheckOnlyOneSelect('chkselect')) {
                Myconfirm('Do you want to Delete!', args);
            }
        }
    </script>
</asp:Content>




