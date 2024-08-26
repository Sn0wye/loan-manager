namespace Risk.Application.DTO;

public class CalculateRiskDTO(double yearlyIncome, double loanAmount, int term)
{
    public double YearlyIncome { get; set; } = yearlyIncome;
    public double LoanAmount { get; set; } = loanAmount;
    public int Term { get; set; } = term;
}