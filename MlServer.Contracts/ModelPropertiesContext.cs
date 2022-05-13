namespace MlServer.Contracts;

public static class ModelPropertiesContext
{
    public static List<string> GetProperties(object model)
    {
        var result = new List<string>();

        foreach (var prop in model.GetType().GetProperties())
        {
            result.Add((prop.GetValue(model) != null ?
                prop.GetValue(model)?.ToString() : "")!);
        }

        return result;
    }

    public static void SetProperties(object model, List<string> properties)
    {
        int index = 0;
        foreach (var prop in model.GetType().GetProperties())
        {
            if (index >= properties.Count())
            {
                break;
            }
            
            prop.SetValue(model, properties[index]);
            index++;
        }
    }
}