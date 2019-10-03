using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace ECommerce.Sample.Models.Mail
{
    public class SendingMail
    {
        OrderDetailRepository ordrepo = new OrderDetailRepository();
        public bool sendMail(string firstName, string lastName, string Customer, string body, int id)
        {
            try
            {
                MailMessage email = new MailMessage();
                string Host = "smtp.live.com";
                string smtpUserName = "gurhangedik@hotmail.com";
                string smtpPassword = "Parola";
                email.From = new MailAddress("gurhangedik@hotmail.com");
                int smtpPort = 587;
                email.IsBodyHtml = true;
                email.Subject = firstName + " " + lastName + ", See you next shopping. ";
                email.Body = body;
                email.To.Add(new MailAddress(Customer));
                email.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient smtp = new SmtpClient(Host, smtpPort);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                AlternateView AlternateView_Html = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                foreach (OrderDetail item in ordrepo.List().ProcessResult.Where(t => t.OrderId == id))
                {
                
                 LinkedResource Picture = new LinkedResource("C:\\Users\\gürhan\\Documents\\GitHub\\ECommerceSample\\ECommerce.Sample\\ECommerce.Sample\\Upload\\" + item.Product.Photo, MediaTypeNames.Image.Jpeg);
                    Picture.ContentId = item.Product.Photo;
                    AlternateView_Html.LinkedResources.Add(Picture);
                    email.AlternateViews.Add(AlternateView_Html);
                }
                smtp.Send(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}