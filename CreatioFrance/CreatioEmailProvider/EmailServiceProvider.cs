using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CreatioEmailProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="CreatioEmailProvider.IEmailServiceProvider" />
    public class EmailServiceProvider : IEmailServiceProvider
    {
        #region Members
        /// <summary>
        /// The _instances
        /// </summary>
        private static IEmailServiceProvider _instance = null;

        /// <summary>
        /// The _locker
        /// </summary>
        private static object _locker = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the get instance.
        /// </summary>
        /// <value>
        /// The get instance.
        /// </value>
        public static IEmailServiceProvider GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new EmailServiceProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the credential.
        /// </summary>
        /// <value>
        /// The credential.
        /// </value>
        public NetworkCredential Credential { get; set; }

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServiceProvider"/> class.
        /// </summary>
        public EmailServiceProvider()
        {
            try
            {
                string userName = ConfigurationManager.AppSettings["email-smtp-user-name"].ToString();

                string password = ConfigurationManager.AppSettings["email-smtp-user-password"].ToString();

                Credential = new NetworkCredential(userName, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Sends the subscription confirmation email asynchronous.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="token">The token.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public async Task SendSubscriptionConfirmationEmailAsync(string subject, string token, string destination)
        {
            try
            {
                string BodyMessage = Properties.Resources.EmailConfirmation;



                string name = destination;

                int index = name.LastIndexOf("@");

                if (index > 0)
                    name = name.Remove(index, name.Length - index);

                BodyMessage = BodyMessage.Replace("{verify-token-email}", token);
                BodyMessage = BodyMessage.Replace("{name}", name);

                await SendEmailAsync(subject,
                                    BodyMessage,
                                    new MailAddress("fennechaim@gmail.com", "Creatio-Entreprise <Email de Confirmation>"),
                                     new MailAddress(destination, destination)
                                     );
            }
            catch (Exception ex)
            {


            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        private async Task SendEmailAsync(string subject, string body, MailAddress source, MailAddress destination)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp-relay.sendinblue.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = Credential
                };

                using (var message = new MailMessage(source, destination)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                    //smtp.Send(message);
                    //Task.Run(() => smtp.SendMailAsync(message));
                    await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}