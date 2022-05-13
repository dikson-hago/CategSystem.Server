using ContractMl = MlServer.Contracts.Models.ML;

namespace MlServer.Orchestrator.Learners.Distriburtors;

public class MlDistributorBase
{
    public void FillEmptyCells(IEnumerable<ContractMl.ObjectCategory> data)
    {
        foreach (var item in data)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.Name.Contains("Sign"))
                {
                    var value = prop.GetValue(item);

                    if (value is null)
                    {
                        prop.SetValue(item, "1");
                    }
                }
            }
        }
    }
}