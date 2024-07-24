<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ELFormat.aspx.cs" Inherits="SystemAdmin.ESS.ELFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="CRM CS Mapping"></asp:Label>
                </div>
            </div> 
            <div class="portlet-body form">
                <div class="form-body">
                    <div class="row"> 
                        <div class="col-md-2">  
                            <div class="form-group"> 
                                <label class="control-label">Company<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList ID="ddlUpdateGroupCompany" class="form-control reqs select2ddl" OnSelectedIndexChanged="ddlUpdateGroupCompany_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div> 
                        <div class="col-md-2">  
                            <div class="form-group"> 
                                <label class="control-label">Service Category<span class="required" aria-required="true"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlServiceTypeName" CssClass="form-control reqs select2ddl" OnSelectedIndexChanged="ddlServiceTypeName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> 
                            </div>
                        </div>  
                        <div class="col-md-2"> 
                            <div class="form-group">
                                <label class="control-label">Assignment<span class="required" aria-required="true"> *</span></label> 
                                <asp:DropDownList runat="server" ID="ddlAssignment" CssClass="form-control reqs select2ddl"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label"></label> 
                            <div style="margin-top: 8px;">
                                <asp:Button ID="btnshow" runat="server" CssClass="btn green" OnClick="btnshow_Click" Text="Get" OnClientClick="return CheckRequiredField('reqs')" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>
                    
                    <!--1 Objectives -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <h3><strong>Objectives</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckObjectives"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveObjectives" runat="server" CssClass="btn blue" OnClick="btnsaveObjectives_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Objectives" />
                        </div>
                    </div>
                    <!--2 Scope of Work -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>Scope of Work</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckScopeofWork"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveScopeofWork" runat="server" CssClass="btn blue" OnClick="btnsaveScopeofWork_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Scope of Work" />
                        </div>
                    </div>
                    <!--3 FIRST-PARTY RESPONSIBILITY -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>First Party Responsibility</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckFirsPartR"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveFirsPartR" runat="server" CssClass="btn blue" OnClick="btnsaveFirsPartR_Click" OnClientClick="return CheckRequiredField('req')" Text="Update First Party Responsibility" />
                        </div>
                    </div>
                    <!--4 Second-PARTY RESPONSIBILITY -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>Second Party Responsibility</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckSecondPartR"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveSecond" runat="server" CssClass="btn blue" OnClick="btnsaveSecond_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Second Party Responsibility" />
                        </div>
                    </div>
                    <!--5 Second-PARTY LIMITATIONS -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>Limitations</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckLimitations"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveLimitations" runat="server" CssClass="btn blue" OnClick="btnsaveLimitations_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Limitations" />
                        </div>
                    </div>
                    <!--6 Report -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>Report</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckReport"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveReport" runat="server" CssClass="btn blue" OnClick="btnsaveReport_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Report" />
                        </div>
                    </div>
                    <!--7 OTHER MATTERS -->
                    <div class="row">
                        <div class="col-md-12"> 
                            <hr />
                            <h3><strong>Other Matters</strong></h3>
                        </div> 
                        <div class="col-md-12">
                            <textarea class="ckeditor" rows="20" runat="server" id="ckOtherMatters"></textarea>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnsaveOtherMatters" runat="server" CssClass="btn blue" OnClick="btnsaveOtherMatters_Click" OnClientClick="return CheckRequiredField('req')" Text="Update Other Matters" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidID" runat="server" />
</asp:Content>




