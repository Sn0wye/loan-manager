using System.Text;
using System.Text.Json;
using Polly;
using Polly.Retry;

namespace Core.Infrastructure;

public class RiskAdapter
{
    private readonly HttpClient _client;
    private readonly ResiliencePipeline _resiliencePipeline;

    public RiskAdapter(HttpClient client)
    {
        _client = client;
        _resiliencePipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(2)
                })
                .AddTimeout(TimeSpan.FromSeconds(30))
                .Build()
            ;
    }


    public async Task<CalculateRiskResponse> CalculateRisk(CalculateRiskRequest request)
    {
        var jsonData = JsonSerializer.Serialize(request);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");


        var response = await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            var response = await _client.PostAsync("risk/calculate", content, ct);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(ct);
            return JsonSerializer.Deserialize<CalculateRiskResponse>(responseString);
        });

        if (response is null) throw new Exception("Failed to calculate risk");

        return response;
    }

    public async Task<CalculateScoreResponse> CalculateScore(CalculateScoreRequest request)
    {
        var jsonData = JsonSerializer.Serialize(request);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            var httpResponse = await _client.PostAsync("score/calculate", content, ct);
            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync(ct);
            return JsonSerializer.Deserialize<CalculateScoreResponse>(responseString);
        });

        if (response is null) throw new Exception("Failed to calculate score");

        return response;
    }
}

public class CalculateRiskRequest
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