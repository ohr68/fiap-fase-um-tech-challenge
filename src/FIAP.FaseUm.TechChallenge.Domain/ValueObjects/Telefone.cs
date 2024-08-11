using System.Text.RegularExpressions;

namespace FIAP.FaseUm.TechChallenge.Domain.ValueObjects
{
    public class Telefone
    {
        public const int LENGTH = 11;
        public const string PATTERN = @"^\(?\d{2}\)?\s?\d{5}-?\d{4}$";

        public string? Numero { get; private set; }
        public string? Ddd { get; private set; }

        protected Telefone() { }

        public Telefone(string telefone)
        {
            if(telefone is null)
                throw new InvalidDataException("O telefone informado não é válido.");

            string telefoneLimpo = ObterTelefoneLimpo(telefone);

            if (telefoneLimpo.Length != LENGTH && !Regex.IsMatch(telefone, PATTERN))
                throw new InvalidDataException("O telefone informado não é válido.");

            Numero = telefone;
            Ddd = ExtrairDdd(telefone);
        }

        private string ExtrairDdd(string telefone)
        {
            string telefoneLimpo = ObterTelefoneLimpo(telefone);

            // Verifica se o número tem pelo menos 10 dígitos (para DDD + número local)
            if (telefoneLimpo.Length >= 10)
            {
                // O DDD está nos primeiros 2 dígitos
                return telefoneLimpo.Substring(0, 2);
            }

            return string.Empty; // Retorna uma string vazia se não houver DDD
        }

        private string ObterTelefoneLimpo(string telefone)
            => Regex.Replace(telefone, @"\D", ""); // Remove todos os caracteres que não são dígitos
    }
}
