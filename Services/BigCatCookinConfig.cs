using BigCatCookinAPI.Services.Interface;

namespace BigCatCookinAPI.Services;

public class BigCatCookinConfig : IBigCatCookinConfig
{
    //API Keys
    private string gptAPIKey { get; set; }
    private string mapAPIKey { get; set; }

    //GPT Prompts
    private string baseRecipePrompt { get; set; }
    private string baseDietPrompt { get; set; }
    private string baseQuestionPrompt { get; set; }
    private string baseExplanationPrompt { get; set; }

    //Endpoints
    private string gptEndpoint { get; set; }

    //Connection strings
    private string recipeDBConnString { get; set; }
    private string userDBConnString { get; set; }
    private string storeDBConnString { get; set; }
    private string couponDBConnString { get; set; }

    public string GptAPIKey
    {
        get => gptAPIKey;
        private set => gptAPIKey = value;
    }

    public string MapAPIKey
    {
        get => mapAPIKey;
        private set => mapAPIKey = value;
    }

    public string BaseRecipePrompt
    {
        get => baseRecipePrompt;
        private set => baseRecipePrompt = value;
    }

    public string BaseDietPrompt
    {
        get => baseDietPrompt;
        private set => baseDietPrompt = value;
    }

    public string BaseQuestionPrompt
    {
        get => baseQuestionPrompt;
        private set => baseQuestionPrompt = value;
    }

    public string GPTEndpoint
    {
        get => gptEndpoint;
        private set => gptEndpoint = value;
    }

    public string BaseExplanationPrompt
    {
        get => baseExplanationPrompt;
        private set => baseExplanationPrompt = value;
    }


    public string RecipeDBConnString
    {
        get => recipeDBConnString;
        private set => recipeDBConnString = value;
    }
    public string UserDBConnString
    {
        get => userDBConnString;
        private set => userDBConnString = value;
    }
    public string StoreDBConnString
    {
        get => storeDBConnString;
        private set => storeDBConnString = value;
    }
    public string CouponDBConnString
    {
        get => couponDBConnString;
        private set => couponDBConnString = value;
    }

    public BigCatCookinConfig(IConfiguration configuration)
    {
        GptAPIKey = configuration.GetValue("OpenAIAPIKey", "");
        MapAPIKey = configuration.GetValue("MapAPIKey", "");
        BaseRecipePrompt = configuration.GetValue("BaseRecipePrompt", "");
        BaseDietPrompt = configuration.GetValue("BaseDietPrompt", "");
        BaseQuestionPrompt = configuration.GetValue("BaseQuestionPrompt", "");
        BaseExplanationPrompt = configuration.GetValue("BaseExplanationPrompt", "");
        GPTEndpoint = configuration.GetValue("GPTEndpoint", "");
        RecipeDBConnString = configuration.GetValue("RecipeDBConnString", "");
        UserDBConnString = configuration.GetValue("UserDBConnString", "");
        StoreDBConnString = configuration.GetValue("StoreDBConnString", "");
        CouponDBConnString = configuration.GetValue("CouponDBConnString", "");
    }
}
