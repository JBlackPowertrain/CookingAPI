namespace BigCatCookinAPI.Models.GPT;

public class GPTResponseModel
{
    public string Id { get; set; }
    public string Object { get; set; }
    public long Created { get; set; }
    public string model { get; set; }
    public IList<GPTChoice> Choices { get; set; }
}

public class GPTChoice
{
    public int Index;
    public GPTMessage message; 
    public string Finish_Reason { get; set; }
}

public class GPTMessage
{
    public string Role { get; set; }
    public string Content { get; set; }
}