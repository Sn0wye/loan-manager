using System.ComponentModel.DataAnnotations.Schema;

namespace Risk.Domain.Entities;

[Table("users")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Age { get; set; }
    public string Document { get; set; }
    public string Password { get; set; }
    public float AnnualIncome { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}