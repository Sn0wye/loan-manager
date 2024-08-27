namespace API.DTO;

public class CreateUserDTO
{
    public CreateUserDTO(string name, string email, string age, string document, double annualIncome)
    {
        Name = name;
        Email = email;
        Age = age;
        Document = document;
        AnnualIncome = annualIncome;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Age { get; set; }
    public string Document { get; set; }
    public double AnnualIncome { get; set; }
}