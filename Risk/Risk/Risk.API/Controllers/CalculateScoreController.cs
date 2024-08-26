using Microsoft.AspNetCore.Mvc;
using Risk.Application.DTO;
using Risk.Application.Model;
using Risk.Service;

namespace Risk.API.Controllers;

[ApiController]
[Route("score")]
public class CalculateScoreController
{
    private readonly CalculateScoreService _calculateScoreService = new();
    
    [HttpPost("calculate")]
    public ActionResult<CalculateScoreResponse> CalculateScore([FromBody] CalculateScoreRequest body)
    {

        var dto = new CalculateScoreDTO(
            body.YearlyIncome,
            body.Risk
        );
        
        var score = _calculateScoreService.CalculateCreditScore(dto);
        
        return new CalculateScoreResponse
        {
            Score = score
        };
    }
}