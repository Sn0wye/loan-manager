using Risk.DTO.Request;
using Risk.Service;
using Xunit;

namespace Risk.Service.Tests;

public class CalculateScoreServiceTests
{
    private readonly CalculateScoreService _service;

    public CalculateScoreServiceTests()
    {
        _service = new CalculateScoreService();
    }

    [Fact]
    public void CalculateCreditScore_ShouldReturnMaxScore_WhenIncomeIsMaximumAndRiskIsZero()
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = 100000, // Maximum income
            Risk = 0 // No risk
        };

        // Act
        var result = _service.CalculateCreditScore(request);

        // Assert
        Assert.Equal(1000, result); // Max score
    }

    [Fact]
    public void CalculateCreditScore_ShouldReturnZero_WhenIncomeIsMinimumAndRiskIsMaximum()
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = 0, // No income
            Risk = 1 // Maximum risk
        };

        // Act
        var result = _service.CalculateCreditScore(request);

        // Assert
        Assert.Equal(0, result); // Min score
    }

    [Fact]
    public void CalculateCreditScore_ShouldReturnScore_WhenIncomeIsAverageAndRiskIsMedium()
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = 50000, // Average income
            Risk = 0.5 // Medium risk
        };

        // Act
        var result = _service.CalculateCreditScore(request);

        // Assert
        Assert.Equal(250, result); // Expected score
    }

    [Fact]
    public void CalculateCreditScore_ShouldClampScoreToMax_WhenCalculatedScoreExceedsMax()
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = 200000, // Higher than maximum income, which would push the score above 1000
            Risk = 0 // No risk
        };

        // Act
        var result = _service.CalculateCreditScore(request);

        // Assert
        Assert.Equal(1000, result); // Score should not be more than 1000
    }

    [Fact]
    public void CalculateCreditScore_ShouldThrowException_WhenRiskIsOutOfRange()
    {
        // Arrange
        var request = new CalculateScoreRequest
        {
            YearlyIncome = 50000,
            Risk = 1.5 // Out of range
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalculateCreditScore(request));
    }
}
