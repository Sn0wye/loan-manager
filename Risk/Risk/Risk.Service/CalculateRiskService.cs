using Risk.Application.DTO;

namespace Risk.Service;

public class CalculateRiskService
{
    public double CalculateRisk(CalculateRiskDTO dto)
    {
        double monthlyInstallment = dto.LoanAmount / dto.Term;

        // Calculate the income-to-installment ratio
        double incomeToInstallmentRatio = dto.YearlyIncome / monthlyInstallment;

        // Normalize the ratio to a value between 0 and 1
        double risk = 1.0 / incomeToInstallmentRatio;

        // Clamp the risk value to the range [0, 1]
        return Math.Min(Math.Max(risk, 0), 1);
    }
}