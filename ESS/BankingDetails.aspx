<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="BankingDetails.aspx.cs" Inherits="SystemAdmin.ESS.BankingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Banking Details"></asp:Label>
                </div>
            </div> 
            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Company<span class="required" aria-required="true"></span></label> 
                            <asp:DropDownList runat="server" ID="ddlCompanyFilter" CssClass="form-control select2ddl"></asp:DropDownList> 
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Default<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList runat="server" ID="ddlIsDefault" CssClass="form-control">
                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
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
                        <asp:ListView ID="LV_Bank_Details" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>#</th>
                                            <%--<th>Group</th>--%>
                                            <th>Company</th>
                                            <th>Account Name</th>
                                            <th>Bank Name</th>
                                            <th>Account No</th>
                                            <th>IBAN</th>
                                            <th>Swift Code</th>
                                            <th>Bank Address</th>
                                            <th>Currency</th>
                                            <th>IsDefault</th>
                                            <th>IsActive</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <div id="itemplaceholder" runat="server"></div>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <asp:HiddenField ID="hdnId" Value='<%#Eval("Autoid") %>' runat="server" />
                                    <asp:HiddenField ID="hdnCompanyId" Value='<%#Eval("CompanyId") %>' runat="server" />
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" Class="checkboxes chkselectforId chkSelect" Autoid='<%# Eval("Autoid")%>' />
                                    </td>
                                    <%--<td><%# Eval("GroupName")%></td>--%>
                                    <td><%# Eval("CompanyName")%></td>
                                    <td><%# Eval("AccountName")%></td>
                                    <td><%# Eval("BankName")%></td>
                                    <td><%# Eval("AccountNo")%></td>
                                    <td><%# Eval("IBAN")%></td>
                                    <td><%# Eval("BranchSwiftCode")%></td>
                                    <td><%# Eval("BankAddress")%></td>
                                    <td><%# Eval("Currency")%></td>
                                    <td><span class='<%# bool.Parse( Eval("IsDefault").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsDefault").ToString())==true?"Yes":"No"%></span></td>
                                    <td><span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span></td>
                                    <td>
                                       <asp:LinkButton ID="btn_default" Text="Set as Default" OnClick="btn_default_Click" CssClass="btn btn-xs bgBlue" OnClientClick="defaultAddress(this);return false;" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr class="tableStatusBg">
                                            <th>#</th>
                                            <%--<th>Group</th>--%>
                                            <th>Company</th>
                                            <th>Account Name</th>
                                            <th>Bank Name</th>
                                            <th>Account No</th>
                                            <th>IBAN</th>
                                            <th>Swift Code</th>
                                            <th>Bank Address</th>
                                            <th>Currency</th>
                                            <th>IsDefault</th>
                                            <th>IsActive</th>
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Company<span class="required" aria-required="true"> *</span></label> 
                                <asp:DropDownList runat="server" ID="ddlCompanyAdd" CssClass="form-control req select2ddl"></asp:DropDownList> 
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Account Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtAccountName" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Bank Name<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtBankName" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Account No<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtAccountNo" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">IBAN<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtIBAN" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Swift Code<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtSwiftCode" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Bank Address<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtBankAddress" class="form-control req" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Currency<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlCurrency" CssClass="form-control req">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="AED" Value="AED"></asp:ListItem>
                                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                </asp:DropDownList>
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
        function defaultAddress(args) {
            Myconfirm('Do you want to Default this Address!', args);
        }
    </script>
</asp:Content>



