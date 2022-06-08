

using KeywordEngine.Models;
using System.ComponentModel;
using System.Reflection;

namespace KeywordEngine.Core;

internal static class ParameterMapper
{
    public static object[] Map(Type keyword,IEnumerable<Parameter> parameters)
    {
        var keywordParameters = keyword.GetConstructors().First().GetParameters();
        var keywordParameterNames = keywordParameters.Select(p => p.Name).ToList();

        var parameterValues = parameters.ToDictionary(item => item.Name.ToLower(), item => (object)item.Value);

        return keywordParameters.Select(parameter => ConvertParameterToArgument(parameter, parameterValues)).ToArray();
    }
    
    
    private static object ConvertParameterToArgument(ParameterInfo parameter, IDictionary<string,object> parameterToMap)
    {

        var name = parameter.Name!.ToLower();
        var type= parameter.ParameterType;
        if (parameterToMap.ContainsKey(name))
        {
            return ConvertToType(parameterToMap[name], type);
        }
        if (!IsNullable(type))
        {
            throw new Exception("");
        }

        return null;
    }

    private static bool IsNullable(Type type) => !type.IsValueType || Nullable.GetUnderlyingType(type) != null;

    private static object ConvertToType(object objectToCovert,Type type)
    {
        var typeConverter= TypeDescriptor.GetConverter(type);
        return typeConverter.ConvertFrom(objectToCovert)!;
    }
}
