using FluentAssertions;
using System.Reflection;
using Domain.Result;

namespace Tests.Result;
public sealed class ErrorTests
{
    [Fact]
    public void Error_codes_must_be_unique()
    {
        List<MethodInfo> methods = typeof(Error)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x.ReturnType == typeof(Error))
            .ToList();

        int numberOfUniqueCodes = methods.Select(GetErrorCode)
            .Distinct()
            .Count();

        numberOfUniqueCodes.Should().Be(methods.Count);
    }

    private string GetErrorCode(MethodInfo method)
    {
        object[] parameters = method.GetParameters()
            .Select<ParameterInfo, object>(x =>
            {
                if (x.ParameterType == typeof(string))
                    return string.Empty;

                if (x.ParameterType == typeof(long))
                    return 0;

                throw new Exception();
            })
            .ToArray();

        var error = (Error)method.Invoke(null, parameters)!;
        return error.Code;
    }
}