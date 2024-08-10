using FluentValidation.Results;
using System.Text;

namespace FIAP.FaseUm.TechChallenge.Extensions.FluentValidation
{
    public static class ValidationErrorsExtensions
    {
        public static string ObterMensagensComoStringUnica(this IEnumerable<ValidationFailure> errosValidacao)
        {
            var erros = new StringBuilder();

            foreach (var erro in errosValidacao)
                erros.AppendLine($"{erro.PropertyName} - {erro.ErrorMessage}");

            return erros.ToString();
        }
    }
}
