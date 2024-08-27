using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

[Table("loans")]
public class Loan(int userId, double amount, int term)
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public LoanStatus Status { get; set; } = LoanStatus.PENDING;
    public double Amount { get; set; } = amount;
    public int Term { get; set; } = term;

    public void ChangeStatus(LoanStatus status)
    {
        Status = status;
    }
}