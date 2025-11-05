<%@ Page Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ShowAllEmailsFormat.aspx.cs" Inherits="SystemAdmin.ESS.ShowAllEmailsFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Style/CommonPolicy.css" rel="stylesheet" /> 
   
    <div class="col-md-12 col-sm-12">
        <div class="portlet box green">
           <%-- <div class="portlet-title">
                <div class="caption">
                    <asp:Label ID="lblPageListTitle" runat="server" Text="Email Format"></asp:Label>
                </div>
            </div> --%>
    </div>  
    <div id="DivAllActiveEmails" runat="server" title="Signature"></div>   
 </div> 
</asp:Content>