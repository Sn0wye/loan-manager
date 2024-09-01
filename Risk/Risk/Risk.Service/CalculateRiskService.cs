using Risk.DTO.Request;

namespace Risk.Service;

public class CalculateRiskService
{
    
    private const int BestInstallmentToIncomeRatio = 100;
    
    public double CalculateRisk(CalculateRiskRequest dto)
    {
        double monthlyInstallment = dto.LoanAmount / dto.Term;

        // Calculate the income-to-installment ratio
        double incomeToInstallmentRatio = dto.YearlyIncome / monthlyInstallment;

        // Early return for extremely low risk
        if (incomeToInstallmentRatio > BestInstallmentToIncomeRatio) return 0;

        // Normalize the ratio to a value between 0 and 1
        double risk = 1.0 / incomeToInstallmentRatio;

        // Clamp the risk value to the range [0, 1]
        risk = Math.Min(Math.Max(risk, 0), 1);
    
        return Math.Round(risk, 3);
    }
}