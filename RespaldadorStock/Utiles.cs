using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace RespaldadorStock
{
    public class Utiles
    {
        public static int FechaToInteger(DateTime date)
        {
            string a = date.Year.ToString();
            string b = date.Month.ToString("00");
            string c = date.Day.ToString("00");
            string d = a + b + c;

            if (d == "10101")
                d = "0";

            return int.Parse(d);
        }

        public static void EnviaMensajeria(string correo, string titulo, string mensaje)
        {
            try
            {
                MailAddress from = new MailAddress("info@deliverypark.cl", "Backline");
                MailAddress to = new MailAddress(correo);
                MailMessage mail = new MailMessage(from, to);
               

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;  //pueden probar con los puertos arriba disponibles              
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.ServicePoint.MaxIdleTime = 1;
                client.Timeout = 12000;
                client.Credentials = new NetworkCredential("info@deliverypark.cl", "##2021DeliveryPark##");
                mail.IsBodyHtml = true;
                mail.Subject = titulo;
                mail.Body = mensaje;

                client.Send(mail);
            }
            catch (Exception ex)
            {
                var o = ex.InnerException;
            }
        }
    }
}
