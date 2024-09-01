using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Infrastructure;
using Core.Repository;

namespace Core.Service;

public class LoanService : ILoanService
{
    private readonly LoanRepository _loanRepository;
    private readonly RiskAdapter _riskAdapter;

    public LoanService(LoanRepository loanRepository, RiskAdapter riskAdapter)
    {
        _loanRepository = loanRepository;
        _riskAdapter = riskAdapter;
    }

    public async Task<LoanApplication> ApplyForLoan(User user, double loanAmount, int term)
    {
        var loan = new Loan
        {
            UserId = user.Id,
            Amount = loanAmount,
            Term = term
        };

        var riskRequest = new CalculateRiskRequest
        {
            YearlyIncome = user.YearlyIncome,
            LoanAmount = loanAmount,
            Term = term
        };

        var riskResponse = await _riskAdapter.CalculateRisk(riskRequest);

        var scoreRequest = new CalculateScoreRequest
        {
            YearlyIncome = user.YearlyIncome,
            Risk = riskResponse.Risk
        };

        var scoreResponse = await _riskAdapter.CalculateScore(scoreRequest);

        Loan? suggestedLoan;
        if (scoreResponse.Score >= 600)
        {
            loan.ChangeStatus(LoanStatus.APPROVED);
            suggestedLoan = await SuggestBetterLoan(user, riskResponse.Risk, scoreResponse.Score);
            return new LoanApplication
            {
                Loan = loan,
                SuggestedLoan = suggestedLoan
            };
        }


        loan.ChangeStatus(LoanStatus.REJECTED);
        suggestedLoan = await SuggestBetterLoan(user, riskResponse.Risk, scoreResponse.Score);
        return new LoanApplication
        {
            Loan = loan,
            SuggestedLoan = suggestedLoan
        };
    }

    private async Task<Loan> SuggestBetterLoan(User user, double risk, double score)
    {
        double recommendedAmount;
        int recommendedTerm;

        // Define thresholds
        double highScoreThreshold = 800;
        double lowScoreThreshold = 400;
        var lowRiskThreshold = 0.3;
        var highRiskThreshold = 0.7;

        if (score >= highScoreThreshold && risk <= lowRiskThreshold)
        {
            // High score, low risk: suggest a larger loan with a longer term
            recommendedAmount = user.YearlyIncome * 0.5; // 50% of yearly income
            recommendedTerm = 36; // Longer term, e.g., 36 months
        }
        else if (score <= lowScoreThreshold || risk >= highRiskThreshold)
        {
            // Low score or high risk: suggest a smaller loan with a shorter term
            recommendedAmount = user.YearlyIncome * 0.2; // 20% of yearly income
            recommendedTerm = 12; // Shorter term, e.g., 12 months
        }
        else
        {
            // Moderate score and risk: suggest a moderate loan
            recommendedAmount = user.YearlyIncome * 0.35; // 35% of yearly income
            recommendedTerm = 24; // Moderate term, e.g., 24 months
        }

        // Create and return the suggested loan
        var newSuggestedLoan = new Loan
        {
            UserId = user.Id,
            Amount = recommendedAmount,
            Term = recommendedTerm
        };
        newSuggestedLoan.ChangeStatus(LoanStatus.PENDING);
        var suggestedLoan = await _loanRepository.AddAsync(newSuggestedLoan);

        return suggestedLoan;
    }
}

public class LoanApplication
{
    public required Loan Loan { get; set; }
    public Loan? SuggestedLoan { get; set; }
}