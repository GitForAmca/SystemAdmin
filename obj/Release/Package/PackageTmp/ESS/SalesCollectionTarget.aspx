<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="SalesCollectionTarget.aspx.cs" Inherits="SystemAdmin.ESS.SalesCollectionTarget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Sales Collection Target"></asp:Label>
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
                    <div class="col-md-3">  
                        <div class="form-group">
                            <label runat="server" class="control-label">Type</label>
                            <asp:DropDownList ID="ddlSearchType" runat="server" class="form-control select2ddl">
                                <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                <asp:ListItem Value="Team" Selected="True" Text="Team"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group">
                            <label class="control-label"></label>
                            <div style="margin-top: 8px;">
                                <asp:Button ID="btnGet" runat="server" CssClass="btn green" Text="Get" OnClick="btnGet_Click" />
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
                                        <%--<li>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" Text="Edit" OnClientClick="return CheckOnlyOneSelect('chkselect');"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                        </li>--%>
                                    </ul> 
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_List_Sales_Target" runat="server" ItemPlaceholderID="itemplaceholder1" OnItemDataBound="lvSales_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>#</th>
                                            <th>Period</th>
                                            <th>Type</th>
                                            <th runat="server" id="thCECIndividual">Individual CEC</th>
                                            <th runat="server" id="thBDIndividual">Individual BD</th>
                                            <th runat="server" id="thParentTeamName">Parent Team</th>
                                            <th runat="server" id="thTeamName">Team</th>
                                            <th runat="server" id="thCECName">CEC Team</th>
                                            <th runat="server" id="thBDName">BD Team</th>
                                            <th>Lead</th>
                                            <th>EL</th>
                                            <th>Client</th>
                                            <th>Consultant</th>
                                            <th>Assignment</th>
                                            <th>Amount</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <div id="itemplaceholder1" runat="server"></div>
                                    </tbody>

                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="viewBtn">   
                                    <td>
                                        <asp:HiddenField ID="hidAutoid" runat="server" Value='<%# Eval("AutoId")%>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" Class="checkboxes chkSelect" AutoId='<%# Eval("AutoId")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("Period")%>
                                    </td>
                                    <td> 
                                        <%# Eval("TargetType")%>
                                       <%-- <asp:DropDownList ID="ddl_SearchTypeTbl" runat="server" class="form-control select2ddl" oldvalue='<%#Eval("TargetType") %>'>
                                            <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                            <asp:ListItem Value="Team" Selected="True" Text="Team"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                    <asp:Panel runat="server" ID="pnlCECIndividual">
                                        <td>  
                                            <asp:HiddenField ID="hdnCECIndName" runat="server" Value='<%# Eval("CECIndId")%>' />
                                            <asp:DropDownList ID="ddl_CECIndNameTbl" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>                             
                                        </td>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlBDIndividual">
                                        <td> 
                                            <asp:HiddenField ID="hdnBDIndName" runat="server" Value='<%# Eval("BDIndId")%>' />
                                            <asp:DropDownList ID="ddl_BDIndNameTbl" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </td>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlTeam">
                                        <td> 
                                            <asp:HiddenField ID="hdnParenTeam" runat="server" Value='<%# Eval("ParentTeam")%>' />
                                            <asp:DropDownList ID="ddl_ParentTeamNameTbl" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </td> 
                                        <td> 
                                        <%# Eval("TeamName")%>
                                           <%-- <asp:HiddenField ID="hdnTeamName" runat="server" Value='<%# Eval("Team")%>' />
                                            <asp:DropDownList ID="ddl_TeamNameTbl" runat="server" CssClass="form-control select2ddl"> 
                                            </asp:DropDownList>--%>
                                        </td> 
                                        <td> 
                                            <asp:HiddenField ID="hdnCECCollectionTeam" runat="server" Value='<%# Eval("CECTeam")%>' />
                                            <asp:DropDownList ID="ddl_CECCollectionTbl" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </td> 
                                        <td> 
                                            <asp:HiddenField ID="hdnBDCollectionTeam" runat="server" Value='<%# Eval("BDTeam")%>' />
                                            <asp:DropDownList ID="ddl_BDCollectionTbl" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </td>
                                    </asp:Panel>
                                    <td> 
                                        <asp:TextBox ID="txt_LeadTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetLead") %>' runat="server"></asp:TextBox>
                                    </td>
                                    <td> 
                                        <asp:TextBox ID="txt_ElTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetEL") %>' runat="server"></asp:TextBox> 
                                    </td>
                                    <td> 
                                        <asp:TextBox ID="txt_ClientTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetClient") %>' runat="server"></asp:TextBox> 
                                    </td>
                                    <td> 
                                        <asp:TextBox ID="txt_ConsultantTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetConsultant") %>' runat="server"></asp:TextBox> 
                                    </td>
                                    <td> 
                                        <asp:TextBox ID="txt_AssignmentTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetAssignment") %>' runat="server"></asp:TextBox> 
                                    </td>
                                    <td> 
                                        <asp:TextBox ID="txt_AmountTbl" CssClass="form-control NumberOnly" Text='<%#Eval("TargetAmount") %>' runat="server"></asp:TextBox> 
                                    </td>
                                    <td>
                                        <div class="form-group m-0">
                                            <div class="input-group">
                                                <span class="input-group-btn">
                                                    <asp:HiddenField ID="hidautoidUpdate" runat="server" Value='<%# Eval("Autoid")%>' />
                                                    <asp:LinkButton ID="UpdateRecord"  OnClick="UpdateRecord_Click" Text="<i class='fa fa-floppy-o'></i>" ToolTip="Update Record" CssClass="bgGreen" runat="server" /> 
                                                </span>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>Period</th>
                                            <th>Type</th>
                                            <th>Name</th>
                                            <th>Team Name</th>
                                            <th>CEC Team</th>
                                            <th>BD Team</th>
                                            <th>Lead</th>
                                            <th>EL</th>
                                            <th>Client</th>
                                            <th>Consultant</th>
                                            <th>Assignment</th>
                                            <th>Amount</th>
                                            <th>Action</th>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Period<span class="required" aria-required="true"> *</span></label>
                                <div class="input-group date-picker input-daterange">
                                    <asp:TextBox CssClass="form-control reqsc" ID="txtPeriodFrom" runat="server" Text="" placeholder="from" autocomplete="off"></asp:TextBox>
                                    <span class="input-group-addon">to </span>
                                    <asp:TextBox CssClass="form-control reqsc" ID="txtPeriodTo" runat="server" Text="" placeholder="to" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">   
                        <div class="col-md-3">  
                            <div class="form-group">
                                <label runat="server" class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlType" runat="server" class="form-control reqsc select2ddl" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="Select Option" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                    <asp:ListItem Value="Team" Text="Team"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>  
                        <div runat="server" id="divIndivdual" visible="false">
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">CEC<span class="required" aria-required="true"> </span></label>
                                            <asp:DropDownList runat="server" ID="ddlCECIndivdual" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </div>
                                    </div> 
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">BD<span class="required" aria-required="true"> </span></label>
                                            <asp:DropDownList runat="server" ID="ddlBDIndividual" CssClass="form-control select2ddl"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divTeam" visible="false">
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Parent Team Name<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList runat="server" ID="ddlParentTeamName" CssClass="form-control select2ddl reqsc"></asp:DropDownList>
                                        </div>
                                    </div> 
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Team Name<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList runat="server" oldname="" onchange="CheckName(this);" ID="ddlTeamName" CssClass="form-control select2ddl reqsc"></asp:DropDownList>
                                        </div>
                                    </div> 
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">CEC Team<span class="required" aria-required="true"> *</span></label>
                                            <asp:DropDownList ID="ddlSalesCollectionCECTeam" class="form-control reqsc" runat="server"></asp:DropDownList> 
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">BD Team<span class="required" aria-required="true"> </span></label>
                                            <asp:DropDownList ID="ddlSalesCollectionBDTeam" class="form-control" runat="server"></asp:DropDownList> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box green">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                Target <i class="fa fa-info-circle" aria-hidden="true" data-toggle="tooltip" data-placement="top" Title="Client Portal Admin"></i>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Amount<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtTargetAmount" CssClass="form-control reqsc NumberWithOneDot" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Lead<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtLead" CssClass="form-control reqsc NumberOnly" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Engagement Letter<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtTargetEL" CssClass="form-control reqsc NumberOnly" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Client<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtTargetClient" CssClass="form-control reqsc NumberOnly" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Consultant<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtTargetConsultant" CssClass="form-control reqsc NumberOnly" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="control-label">Assignment<span class="required" aria-required="true"> *</span></label>
                                                        <asp:TextBox ID="txtTargetAssignment" CssClass="form-control reqsc NumberOnly" runat="server" Text="0" placeholder="input here"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 text-right pull-right">      
                        </div> 
                    </div>
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn blue" OnClientClick="return CheckRequiredField('reqsc');" OnClick="btnAdd_Click" /> 
                            <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn default" OnClick="btnCancel_Click" /> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidId" runat="server" />
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtPeriodFrom').datepicker({
                format: "M-yyyy",
                startView: "months",
                minViewMode: "months",
                autoclose: true
            });
            $('#ContentPlaceHolder1_txtPeriodTo').datepicker({
                format: "M-yyyy",
                startView: "months",
                minViewMode: "months",
                autoclose: true
            });
        });
    </script>
    <script>
        function checkDelete(args) {
            var isok = CheckOnlyOneSelect('checkboxes');
            if (isok) {
                Myconfirm('Do you want to Delete?', args);
            }
        }
        function CheckName(args) {
            debugger
            var value = $(args).val();
            $(args).val(value);
            if (value == "") {
                return;
            }
            var Data = JSON.stringify({ value: value, oldname: $(args).attr('oldname'), periodfrom: $("#<%=txtPeriodFrom.ClientID%>").val(), periodto: $("#<%=txtPeriodTo.ClientID%>").val() });
        $.ajax({
            dataType: "json",
            type: "POST",
            data: Data,
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "SalesCollectionTarget.aspx/CheckName",
            success: function (Result) {
                if (Result.d != "0") {
                    ShowWarning('Sorry, Record Already exist')
                    $(args).val($(args).attr('oldname'));
                    $('#<%= ddlTeamName.ClientID%>').val('');
                    $('#select2-chosen-2').html('');
                }
            },
            error: function (errMsg) {
                ShowError(errMsg);
            }
        });
        }
    </script>
</asp:Content>