using System.ComponentModel.DataAnnotations;

public class ApplyForLoanRequest
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Document is required.")]
    public string Document { get; set; }

    [Required(ErrorMessage = "Yearly income is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Yearly income must be greater than 0.")]
    public double YearlyIncome { get; set; }

    [Required(ErrorMessage = "Loan amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Loan amount must be greater than 0.")]
    public double LoanAmount { get; set; }

    [Required(ErrorMessage = "Term is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Term must be at least 1 month.")]
    public int Term { get; set; }
}