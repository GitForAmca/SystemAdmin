<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="ChildMenu.aspx.cs" Inherits="SystemAdmin.Menu.ChildMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Child Menu"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Type<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlMenuTypeFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Parent Menu<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlParentMenuFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Sub Parent Menu<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlSubParentMenuFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2ddl">
                            <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Is Default<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlDefault" runat="server" CssClass="form-control select2ddl">
                            <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div> 
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
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
                                <li>
                                    <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnDelete_Click"><i class="fa fa-trash"></i>Delete</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_ParentMenu" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Menu Name</th>
                                        <th>Sub Parent Name</th>
                                        <th>Parent Name</th>
                                        <th>Type</th>
                                        <th>Industry</th>
                                        <th>Is Master Menu</th>
                                        <th>Is Default</th>
                                        <th>Is Active</th>
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
                                    <%# Eval("MenuName") %>
                                </td>
                                <td>
                                    <%# Eval("SubParentMenuName") %>
                                </td>
                                <td>
                                    <%# Eval("ParentMenuName") %>
                                </td>
                                <td>
                                    <%# Eval("MenuType") %>
                                </td>
                                <td>
                                    <%# Eval("Description") %>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsMasterMenu").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsMasterMenu").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsDefault").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsDefault").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
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
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlMenuType" class="form-control select2ddl req" OnSelectedIndexChanged="ddlMenuType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Parent Menu<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlParentMenu" class="form-control select2ddl req" OnSelectedIndexChanged="ddlParentMenu_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Sub Parent Menu<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlSubParentMenu" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtMenuName" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Url<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtMenuURL" class="form-control req" oldname="" onblur="CheckURL(this);" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3" id="divIndustries" runat="server">
                        <div class="form-group">
                            <label class="control-label">Industries<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlIndustries" class="form-control select2ddl req" OnSelectedIndexChanged="ddlIndustries_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divGroup" runat="server"> 
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlGroup" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlRegion" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Primary Person<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlPrimaryPerson" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Secondary Person<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlSecondayPerson" CssClass="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" id="divManualStatus" runat="server">
                        <div class="form-group">
                            <label class="control-label">Manual Status<span class="required" aria-required="true"> </span></label>
                            <asp:ListBox ID="lstStatus" SelectionMode="Multiple" class="form-control multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr />
                        <h4><strong><u>Action</u></strong></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <div class="form-group">
                            <label class="control-label">Name</label>
                            <asp:TextBox runat="server" ID="txtaction" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1 text-right">
                        <label class="control-label">&nbsp;</label>
                        <div>
                            <asp:Button ID="btnactionadd" CssClass="btn blue" runat="server" Text="Add Action" OnClick="btnactionadd_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="lvaction" runat="server" ItemPlaceholderID="plchldr">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr style="background: #f2f2f2">
                                            <th>Action Name</th>
                                            <%--<th>Delete</th>--%>
                                        </tr>
                                    </thead>
                                    <tr id="plchldr" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblActionname" runat="server" Text='<%# Eval("ActionName") %>' />
                                    </td>
                                    <%--<td>
                                        <asp:Button ID="btndelete" CssClass="btn btn-xs btn-danger" runat="server" Text="Delete" CommandArgument='<%# Container.DataItemIndex %>' OnClick="btndelete_Click" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Is Master Menu</label>
                            <asp:CheckBox ID="chkMasterMenu" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="col-md-12 pull-right text-right">
                        <div class="form-group">
                            <label class="control-label">Default</label>
                            <asp:CheckBox ID="chkDefault" runat="server"></asp:CheckBox>
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
        function CheckURL(args) {
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
                url: "ChildMenu.aspx/CheckURL",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] URL name already exist')
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
 
