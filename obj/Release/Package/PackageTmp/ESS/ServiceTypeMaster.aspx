<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="ServiceTypeMaster.aspx.cs" Inherits="SystemAdmin.ESS.ServiceTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Service Type Master"></asp:Label>
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
                                            <th>Service Type Name</th>
                                            <th>Manager</th>
                                            <th>Asst. Manager</th>
                                            <th>Primary Supervisor</th>
                                            <th>Secondary Supervisor</th>
                                            <th>Primary EA</th>
                                            <th>Secondary EA</th>
                                            <th>Primary Assigner</th>
                                            <th>Secondary Assigner</th>
                                            <th>Primary Reviewer</th>
                                            <th>Secondary Reviewer</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("ServiceTypeMainId")%>' GroupId='<%# Eval("GroupId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("ServiceTypeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ManagerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("AsstManagerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimarySupervisorName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondarySupervisorName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryEAName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryEAName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryAssignerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryAssignerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryReviewerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryReviewerName")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedOn")%>
                                    </td>
                                    <td>
                                        <%# Eval("Createdby")%>
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
                                            <th>Service Type Name</th>
                                            <th>Created date</th>
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Service Type Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtServiceName" oldname="" onblur="CheckName(this);" CssClass="form-control req" runat="server" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Manager<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList runat="server" ID="ddlManager" CssClass="form-control select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Asst. Manager<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlAsstManager" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Primary Supervisor<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimarySupervisor" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Secondary Supervisor<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondarySupervisor" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Primary EA<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryEA" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Secondary EA<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryEA" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Primary Assigner<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryAssigner" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Secondary Assigner<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryAssigner" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Primary Reviewer<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryReviewer" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Secondary Reviewer<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryReviewer" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">

                                <div class="form-group">
                                    <label class="control-label">&nbsp;</label>
                                    <asp:CheckBox ID="chkactive" runat="server" class="form-control req" Style='border: none !important' Checked="true" Text="Is Active" />
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
                url: "ServiceTypeMaster.aspx/CheckName",
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
    </script>
</asp:Content>