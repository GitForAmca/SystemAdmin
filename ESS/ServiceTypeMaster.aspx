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
                                            <th>Primary EA</th>
                                            <th>Secondary EA</th>
                                            <th>Primary Coordinator</th>
                                            <th>Secondary Coordinator</th>
                                            <th>Created On</th>
                                            <th>Created By</th>
                                            <th>IsActive</th>
                                            <th>Action</th>
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
                                        <asp:HiddenField ID="hdnMainId" runat="server" Value='<%#Eval("Autoid") %>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("ServiceTypeMainId")%>' GroupId='<%# Eval("GroupId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("ServiceTypeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryEAName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryEAName")%>
                                    </td>
                                    <td>
                                        <%# Eval("PrimaryCoordinatorName")%>
                                    </td>
                                    <td>
                                        <%# Eval("SecondaryCoordinatorName")%>
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
                                    <td>
                                        <asp:LinkButton ID="btnUpdateType" OnClick="btnUpdateType_Click" Text="<i class='fa fa-edit'></i>" data-toggle="tooltip" data-placement="top" title="Update Operations" CssClass="btn btn-xs bgGreen"  CommandArgument='<%# Eval("Autoid") %>' runat="server" />
                                    </td>
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Service Type Name</th>
                                            <th>Primary EA</th>
                                            <th>Secondary EA</th>
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Service Type Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtServiceName" oldname="" onblur="CheckName(this);" CssClass="form-control req" runat="server" placeholder="input here"></asp:TextBox>
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
                                <label class="control-label">Primary Coordinator<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryCoordinator" CssClass="form-control req select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Secondary Coordinator<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryCoordinator" CssClass="form-control select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group text-right">
                                <label class="control-label">&nbsp;</label>
                                <asp:CheckBox ID="chkactive" runat="server" class="form-control req" Style='border: none !important' Checked="true" Text="Is Active" />
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
    <div id="PopUpUpdateOperations" tabindex="-1" data-width="400" class="modal fade" style="display: none">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" style="color: #fff;">Update</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlTypeUpdate" OnSelectedIndexChanged="ddlTypeUpdate_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Manager<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList runat="server" ID="ddlManager" class="form-control select2ddl1 "></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Asst. Manager<span class="required" aria-required="true"> </span></label>
                                <asp:DropDownList runat="server" ID="ddlAsstManager" class="form-control select2ddl1 "></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Primary Supervisor<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimarySupervisor" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Secondary Supervisor<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondarySupervisor" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Primary Assigner<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryAssigner" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Secondary Assigner<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryAssigner" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Primary Reviewer<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlPrimaryReviewer" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Secondary Reviewer<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlSecondaryReviewer" class="form-control select2ddl1 reqpop"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ListView ID="LV_Operations" runat="server" ItemPlaceholderID="itemplaceholdercat">
                                <LayoutTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr class="tableStatusBg"> 
                                                <th>Type</th>
                                                <th>Manager</th>
                                                <th>Asst. Manager</th>
                                                <th>Primary Supervisor</th>
                                                <th>Secondary Supervisor</th>
                                                <th>Primary Assigner</th>
                                                <th>Secondary Assigner</th>
                                                <th>Primary Reviewer</th>
                                                <th>Secondary Reviewer</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <div id="itemplaceholdercat" runat="server"></div>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Type") %></td>
                                        <td><%#Eval("ManagerName") %></td>
                                        <td><%#Eval("AsstManagerName") %></td>
                                        <td><%#Eval("PrimarySupervisorName") %></td>
                                        <td><%#Eval("SecondarySupervisorName") %></td>
                                        <td><%#Eval("PrimaryAssignerName") %></td>
                                        <td><%#Eval("SecondaryAssignerName") %></td>
                                        <td><%#Eval("PrimaryReviewerName") %></td>
                                        <td><%#Eval("SecondaryReviewerName") %></td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Type</th>
                                                <th>Manager</th>
                                                <th>Asst. Manager</th>
                                                <th>Primary Supervisor</th>
                                                <th>Secondary Supervisor</th>
                                                <th>Primary Assigner</th>
                                                <th>Secondary Assigner</th>
                                                <th>Primary Reviewer</th>
                                                <th>Secondary Reviewer</th>
                                            </tr>
                                        </thead>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateOperationValues" runat="server" CssClass="btn green" Text="Update" OnClick="btnUpdateOperationValues_Click" OnClientClick="return CheckRequiredField('reqpop');" />
                    <button type="button" data-dismiss="modal" class="btn">Close</button>
                </div>
                <!-- END FORM-->
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
        function OpenPopUpUpdateOperations() {
            $("#PopUpUpdateOperations").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
</asp:Content>