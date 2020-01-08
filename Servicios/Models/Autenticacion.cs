namespace Servicios.Models
{
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;

    public enum TipoWeb
    {
        Mozart = 1
    }

    public class Autenticacion
    {
        public TipoWeb InWebId { get; set; }
        public string StUsuario { get; set; }
        public string StClave { get; set; }

        public bool ValidarCredenciales(Autenticacion datos)
        {
            var respuesta = false;

            try
            {
                var oUser = datos.StUsuario ?? "";
                var oPass = datos.StClave ?? "";
                var _user = Encriptar(ConfigurationManager.AppSettings["key_usuarioTk"].ToString());
                var _pass = Encriptar(ConfigurationManager.AppSettings["key_claveTk"].ToString());

                if (datos.InWebId.Equals(TipoWeb.Mozart) && _user.Equals(oUser) && _pass.Equals(oPass))
                    respuesta = true;
            }
            catch { }

            return respuesta;
        }

        private static string Encriptar(string cadena)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}