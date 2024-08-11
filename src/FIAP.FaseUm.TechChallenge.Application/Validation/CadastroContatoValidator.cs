using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FIAP.FaseUm.TechChallenge.Application.Validation
{
    public class CadastroContatoValidator : AbstractValidator<CadastroContatoDto>
    {
        public CadastroContatoValidator()
        {
            RuleFor(c => c.nome)
                .NotEmpty()
                .WithMessage("O campo nome é obrigatório.")
                .MaximumLength(250)
                .WithMessage("O campo nome deve conter no máximo 250 caracteres.");


            RuleFor(c => c.telefone)
                .NotEmpty()
                .WithMessage("O campo telefone é obrigatório.")
                .MaximumLength(55)
                .WithMessage("O campo telefone deve conter no máximo 55 caracteres.")
                .Custom((telefone, context) =>
                {
                    string telefoneLimpo = Regex.Replace(telefone, @"\D", "");
                    if (telefoneLimpo.Length != Telefone.LENGTH && !Regex.IsMatch(telefone, Telefone.PATTERN))
                        context.AddFailure("O telefone informado não é válido.");
                });

            RuleFor(c => c.email)
                .NotEmpty()
                .WithMessage("O campo e-mail é obrigatório.")
                .MaximumLength(150)
                .WithMessage("O campo e-mail deve conter no máximo 150 caracteres.")
                .EmailAddress()
                .WithMessage("Informe um e-mail válido");
        }
    }
}
