namespace Servicios
{
    using System.Web.Services;
    using Servicios.Models;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Servicio para envio de correos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class wsMails : WebService
    {
        [WebMethod]
        public Respuesta EnviarCorreo(Autenticacion credenciales, Correo informacion)
        {
            var resultado = new Respuesta();

            try
            {
                var validar = new Autenticacion().ValidarCredenciales(credenciales);

                if (!validar)
                    throw new Exception("Las credenciales de acceso no son correctas.");

                if (string.IsNullOrEmpty(informacion.CorreoEmisor))
                    throw new Exception("No ha indicado el correo emisor.");

                if (string.IsNullOrEmpty(informacion.NombreEmisor))
                    throw new Exception("No ha indicado el nombre del emisor.");

                if (string.IsNullOrEmpty(informacion.Destinatarios))
                    throw new Exception("No ha indicado el o los correos de destino.");

                if (string.IsNullOrEmpty(informacion.Asunto))
                    throw new Exception("No ha indicado el asunto del correo.");

                if (string.IsNullOrEmpty(informacion.CuerpoHtml))
                    throw new Exception("No ha indicado el cuerpo del correo en HTML.");

                Task.Run(() => new Code.Mails().ExecuteMail(informacion)).GetAwaiter().GetResult();

                resultado.Tipo = TipoRespuesta.Exito;
                resultado.Valor = "Ok";
            }
            catch (Exception ex)
            {
                resultado.Tipo = TipoRespuesta.Error;
                resultado.Valor = ex.Message;
            }

            return resultado;
        }
    }
}
