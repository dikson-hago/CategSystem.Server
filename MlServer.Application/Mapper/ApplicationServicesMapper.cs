using ContractML = MlServer.Contracts.Models.ML;

namespace MlServer.Application.Handlers;

public static class ApplicationServicesMapper
{
    public static List<string> MapToList(this ContractML.ObjectCategory objectCategory)
    {
        var result = new List<string>();

        foreach (var prop in objectCategory.GetType().GetProperties())
        {
            if (prop.Name.Contains("Sign"))
            {
                string value = "";
                if (prop.GetValue(objectCategory) is not null)
                {
                    value = prop.GetValue(objectCategory).ToString();
                }
                result.Add(value);
            }
        }

        return result;
    }
}