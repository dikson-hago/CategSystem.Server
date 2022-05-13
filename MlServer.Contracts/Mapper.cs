namespace MlServer.Contracts;

public static class Mapper
{
    public static TTo Map<TFrom, TTo>(TFrom objectFrom)
    {
        var result = Activator.CreateInstance<TTo>();
        
        var props= ModelPropertiesContext.GetProperties(objectFrom);
        
        ModelPropertiesContext.SetProperties(result, props);

        return result;
    }
}