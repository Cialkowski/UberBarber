using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UberBarber.User
{
    public class MailSender
    {
        public NetworkCredential login = new NetworkCredential("miecho.pierwszy966@gmail.com", "luomhgaxzwlsxjah");
        public SmtpClient client;
        public MailMessage msg_email = new MailMessage();

        public MailSender()
        {
            // set super email data
            this.msg_email = new MailMessage();
            this.login = new NetworkCredential("miecho.pierwszy966@gmail.com", "luomhgaxzwlsxjah");
            this.client = new SmtpClient("smtp.gmail.com");
            this.client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg_email = new MailMessage { From = new MailAddress("miecho.pierwszy966@gmail.com", "UberBarber 2023", Encoding.UTF8) };
        }
        public enum Action
        {
            // enum for email actions
            Edit,
            Add
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // This method shows feedback for every scenario.
            if (e.Cancelled)
                MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            if (e.Error != null)
                MessageBox.Show(string.Format("{0} {1}", e.Error), "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("E-mail sended", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void Send(Action action, string email, string first_name, string password, string role)
        {
            // This method sends an email to user.

            msg_email.Subject = "Your UberBarber data";
            try
            {
                if (!(email.Equals("")))
                {
                    msg_email.To.Add(new MailAddress(email));
                }
                if (action == Action.Add)
                // email content for creating new user
                {
                    msg_email.Body = "<h2>Hello dear " + first_name + " - our new " + role + "<h2/><p> You are receiving this message because Your data was added to our UserBarber database.<br />Do not show this message to anyone.<p/>" +
                     "<p><b>Username: " + first_name + "<br />Password: " + password + "</b><p/> <p>Yours sincerely, <br /> Admin of UberBarber 2023<p/>";
                }
                else if (action == Action.Edit)
                // email content for edited user
                {
                    msg_email.Body = "<h2>Hello dear " + first_name + "<h2/><p> You are receiving this message because Your data was edited in our UberBarber database.<br />Do not show this message to anyone.<p/>" +
                  "<p><b>Username: " + first_name + "<br />Password: " + password + "</b><p/> <p>Yours sincerely, <br /> Admin of UberBarber 2023<p/>";
                }

                msg_email.BodyEncoding = Encoding.UTF8;
                msg_email.IsBodyHtml = true;
                msg_email.Priority = MailPriority.Normal;
                msg_email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                string userstate = "Sending...";
                client.SendAsync(msg_email, userstate);
            }
            catch
            {
                MessageBox.Show("E-mail can not be send - no e-mail address has been entered", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
