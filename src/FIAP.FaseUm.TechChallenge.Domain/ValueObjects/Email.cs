using System.Text.RegularExpressions;

namespace FIAP.FaseUm.TechChallenge.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; set; }

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length < 5)
                throw new InvalidDataException("O e-mail informado não é válido.");

            Endereco = address.ToLower().Trim();
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (!Regex.IsMatch(address, pattern))
                throw new InvalidDataException("O e-mail informado não é válido.");
        }
    }
}
