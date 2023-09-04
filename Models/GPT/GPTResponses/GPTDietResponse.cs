namespace BigCatCookinAPI.Models.GPT.GPTResponses;

public class GPTDietResponse
{
    public IEnumerable<Diet> DietIdeas { get; set; }
}

public class Diet
{
    public string DietName { get; set; }
    public string Reasoning { get; set; }
    public string Benefits { get; set; }
    public string Drawbacks { get; set; }
    public string Disclaimer { get; set; }
}