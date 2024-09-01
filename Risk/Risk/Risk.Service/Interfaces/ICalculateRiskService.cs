using Risk.DTO.Request;

namespace Risk.Service;

public interface ICalculateRiskService
{
    double CalculateRisk(CalculateRiskRequest dto);
}