namespace Servicios.Models
{
    using System.Collections.Generic;

    public class Correo
    {
        public string NombreEmisor { get; set; }
        public string CorreoEmisor { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string CuerpoTexto { get; set; }
        public string CuerpoHtml { get; set; }
        public List<Adjunto> Adjuntos { get; set; }
    }
}