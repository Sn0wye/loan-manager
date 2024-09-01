using Risk.DTO.Request;

namespace Risk.Service;

public interface ICalculateScoreService
{
    int CalculateCreditScore(CalculateScoreRequest dto);
}