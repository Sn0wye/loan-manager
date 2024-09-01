using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

[Table("users")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Document { get; set; }
    public double YearlyIncome { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}