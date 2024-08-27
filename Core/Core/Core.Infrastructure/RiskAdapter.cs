using System.Text;
using System.Text.Json;

namespace Core.Infrastructure;

public class RiskAdapter
{
    private readonly HttpClient _client;

    public RiskAdapter(HttpClient client)
    {
        _client = client;
    }


    public async Task<CalculateRiskResponse> CalculateRisk(CalculateRiskRequest request)
    {
        string jsonData = JsonSerializer.Serialize(request);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("risk/calculate", content);
        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CalculateRiskResponse>(responseString);
    }

    public async Task<CalculateScoreResponse> CalculateScore(CalculateScoreRequest request)
    {
        string jsonData = JsonSerializer.Serialize(request);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("score/calculate", content);
        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CalculateScoreResponse>(responseString);
    }
}

public class CalculateRiskRequest()
{
    public double TotalIncome { get; set; }
    public double LoanAmount { get; set; }
    public int Term { get; set; }
}

public class CalculateRiskResponse
{
    public double Risk { get; set; }
}

public class CalculateScoreRequest
{
    public double YearlyIncome { get; set; }
    public double Risk { get; set; }
}

public class CalculateScoreResponse
{
    public int Score { get; set; }
}