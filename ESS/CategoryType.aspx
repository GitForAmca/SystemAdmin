<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CategoryType.aspx.cs" Inherits="SystemAdmin.ESS.CategoryType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Category Type"></asp:Label>
                </div>
            </div>
            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                     <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Type<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList ID="ddlTypeSearch" OnSelectedIndexChanged="ddlTypeSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2ddl req">
                               <asp:ListItem Text="Select" Value=""></asp:ListItem>
                               <asp:ListItem Text="Entity" Value="Entity"></asp:ListItem>
                               <asp:ListItem Text="Assignment" Value="Assignment"></asp:ListItem>
                               <asp:ListItem Text="Consultant" Value="Consultant"></asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                    </div> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Action<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList ID="ddlActionSearch"  OnSelectedIndexChanged="ddlActionSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2ddl req">
                               <asp:ListItem Text="Select" Value=""></asp:ListItem>
                               <asp:ListItem Text="Automated" Value="Automated"></asp:ListItem>
                               <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                    </div>  
                     <div class="col-md-3">
                         <div class="form-group">
                             <label class="control-label">Department<span class="required" aria-required="true"> </span></label>
                             <asp:ListBox ID="ddlDepartmentSearch" SelectionMode="Multiple"  OnSelectedIndexChanged="lstDepartmentSearch_SelectedIndexChanged" class="form-control req multiselectddl" runat="server"></asp:ListBox>  
                         </div> 
                     </div> 
                    <div class="col-md-1">
                        <div class="form-group">
                            <label class="control-label"><span class="required" aria-required="true"></span></label>
                            <div>
                                <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="btn gray btn-exc1 pull-right" OnClick="btnGet_Click" />
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
                                            <th>Category</th>
                                            <th>Type</th>
                                            <th>Action</th>
                                            <th>DepartmentName</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Id")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("Category")%>
                                    </td>
                                    <td>
                                        <%# Eval("Type")%>
                                    </td>
                                    <td>
                                        <%# Eval("Action")%>
                                    </td>
                                    <td>
                                        <%# Eval("DepartmentName")%>
                                    </td>                          
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                             <th>#</th>
                                             <th>Category</th>
                                             <th>Type</th>
                                             <th>Action</th>
                                             <th>DepartmentName</th>
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
                                <label class="control-label">Group Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtGroupName" CssClass="form-control req" runat="server" oldname="" onblur="CheckName(this);" placeholder="input Group Name"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2ddl req">
                                   <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                   <asp:ListItem Text="Entity" Value="Entity"></asp:ListItem>
                                   <asp:ListItem Text="Assignment" Value="Assignment"></asp:ListItem>
                                   <asp:ListItem Text="Consultant" Value="Consultant"></asp:ListItem>
                                </asp:DropDownList>
                            </div> 
                        </div> 
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Action<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control select2ddl req">
                                   <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                   <asp:ListItem Text="Automated" Value="Automated"></asp:ListItem>
                                   <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                                </asp:DropDownList>
                            </div> 
                        </div>  
                         <div class="col-md-3">
                             <div class="form-group">
                                 <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                                 <asp:ListBox ID="ddlDepartment" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>  
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