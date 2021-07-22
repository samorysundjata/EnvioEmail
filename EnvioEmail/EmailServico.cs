using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EnvioEmail
{
    public class EmailServico : MailMessage
    {
        #region Private Fields

        private readonly string EMAIL_HOST = ConfigurationManager.AppSettings["emailServico:email_host"];
        private readonly string EMAIL_ORIGEM = ConfigurationManager.AppSettings["emailServico:email_remetente"];
        private readonly string EMAIL_SENHA = ConfigurationManager.AppSettings["emailServico:email_senha"];

        #endregion Private Fields

        #region Public Methods

        public void MandarEmail(MailMessage mensagem)
        {
            try
            {
                using (var mensagemEmail = new MailMessage())
                {
                    mensagemEmail.From = new MailAddress(EMAIL_ORIGEM);
                    mensagemEmail.To.Add(new MailAddress(mensagem.To.ToString()));

                    mensagemEmail.Subject = mensagem.Subject;
                    mensagemEmail.Body = mensagem.Body;
                    mensagemEmail.BodyEncoding = Encoding.UTF8;
                    mensagemEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                    mensagemEmail.Priority = MailPriority.Normal;

                    using (var smtpCliente = new SmtpClient())
                    {
                        smtpCliente.Host = EMAIL_HOST;
                        smtpCliente.Port = 587;
                        smtpCliente.EnableSsl = true;
                        smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpCliente.Credentials = new NetworkCredential(EMAIL_ORIGEM, EMAIL_SENHA);
                        smtpCliente.UseDefaultCredentials = false;

                        smtpCliente.Send(mensagemEmail);
                    }
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Mensagem : {0} " + ex.Message);
                return;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Mensagem SMPT Fail : {0} " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensagem Exception : {0} " + ex.Message);
                return;
            }

            Console.WriteLine("Email enviado com sucesso para " + mensagem.To.ToString() + "!");
            Console.ReadKey();
        }

        #endregion Public Methods
    }
}