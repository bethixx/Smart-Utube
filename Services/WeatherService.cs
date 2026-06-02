using System.Text.Json;

public class WeatherService
{
    private readonly HttpClient _http;

    public WeatherService(HttpClient http)
    {
        _http = http;
    }

    public async Task<double?> GetTemperatureAsync()
    {
        var url =
            "https://api.open-meteo.com/v1/forecast?latitude=53.13&longitude=23.16&current_weather=true";

        var json = await _http.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);

        var temp = doc.RootElement
            .GetProperty("current_weather")
            .GetProperty("temperature")
            .GetDouble();

        return temp;
    }
}