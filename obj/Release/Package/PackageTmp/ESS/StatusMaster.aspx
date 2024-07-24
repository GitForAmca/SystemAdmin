<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="StatusMaster.aspx.cs" Inherits="SystemAdmin.ESS.StatusMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Status Master"></asp:Label>
                </div>
            </div> 
            <div id="divView" runat="server" class="portlet-body">
                <div class="row"> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Industries<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList ID="ddlIndustriesFilter" class="form-control select2ddl" OnSelectedIndexChanged="ddlIndustriesFilter_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlGroupFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Department<span class="required" aria-required="true"></span></label> 
                            <asp:DropDownList runat="server" ID="lstdepartmentsearch" CssClass="form-control select2ddl"></asp:DropDownList> 
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Type<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList runat="server" ID="ddlTypeSearch" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Action<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList runat="server" ID="ddlActionSearch" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Category<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList runat="server" ID="ddlCategorySearch" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">IsActive<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList runat="server" ID="ddlIsActive" CssClass="form-control">
                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label class="control-label"></label>
                            <div style="margin-top: 8px;">
                                <asp:Button ID="btnGet" runat="server" CssClass="btn green" Text="Get" OnClick="btnGet_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn resetbtn" Text="Reset" OnClick="btnReset_Click" />
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
                                            <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnEdit" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('chkSelect');" runat="server" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
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
                        <asp:ListView ID="LV_StatusBox" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>#</th>
                                            <th>Status Name</th>
                                            <th>Department</th>
                                            <th>Type</th>
                                            <th>Action</th>
                                            <th>Category</th>
                                            <th>IsActive</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" Class="checkboxes chkselectforId chkSelect" Autoid='<%# Eval("AutoId")%>' />
                                    </td>
                                    <td><%# Eval("StatusName")%></td>
                                    <td><%# Eval("Department")%></td>
                                    <td><%# Eval("Type")%></td>
                                    <td><%# Eval("Action")%></td>
                                    <td><%# Eval("Category")%></td>
                                    <td><span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span></td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>#</th>
                                            <th>Status Name</th>
                                            <th>IsActive</th>
                                            <th>Department</th>
                                            <th>Type</th>
                                            <th>Action</th>
                                            <th>Category</th> 
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="50" style="text-align: center">no record found</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div id="divAddEdit" runat="server" class="portlet-body form" visible="false">
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-12">
                            <label class="control-label">Industries<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlIndustries" class="form-control select2ddl req" OnSelectedIndexChanged="ddlIndustries_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
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
                                <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtStatusName" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                                <asp:ListBox ID="lstDepartmentSave" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlTypeSave" CssClass="form-control req"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Action<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlActionSave" CssClass="form-control req"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Category<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlCategorySave" CssClass="form-control req"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div style="margin-top: 25px" class="form-group">
                                <label class="control-label">IsActive</label>
                                <asp:CheckBox ID="chckIsActive" Checked="true" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req')" runat="server" CssClass="btn green" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
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



