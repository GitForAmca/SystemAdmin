<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="LicenseActivity.aspx.cs" Inherits="SystemAdmin.ESS.LicenseActivity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="License Activity"></asp:Label>
                </div>
            </div> 

            <div id="divView" runat="server" class="portlet-body">
                <div class="row">
                    <%--action div start--%>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">License Activity<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlLicenseActivity" runat="server" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control select2ddl">
                                <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"></span></label>
                            <div> 
                            <asp:Button ID="btnGet" runat="server" class="btn blue" OnClick="btnGet_Click" Text="Get" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn default" OnClick="btnReset_Click" Text="Reset" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
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
                                        <li>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnDelete_Click" Text="Edit" OnClientClick="DeleteQuestions(this);return false;"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--action div End--%>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12"> 

                        <%--<asp:ListView ID="LV_LicenseActivity" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>License Activity</th>
                                            <th>Activity Code</th>
                                            <th>Is Active</th>
                                            <th>Added By</th>
                                            <th>Added On</th>
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
                                        <%# Eval("LicenseActivityName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ActivityCode")%>
                                    </td> 
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                    </td>  
                                    <td>
                                        <%# Eval("CreatedBy")%>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedOn")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>License Activity</th>
                                            <th>Activity Code</th>
                                            <th>Is Active</th>
                                            <th>Added By</th>
                                            <th>Added On</th>
                                        </tr>
                                    </thead>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>--%>
                         
                    <div class="col-md-2  pull-right">
                        <div class="form-group">
                        <input type="text" id="txtSearchActivity" class="form-control" placeholder="Search..." />
                        </div>
                    </div>


                        <asp:ListView ID="LV_LicenseActivity" runat="server" ItemPlaceholderID="itemplaceholder" OnPagePropertiesChanging="LV_LicenseActivity_PagePropertiesChanging">
                            <LayoutTemplate>
                                <table id="tblLicenseActivity" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>License Activity</th>
                                            <th>Activity Code</th>
                                            <th>Is Active</th>
                                            <th>Added By</th>
                                            <th>Added On</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemplaceholder" runat="server"></tr>
                                    </tbody>
                                </table> 

                                <div style="display: flex; justify-content: space-between; align-items: center; margin-top: 10px;">
                                    <!-- LEFT SIDE : TOTAL RECORDS -->
                                    <div>Total Records :
                                        <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="true"></asp:Label></div>

                                    <!-- RIGHT SIDE : PAGING -->
                                    <div>
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="LV_LicenseActivity" PageSize="10">
                                            <Fields>
                                                <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="btn btn-default" CurrentPageLabelCssClass="btn btn-primary" NextPageText="Next" PreviousPageText="Prev" ButtonCount="5" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </div>                                 
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="searchRow">
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("AutoId")%>' />
                                    </td>
                                    <td><%# Eval("LicenseActivityName") %></td>
                                    <td><%# Eval("ActivityCode") %></td>
                                    <td><span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "label label-success" : "label label-danger" %>'><%# Convert.ToBoolean(Eval("IsActive")) ? "Yes" : "No" %></span></td>
                                    <td><%# Eval("CreatedBy") %></td>
                                    <td><%# Eval("CreatedOn") %></td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered">
                                    <tr>
                                        <td>No Records Found</td>
                                    </tr>
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
                                <label class="control-label">License Activity<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtLicenseActivity" CssClass="form-control reqRec" runat="server" placeholder="input here"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">Activity Code<span class="required" aria-required="true"> *</span></label>
                                <asp:TextBox ID="txtActivityCode" CssClass="form-control reqRec" runat="server" placeholder="input here"></asp:TextBox>
                             </div>
                        </div>
                        <div class="col-md-1 pull-right">
                            <label class="control-label">&nbsp;</label>
                            <asp:CheckBox ID="chkactive" runat="server" class="form-control" Style='border: none !important' Checked="true" Text="Is Active" />
                        </div>
                    </div>
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClick="btnsave_Click" OnClientClick="return CheckRequiredField('reqRec')" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
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


        $(document).ready(function () {

            $("#txtSearchActivity").on("keyup", function () {

                var value = $(this).val().toLowerCase();

                $("#tblLicenseActivity tbody tr.searchRow").each(function () {

                    var found = $(this).text().toLowerCase().indexOf(value) > -1;

                    $(this).toggle(found);

                });

            });

        });
    </script>
</asp:Content>




