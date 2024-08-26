using System.ComponentModel.DataAnnotations.Schema;
using Risk.Domain.Enums;

namespace Risk.Domain.Entities;

[Table("loans")]
public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.PENDING;
    public int Amount { get; set; }
    public int Term { get; set; }
}