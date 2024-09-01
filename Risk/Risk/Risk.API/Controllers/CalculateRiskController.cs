using Microsoft.AspNetCore.Mvc;
using Risk.DTO.Request;
using Risk.DTO.Response;
using Risk.Service;

namespace Risk.API.Controllers;

[ApiController]
[Route("risk")]
public class CalculateRiskController
{
    private readonly ICalculateRiskService _calculateRiskService;
    
    public CalculateRiskController(ICalculateRiskService calculateRiskService)
    {
        _calculateRiskService = calculateRiskService;
    }
    
    [HttpPost("calculate")]
    public ActionResult<CalculateRiskResponse> CalculateRisk([FromBody] CalculateRiskRequest body)
    {

        var risk = _calculateRiskService.CalculateRisk(body);
        
        return new CalculateRiskResponse
        {
            Risk = risk
        };
    }
}