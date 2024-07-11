using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using SystemAdmin.App_Code;


namespace SystemAdmin.App_Code
{
    public class clsMail
    {
        public string SendLoginOTP(int EmpId)
        {
            string msg = "";
            LoginPL PL = new LoginPL();
            PL.OpCode = 2;
            PL.AutoId = EmpId;
            LoginDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                string otp = PL.dt.Rows[0]["OTPCode"].ToString();
                string StrBoby = "<p>Dear " + PL.dt.Rows[0]["Employeename"].ToString() + ",</p>";
                StrBoby += "<p>To verify your User Id, please use the below Verification Code. Do not share this code with others, including AMCA employees.</p>";
                StrBoby += "<p>Your Verification Code: <span style='font-size: 18px; font-weight: bold'>" + otp + "</span></p>";
                StrBoby += "<p>Regards,<br>AMCA</p>";
                string status = new clsGeneral().SendMailOTP("", "", PL.dt.Rows[0]["UserName"].ToString(), "", "", "Your Login request using Verification Code", StrBoby, "", 587, true);
                if (status == "Successful")
                {
                    msg = "Success";
                }
                else
                {
                    msg = "Failed";
                }
            }
            return msg;
        }
    }
}