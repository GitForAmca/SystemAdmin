<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeBehind="ReviewMasterControl.aspx.cs" Inherits="SystemAdmin.ESS.ReviewMasterControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Review Master Control"></asp:Label>
                </div>
            </div> 
            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Group<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlGroupFilter" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label">Element Name<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlElementName" CssClass="form-control select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label">Service Type Name<span class="required" aria-required="true"> </span></label>
                            <asp:DropDownList runat="server" ID="ddlServiceTypeName" CssClass="form-control select2ddl"></asp:DropDownList> 
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
                                            <asp:LinkButton ID="lnkbtnUpdate" OnClick="lnkbtnUpdate_Click" runat="server" OnClientClick="return CheckOnlyOneSelect('checkboxes')"><i class="fa fa-ra"></i> Update</asp:LinkButton>
                                        </li>
                                    </ul> 
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="LV_Review_Control" runat="server" ItemPlaceholderID="itemplaceholder">
                            <LayoutTemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>     
                                            <th>Is Review</th>    
                                            <th>Service Type Name</th>
                                            <th>Element Name</th>
                                            <th>Updated On/By</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server"  CssClass="checkboxes chkselectAdvance chkselectfordelete" AutoId='<%# Eval("AutoId")%>' />
                                        <span class='<%# bool.Parse( Eval("IsReview").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsReview").ToString())==true?"Yes":"No"%></span>
                                    </td>
                                    <td>
                                        <%# Eval("ServiceTypeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("ElementName")%>
                                    </td>
                                    <td>
                                        <%# Eval("UpdatedOn")%> / <%# Eval("UpdatedByName")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="table table-bordered">
                                    <tbody>
                                        <tr><td colspan="50" style="text-align:center">No Record Found</td></tr>
                                    </tbody>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="PopUpUpdateReviewControl" tabindex="-1" data-width="400" class="modal fade" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-green">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" style="color: #fff;">Update Control</h4>
                </div>
                <div class="modal-body">
                    <!-- BEGIN FORM-->
                    <div class="form form-horizontal">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:RadioButton ID="Reviewyes" Text="Yes" runat="server" GroupName="Review" />
                                    <asp:RadioButton ID="Reviewno" Text="No" runat="server" GroupName="Review" />
                                </div>
                            </div>                                   
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateControl" runat="server" CssClass="btn blue" Text="Update" OnClick="btnUpdateControl_Click" />
                    <button type="button" data-dismiss="modal" class="btn">Close</button>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" />
    <script>
        function OpenPopUpUpdateReviewControl() {
            $("#PopUpUpdateReviewControl").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
</asp:Content>
