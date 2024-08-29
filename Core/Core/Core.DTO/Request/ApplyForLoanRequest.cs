namespace Core.DTO.Request;

public class ApplyForLoanRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Document { get; set; }
    public double AnnualIncome { get; set; }
    public double LoanAmount { get; set; }
    public int Term { get; set; }
}