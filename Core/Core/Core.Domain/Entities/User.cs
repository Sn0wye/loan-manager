using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

[Table("users")]
public class User(string name, string email, int age, string document, double annualIncome)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public int Age { get; set; } = age;
    public string Document { get; set; } = document;
    public double AnnualIncome { get; set; } = annualIncome;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}