using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NutrientRetrieval.Translation;

public static class TranslationApi
{
    private const string ResourceKey = "46b2156e2c2b408bb356ab7030fc5d1a";
    private const string Endpoint = "https://api.cognitive.microsofttranslator.com/";
    private const string Route = "/translate?api-version=3.0&from=en&to=es";
    private const string Location = "brazilsouth";
    
    private static readonly HttpClient Client = new();

    public static async Task<string?> Translate(string input)
    {
        object[] body = {new {Text = input}};
        var requestBody = JsonConvert.SerializeObject(body);
        
        using var request = new HttpRequestMessage();
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri($"{Endpoint}{Route}");
        request.Headers.Add("Ocp-Apim-Subscription-Key", ResourceKey);
        request.Headers.Add("Ocp-Apim-Subscription-Region", Location);
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        
        var response = await Client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var array = JsonConvert.DeserializeObject<TranslationResult[]>(content);
        return array!.First().Translations.First().Text;
    }
}