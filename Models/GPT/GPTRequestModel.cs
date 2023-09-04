namespace BigCatCookinAPI.Models.GPT;

public class GPTRequestModel
{
    public string Model { get; set; }
    public IList<GPTMessage> Messages { get; set; }
    public float temperature { get; set; }
    public int Max_Tokens { get; set; }
    public int Top_P { get; set; }
    public float Frequency_Penalty { get; set; }
    public float Presence_Penalty { get; set; }

    public static GPTRequestModel GetRecipe(string message)
    {
        GPTMessage _message = new GPTMessage()
        {
            Content = message,
            Role = "user"
        };
        GPTRequestModel model = new GPTRequestModel()
        {
            Model = "gpt-4",
            temperature = 0.5f,
            Max_Tokens = 3072,
            Top_P = 1,
            Frequency_Penalty = 0,
            Presence_Penalty = 0,
            Messages = new GPTMessage[] { _message }
        };

        return model;
    }
}
