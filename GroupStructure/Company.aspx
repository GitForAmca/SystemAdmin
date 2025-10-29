<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeBehind="Company.aspx.cs" Inherits="SystemAdmin.GroupStructure.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Company"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Group<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlGroupFilter" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Organization<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList ID="ddlOrganizationSearch" class="form-control select2ddl" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2ddl">
                            <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <label class="control-label"></label>
                    <div class="form-group">
                        <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"></span></label>
                        <div>
                            <div class="btn-group pull-right">
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
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Company" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr class="bgprimary">
                                        <th>#</th>
                                        <th>Company</th>
                                        <th>Organization</th>
                                        <th>Group</th>
                                        <th>Industry</th>
                                        <th>Region</th> 
                                        <th>Website</th>
                                        <th>Head of Company</th>
                                        <th>Is Active</th>
                                        <th>Created</th>
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
                                    <%# Eval("CompanyName") %>
                                </td>
                                <td>
                                    <%# Eval("Organization") %>
                                </td>
                                <td>
                                    <%# Eval("GroupName") %>
                                </td>
                                <td>
                                    <%# Eval("Industry") %>
                                </td>
                                <td>
                                    <%# Eval("Region") %>
                                </td> 
                                <td>
                                    <asp:HyperLink ID="hlWebsite" runat="server" NavigateUrl='<%#  Eval("Website") != null && !Eval("Website").ToString().StartsWith("http", StringComparison.OrdinalIgnoreCase) ? "http://" + Eval("Website")  : Eval("Website") %>' Target="_blank" Text='<%# Eval("Website") %>'></asp:HyperLink> 
                                </td>
                                <td>
                                    <%# Eval("HOD") %>
                                </td>
                                <td>
                                    <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                </td>
                                <td>
                                    By :  <%# Eval("CreatedBy") %>
                                    <br />
                                    On : <%# Eval("CreatedOn") %> 
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
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Company Name<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtCompanyName" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Organization<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlOrganization" class="form-control select2ddl req" OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Head of Company<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlHOD" class="form-control select2ddl req"   runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Unit No<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtUnitNo" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Tower<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtTower" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Area<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtArea" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlRegion" class="form-control select2ddl req" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">States<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlStates" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">TRN No<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtTRN" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Stamp URL </label>
                            <asp:TextBox ID="txtStampURL" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Group Address</label>
                            <asp:TextBox ID="txtGroupAddress" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Email<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtEmail" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Mobile No<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtMobileNo" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Tel No<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtTelNo" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Domain <span class="required" aria-required="true">*</span></label>
                            <asp:DropDownList ID="ddlDomain" OnSelectedIndexChanged="ddlDomain_SelectedIndexChanged" AutoPostBack="true" class="form-control select2ddl req" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Website<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtWebsite" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Location URL<span class="required" aria-required="true"> *</span></label>
                            <asp:TextBox ID="txtLocationURL" class="form-control req" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Logo URL</label>
                            <asp:TextBox ID="txtLogourl" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Work Locations</label>
                            <asp:ListBox ID="lstworklocations"  SelectionMode="Multiple" class="form-control req multiselectddl" runat="server"  ></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group" style="padding-top: 25px;">
                            <label class="control-label">Active</label>
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="col-md-12 pull-right">
                        <div class="form-group pull-right">
                            <asp:Button ID="btnAdd" runat="server" class="btn blue" OnClick="btnAdd_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hidAutoid" runat="server" Value="" />
</asp:Content>
