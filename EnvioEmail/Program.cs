using System;
using System.Net.Mail;

namespace EnvioEmail
{
    internal class Program
    {
        #region Private Methods

        private static void EscreverMensagem()
        {
            using (var mensagem = new MailMessage())
            {
                Console.WriteLine("Escreva o seu e-mail");
                string remetente = Console.ReadLine();
                mensagem.To.Add(remetente);
                Console.WriteLine("Escreva o assunto do email");
                mensagem.Subject = Console.ReadLine();
                Console.WriteLine("Escreva a sua mensagem");
                mensagem.Body = Console.ReadLine();

                using (var emailManda = new EmailServico())
                {
                    emailManda.MandarEmail(mensagem);
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Este é o envio de mail, se quiser mandar uma mensagem digite 'Mandar'");

            var chave = Console.ReadLine();

            if (chave == "Mandar")
                EscreverMensagem();
            else
                Console.WriteLine("Seu comando foi incorreto, reinicie a aplicação."); 
        }

        #endregion Private Methods
    }
}