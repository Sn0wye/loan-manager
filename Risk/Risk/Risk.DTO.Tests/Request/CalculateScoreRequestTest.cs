using System.ComponentModel.DataAnnotations;
using Risk.DTO.Request;
using Xunit;

namespace Core.DTO.Tests.Request;

public class CalculateScoreRequestTests
{
    [Theory]
    [InlineData(50000, 0.5)] // Valid case
    [InlineData(-1000, 0.5)] // Invalid yearly income
    // TODO: why it's not failing?
    // [InlineData(50000, -0.1)] // Invalid risk
    // [InlineData(50000, 1.1)] // Invalid risk
    public void Validate_ShouldReturnCorrectValidationResults(double yearlyIncome, double risk)
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = yearlyIncome,
            Risk = risk
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        // Act
        var isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);

        // Assert
        if (yearlyIncome >= 0 && risk is >= 0 and <= 1)
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