<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master"  AutoEventWireup="true" ValidateRequest="false" CodeBehind="HRMSEmailFormat.aspx.cs" Inherits="SystemAdmin.ESS.HRMSEmailFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Email Format"></asp:Label>
                </div>
            </div>
            <div id="divView" runat="server" class="portlet-body">
                <div class="row"> 
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Type</label>
                            <asp:DropDownList runat="server" ID="ddlTypeSearch" CssClass="form-control select2ddl req" OnSelectedIndexChanged="ddlTypeSearch_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="" Text="Select Type"></asp:ListItem>
                                <asp:ListItem Value="Internal" Text="Internal"></asp:ListItem>
                                <asp:ListItem Value="External" Text="External"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label class="control-label">Is Active<span class="required" aria-required="true"></span></label>
                            <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2ddl" OnSelectedIndexChanged="ddlIsActiveSearch_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Choose an item" Value=""></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                            </asp:DropDownList>
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
                                            <th>Name</th>
                                            <th>Function Name</th>
                                            <th>Subject</th>
                                            <th>Description</th> 
                                            <th>Type</th>  
                                            <th>Is Active</th>
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
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkboxes chkselect" Autoid='<%# Eval("Id")%>'  />
                                    </td>
                                    <td>
                                        <%# Eval("EmailName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Name")%>
                                    </td>
                                    <td>
                                        <%# Eval("Subject")%>
                                    </td>
                                    <td>
                                        <%# Eval("Description")%>
                                    </td>
                                    <td>
                                        <%# Eval("Type")%>
                                    </td> 
                                    <td>
                                        <span class='<%# bool.Parse( Eval("IsActive").ToString())==true?"label label-sm label-success":"label label-sm label-danger"%>' runat="server"><%# bool.Parse( Eval("IsActive").ToString())==true?"Yes":"No"%></span>
                                    </td>                                   
                                </tr>
                            </itemtemplate>
                            <emptydatatemplate>
                                <table class="table table-bordered table-hover mydatatable">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Type</th> 
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
                  
                    <div class="row" runat="server" id="divAssignment">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                 
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Function Name<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtFunctionName" CssClass="form-control req" runat="server" oldname="" onblur="CheckName(this);" placeholder="input Function Name"></asp:TextBox>
                                            </div>
                                        </div> 
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtname" CssClass="form-control req" runat="server" placeholder="input Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Type<span class="required" aria-required="true"> *</span></label>
                                                <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control select2ddl req">
                                                    <asp:ListItem Value="" Text="Select Type"></asp:ListItem>
                                                    <asp:ListItem Value="Internal" Text="Internal"></asp:ListItem>
                                                    <asp:ListItem Value="External" Text="External"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Subject<span class="required" aria-required="true"> *</span></label>
                                                <asp:TextBox ID="txtSubject" CssClass="form-control req" runat="server" placeholder="input Subject"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label class="control-label">Description<span class="required" aria-required="true"> </span></label>
                                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" class="form-control" runat="server"  placeholder="input here"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1" style="margin-top: -3%">
                                            <asp:CheckBox ID="chkactive" runat="server" class="form-control req" Style='border: none !important' Checked="true" Text="Is Active" />
                                        </div> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--1 Body -->
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                            <h3><strong>Email Body</strong></h3>
                        </div>
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckObjectives"></textarea>
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
     
 </div>
    <asp:HiddenField ID="hidID" runat="server" />
     
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
                url: "HRMSEmailFormat.aspx/CheckName",
                success: function (Result) {
                    if (Result.d != "0") {
                        ShowWarning('Sorry, [ ' + text + ' ] Email Name already exist')
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
