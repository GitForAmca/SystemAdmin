<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="PolicyMaster.aspx.cs" Inherits="SystemAdmin.ESS.PolicyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Policy Master"></asp:Label>
                </div>
            </div>
            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Category<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlCategorySearch" OnSelectedIndexChanged="ddlCategorySearch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control desg"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3"  id="divRegionSearch" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlRegionSearch" runat="server" CssClass="form-control req" OnSelectedIndexChanged="ddlRegionSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divIndustrySearch" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlIndustrySearch" OnSelectedIndexChanged="ddlIndustrySearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control req"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divCompanySearch" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Company<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlCompanySearch" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3" id="divDepartmentSearch" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlDepartmentSearch" CssClass="form-control select2ddl desg" OnSelectedIndexChanged="ddlDepartmentSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divSubDepartmentSearch" runat="server" visible="false">
                        <div class="form-group">
                            <label class="control-label">Sub Department<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlSubDepartmentSearch" CssClass="form-control select2ddl desg" OnSelectedIndexChanged="ddlSubDepartmentSearch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                            <th>Policy Name</th>
                                            <th>Category</th>
                                            <th>Region</th> 
                                            <th>Industry</th> 
                                            <th>Company</th> 
                                            <th>Department</th> 
                                            <th>Sub Department</th> 
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Id")%>'/>
                                    </td>
                                    <td>
                                        <%# Eval("PolicyName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Category")%>
                                    </td>
                                    <td>
                                        <%# Eval("Region")%>
                                    </td>
                                    <td>
                                        <%# Eval("Industry")%>
                                    </td>
                                    <td>
                                        <%# Eval("CompanyName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Department")%>
                                    </td>   
                                    <td>
                                        <%# Eval("SubDepartment")%>
                                    </td>                                        
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th> 
                                            <th>Policy Name</th>
                                            <th>Category</th>
                                            <th>Region</th> 
                                            <th>Industry</th> 
                                            <th>Company</th> 
                                            <th>Department</th> 
                                            <th>Sub Department</th> 
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
                        <div class="col-md-12"> 
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Category<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList runat="server" ID="ddlCategory"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control req"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divRegion" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control req" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divIndustry" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList ID="ddl_industry" OnSelectedIndexChanged="ddl_industry_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control req"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divCompany" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label">Company<span class="required" aria-required="true"> *</span></label>
                                            <asp:ListBox ID="ddlAMCAGroupCompany" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divDepartment" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control select2ddl req" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3"  id="divSubDepartment" runat="server" visible="false">
                                         <div class="form-group">
                                             <label class="control-label">Sub Department<span class="required" aria-required="true"> *</span></label>
                                             <asp:DropDownList runat="server" ID="ddlSubDepartment" CssClass="form-control select2ddl req" OnSelectedIndexChanged="ddlSubDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                         </div>
                                    </div>
                                    <div class="col-md-3" id="divPolicyName" runat="server" visible="false">
                                         <div class="form-group">
                                             <label class="control-label">Name<span class="required" aria-required="true">*</span></label> 
                                             <asp:TextBox ID="txtPolicyName"  CssClass="form-control req" runat="server"></asp:TextBox>
                                         </div>
                                    </div>  
                                    <div class="col-md-12" id="divAMCAEmployeeHandBook" runat="server" visible="false">
                                        <h3><strong>AMCA Employee HandBook</strong></h3>
                                        <textarea class="ckeditor" rows="20" runat="server" id="txteditorAMCAHandbook"></textarea>
                                    </div>   
                                    <div class="col-md-12" id="divDepartmentalPolicy" runat="server" visible="false">
                                        <h3><strong>Departmental Policy</strong></h3>
                                        <textarea class="ckeditor" rows="20" runat="server" id="txteditorDepartmentalPolicy"></textarea>
                                    </div>   
                                    <div class="col-md-12" id="divSubDepartmentalPolicy" runat="server" visible="false">
                                        <h3><strong>Sub Departmental Policy</strong></h3>
                                        <textarea class="ckeditor" rows="20" runat="server" id="txteditorSubDepartmentalPolicy"></textarea>
                                    </div> 
                                </div> 
                        </div>
                    </div> 
                  </div> 
                    <!--1 Body --> 
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