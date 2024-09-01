using System.ComponentModel.DataAnnotations;

namespace Risk.DTO.Request;

public class CalculateRiskRequest
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Total income must be a non-negative number.")]
    public double TotalIncome { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Loan amount must be a non-negative number.")]
    public double LoanAmount { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Term must be at least 1 month.")]
    public int Term { get; set; }
}