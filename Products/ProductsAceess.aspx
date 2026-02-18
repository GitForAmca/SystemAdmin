<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ProductsAceess.aspx.cs" Inherits="SystemAdmin.Products.ProductsAceess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="col-md-12 col-sm-12">
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="Products Access"></asp:Label>
            </div>
        </div>
        <div id="divView" runat="server" class="portlet-body">
            <div class="row">  
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label">Employee Name<span class="required" aria-required="true"> </span></label>
                        <asp:DropDownList runat="server" ID="ddlEmpNameFilter" CssClass="form-control req select2ddl"></asp:DropDownList>
                    </div>
                </div> 
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"> </span></label>
                        <div>
                            <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="btn blue" OnClick="btnGet_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn default" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 pull-right">
                    <div class="form-group">
                        <label class="control-label"><span class="required" aria-required="true"></span></label>
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
                                        <th>Employee</th>
                                        <th>Products</th>
                                        <th>Active</th> 
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
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <a href="javascript:void(0)" onclick="GetProductList(<%# Eval("Autoid")%>)"><%#Eval("EmpName")%></a>
                                </td>
                                <td>
                                    <%#Eval("CountProduct")%>
                                </td>
                                <td>
                                    <span class='<%# Eval("active").ToString() == "1"
                                        ? "label label-sm label-success"
                                        : "label label-sm label-warning" %>'>
                                        <%# Eval("active").ToString() == "1" ? "Active" : "Inactive" %>
                                    </span>
                                </td>
                  
                            </tr>
                        </itemtemplate>
                        <emptydatatemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Employee</th>
                                        <th>Active</th> 
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
                <div class="row">
                    <div class="col-md-3" runat="server" id="divEmpDDL">
                        <div class="form-group">
                            <label class="control-label">Employee Name<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlEmpName" CssClass="form-control req select2ddl"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divEmptxt">
                        <div class="form-group">
                            <label class="control-label">Employee Name<span class="required" aria-required="true"> *</span></label>
                            <asp:Label runat="server" ID="lblEmpName" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>               
                <div class="row"> 
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Product List<span class="required" aria-required="true">*</span></label>
                            <asp:CheckBoxList ID="chkProductList" runat="server" RepeatDirection="Horizontal" DataTextField="ProductsName" DataValueField="Autoid" />
                        </div>
                    </div> 
                </div>
                <div class="form-actions right">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn blue" OnClientClick="return CheckRequiredField('req');" OnClick="btnsave_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn default" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="PopUpProductList" tabindex="-1" data-width="400" class="modal fade" style="display: none">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-green">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                <h4 class="modal-title" style="color: #fff;">Products</h4>
            </div>
            <div class="modal-body">
                <!-- BEGIN FORM-->
                <div class="form form-horizontal">
                    <div class="form-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h4><strong id="lblEmpNamePopUp"></strong></h4>
                            </div>
                            <div class="col-md-12" id="divGetProductList"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END FORM-->
        </div>
    </div>
</div>
<asp:HiddenField ID="hidID" runat="server" />
<script>
    function GetProductList(EmpId){
        var Data = JSON.stringify({ EmpId: EmpId });
        $.ajax({
            url: "ProductsAceess.aspx/GetProductList",
            data: Data,
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (mydata) {
                debugger
                var row = " <table class='table table-bordered'>";
                row += "<thead><tr style='background: #444d58; color: #fff'><th>Name</th></tr></thead>";
                if (mydata.d != "") {
                    var jsondata = JSON.parse(mydata.d);
                    $("#lblEmpNamePopUp").html(jsondata[0]["EmpName"]);
                    row += "<tbody>";
                    $.each(jsondata, function (key, value) {
                        row += "<tr><td>" + value.ProductsName + "</td></tr>";
                    });
                    row += "</tbody>";
                }
                row += "</table>";
                $("#divGetProductList").html(row);
                $("#PopUpProductList").modal({
                    backdrop: 'static',
                    keyboard: false
                });
            }
        });
        $("#divGetProductList").html('');
    }
</script>
</asp:Content>
