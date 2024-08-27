using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Infrastructure;
using Core.Repository;

namespace Core.Service;

public class LoanService
{
    private readonly LoanRepository _loanRepository;
    private readonly RiskAdapter _riskAdapter;

    public LoanService(LoanRepository loanRepository, RiskAdapter riskAdapter)
    {
        _loanRepository = loanRepository;
        _riskAdapter = riskAdapter;
    }

    public async Task<LoanApplicationResult> ApplyForLoan(User user, double loanAmount, int term)
    {
        var loan = new Loan(
            user.Id,
            loanAmount,
            term
        );

        var riskRequest = new CalculateRiskRequest();
        riskRequest.TotalIncome = user.AnnualIncome;
        riskRequest.LoanAmount = loanAmount;
        riskRequest.Term = term;

        var riskResponse = await _riskAdapter.CalculateRisk(riskRequest);

        var scoreRequest = new CalculateScoreRequest();
        scoreRequest.YearlyIncome = user.AnnualIncome;
        scoreRequest.Risk = riskResponse.Risk;

        var scoreResponse = await _riskAdapter.CalculateScore(scoreRequest);

        Loan? suggestedLoan;
        if (scoreResponse.Score >= 600)
        {
            loan.ChangeStatus(LoanStatus.APPROVED);
            suggestedLoan = SuggestBetterLoan(user, riskResponse.Risk, scoreResponse.Score);
            return new LoanApplicationResult
            {
                Loan = loan,
                SuggestedLoan = suggestedLoan
            };
        }


        loan.ChangeStatus(LoanStatus.REJECTED);
        suggestedLoan = SuggestBetterLoan(user, riskResponse.Risk, scoreResponse.Score);
        return new LoanApplicationResult
        {
            Loan = loan,
            SuggestedLoan = suggestedLoan
        };
    }

    private Loan SuggestBetterLoan(User user, double risk, double score)
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
            recommendedAmount = user.AnnualIncome * 0.5; // 50% of annual income
            recommendedTerm = 36; // Longer term, e.g., 36 months
        }
        else if (score <= lowScoreThreshold || risk >= highRiskThreshold)
        {
            // Low score or high risk: suggest a smaller loan with a shorter term
            recommendedAmount = user.AnnualIncome * 0.2; // 20% of annual income
            recommendedTerm = 12; // Shorter term, e.g., 12 months
        }
        else
        {
            // Moderate score and risk: suggest a moderate loan
            recommendedAmount = user.AnnualIncome * 0.35; // 35% of annual income
            recommendedTerm = 24; // Moderate term, e.g., 24 months
        }

        // Create and return the suggested loan
        var loan = new Loan(user.Id, recommendedAmount, recommendedTerm);
        loan.ChangeStatus(LoanStatus.PENDING);
        _loanRepository.Add(loan);

        return loan;
    }
}

public class LoanApplicationResult
{
    public Loan Loan { get; set; }
    public Loan? SuggestedLoan { get; set; }
}