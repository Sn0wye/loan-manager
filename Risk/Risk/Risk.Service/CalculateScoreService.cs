using Risk.Application.DTO;

namespace Risk.Service;

public class CalculateScoreService
{
       private const double MaximumIncome = 100000;
       
       public int CalculateCreditScore(CalculateScoreDTO dto)
       {
              // Ensure risk is between 0 and 1
              if (dto.Risk is < 0 or > 1)
              {
                     throw new ArgumentOutOfRangeException(nameof(dto.Risk), "Risk must be between 0 and 1.");
              }

              // Calculate base score
              double baseScore = (dto.YearlyIncome / MaximumIncome) * 1000;

              // Calculate final credit score
              double creditScore = baseScore * (1 - dto.Risk);

              // Ensure credit score is within the 0-1000 range and return as an integer
              return (int)Math.Max(0, Math.Min(1000, creditScore));
       }
}