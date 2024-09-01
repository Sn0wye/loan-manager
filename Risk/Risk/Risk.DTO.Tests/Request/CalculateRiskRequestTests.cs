using Risk.DTO.Request;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Core.DTO.Tests.Request;

public class CalculateRiskRequestTests
{
    [Theory]
    [InlineData(1000, 5000, 12)] // Valid case
    [InlineData(-100, 5000, 12)] // Invalid yearly income
    [InlineData(1000, -5000, 12)] // Invalid loan amount
    [InlineData(1000, 5000, 0)] // Invalid term
    public void Validate_ShouldReturnCorrectValidationResults(double yearlyIncome, double loanAmount, int term)
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            YearlyIncome = yearlyIncome,
            LoanAmount = loanAmount,
            Term = term
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        // Act
        var isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

        // Assert
        if (yearlyIncome >= 0 && loanAmount >= 0 && term >= 1)
        {
            Assert.True(isValid); // Should pass validation
            Assert.Empty(validationResults); // No validation errors
        }
        else
        {
            Assert.False(isValid); // Should fail validation
            Assert.NotEmpty(validationResults); // Should have validation errors
        }
    }
}
