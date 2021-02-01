using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
   public class EmailService
    {
        public static void SendMail(string subject,string message,string to)
        {
            string email = "charity.gmh@gmail.com";
            string password = "charity2020";
            //string email = ConfigurationManager.AppSettings["emailAddress"];
           // string password = ConfigurationManager.AppSettings["emailPassword"];

           //string email = ConfigurationManager.AppSettings["emailAddress"];
           //string password = ConfigurationManager.AppSettings["emailPassword"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
        public static void offerDonationMail(string subject, int donationCode, string to)
        {
            string email = "charity.gmh@gmail.com";
            string password = "charity2020";
            //string email = ConfigurationManager.AppSettings["emailAddress"];
            // string password = ConfigurationManager.AppSettings["emailPassword"];
           // string Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("http://localhost:4200/searchGMH"));

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body =
                string.Format(
                                @"
                    <div dir=' ltr'>
                    <h1>Hello!</h1>
                    <h3>{{subject}}</h3>
                    <div style='
                      position: relative;
                      padding: 0.75rem 1.25rem;
                      margin-bottom: 1rem;
                      border: 1px solid transparent;
                      border-radius: 0.25rem;
                      color: #0c5460;
                      width: 50%;
                      background-color: #d1ecf1;
                      border-color: #bee5eb;'>

                    < label > Id: { 0}</ label >
   
                       < br />
   
                       < label > First Name: { 1}</ label >
      
                          < br />
      
                          < label > Last Name: { 2}</ label >
         
                             < br />
         
                             < label > Phone Number: { 3}</ label >
            
                                < br />
            
                                < label > Address: { 4}</ label >
               
                                   < br />
               
                                   < br />
               
                                   </ div >
               
                                   < div >
               
                                   < div style = 'display: inline-block;' >
                
                                     < form action = 'https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true' method = 'post' >
                   
                                           < button type = 'submit'
                           style = 'display: inline-block;
                           font - weight: 400;
            text - align: center;
            vertical - align: middle;
            -webkit - user - select: none;
            -moz - user - select: none;
            -ms - user - select: none;
            user - select: none;
        border: 1px solid transparent;
        padding: 0.375rem 0.75rem;
            font - size: 1rem;
            line - height: 1.5;
            border - radius: 0.25rem;
        transition: color 0.15s ease-in-out, background - color 0.15s ease-in-out, border - color 0.15s ease-in-out, box - shadow 0.15s ease-in-out;
        color: #fff;
                           background - color: #28a745;
                           border - color: #28a745;'>
                        Confirm
                          </ button >
                        </ form >
                    </ div >
                     < form action = 'https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/false' method = 'post' >
   
                           < button type = 'submit'
                           style = 'display: inline-block;
                           font - weight: 400;
            text - align: center;
            vertical - align: middle;
            -webkit - user - select: none;
            -moz - user - select: none;
            -ms - user - select: none;
            user - select: none;
        border: 1px solid transparent;
        padding: 0.375rem 0.75rem;
            font - size: 1rem;
            line - height: 1.5;
            border - radius: 0.25rem;
        transition: color 0.15s ease-in-out, background - color 0.15s ease-in-out, border - color 0.15s ease-in-out, box - shadow 0.15s ease-in-out;
        color: #fff;
                           background - color: #dc3545;
                           border - color: #dc3545;'>
                        UnConfirm
                    </ button >
                 </ form >
                 </ div >
                   </ div >
                "
                , 1,
                      "5",
                      "6",
                      0,
                      "7"
                       ); 
            msg.IsBodyHtml = true;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
    }

    public class CopyOfEmailService
    {
        public static void SendMail(string subject, string message, string to)
        {
            string email = "charity.gmh@gmail.com";
            string password = "charity2020";
            //string email = ConfigurationManager.AppSettings["emailAddress"];
            // string password = ConfigurationManager.AppSettings["emailPassword"];

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
        public static void offerDonationMail(string subject, int donationCode, string to)
        {
            string email = "charity.gmh@gmail.com";
            string password = "charity2020";
            //string email = ConfigurationManager.AppSettings["emailAddress"];
            // string password = ConfigurationManager.AppSettings["emailPassword"];
            // string Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("http://localhost:4200/searchGMH"));

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body =
                string.Format(
                                @"
                    <div dir=' ltr'>
                    <h1>Hello!</h1>
                    <h3>{{subject}}</h3>
                    <div style='
                      position: relative;
                      padding: 0.75rem 1.25rem;
                      margin-bottom: 1rem;
                      border: 1px solid transparent;
                      border-radius: 0.25rem;
                      color: #0c5460;
                      width: 50%;
                      background-color: #d1ecf1;
                      border-color: #bee5eb;'>
");
            //    <label> Id: {0}</label>
            //    <br />
            //    <label> First Name: {1}</ label>
            //    <br />
            //    <label> Last Name: {2}</label>
            //    <br />
            //    <label> Phone Number: {3}</label>
            //    <br />
            //    <label> Address: {4}</label>
            //    <br />
            //    <br />
            //    </div>
            //    <div>
            //    <div style='display: inline-block;'>
            //     <form action='https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true' method='post' >
            //        <button type='submit'
            //           style='display: inline-block;
            //           font-weight: 400;
            //           text-align: center;
            //           vertical-align: middle;
            //           -webkit-user-select: none;
            //           -moz-user-select: none;
            //           -ms-user-select: none;
            //           user-select: none;
            //           border: 1px solid transparent;
            //           padding: 0.375rem 0.75rem;
            //           font-size: 1rem;
            //           line-height: 1.5;
            //           border-radius: 0.25rem;
            //           transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            //           color: #fff;
            //           background-color: #28a745;
            //           border-color: #28a745;'>
            //        Confirm
            //          </button>
            //        </form>
            //    </div>
            //     <form action='https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/false' method='post'>
            //        <button type='submit'
            //           style='display: inline-block;
            //           font-weight: 400;
            //           text-align: center;
            //           vertical-align: middle;
            //           -webkit-user-select: none;
            //           -moz-user-select: none;
            //           -ms-user-select: none;
            //           user-select: none;
            //           border: 1px solid transparent;
            //           padding: 0.375rem 0.75rem;
            //           font-size: 1rem;
            //           line-height: 1.5;
            //           border-radius: 0.25rem;
            //           transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            //           color: #fff;
            //           background-color: #dc3545;
            //           border-color: #dc3545;'>
            //        UnConfirm
            //    </button>
            // </form>
            // </div>
            //   </div>
            //"
            //, doctor.doctorId,
            //       doctor.firstName,
            //       doctor.lastName,
            //       doctor.doctorPhone,
            //       doctor.address
            //       );
            msg.IsBodyHtml = true;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
    }
}
