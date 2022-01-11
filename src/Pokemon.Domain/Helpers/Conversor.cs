using System;
using System.Text;

namespace Pokemon.Domain.Helpers
{
    public static class Conversor
    {
        //convert string para base64
        public static string EncodeToBase64(string texto)
        {
            try
            {
                if (texto == null) return null;
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //converte de base64 para texto
        public static string DecodeFrom64(string dados)
        {
            try
            {
                if (dados == null) return null;
                byte[] dadosAsBytes = Convert.FromBase64String(dados);
                string resultado = ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
