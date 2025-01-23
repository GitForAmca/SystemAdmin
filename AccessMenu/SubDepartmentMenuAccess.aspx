<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="SubDepartmentMenuAccess.aspx.cs" Inherits="SystemAdmin.AccessMenu.SubDepartmentMenuAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Sub Department Access Menu"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Industry<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlIndustryFilter" OnSelectedIndexChanged="ddlIndustriesFilter_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Department<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlDepartmentFilter" OnSelectedIndexChanged="ddldepartmentFilter_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Sub Department<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlSubDepartmentFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <div class="btn-group pull-right" style="margin-left: 8px;">
                            <button class="btn dropdown-toggle" data-toggle="dropdown">
                                Action <i class="fa fa-angle-down"></i>
                            </button>
                            <ul class="dropdown-menu pull-right">
                                <li>
                                    <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxes');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_ParentMenu_Access" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Sub Department</th>
                                        <th>Department</th>
                                        <th>Sub Parent Menu</th>
                                        <th>Industry</th>
                                        <th>Region</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" class="checkboxes" runat="server" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <%# Eval("SubDepartmentName") %>
                                </td>
                                <td>
                                    <%# Eval("DepartmentName") %>
                                </td>
                                <td>
                                    <%# Eval("SubParentMenuName") %>
                                </td>
                                <td>
                                    <%# Eval("IndustryName") %>
                                </td>
                                <td>
                                    <%# Eval("RegionName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Industries<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlIndustries" class="form-control select2ddl req reqdep" OnSelectedIndexChanged="ddlIndustries_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlRegion" SelectionMode="Multiple" class="form-control req reqdep multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Sub Department<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlSubDepartment" oldname="" onchange="CheckName(this);" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Sub Parent Menu<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="lstSubParentMenu" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-actions right">
                <div class="row">
                    <div class="col-md-12"> 
                        <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
    <script>
        function CheckName(args) {
            debugger
            var value = $(args).val();
            $(args).val(value);
            if (value == "") {
                return;
            }
            var Data = JSON.stringify({ value: value, oldname: $(args).attr('oldname'), industry: $('#<%= ddlIndustries.ClientID%>').val() });
            $.ajax({
                dataType: "json",
                type: "POST",
                data: Data,
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "SubDepartmentMenuAccess.aspx/CheckName",
                success: function (Result) {
                if (Result.d != "0") {
                    ShowWarning('Sorry, Record Already exist')
                    $(args).val($(args).attr('oldname'));
                    $('#<%= ddlDepartment.ClientID%>').val('');
                    $('#select2-chosen-3').html('');
                }
            },
            error: function (errMsg) {
                ShowError(errMsg);
            }
        });
        }
    </script>
</asp:Content>
 


