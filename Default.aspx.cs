using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using AMCAPropertiesAdmin.App_Code;
namespace AMCAPropertiesAdmin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["initialLoad"] = "0";
                if (Session["Login"] != null)
                {
                    DateTime Logintime = DateTime.Parse(Session["login_time"].ToString());
                    DateTime curtime = DateTime.Now;
                    int sessionActiveTime = Convert.ToInt32(curtime.Subtract(Logintime).TotalMinutes);
                    string hostName = Environment.MachineName.ToString();
                    // DataTable dtLogininfo = new UserBL().GetUserInformation(Session["UserId"].ToString(), Session["LoginsessionValue"].ToString(), 0, 0, 2, sessionActiveTime, null);
                    Session.Abandon();
                    Response.Redirect("~/Default.aspx", true);
                    //Response.Redirect("~/Dashboard/DashBoard.aspx", true);
                }
                pnllogin.Visible = true;
                pnlotp.Visible = false;
                // Session.Abandon();
                lblYear.Text = DateTime.Now.Year.ToString();
            }
        }
        void updateOTP(int id)
        {
            LoginPL PL = new LoginPL();
            PL.OpCode = 1;
            PL.AutoId = id;
            LoginDL.returnTable(PL);
        }
        protected void lnkBackBtn_Click(object sender, EventArgs e)
        {
            pnllogin.Visible = true;
            pnlotp.Visible = false;
            divBackButtom.Visible = false;
        }
        public string matchOTP(int id)
        {
            string msg = "";
            LoginPL PL = new LoginPL();
            PL.OpCode = 2;
            PL.AutoId = id;
            LoginDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                msg = PL.dt.Rows[0]["OTPCode"].ToString();
            }
            else
            {
                msg = "Failed";
            }
            return msg;
        }
        protected void btnMainLogin_Click(object sender, EventArgs e)
        {
            LoginPL PL = new LoginPL();
            PL.OpCode = 3;
            PL.UserName = txtusername.Text;
            LoginDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows[0]["OTPCode"].ToString().Equals(txtotp.Text.ToString()))
            {
                CreateSessionDetail(dt);
                Session.Timeout = 540;
            }
            else
            {
                RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowMOTP('Invalid Verification Code!!')</script>");
            }
        }
        protected void btnSendEmailForOTP_Click(object sender, EventArgs e)
        {
            string returnmsg = new clsMail().SendLoginOTP(Convert.ToInt32(hdnEmpId.Value));
            //string returnmsg = "Success";
            if (returnmsg == "Success")
            {
                RegisterStartupScript("applyCSS", "<script style='text/javascript' >SuccessOTP('Sent Successfully!!')</script>");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "countdown();", true);
            }
            else
            {
                RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowM('Something went wrong!!')</script>");
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtusername.Text != "" && txtpass.Text != "")
            {
                LoginPL PL = new LoginPL();
                PL.OpCode = 3;
                PL.UserName = txtusername.Text;
                LoginDL.returnTable(PL);
                DataTable dt = PL.dt;
                if (PL.dt.Rows.Count > 0)
                {
                    if (PL.dt.Rows[0]["active"].ToString() == "1" && PL.dt.Rows[0]["IsLoginEnabled"].ToString() != "0")
                    {
                        if (PL.dt.Rows[0]["UserPasswd"].ToString().Equals(txtpass.Text.ToString()))
                        {
                            int empid = Convert.ToInt32(PL.dt.Rows[0]["Autoid"].ToString());
                            updateOTP(empid);
                            hdnEmpId.Value = PL.dt.Rows[0]["Autoid"].ToString();
                            string returnmsg = "Success";
                            if (returnmsg == "Success")
                            {
                                lblEmpId.Text = PL.dt.Rows[0]["EmployeeId"].ToString();
                                lblHelloUsername.Text = "Hello, " + PL.dt.Rows[0]["Employeename"].ToString();
                                imgUserAvatar.ImageUrl = "https://portal.amca.ae/" + PL.dt.Rows[0]["PortalProfile"].ToString();
                                pnllogin.Visible = false;
                                pnlotp.Visible = true;
                            }
                        }
                        else
                        {
                            RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowM('Incorrect password entered!!')</script>");
                        }
                    }
                    else
                    {
                        RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowM('Not an active member!!')</script>");
                    }
                }
                else
                {
                    RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowM('Invalid user id!!')</script>");
                }
            }
            else
            {
                RegisterStartupScript("applyCSS", "<script style='text/javascript' >ShowM('Please enter userid and password')</script>");
            }
        }

        void updateLoginHistory(int id)
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];

            LoginPL PL = new LoginPL();
            PL.OpCode = 4;
            PL.AutoId = id;
            PL.IPAddress = ipAddress;
            LoginDL.returnTable(PL);
        }
        void CreateSessionDetail(DataTable dtLogininfo)
        {
            Session["UserAutoId"] = dtLogininfo.Rows[0]["Autoid"].ToString();
            Session["UserName"] = dtLogininfo.Rows[0]["Employeename"].ToString();
            updateLoginHistory(Convert.ToInt32(Session["UserAutoId"]));
            Response.Redirect("View/Dashboard.aspx", true);
        }
    }
}