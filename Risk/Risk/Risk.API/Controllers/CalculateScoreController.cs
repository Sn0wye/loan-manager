using Microsoft.AspNetCore.Mvc;
using Risk.DTO.Request;
using Risk.DTO.Response;
using Risk.Service;

namespace Risk.API.Controllers;

[ApiController]
[Route("score")]
public class CalculateScoreController
{
    private readonly ICalculateScoreService _calculateScoreService;
    
    public CalculateScoreController(ICalculateScoreService calculateScoreService)
    {
        _calculateScoreService = calculateScoreService;
    }
    
    [HttpPost("calculate")]
    public ActionResult<CalculateScoreResponse> CalculateScore([FromBody] CalculateScoreRequest body)
    {
        var score = _calculateScoreService.CalculateCreditScore(body);
        
        return new CalculateScoreResponse
        {
            Score = score
        };
    }
}