namespace Servicios.Models
{
    public enum TipoRespuesta
    {
        Exito = 0,
        Alerta = 1,
        Error = 2
    }

    public class Respuesta
    {
        public TipoRespuesta Tipo { set; get; }
        public string Valor { set; get; }
        public string OtroValor { set; get; }
    }
}