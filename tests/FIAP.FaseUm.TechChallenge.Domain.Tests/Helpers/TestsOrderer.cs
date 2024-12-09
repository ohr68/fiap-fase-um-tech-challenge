using Xunit.Abstractions;
using Xunit.Sdk;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Helpers
{
    public class TestsOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase => ObterOrdem(testCase));
        }

        private int ObterOrdem(ITestCase testCase)
        {
            var testOrderAttribute = testCase.TestMethod.Method
                .GetCustomAttributes(typeof(TestOrderAttribute))
                .FirstOrDefault();

            return testOrderAttribute == null
                ? int.MaxValue
                : testOrderAttribute.GetNamedArgument<int>("Order");
        }
    }
}
