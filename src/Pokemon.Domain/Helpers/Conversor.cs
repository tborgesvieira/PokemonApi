using System;
using System.Text;

namespace Pokemon.Domain.Helpers
{
    public static class Conversor
    {
        //convert string para base64
        public static string EncodeToBase64(this string texto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(texto)) return texto;
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
