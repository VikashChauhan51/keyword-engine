using KeywordEngine.Abstraction;
using KeywordEngine.Exceptions;
using KeywordEngine.Models;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace KeywordEngine.Core;

internal static class ParameterMapper
{
    public static object[] Map(Type keyword, IEnumerable<Parameter> parameters, ITestContext testContext, IDependencyResolver? dependencyResolver)
    {
        var constructors = keyword.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic && c.IsPublic).ToList();

        if (constructors.Any())
        {
            var keywordParameters = constructors.First().GetParameters();

            if (keywordParameters.Any(parm =>
            parm.ParameterType != typeof(string) &&
            parm.ParameterType != typeof(object) &&
            parm.ParameterType != typeof(ITestContext) &&
            (parm.ParameterType.IsInterface ||
            parm.ParameterType.IsClass ||
            parm.ParameterType.IsAbstract) &&
            dependencyResolver is null))
            {
                throw new DependencyResolverNotFoundException(nameof(keyword));
            }

            var keywordParameterNames = keywordParameters.Select(p => p.Name!.ToLower()).ToList();
            var parameterValues = parameters.ToDictionary(item => item.Name.ToLower(), item => (object)item.Value);

            return keywordParameters.Select(parameter => ConvertParameterToArgument(parameter, parameterValues, testContext, nameof(keyword), dependencyResolver)).ToArray();

        }

        return new object[0];
    }


    private static object ConvertParameterToArgument(ParameterInfo parameter, IDictionary<string, object> parameterToMap, ITestContext testContext, string keyword, IDependencyResolver? dependencyResolver)
    {

        var name = parameter.Name!.ToLower();
        var type = parameter.ParameterType;

        if (type == typeof(ITestContext))
        {
            return testContext;
        }

        if (parameterToMap.TryGetValue(name, out var value))
        {
            if (parameter.ParameterType.IsEnum &&
            Enum.TryParse(parameter.ParameterType, value.ToString(), true, out var enumValue))
            {
                return enumValue!;
            }

            if (parameter.ParameterType == typeof(Guid) && Guid.TryParse(parameterToMap[name].ToString(), out var guidvalue))
            {
                return guidvalue;
            }

            return ConvertToType(value, type);
        }

        var obj = dependencyResolver?.GetService(type);

        if (!IsNullable(type) && obj is null)
        {
            throw new DependencyNotFoundException(name, nameof(type), keyword);
        }

        return obj!;
    }

    private static bool IsNullable(Type type) => !type.IsValueType || Nullable.GetUnderlyingType(type) != null;

    private static object ConvertToType(object objectToCovert, Type type)
    {
        var typeConverter = TypeDescriptor.GetConverter(type);
        try
        {

            return typeConverter.ConvertFrom(objectToCovert)!;
        }
        catch
        {
            return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, objectToCovert)!;
        }

    }
}
