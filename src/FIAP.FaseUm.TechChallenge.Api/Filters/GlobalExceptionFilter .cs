using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FIAP.FaseUm.TechChallenge.Custom.Exceptions.Config;

namespace FIAP.FaseUm.TechChallenge.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,

                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                NotFoundException => StatusCodes.Status404NotFound,

                ValidationException => StatusCodes.Status422UnprocessableEntity,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(new ProblemDetails
            {
                Title = "Ocorreu um erro",
                Detail = context.Exception.Message,
                Type = context.Exception.GetType().Name,
                Status = statusCode                
            });
        }
    }
}
