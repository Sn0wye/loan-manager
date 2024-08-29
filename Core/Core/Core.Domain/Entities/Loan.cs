using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

[Table("loans")]
public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.PENDING;
    public double Amount { get; set; }
    public int Term { get; set; }

    public void ChangeStatus(LoanStatus status)
    {
        Status = status;
    }
}