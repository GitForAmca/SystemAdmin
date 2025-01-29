<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="PortalSettings.aspx.cs" Inherits="SystemAdmin.ESS.PortalSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Portal Settings"></asp:Label>
                </div>

                <div class="pull-right" style="display: flex; gap: 5px; align-items: center; justify-content: center; padding: 4px 0;">
                    <asp:LinkButton
                        ID="lnk_savecomdoj"
                        runat="server"
                        CssClass="btn bgGreen"
                        OnClick="btnSave"
                        OnClientClick="return CheckRequiredField('req')"
                        ToolTip="Save">
            <i class="fa fa-floppy-o"></i> 
                    </asp:LinkButton>

                    <asp:LinkButton
                        ID="isEditBtnCompany"
                        runat="server"
                        CssClass="btn bgGreen"
                        OnClick="btnEdit"
                        ToolTip="Edit">
            <i class="fa fa-pencil"></i> 
                    </asp:LinkButton>
                </div>
            </div>

            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="section-title">Portal Downtime</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Start Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlStartTime" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">End Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlEndTime" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Downtime Message<span class="required" aria-required="true">*</span></label>
                            <asp:TextBox ID="txtDowntimeMessage" CssClass="form-control req reqField" runat="server" placeholder="Insert here"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Excluded Employees</label>
                            <asp:ListBox ID="ddlExcludedEmployees" SelectionMode="Multiple" class="form-control multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlRegion" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <h4 class="section-title">Organizational Portal Downtime</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Start Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlStartTimeOrg" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">End Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlEndTimeOrg" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Downtime Message<span class="required" aria-required="true">*</span></label>
                            <asp:TextBox ID="txtDowntimeMessageOrg" CssClass="form-control req reqField" runat="server" placeholder="Insert here"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Excluded Employees</label>
                            <asp:ListBox ID="ddlExcludedEmployeesOrg" SelectionMode="Multiple" class="form-control multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:ListBox ID="ddlRegionOrg" SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"></asp:ListBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <h4 class="section-title">HRMS Request</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Start Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlStartTimeHrms" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">End Time<span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlEndTimeHrms" runat="server" CssClass="form-control select2ddl req">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Validation Period<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlValidationPeriod" CssClass="form-control req" runat="server">
                                <asp:ListItem Text="Select Option" Value=""></asp:ListItem>
                                <asp:ListItem Text="1 Day" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 Days" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3 Days" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4 Days" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5 Days" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" />
    <script> 
        function DeleteQuestions(args) {
            if (CheckOnlyOneSelect('chkselect')) {
                Myconfirm('Do you want to Delete!', args);
            }
        }
    </script>
    <style>
        .disabled {
            pointer-events: none !important;
            background-color: #eeeeee !important;
        }

            .disabled ul, .disabled li {
                background-color: #eeeeee !important;
            }

            .disabled .select2-container-multi .select2-choices .select2-search-choice {
                border: 1px solid #bbb !important;
            }
    </style>
</asp:Content>
