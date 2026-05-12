<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="RequestBox.aspx.cs" Inherits="SystemAdmin.ESS.Request_Box" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Request Box"></asp:Label>
                </div>
            </div> 

            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <%--action div start--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Request<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlRequestFilter" OnSelectedIndexChanged="ddlIndustryFilter_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Department<span class="required" aria-required="true"></span></label>
                            <asp:ListBox ID="lstDepartmentFilter" SelectionMode="Multiple" class="form-control select2ddl reqRec" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Type <span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlTypeFilter" class="form-control select2ddl reqRec" runat="server">
                            <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                            <asp:ListItem Value="Automatic" Text="Automatic"></asp:ListItem>
                            <asp:ListItem Value="Manual" Text="Manual"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Reciever Type<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlRecieverTypeFilter" OnSelectedIndexChanged="ddlRecieverType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2ddl reqRec" runat="server">
                                <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                <asp:ListItem Value="CEC-CSR" Text="CEC-CSR"></asp:ListItem>
                                <asp:ListItem Value="CRM" Text="CRM"></asp:ListItem>
                                <asp:ListItem Value="CSR" Text="CSR"></asp:ListItem>
                                <asp:ListItem Value="EA" Text="EA"></asp:ListItem>
                                <asp:ListItem Value="Operation" Text="Operation"></asp:ListItem>
                                <asp:ListItem Value="Static" Text="Static"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Request Action<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlRequestActionFilter" CssClass="form-control select2ddl reqRec" runat="server">
                                <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                <asp:ListItem Value="Assignment" Text="Assignment"></asp:ListItem>
                                <asp:ListItem Value="CEM" Text="CEM"></asp:ListItem>
                                <asp:ListItem Value="Client" Text="Client"></asp:ListItem>
                                <asp:ListItem Value="Questionnaire" Text="Questionnaire"></asp:ListItem>
                                <asp:ListItem Value="Report" Text="Report"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlIsActiveFilter" runat="server" CssClass="form-control select2ddl">
                                <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label"><span class="required" aria-required="true"></span></label>
                            <div>
                                <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
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
                                            <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i> Add</asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    <%--action div End--%>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_LicenseIndustry" runat="server" OnItemDataBound="LV_LicenseIndustry_ItemDataBound" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr class="bgprimary">
                                            <th>#</th>
                                            <th>Request</th> 
                                            <th>Department</th>
                                            <th>Assigner</th>
                                            <th>Type</th>
                                            <th>Is Active</th>
                                            <th>Created</th>
                                            <th>Updated</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("AutoId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("RequestName")%>
                                    </td>
                                    <td id="td_approvers">
                                        <div class="company-tags"><%# Eval("Department")%> </div>
                                    </td>
                                    <td>
                                        <div style="display: flex; margin-top: 3px;">
                                            <div><span style="width: 110px; display: inline-block; background: #ddd; padding: 0px 4px">P Assigner :</span></div>
                                            <div><%# Eval("PrimaryAssigner") %></div>
                                        </div>
                                        <div style="display: flex; margin-top: 3px;">
                                            <div><span style="width: 110px; display: inline-block; background: #ddd; padding: 0px 4px">S Assigner :</span></div>
                                            <div><%# Eval("SecondaryAssigner") %></div>
                                        </div>
                                        <div style="display: flex; margin-top: 3px;">
                                            <div><span style="width: 110px; display: inline-block; background: #ddd; padding: 0px 4px">Reciever Type :</span></div>
                                            <div><%# Eval("ReceiverType") %></div>
                                        </div>
                                        <div style="display: flex; margin-top: 3px;">
                                            <div><span style="width: 110px; display: inline-block; background: #ddd; padding: 0px 4px">Request Action :</span></div>
                                            <div><%# Eval("RequestType") %></div>
                                        </div>
                                    </td>
                                    <td>
                                        <%# Eval("Type")%>
                                    </td>
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                    </td>
                                    <td>
                                        By : <%# Eval("CreatedBy") %><br />
                                        On : <%# Eval("CreatedOn") %> 
                                    </td>
                                    <td>
                                        By : <%# Eval("UpdatedBy") %><br />
                                        On : <%# Eval("UpdatedOn") %> 
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Request</th> 
                                            <th>Department</th>
                                            <th>Assigner</th>
                                            <th>Type</th>
                                            <th>Is Active</th>
                                            <th>Created</th>
                                            <th>Updated</th>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Request Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtRequestName" onblur="checkRequestType(this);"  CssClass="form-control reqRec" runat="server" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Department<span class="required" aria-required="true"> *</span></label>
                                <asp:ListBox ID="lstDepartment" SelectionMode="Multiple" class="form-control select2ddl reqRec" runat="server"></asp:ListBox>
                            </div>
                        </div> 
                        <div class="col-md-3">
                            <label class="control-label">Type <span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlType" class="form-control select2ddl reqRec" runat="server">
                                <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                <asp:ListItem Value="Automatic" Text="Automatic"></asp:ListItem>
                                <asp:ListItem Value="Manual" Text="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Reciever Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlRecieverType" OnSelectedIndexChanged="ddlRecieverType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2ddl reqRec" runat="server">
                                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                    <asp:ListItem Value="CEC-CSR" Text="CEC-CSR" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="CRM" Text="CRM"></asp:ListItem>
                                    <asp:ListItem Value="CSR" Text="CSR"></asp:ListItem>
                                    <asp:ListItem Value="EA" Text="EA"></asp:ListItem>
                                    <asp:ListItem Value="Operation" Text="Operation"></asp:ListItem>
                                    <asp:ListItem Value="Static" Text="Static"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div runat="server" id="divRequestAction">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Request Action<span class="required" aria-required="true"> *</span></label>
                                    <asp:DropDownList ID="ddlRequestAction" CssClass="form-control select2ddl reqRec" runat="server">
                                        <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                                        <asp:ListItem Value="Assignment" Text="Assignment"></asp:ListItem>
                                        <asp:ListItem Value="CEM" Text="CEM"></asp:ListItem>
                                        <asp:ListItem Value="Client" Text="Client"></asp:ListItem>
                                        <asp:ListItem Value="Questionnaire" Text="Questionnaire"></asp:ListItem>
                                        <asp:ListItem Value="Report" Text="Report"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divPrimaryOrSecondaryAssigner" visible="false">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Primary Person<span class="required" aria-required="true"> *</span></label>
                                    <asp:DropDownList ID="ddlPrimaryPerson" class="form-control select2ddl reqRec" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Secondary Person<span class="required" aria-required="true"> *</span></label>
                                    <asp:DropDownList ID="ddlSecondayPerson" CssClass="form-control select2ddl reqRec" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label">Menu<span class="required" aria-required="true"></span></label>
                                <asp:ListBox ID="lstMenu" SelectionMode="Multiple" class="form-control select2ddl" runat="server"></asp:ListBox>
                            </div>
                        </div> 
                    </div> 
                    <div class="row">
                        <div class="col-md-1 pull-right"> 
                            <asp:CheckBox ID="chkactive" runat="server" class="form-control" Style='border: none !important' Checked="true" Text="Is Active" />
                        </div> 
                    </div>
                    <div class="row">
                        <div class="col-md-1 pull-right">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClick="btnsave_Click" OnClientClick="return CheckRequiredField('reqRec')" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click"   />
                        </div>
                    </div> 
                </div> 
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hidID" runat="server" />    
    
    <style>
        .company-tags {
            max-height: 80px;
            overflow-y: auto;
            padding: 5px;
            display: flex;
            gap: 4px;
            flex-direction: column;
        }

        .company-tag {
            display: inline-block;
            background-color: #e0f0ff;
            color: #004080;
            padding: 2px 8px;
            border-radius: 12px;
            font-size: 12px;
            white-space: nowrap;
        }

        .company-tags {
            max-height: 80px;
            overflow-y: auto;
            padding: 5px;
            display: flex;
            gap: 4px;
            flex-direction: column;
        }
    </style>

    <script> 
        function checkRequestType(args) {
            var text = $(args).val().trim();
            $(args).val(text);
            if (text.trim() == "") {
                return;
            }
            var Data = JSON.stringify({ reuqestName: text });
            $.ajax({
                dataType: "json",
                type: "POST",
                data: Data,
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "RequestBox.aspx/checkRequestType",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] Request Name already exist')
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




