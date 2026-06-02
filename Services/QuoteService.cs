using System.Text.Json;
using Smart_Utube.DTOs;
using Smart_Utube.DTOs.Quote;

public class QuoteService : IQuoteService
{
    private readonly HttpClient _httpClient;

    public QuoteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<QuoteDto?> GetRandomQuoteAsync()
    {
        var response = await _httpClient.GetAsync("https://dummyjson.com/quotes/random");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<QuoteDto>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }
}