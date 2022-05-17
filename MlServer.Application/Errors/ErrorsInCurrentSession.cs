namespace MlServer.Application.Errors;

public class ErrorsInCurrentSession
{
    private List<string> Errors { get; set; }

    public ErrorsInCurrentSession()
    {
        Errors = new List<string>();
    }

    public void AddError(string error)
    {
        Errors.Add(error);
    }

    public int GetErrorsAmount()
    {
        return Errors.Count;
    }

    public List<string> GetErrors()
    {
        List<string> result = new List<string>(Errors);
        Errors.Clear();
        return result;
    }
}