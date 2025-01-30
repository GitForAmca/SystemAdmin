<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="QuestionnaireMaster.aspx.cs" Inherits="SystemAdmin.ESS.QuestionnaireMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Questionnaire Master"></asp:Label>
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
                                            <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i> Add</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnDelete_Click" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
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
                        <asp:ListView ID="LV" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Particulars</th>
                                            <th>Description</th>
                                            <th>Category</th>
                                            <th>Type</th>
                                            <th>Questionnaire</th>
                                            <th>By / On</th>
                                            <th>IsActive</th>
                                            <th>By Default</th>
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
                                        <%# Eval("Questions")%>
                                    </td>
                                    <td>
                                        <%# Eval("Description")%>
                                    </td>
                                    <td>
                                        <%# Eval("Type")%>
                                    </td>
                                    <td>
                                        <%# Eval("SystemType")%>
                                    </td>
                                    <td>
                                        <%# Eval("Questionnaire")%>
                                    </td>
                                    <td>
                                        <%# Eval("Createdby")%> / <%# Eval("CreatedOnfrom")%>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-warning"%>' runat="server"><%# Eval("IsActive")%></span>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("isMandatory").ToString())==true?"label label-sm label-success":"label label-sm label-warning"%>' runat="server"><%# Eval("isMandatory")%></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Document Name</th>
                                            <th>Document Type</th>
                                            <th>Created date</th>
                                            <th>Created By</th>
                                            <th>IsActive</th>
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
                                <label class="control-label">Particulars<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtDocumentName" CssClass="form-control reqRec" TextMode="MultiLine" Rows="5" runat="server" oldname="" onblur="CheckName(this);" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Description<span class="required" aria-required="true"> </span></label>
                                <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Category<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlDocumentType" runat="server" class="form-control reqRec">
                                    <asp:ListItem Value="" Text="Select Option" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Legal" Text="Legal"></asp:ListItem>
                                    <asp:ListItem Value="Financial" Text="Financial"></asp:ListItem>
                                    <asp:ListItem Value="Confirmation and Undertakings" Text="Confirmation and Undertakings"></asp:ListItem>
                                    <asp:ListItem Value="Clearance" Text="Clearance"></asp:ListItem>
                                    <asp:ListItem Value="Assignment" Text="Assignment"></asp:ListItem>
                                    <asp:ListItem Value="Identification" Text="Identification"></asp:ListItem>
                                    <asp:ListItem Value="Employment" Text="Employment"></asp:ListItem>
                                    <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlSystemType" runat="server" class="form-control reqRec">
                                    <asp:ListItem Value="" Text="Select Option" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Questionnaire" Text="Questionnaire"></asp:ListItem>
                                    <asp:ListItem Value="Checklist" Text="Checklist"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label">Questionnaire<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlQuestionnaire" runat="server" class="form-control reqRec">
                                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                    <asp:ListItem Value="KYB" Text="KYB"></asp:ListItem>
                                    <asp:ListItem Value="KYC" Text="KYC"></asp:ListItem> 
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">&nbsp;</label>
                            <asp:CheckBox ID="chkDefault" runat="server" class="form-control" Style='border: none !important' Text="By Default" />
                        </div>
                        <div class="col-md-3">
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
                url: "QuestionnaireMaster.aspx/CheckName",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] Document name already exist')
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



