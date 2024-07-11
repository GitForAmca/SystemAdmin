<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="ServiceMaster.aspx.cs" Inherits="SystemAdmin.ESS.ServiceMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Service Master"></asp:Label>
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
                            <label class="control-label"><span class="required" aria-required="true"> </span></label>
                            <div>
                                <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="btn blue" OnClick="btnGet_Click" />
                            </div>
                        </div>
                    </div>
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
                                            <th>Name</th>
                                            <th>Type</th>
                                            <th>Particulars</th>
                                            <th>Questionnaire</th>
                                            <th>Consultant Price</th>
                                            <th>Client Price</th>
                                            <th>Authority Fee</th>
                                            <th>Consultant Description</th>
                                            <th>Consultant ETC</th>
                                            <th>Client Description</th>
                                            <th>Client ETC</th>
                                            <th>Remarks</th>
                                            <th>Created On</th>
                                            <th>Created By</th>
                                            <th>IsActive</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("ServiceMainId")%>' GroupId='<%# Eval("GroupId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("ServiceName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ServiceTypeName")%>
                                    </td>
                                    <td>
                                        <a href="javascript:;" onclick="RequiredDocumnetQuestionPopup('<%# Eval("Autoid")%>');return false;" ><%# Eval("NoofdocumentQuestion")%></a>
                                    </td> 
                                    <td>
                                        <%# Eval("Questionnaire")%>
                                    </td>
                                    <td>
                                        <%# Eval("ConsultantPrice")%>
                                    </td>
                                    <td>
                                        <%# Eval("ClientPrice")%>
                                    </td>
                                    <td>
                                        <%# Eval("AuthorityFee")%>
                                    </td>
                                    <td>
                                        <%# Eval("ConsultantDescription")%>
                                    </td>
                                    <td>
                                        <%# Eval("ConsultantETC")%>
                                    </td>
                                    <td>
                                        <%# Eval("ClientDescription")%>
                                    </td>
                                    <td>
                                        <%# Eval("ClientETC")%>
                                    </td>
                                    <td>
                                        <%# Eval("MainRemarks")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedOn")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedBy")%>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-warning"%>' runat="server"><%# Eval("IsActive")%></span>
                                    </td>
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Type</th>
                                            <th>Particulars</th>
                                            <th>Questionnaire</th>
                                            <th>Consultant Price</th>
                                            <th>Client Price</th>
                                            <th>Authority Fee</th>
                                            <th>Consultant Description</th>
                                            <th>Consultant ETC</th>
                                            <th>Client Description</th>
                                            <th>Client ETC</th>
                                            <th>Remarks</th>
                                            <th>Created On</th>
                                            <th>Created By</th>
                                            <th>IsActive</th>
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
                    <div class="row" runat="server" id="divAddGroup" visible="true">
                        <div class="col-md-12">
                            <h4><strong>Company List</strong></h4>
                            <asp:CheckBoxList ID="chkGroupCompany" runat="server" RepeatDirection="Horizontal" DataTextField="Name" DataValueField="AutoId" OnDataBinding="chkGroupCompany_DataBinding" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="divUpdateGroup" visible="false">
                        <div class="col-md-3">
                            <h4><strong>Company List</strong></h4>
                            <asp:DropDownList ID="ddlUpdateGroupCompany" OnSelectedIndexChanged="ddlUpdateGroupCompany_SelectedIndexChanged" AutoPostBack="true" class="form-control requp select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row" runat="server" id="divAssignment">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Assignment
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Service Name<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtServiceName" CssClass="form-control req" runat="server" oldname="" onblur="CheckName(this);" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Service Type<span class="required" aria-required="true"> *</span></label>
                                                <asp:DropDownList runat="server" ID="ddlServiceType" CssClass="form-control select2ddl req"></asp:DropDownList>
                                            </div>
                                        </div> 
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Questionnaire<span class="required" aria-required="true"> *</span></label>
                                                <asp:DropDownList ID="ddlQuestionnaire" runat="server" class="form-control select2ddl req" OnSelectedIndexChanged="ddlQuestionnaire_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                                    <asp:ListItem Value="KYB" Text="KYB"></asp:ListItem>
                                                    <asp:ListItem Value="KYC" Text="KYC"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Assignment period<span class="required" aria-required="true"> *</span></label>
                                                <asp:ListBox ID="ddlAssignmentPeriod" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label class="control-label">Remarks<span class="required" aria-required="true"> </span></label>
                                                <asp:TextBox ID="txtMainRemarks" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-right">
                                            <asp:CheckBox ID="chkactive" runat="server" class="form-control req" Style='border: none !important' Checked="true" Text="Is Active" />
                                        </div> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divPriceList">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Price
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Consultant Price<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtConsultantPrice" CssClass="form-control req NumberWithOneDot" runat="server" Text="0.00" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Client Price<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtClientprice" CssClass="form-control req NumberWithOneDot" runat="server" Text="0.00" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Authority Fee<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtAuthorityFee" CssClass="form-control req NumberWithOneDot" runat="server" Text="0.00" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divDeadline">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Estimated Time Frame
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Consultant Description<span class="required" aria-required="true"> </span></label>
                                                <asp:TextBox ID="txtConsultantDescription" CssClass="form-control" runat="server" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Consultant ETC<span class="required" aria-required="true"> </span></label>
                                                <asp:DropDownList ID="ddlConsultantETC" runat="server" class="form-control select2ddl"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Client Description<span class="required" aria-required="true"> </span></label>
                                                <asp:TextBox ID="txtClientDescription" CssClass="form-control " runat="server" placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Client ETC<span class="required" aria-required="true"> </span></label>
                                                <asp:DropDownList ID="ddlClientETC" runat="server" class="form-control select2ddl"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Particulars
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <%--<div class="col-md-2">
                                            <div class="form-group">
                                                <label class="control-label">Type<span class="required" aria-required="true"> </span></label>
                                                <asp:DropDownList ID="ddlSystemType" runat="server" class="form-control" OnSelectedIndexChanged="ddlSystemType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="" Text="All" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Questionnaire" Text="Questionnaire"></asp:ListItem>
                                                    <asp:ListItem Value="Checklist" Text="Checklist"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-12">
                                            <asp:ListView ID="LVQuesName" runat="server" ItemPlaceholderID="itemplaceholder1">
                                                <LayoutTemplate>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Questions</th>                                                                                                      
                                                                <th>Category</th>                                                   
                                                                <th>Type</th>                                                     
                                                                <th>Questionnaire</th>                                                 
                                                                <th>CRM Mandatory?</th>                                                   
                                                                <th>Operation Mandatory?</th>                                                   
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <div id="itemplaceholder1" runat="server"></div>
                                                        </tbody>

                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkDocumentQu" runat="server" CssClass="checkboxes chkDocument" Documentid='<%# Eval("Documentid")%>'   />
                                                        </td>
                                                        <td>
                                                            <%# Eval("Questions")%>
                                                        </td> 
                                                        <td>
                                                            <%# Eval("SystemType")%>
                                                        </td> 
                                                        <td>
                                                            <%# Eval("Type")%>
                                                        </td> 
                                                        <td>
                                                            <%# Eval("Questionnaire")%>
                                                        </td>                                            
                                                        <td>
                                                            <asp:CheckBox ID="chkIsRequiredQu" runat="server" CssClass="checkboxes chkIsRequired" />
                                                        </td>                                         
                                                        <td>
                                                            <asp:CheckBox ID="chkIsRequiredQuOp" runat="server" CssClass="checkboxes chkIsRequired" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table class="table table-bordered table-hover datatable">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Document Name</th>                                                                                                      
                                                                <th>IsRequired</th> 
                                                                <th>IsRequired Operation</th> 
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
    <div id="PopupViewReqDocQuestion" tabindex="-1" data-width="400" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" id="PopupViewReqDocQuestionTitle">Questionnaire</h4>
                </div>
                <div class="modal-body">
                    <div class="form-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <table id="tblReqQuestionDoc" class="table table-bordered table-hover">
                                        <thead><tr><th>Questions</th><th>Mandatory</th></tr></thead>
                                        <tbody></tbody>                                                   
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                     <button type="button" data-dismiss="modal" class="btn">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidGroupID" runat="server" />
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
                url: "ServiceMaster.aspx/CheckName",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] Service Type Name already exist')
                        $(args).val($(args).attr('oldname'));
                    }
                },
                error: function (errMsg) {
                    ShowError(errMsg);
                }
            });
        }
        function RequiredDocumnetQuestionPopup(DocumentId) {
            $('#tblReqQuestionDoc tbody').html('');
            FillDocumentDataQuestions(DocumentId);
            $("#PopupViewReqDocQuestion").modal({
                backdrop: 'static',
                keyboard: false

            });
            $("#PopupViewReqDocQuestion").modal('show');
        }
        function FillDocumentDataQuestions(DocumentId) {
            var Data = JSON.stringify({ DocumentId: DocumentId });
            $.ajax({
                dataType: "json",
                type: "POST",
                data: Data,
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "ServiceMaster.aspx/QuestionsData",
                success: function (Result) {
                    if (Result.d != "") {
                        debugger;
                        var jsondata = JSON.parse(Result.d);
                        var row = "";
                        $.each(jsondata, function (key, value) {
                            row = "<tr>";
                            row += "<td>" + value.DocumentName + "</td>";
                            row += "<td>" + value.IsRequired + "</td>";
                            row += "</tr>";
                            $('#tblReqQuestionDoc tbody').append(row);
                        });
                    }

                },
                error: function (errMsg) {
                    ShowError(errMsg);
                }
            });
        }
    </script>
</asp:Content>