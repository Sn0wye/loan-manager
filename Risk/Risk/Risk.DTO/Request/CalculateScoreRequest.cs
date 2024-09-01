using System.ComponentModel.DataAnnotations;

namespace Risk.DTO.Request;

public class CalculateScoreRequest
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Yearly income must be a non-negative number.")]
    public double YearlyIncome { get; set; }
    
    [Range(0, 1, ErrorMessage = "Risk must be between 0 and 1.")]
    public double Risk { get; set; }
}