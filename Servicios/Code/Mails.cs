namespace Servicios.Code
{
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Servicios.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;

    public class Mails
    {
        public async Task ExecuteMail(Correo informacion, String apiKey = "")
        {
            try
            {
                if (string.IsNullOrEmpty(apiKey))
                    apiKey = ConfigurationManager.AppSettings["SendGridKey"].ToString();

                var listaDestinos = new List<EmailAddress>();
                var cliente = new SendGridClient(apiKey);

                var emisor = new EmailAddress(informacion.CorreoEmisor, informacion.NombreEmisor);
                var asunto = informacion.Asunto;
                var contenidoTextoPlano = informacion.CuerpoTexto ?? "";
                var contenidoHtml = informacion.CuerpoHtml;

                if (informacion.Destinatarios.Contains(";"))
                {
                    foreach (var destino in informacion.Destinatarios.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(destino))
                            listaDestinos.Add(new EmailAddress(destino));
                    }
                }
                else
                    listaDestinos.Add(new EmailAddress(informacion.Destinatarios));

                if (listaDestinos.Count.Equals(0))
                    throw new Exception("No se ha registrado ningún correo de destino.");

                var mensaje = MailHelper.CreateSingleEmailToMultipleRecipients(emisor,
                                                                                listaDestinos,
                                                                                asunto,
                                                                                contenidoTextoPlano,
                                                                                contenidoHtml);

                if (informacion.Adjuntos != null && informacion.Adjuntos.Any())
                {
                    foreach (var adjunto in informacion.Adjuntos)
                    {
                        mensaje.AddAttachment(adjunto.NombreArchivo, adjunto.ArchivoBytes);
                    }
                }

                var response = await cliente.SendEmailAsync(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}