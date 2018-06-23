using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Net.Mail;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using System.Collections.Specialized;
//using System.Web.UI.WebControls;
using RazorEngine.Templating;
using RazorEngine;

namespace TABusinessLayer
{
   public class SendEmails
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        string e_mail_address = null;
        string foodname = null;
        string qty = null;
        string emailbody = null;
        string orderDate = null;
        public string AdminEmailAdd { get; set; }
        public string LogonEmailAdd { get; set; }
        public string LogonPwd { get; set; }
        public string AcctNumber { get; set; }
        public string AcctName { get; set; }
        public string AcctBank { get; set; }
        public string AdminPhone { get; set; }
        public string SubscriptionFee { get; set; }
        public string AdvertSubscriptionFee { get; set; }

        public void GetAdminEmailDetails()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd2 = new SqlCommand("spGetAdminDetails", con);
                cmd2.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rd2 = cmd2.ExecuteReader();

                if (rd2.HasRows)
                {
                    while (rd2.Read())
                    {
                        //bra = (string)rd2["bra"].ToString();
                        AdminEmailAdd = (string)rd2["AdminEmail"].ToString();
                        LogonEmailAdd = (string)rd2["LoginEmail"].ToString();
                        LogonPwd = (string)rd2["LoginPwd"].ToString();
                        AcctNumber = (string)rd2["AcctNumber"].ToString();
                        AcctName = (string)rd2["AcctName"].ToString();
                        AcctBank = (string)rd2["AcctBank"].ToString();
                        AdminPhone = (string)rd2["AdminPhone"].ToString();
                        SubscriptionFee = (string)rd2["SubscriptionFee"].ToString();
                        AdvertSubscriptionFee = (string)rd2["AdvertSubscriptionFee"].ToString();

                    }
                }
                rd2.Dispose();
            }
        }

        public void CheckExpiredRegistration()
        {
            GetAdminEmailDetails();
           // List<SellerDetails> cods = new List<SellerDetails>();

            if (AdminEmailAdd != null || AdminEmailAdd != "")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("spGetAboutToExpiredClients", con);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rd2 = cmd2.ExecuteReader();

                    if (rd2.HasRows)
                    {
                        while (rd2.Read())
                        {
                            SellerDetails cod = new SellerDetails();
                            cod.sellerEmail = (string)rd2["SellerEmail"].ToString();
                            cod.sellerName = (string)rd2["sellerName"].ToString();
                            cod.daysLeftToExpire = rd2["daysLeftToExpire"].ToString();
                            cod.AcctBank = AcctBank;
                            cod.AcctName = AcctName;
                            cod.AcctNumber = AcctNumber;
                            cod.AdminPhone = AdminPhone;
                            cod.SubscriptionFee = SubscriptionFee;
                            ExpiryWarningsToClient(cod);
                            //cods.Add(cod);
                        }
                    }
                    rd2.Dispose();
                }
                //Send emails
                

            }
        }


        public void CheckExpiredAdvertSlots()
        {
            GetAdminEmailDetails();
            // List<SellerDetails> cods = new List<SellerDetails>();

            if (AdminEmailAdd != null || AdminEmailAdd != "")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("spGetAboutToExpireAdvertSlots", con);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rd2 = cmd2.ExecuteReader();

                    if (rd2.HasRows)
                    {
                        while (rd2.Read())
                        {
                            SellerDetails cod = new SellerDetails();
                            cod.sellerEmail = (string)rd2["SellerEmail"].ToString();
                            cod.sellerName = (string)rd2["sellerName"].ToString();
                            cod.daysLeftToExpire = rd2["daysLeftToExpire"].ToString();
                            cod.AcctBank = AcctBank;
                            cod.AcctName = AcctName;
                            cod.AcctNumber = AcctNumber;
                            cod.AdminPhone = AdminPhone;
                            cod.SubscriptionFee = AdvertSubscriptionFee;
                            AdvertExpiryWarningsToClient(cod);
                            //cods.Add(cod);
                        }
                    }
                    rd2.Dispose();
                }
                //Send emails


            }
        }

        public void SendEnquiryToEmail(TABusinessLayer.ContactUs Enquiry)
        {
            GetAdminEmailDetails();
            if (AdminEmailAdd != null || AdminEmailAdd != "")
            {
                e_mail_address = AdminEmailAdd;//Admin email
                string todayis = Convert.ToString(DateTime.Now.ToString());
                emailbody = "Please find Enquiry details:\n Comment - \t" + (string)Enquiry.Comment.ToString() + ",\n from - " + (string)Enquiry.FirstName.ToString() + " " + (string)Enquiry.LastName.ToString() + ",\n email  - " + (string)Enquiry.Email.ToString() + ",\t Date - " + todayis + " -end of msg ";
                string subject = "Enquiry And Questions";
                SendEmail(emailbody, e_mail_address, subject);
            }
        }

       public void InformSellerOfContactRequest(SellerDetails det)
       {
            GetAdminEmailDetails();
            if (AdminEmailAdd != null || AdminEmailAdd != "")
            {
               
                CustomerSendEmail(det);
            }
       }
 
       public void CustomerSendEmail( SellerDetails model)
        {
            try
            {
                
                string Customertemplate =
                @"<html>
<head>
<style>
table {
    width:100%;
}
table, th, td {
    border: 1px solid #e56e94;
    border-collapse: collapse;
}
th, td {
    padding: 5px;
    text-align: left;
}
table#t01 tr:nth-child(even) {
    background-color: #ffffff;
}
table#t01 tr:nth-child(odd) {
   background-color:#ffdfdd;
}
table#t01 th	{
    background-color: #810541;
    color: white;
}
</style>
</head>


<body >
<div style='position:absolute; height:50px; font-size:15px; font face = 'cursive';
width:600px; background-color:#FBBBB9; padding:30px;'> 

<font color='#F6358A'; face='fantasy'>TRENDAFRIK</font><font color='#F6358A'; face='ar hermann'> AFRICAN FASHION FABRIQS!</font>
        </div>
        <br />


<div style='background-color: #ece8d4;style='color:grey; font-size:15px;'
    font face='Helvetica, Arial, sans-serif'
width:600px; height:600px; padding:30px; margin-top:30px;'>

<p>Dear @Model.sellerName,<p>
<br />

            <p>Please Be informed that Your Contact Details have been passed onto a Viewer on the Website</p>
            <p> With the following details:<br />
                <p>Name: @Model.sellerName <br />
                  Phone: @Model.sellerPhone
                      </p>  
<br />

<p> Your Address was not given so Customers from ALL Locations call you First! </p>

            <p>Thank you for allowing us serve you...</p><br />
            <a href='www.trendafrik.com'><b>TrendAfrik</b></a>

</body>
</html>"

                ; 
                //string mailBody = RazorEngine.Razor.Parse(Customertemplate, model);
                string mailBody = "<html> <head> <style> table {    width:100%;} table, th, td {    border: 1px solid #e56e94;     border-collapse: collapse;} th, td {    padding: 5px;     text-align: left;} table#t01 tr:nth-child(even) {    background-color: #ffffff; } table#t01 tr:nth-child(odd) {   background-color:#ffdfdd; } table#t01 th	{    background-color: #810541;     color: white; } </style> </head> <body > <div style='position:absolute; height:50px; font-size:15px; font face = 'cursive'; width:600px; background-color:#FBBBB9; padding:30px;'> <font color='#F6358A'; face='fantasy'>TRENDAFRIK</font><font color='#F6358A'; face='ar hermann'> BEAUTIFULLY AFRICAN!!</font> </div> <br /> <div style='background-color: #ece8d4;style='color:grey; font-size:15px;' font face='Helvetica, Arial, sans-serif' width:600px; height:600px; padding:30px; margin-top:30px;'> <p>Dear " + model.sellerName + ",<p> <br /> <p>Please Be informed that Your Contact Details have been passed onto a Viewer on the Website</p> <p> With the following details:<br /> <p>Name: " + model.sellerName + " <br /> Phone: " + model.sellerPhone + "  </p> <br />  <p> Your Address was not given so Customers from ALL Locations call you First! </p> <p>Thank you for allowing us serve you...</p><br /> <a href='www.trendafrik.com'><b>TrendAfrik</b></a> </body >";
                
                
                
                MailMessage mailMessage = new MailMessage(LogonEmailAdd, model.sellerEmail);//mailDefinition.CreateMailMessage(mailTo, ldReplacements, control); //(mailTo, ldReplacements,emailmessage,this );
                mailMessage.From = new MailAddress(LogonEmailAdd, "TrendAfrik");
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Your Online Picture Has Been Viewed";
                mailMessage.Body = mailBody;

                SmtpClient smtpClient = new SmtpClient("mail.trendafrik.com", 25);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = LogonEmailAdd,
                    Password = LogonPwd
                };
                smtpClient.EnableSsl = false;
                smtpClient.Send(mailMessage);


            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                throw;
            }

        }


       public void ExpiryWarningsToClient(SellerDetails model)
       {
           try
           {

               string Customertemplate =
               @"<html>
<head>
<style>
table {
    width:100%;
}
table, th, td {
    border: 1px solid #e56e94;
    border-collapse: collapse;
}
th, td {
    padding: 5px;
    text-align: left;
}
table#t01 tr:nth-child(even) {
    background-color: #ffffff;
}
table#t01 tr:nth-child(odd) {
   background-color:#ffdfdd;
}
table#t01 th	{
    background-color: #810541;
    color: white;
}
</style>
</head>
<body>

<body >
<div style='position:absolute; height:50px; font-size:15px; font face = 'cursive';
width:600px; background-color:#FBBBB9; padding:30px;'> 

<font color='#F6358A'; face='fantasy'>TRENDAFRIK</font><font color='#F6358A'; face='ar hermann'> AFRICAN FASHION FABRIQS!</font>
        </div>
        <br />


<div style='background-color: #ece8d4;style='color:grey; font-size:15px;'
    font face='Helvetica, Arial, sans-serif'
width:600px; height:600px; padding:30px; margin-top:30px;'>

<p>Dear @Model.sellerName,<p>
<br />

            <p>Please Be informed that Your Subscribtion expires in @Model.daysLeftToExpire day(s)</p>
            
            <br />
            <p>Call our Contact Number on @Model.AdminPhone for Renewal Details OR 
 Pay  into the following Account
<br/>
        Account Name: @Model.AcctName <br/>
        Account Number: @Model.AcctNumber <br/>
        Bank : @Model.AcctBank <br/>
        Subscription Fee: @Model.SubscriptionFee</p>
    <p><b>Please Use Your Registerd Name with us as Depositor Name. For Online Transfers
 use Your registered Name as Remark or Reference</b>
</p>

            <p>Thank you for allowing us serve you...</p><br />
            <a href='www.trendafrik.com'><b>TrendAfrik</b></a>

</body>
</html>"

               ;
               string mailBody = RazorEngine.Razor.Parse(Customertemplate, model);

               MailMessage mailMessage = new MailMessage(LogonEmailAdd, model.sellerEmail);//mailDefinition.CreateMailMessage(mailTo, ldReplacements, control); //(mailTo, ldReplacements,emailmessage,this );
               mailMessage.From = new MailAddress(LogonEmailAdd, "TrendAfrik");
               mailMessage.IsBodyHtml = true;
               mailMessage.Subject = "Your Subscription will soon Expire...";
               mailMessage.Body = mailBody;

               SmtpClient smtpClient = new SmtpClient("mail.trendafrik.com", 25);
               smtpClient.Credentials = new System.Net.NetworkCredential()
               {
                   UserName = LogonEmailAdd,
                   Password = LogonPwd
               };
               smtpClient.EnableSsl = false;
               smtpClient.Send(mailMessage);


               if (model.daysLeftToExpire.Trim() == "1" || model.daysLeftToExpire.Trim() == "0")
               {

               mailMessage = new MailMessage(LogonEmailAdd, AdminEmailAdd);//mailDefinition.CreateMailMessage(mailTo, ldReplacements, control); //(mailTo, ldReplacements,emailmessage,this );
               mailMessage.From = new MailAddress(LogonEmailAdd, "TrendAfrik");
               mailMessage.IsBodyHtml = true;
               mailMessage.Subject = "Client Subscription will soon Expire...";
               mailMessage.Body = mailBody;

               smtpClient = new SmtpClient("mail.trendafrik.com", 25);
               smtpClient.Credentials = new System.Net.NetworkCredential()
               {
                   UserName = LogonEmailAdd,
                   Password = LogonPwd
               };
               smtpClient.EnableSsl = false;
               smtpClient.Send(mailMessage);
               }

           }
           catch (Exception ex)
           {
               ex.StackTrace.ToString();
               throw;
           }

       }


       public void AdvertExpiryWarningsToClient(SellerDetails model)
       {
           try
           {

               string Customertemplate =
               @"<html>
<head>
<style>
table {
    width:100%;
}
table, th, td {
    border: 1px solid #e56e94;
    border-collapse: collapse;
}
th, td {
    padding: 5px;
    text-align: left;
}
table#t01 tr:nth-child(even) {
    background-color: #ffffff;
}
table#t01 tr:nth-child(odd) {
   background-color:#ffdfdd;
}
table#t01 th	{
    background-color: #810541;
    color: white;
}
</style>
</head>
<body>

<body >
<div style='position:absolute; height:50px; font-size:15px; font face = 'cursive';
width:600px; background-color:#FBBBB9; padding:30px;'> 

<font color='#F6358A'; face='fantasy'>TRENDAFRIK</font><font color='#F6358A'; face='ar hermann'> AFRICAN FASHION FABRIQS!</font>
        </div>
        <br />


<div style='background-color: #ece8d4;style='color:grey; font-size:15px;'
    font face='Helvetica, Arial, sans-serif'
width:600px; height:600px; padding:30px; margin-top:30px;'>

<p>Dear @Model.sellerName,<p>
<br />

            <p>Please Be informed that Your Advert Slot Subscribtion expires in @Model.daysLeftToExpire day(s)</p>
            
            <br />
            <p>Call our Contact Number on @Model.AdminPhone for Renewal Details OR 
 Pay  into the following Account
<br/>
        Account Name: @Model.AcctName <br/>
        Account Number: @Model.AcctNumber <br/>
        Bank : @Model.AcctBank <br/>
        Subscription Fee: @Model.SubscriptionFee</p>
    <p><b>Please Use Your Registerd Name with us as Depositor Name. For Online Transfers
 use Your registered Name as Remark or Reference</b>
</p>

            <p>Thank you for allowing us serve you...</p><br />
            <a href='www.trendafrik.com'><b>TrendAfrik</b></a>

</body>
</html>"

               ;
               string mailBody = RazorEngine.Razor.Parse(Customertemplate, model);
//send to client
               MailMessage mailMessage = new MailMessage(LogonEmailAdd, model.sellerEmail);//mailDefinition.CreateMailMessage(mailTo, ldReplacements, control); //(mailTo, ldReplacements,emailmessage,this );
               mailMessage.From = new MailAddress(LogonEmailAdd, "TrendAfrik");
               mailMessage.IsBodyHtml = true;
               mailMessage.Subject = "Your Advert Subscription will soon Expire...";
               mailMessage.Body = mailBody;

               SmtpClient smtpClient = new SmtpClient("mail.trendafrik.com", 25);
               smtpClient.Credentials = new System.Net.NetworkCredential()
               {
                   UserName = LogonEmailAdd,
                   Password = LogonPwd
               };
               smtpClient.EnableSsl = false;
               smtpClient.Send(mailMessage);

               //send to admin
              mailMessage = new MailMessage(LogonEmailAdd, AdminEmailAdd);//mailDefinition.CreateMailMessage(mailTo, ldReplacements, control); //(mailTo, ldReplacements,emailmessage,this );
               mailMessage.From = new MailAddress(LogonEmailAdd, "TrendAfrik");
               mailMessage.IsBodyHtml = true;
               mailMessage.Subject = "Customers Advert Subscription will soon Expire...";
               mailMessage.Body = mailBody;

               smtpClient = new SmtpClient("mail.trendafrik.com", 25);
               smtpClient.Credentials = new System.Net.NetworkCredential()
               {
                   UserName = LogonEmailAdd,
                   Password = LogonPwd
               };
               smtpClient.EnableSsl = false;
               smtpClient.Send(mailMessage);
           }
           catch (Exception ex)
           {
               ex.StackTrace.ToString();
               throw;
           }

       }
  
        public void SendEmail(string emailbody, string emailAdd, string subject)
        {
            try
            {
                MailMessage mailmessage = new MailMessage(AdminEmailAdd, emailAdd);
                mailmessage.Subject = subject;
                mailmessage.Body = emailbody;

                SmtpClient smptclient = new SmtpClient("mail.trendafrik.com", 25);//587   smtp.mail.yahoo.com mail.quickysale.com 25
                smptclient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = LogonEmailAdd,
                    Password = LogonPwd
                };
                smptclient.EnableSsl = false;
                smptclient.Send(mailmessage);
                //Console.WriteLine("message sent");
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                throw;
            }


        }

      
    }
}
