namespace BigCatCookinAPI.Services.Interface;

public interface IBigCatCookinConfig
{
    string GptAPIKey { get; }
    string MapAPIKey { get; }
    string BaseRecipePrompt { get; }
    string BaseDietPrompt { get; }
    string BaseQuestionPrompt { get; }
    string BaseExplanationPrompt { get; }

    string GPTEndpoint { get; }


    string RecipeDBConnString { get; }
    string UserDBConnString { get; }
    string StoreDBConnString { get; }
    string CouponDBConnString { get; }
}
