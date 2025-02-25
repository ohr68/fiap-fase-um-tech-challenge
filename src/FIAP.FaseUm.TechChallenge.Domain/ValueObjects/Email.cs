﻿using System.Text.RegularExpressions;

namespace FIAP.FaseUm.TechChallenge.Domain.ValueObjects
{
    public class Email
    {
        public const int MIN_LENGTH = 5;

        public string? Endereco { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length < MIN_LENGTH)
                throw new InvalidDataException("O e-mail informado não é válido.");
            
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (!Regex.IsMatch(address, pattern))
                throw new InvalidDataException("O e-mail informado não é válido.");

            Endereco = address.ToLower().Trim();
        }

        public override string ToString() 
            => Endereco;
    }
}
