<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AMCAPropertiesAdmin.Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="robots" content="noindex,nofollow">
    <meta name="googlebot" content="noindex,nofollow">
    <title>Login</title>
    <link href="https://fonts.googleapis.com/css2?family=Nunito:ital,wght@0,200..1000;1,200..1000&display=swap" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/")%>assets/global/plugins/font-awesome/css/font-awesome.min.css" />
    <link href="../../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css"> 
    <link href="css/icon.css" rel="stylesheet" type="text/css"> 
    <script src="../../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script> 
    <script src="../../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script> 
    <!-- END CORE PLUGINS --> 
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/global/plugins/select2/select2.min.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS --> 
    <script src="../../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout3/scripts/layout.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout3/scripts/demo.js" type="text/javascript"></script>
    <script src="../../assets/admin/pages/scripts/login-soft.js" type="text/javascript"></script>
    <script>
        function ShowM(msg) {
            $(".alert alert-danger display-hide").fadeIn('slow').delay(6000).fadeOut().find('span');
            $("#divMsg").fadeIn('slow').delay(8000).fadeOut().find('span').html(msg);
            $("#divMsg").focus();
        }
        function ShowMOTP(msg) {
            $(".alert alert-danger display-hide").fadeIn('slow').delay(6000).fadeOut().find('span');
            $("#divOTP").fadeIn('slow').delay(8000).fadeOut().find('span').html(msg);
            $("#divOTP").focus();
        }
        function SuccessOTP(msg) {
            $(".alert alert-success display-hide").fadeIn('slow').delay(6000).fadeOut().find('span');
            $("#divSuccessOTP").fadeIn('slow').delay(8000).fadeOut().find('span').html(msg);
            $("#divSuccessOTP").focus();
        }
    </script>
    <script>
        function startTime() {
            var today = new Date();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();
            m = checkTime(m);
            s = checkTime(s);
            document.getElementById('time').innerHTML =
                h + ":" + m + ":" + s;
            var t = setTimeout(startTime, 500);
        }
        function checkTime(i) {
            if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
            return i;
        }
    </script>
    <style type="text/css">
        body{
            font-family: "Nunito", sans-serif;
            background: #B1B2B3;
            background-size: cover;
        }
        .form-container{
            padding: 40px 30px;
            box-shadow: 0px 2px 7px rgba(0, 0, 0, 0.40);
            -moz-box-shadow: 0px 2px 7px rgba(0, 0, 0, 0.40);
            border-radius: 6px;
            position: absolute;
            width: 450px;
            left: 50%;
            right: 50%;
            transform: translate(-50%,-50%);
            top: 45%;
            background: #fff;
        }
        .form-container .form-control{
            height: 40px;
        }
        .form-container .linkweb a{
            color: #5e2193;
            font-weight: bold;
        }
        .img-middle{
            margin: 0 auto;
        }
        .btn-theme{
            background: #b7091e;
            width: 100%;
            color: #fff !important;
            font-weight: bold;
            outline: 0 !important;
        }
        .btn-theme:hover{
            color: #fff;
            opacity: 0.9;
            font-weight: 500;
        }
        .imgavatar{
            height: 130px;
            width: 130px;
            border-radius: 100%;
            object-fit: cover;
            object-position: top;
            margin-top: 20px;
        }
        .backbutton{
            text-align: center;
            border-radius: 100%;
            color: #038cd8;
        }
    </style>
</head>
    <body onload="startTime()">
        <div class="container-fluid">
            <div class="row">
                <div class="form-container">
                    <form id="frm" runat="server" class="login-form">
                        <asp:ScriptManager ID="scrptmngr" runat="server"></asp:ScriptManager> 
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <img src="../images/AmcaPropertiesMainLogo.svg" class="img-responsive img-middle" width="250">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <div id="time" style="font-size: 20px; font-weight: 500;"></div>  
                                <div id="date" style="background: #5e2193;color: #fff;padding: 3px 6px;display: inline-block;"></div>
                            </div>
                        </div>
                        <asp:Panel ID="pnllogin" runat="server" DefaultButton="btnLogin">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h1><strong>Login</strong></h1>
                                    <p>Enter your valid credential</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divMsg" class="alert alert-danger display-hide" style="display: none;">
                                        <button class="close" data-close="alert"></button>
                                        <span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Employee Id</label>
                                        <div class="field">
                                            <asp:TextBox ID="txtusername" runat="server" MaxLength="80" TabIndex="1" placeholder="EMP#000" CssClass="form-control form-control-solid placeholder-no-fix" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Password</label>
                                        <div class="field">
                                            <asp:TextBox ID="txtpass" runat="server" MaxLength="20" TextMode="Password" Text="" placeholder="Enter password here..." TabIndex="2" CssClass="form-control form-control-solid placeholder-no-fix" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group text-right">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-theme form-control" style="background-color:#D7B46A;" Text="Login" CausesValidation="false" OnClick="btnLogin_Click" TabIndex="3" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <div class="form-group">
                                        <p class="linkweb">Copyright &copy; <asp:Label runat="server" ID="lblYear"></asp:Label> <a href="javascript:void(0)" target="_blank">JOLAHA</a> </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlotp" runat="server" DefaultButton="btnLogin" visible="false">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <div class="form-group">
                                        <h2><strong>Verify your Identity</strong></h2>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <div class="form-group">
							            <asp:Image ID="imgUserAvatar" CssClass="imgavatar" runat="server" />
                                        <h3 style="color: #D7B46A"><strong><asp:Label runat="server" ID="lblHelloUsername"></asp:Label></strong></h3>
                                        <p><strong>(<asp:Label runat="server" ID="lblEmpId"></asp:Label></strong>)</p>
                                        <%--<p>Enter the Verification Code sent to <span style="font-weight: bold;"><asp:Label runat="server" ID="lblUserEmail"></asp:Label></span> </p>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divOTP" class="alert alert-danger display-hide" style="display: none;">
                                        <button class="close" data-close="alert"></button>
                                        <span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divSuccessOTP" class="alert alert-success display-hide" style="display: none;">
                                        <button class="close" data-close="alert"></button>
                                        <span></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Verification Code</label>
                                        <div class="field">
                                            <asp:TextBox ID="txtotp" runat="server" MaxLength="6" Text="" placeholder="Enter your verification code here..." TabIndex="3" CssClass="form-control form-control-solid placeholder-no-fix" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group text-right">
                                        <asp:Button ID="btnMainLogin" runat="server" CssClass="btn btn-theme form-control" style="background-color:#D7B46A;" Text="Verify" CausesValidation="false" OnClick="btnMainLogin_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <strong>or</strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <span class="timer">
                                        <span id="counter"></span>
                                    </span>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group text-center">
                                        <p style="color: green;" id="cou"></p>
                                        <asp:Button runat="server" ID="btnSendEmailForOTP" CssClass="btnsendagain" Text="Send Verification Code" OnClick="btnSendEmailForOTP_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12 text-center" runat="server"  id="divBackButtom" visible="false">
                                    <asp:LinkButton ID="lnkBackBtn" CssClass="backbutton" OnClick="lnkBackBtn_Click" runat="server"><i class="fa fa-chevron-left" aria-hidden="true"></i> Back to Login</asp:LinkButton>
                                </div>
                            </div>  
                        </asp:Panel>
                        <asp:HiddenField ID="hdnEmpId" runat="server" Value="" />
                        <script type="text/javascript">
                            function countdown() {
                                debugger
                                $.fn.timedDisable = function (time) {
                                    if (time == null) {
                                        time = 5;
                                    }
                                    var seconds = Math.ceil(time); 
                                    return $(this).each(function () {
                                        $(this).attr('disabled', 'disabled');
                                        var disabledElem = $(this);
                                        var originalText = this.innerHTML; 
                                        $("#cou").text('Resend in ' + originalText + ' ' + seconds + '');
                                        var interval = setInterval(function () {
                                            seconds = seconds - 1;
                                            $("#cou").text('Resend in ' + originalText + ' ' + seconds + '');
                                            if (seconds === 0) { 
                                                disabledElem.removeAttr('disabled')
                                                    .text(originalText); 
                                                clearInterval(interval); 
                                                $("#cou").text('');
                                            }
                                        }, 1000);
                                    });
                                };

                                $(function () {
                                    $('#btnSendEmailForOTP').timedDisable(60);
                                });
                            }
                        </script>
                    </form>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function formatDate(date) {
                var monthNames = [
                    "January", "February", "March",
                    "April", "May", "June", "July",
                    "August", "September", "October",
                    "November", "December"
                ];

                var day = date.getDate();
                var monthIndex = date.getMonth();
                var year = date.getFullYear();
                return monthNames[monthIndex] + ' ' + day + ', ' + year;
            }
            document.getElementById("date").innerHTML = formatDate(new Date());


        </script>
        <style type="text/css">
            .linkotp{
                color: blue;
            }
            .btnsendagain{
                white-space: normal;
                word-wrap: break-word;
                border: 0px;
                padding: 4px;
                border: 3px solid #018c25;
                background: #018c25;
                border-radius: 4px;
                color: #fff;
            }
        </style>
    </body>
</html>
 