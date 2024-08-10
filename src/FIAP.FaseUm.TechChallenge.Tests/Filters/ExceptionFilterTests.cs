using FIAP.FaseUm.TechChallenge.Api.Filters;
using FIAP.FaseUm.TechChallenge.Custom.Exceptions.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FIAP.FaseUm.TechChallenge.Tests.Filters
{
    [TestFixture]
    public class ExceptionFilterTests
    {
        private ActionContext _actionContext;

        [SetUp]
        public void Setup()
        {
            this._actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
        }

        [TestCase(TestName = "Bad Request Exception")]
        public void OnException_ShouldSetCorrectStatusCodeForBadRequestException()
        {
            // Arrange
            var ex = new BadRequestException("Bad request");
            var exceptionContext = new ExceptionContext(this._actionContext, new List<IFilterMetadata>())
            {
                Exception = ex
            };
            var filter = new GlobalExceptionFilter();

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var objectResult = exceptionContext.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.That(StatusCodes.Status400BadRequest == (objectResult.Value as ProblemDetails)!.Status, "O status code resultante é diferente do esperado");
        }

        [TestCase(TestName = "Not Found Exception")]
        public void OnException_ShouldSetCorrectStatusCodeForNotFoundException()
        {
            // Arrange
            var ex = new NotFoundException("Not found");
            var exceptionContext = new ExceptionContext(this._actionContext, new List<IFilterMetadata>())
            {
                Exception = ex
            };
            var filter = new GlobalExceptionFilter();

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var objectResult = exceptionContext.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.That(StatusCodes.Status404NotFound == (objectResult.Value as ProblemDetails)!.Status, "O status code resultante é diferente do esperado");
        }


        [TestCase(TestName = "Validation Exception")]
        public void OnException_ShouldSetCorrectStatusCodeForValidationException()
        {
            // Arrange
            var ex = new ValidationException("Validation error");
            var exceptionContext = new ExceptionContext(this._actionContext, new List<IFilterMetadata>())
            {
                Exception = ex
            };
            var filter = new GlobalExceptionFilter();

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var objectResult = exceptionContext.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.That(StatusCodes.Status422UnprocessableEntity == (objectResult.Value as ProblemDetails)!.Status, "O status code resultante é diferente do esperado");
        }

        [TestCase(TestName = "Internal Exception")]
        public void OnException_ShouldSetCorrectStatusCodeForInternalException()
        {
            // Arrange
            var ex = new Exception("Internal error");
            var exceptionContext = new ExceptionContext(this._actionContext, new List<IFilterMetadata>())
            {
                Exception = ex
            };
            var filter = new GlobalExceptionFilter();

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var objectResult = exceptionContext.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.That(StatusCodes.Status500InternalServerError == (objectResult.Value as ProblemDetails)!.Status, "O status code resultante é diferente do esperado");
        }
    }
}
