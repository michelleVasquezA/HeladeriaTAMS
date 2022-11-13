using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace HeladeriaTAMS.Integration.Sendgrid
{
    public class SendMailIntegration
    {
        public const string SEND_SENDGRID="SENDGRID";
        public const string SEND_REST="REST";

        private string ACCESS_TOKEN ="";
        private string From = "tatiana_florez@usmp.pe"; // settings\sender authentification
        private string FromLabel = "Mail Service";

        private const string URL_API_SENDGRID = "https://api.sendgrid.com/v3/mail/send";


        public async Task SendMail(string correoDestino,string userDestino,string titulo, string contenido,string method){
            await SendMailSengrid(correoDestino,userDestino, titulo, contenido);
        }
 
        private async Task SendMailSengrid(string correoDestino,string userDestino ,string titulo, string contenido){
            ACCESS_TOKEN = System.Environment.GetEnvironmentVariables()["SENDGRID_KEY"].ToString();
            var client = new SendGridClient(ACCESS_TOKEN);
            var from = new EmailAddress(From, FromLabel);
            var subject = titulo;
            //var to = new EmailAddress("correoDestino", "userDestino");
            var to = new EmailAddress("diego.alejandro.13@hotmail.com", "diego");
            var plainTextContent = contenido;
            var htmlContent = "contenido";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

   
    }
}