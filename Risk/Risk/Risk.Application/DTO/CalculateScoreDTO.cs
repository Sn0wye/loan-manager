namespace Risk.Application.DTO;

public class CalculateScoreDTO(double yearlyIncome, double risk)
{
    public double YearlyIncome { get; set; } = yearlyIncome;
    public double Risk { get; set; } = risk;
}