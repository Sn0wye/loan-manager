using Risk.Application.DTO;
using Risk.Application.Model;
using Microsoft.AspNetCore.Mvc;
using Risk.Service;

namespace Risk.API.Controllers;

[ApiController]
[Route("risk")]
public class CalculateRiskController
{
    private readonly CalculateRiskService _calculateRiskService = new();
    
    [HttpPost("calculate")]
    public ActionResult<CalculateRiskResponse> CalculateRisk([FromBody] CalculateRiskRequest body)
    {

        var dto = new CalculateRiskDTO(
            body.TotalIncome,
            body.LoanAmount,
            body.Term
        );

        var risk = _calculateRiskService.CalculateRisk(dto);
        
        return new CalculateRiskResponse
        {
            Risk = risk
        };
    }
}