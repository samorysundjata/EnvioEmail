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
                Console.WriteLine("Digite aqui o seu e-mail 'seuemail@provedor.com' ");
                string remetente = Console.ReadLine();
                mensagem.To.Add(remetente);
                Console.WriteLine("Escreva o assunto do email");
                mensagem.Subject = Console.ReadLine();
                Console.WriteLine("Escreva a sua mensagem");
                mensagem.Body = Console.ReadLine();

                using var emailManda = new EmailServico();
                emailManda.MandarEmail(mensagem);
            }
        }

        private static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MenuPrincipal();
            }
        }

        private static bool MenuPrincipal()
        {
            Console.Clear();            
            Console.WriteLine("Este é o Envio de Email, escolha uma opção:");
            Console.WriteLine("1) Mandar email");
            Console.WriteLine("2) Sair");
            Console.Write("\r\nEscolha uma opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    EscreverMensagem();
                    return true;
                case "2":                    
                    return false;
                default:
                    return true;
            }
        }

        #endregion Private Methods
    }
}