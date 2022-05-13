using MlServer.Learners.Models;

using ContractMl = MlServer.Contracts.Models.ML;

namespace MlServer.Orchestrator.Learners.Mapper;

public static class OrchLearnersMapper
{
    private static List<string> GetParamsList(this ContractMl.ObjectCategory objectCategory)
    {
        var result = new List<string>();
        
        foreach (var prop in objectCategory.GetType().GetProperties())
        {
            if (prop.Name != "TableName" && prop.Name != "Category")
            {
                var item = prop.GetValue(objectCategory) ?? "";
                result.Add(item.ToString()!);
            }
        }

        return result;
    }
    
    public static ObjectCategory MapToObjectCategory(this ContractMl.ObjectCategory objectCategory, bool isForLearn)
    {
        var result = new ObjectCategory();

        var items = objectCategory.GetParamsList();
        int itemIndex = 0;
        foreach (var prop in result.GetType().GetProperties())
        {
            if(prop.Name.Contains("Sign")) {
                prop.SetValue(result, items[itemIndex]);
                itemIndex++;
            }
        }

        if (isForLearn)
        {
            result.Category = objectCategory.Category;
        }
        
        return result;
    }

    public static IEnumerable<ObjectCategory> MapToIEnumObjCategoryForLearn(this IEnumerable<ContractMl.ObjectCategory> data)
    {
        return data.Select(x => x.MapToObjectCategory(true));
    }
}