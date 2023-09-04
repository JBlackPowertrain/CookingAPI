using BigCatCookinAPI.Models.GPT;
using BigCatCookinAPI.Models.GPT.GPTResponses;
using BigCatCookinAPI.Models.GPT.GPTResponses.GPTRecipes;
using BigCatCookinAPI.Services.Interface;
using BigCatCookinAPI.Utilities;
using Newtonsoft.Json;
using System.Text;

namespace BigCatCookinAPI.Services;

public class ChatGPTService : IChatGPTService
{
    private readonly IBigCatCookinConfig _webConfig;

    public ChatGPTService(IBigCatCookinConfig webConfig)
    {
        _webConfig = webConfig;
    }

    public async Task<string> AnswerFAQ(string genericQuestion)
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization",
            string.Format("Bearer {0}", _webConfig.GptAPIKey));
        client.DefaultRequestHeaders.Add("Accept", "*/*");
        //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.Add("Connection", "keep-alive");

        string _jsonCont = string.Format(_webConfig.BaseQuestionPrompt, genericQuestion);

        StringContent jsonRequest = new StringContent(
            JsonConvert.SerializeObject(GPTRequestModel.GetRecipe(_jsonCont),
            Formatting.None,
            LowercaseContractResolver.GetLowercaseResolver()),
                Encoding.UTF8,
                "application/json");

        jsonRequest.Headers.ContentType.CharSet = "";

        try
        {
            using (HttpResponseMessage resp = await client.PostAsync(_webConfig.GPTEndpoint,
                jsonRequest))
            {
                GPTResponseModel gptResp = JsonConvert.DeserializeObject<GPTResponseModel>(
                    await resp.Content.ReadAsStringAsync());

                return gptResp.Choices[0].message.Content;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<GPTDietResponse> GetDietPlans(string dietaryIssues, 
        string allergies, 
        string pallet)
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization",
            string.Format("Bearer {0}", _webConfig.GptAPIKey));
        client.DefaultRequestHeaders.Add("Accept", "*/*");
        //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.Add("Connection", "keep-alive");

        string _jsonCont = string.Format(_webConfig.BaseDietPrompt,
            dietaryIssues,
            pallet,
            allergies);

        StringContent jsonRequest = new StringContent(
            JsonConvert.SerializeObject(GPTRequestModel.GetRecipe(_jsonCont),
            Formatting.None,
            LowercaseContractResolver.GetLowercaseResolver()),
                Encoding.UTF8,
                "application/json");

        jsonRequest.Headers.ContentType.CharSet = "";

        try
        {
            using (HttpResponseMessage resp = await client.PostAsync(_webConfig.GPTEndpoint,
                jsonRequest))
            {
                GPTResponseModel gptResp = JsonConvert.DeserializeObject<GPTResponseModel>(
                    await resp.Content.ReadAsStringAsync());

                return JsonConvert.DeserializeObject<GPTDietResponse>(
                    gptResp.Choices[0].message.Content.Replace("\n", "").Replace("\\", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<GPTRecipeResponse> GetRecipe(string allergies,
        string dietaryRestrictions,
        string pallet,
        string availableAppliances,
        string availableUtensils,
        string ingredients,
        string previousDishes)
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Authorization",
            string.Format("Bearer {0}", _webConfig.GptAPIKey));
        client.DefaultRequestHeaders.Add("Accept", "*/*");
        //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        client.DefaultRequestHeaders.Add("Connection", "keep-alive");

        string _jsonCont = string.Format(_webConfig.BaseRecipePrompt,
            dietaryRestrictions,
            allergies,
            pallet,
            availableAppliances,
            availableUtensils,
            ingredients,
            previousDishes);

        StringContent jsonRequest = new StringContent(
            JsonConvert.SerializeObject(GPTRequestModel.GetRecipe(_jsonCont), 
            Formatting.None, 
            LowercaseContractResolver.GetLowercaseResolver()), 
                Encoding.UTF8,
                "application/json");

        jsonRequest.Headers.ContentType.CharSet = ""; 

        try
        {
            using (HttpResponseMessage resp = await client.PostAsync(_webConfig.GPTEndpoint,
                jsonRequest))
            {
                GPTResponseModel gptResp = JsonConvert.DeserializeObject<GPTResponseModel>(
                    await resp.Content.ReadAsStringAsync());

                return JsonConvert.DeserializeObject<GPTRecipeResponse>(
                    gptResp.Choices[0].message.Content.Replace("\n","").Replace("\\",""));
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
