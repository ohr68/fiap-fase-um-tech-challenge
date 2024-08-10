using System.Text.RegularExpressions;

namespace FIAP.FaseUm.TechChallenge.Domain.ValueObjects
{
    public class Telefone
    {
        public string Numero { get; set; }
        public string Ddd { get; set; }

        public Telefone(string telefone)
        {
            Numero = telefone;
            Ddd = ExtrairDdd(telefone);
        }

        private string ExtrairDdd(string telefone)
        {
            // Remove todos os caracteres que não são dígitos
            string telefoneLimpo = Regex.Replace(telefone, @"\D", "");

            // Verifica se o número tem pelo menos 10 dígitos (para DDD + número local)
            if (telefoneLimpo.Length >= 10)
            {
                // O DDD está nos primeiros 2 dígitos
                return telefoneLimpo.Substring(0, 2);
            }

            return string.Empty; // Retorna uma string vazia se não houver DDD
        }
    }
}
