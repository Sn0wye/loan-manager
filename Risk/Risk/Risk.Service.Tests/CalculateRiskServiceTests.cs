using Risk.DTO.Request;
using Xunit;

namespace Risk.Service.Tests;

public class CalculateRiskServiceTests
{
    private readonly CalculateRiskService _service;

    public CalculateRiskServiceTests()
    {
        _service = new CalculateRiskService();
    }

    [Fact]
    public void CalculateRisk_ShouldReturnCorrectRisk_WhenIncomeIsHigherThanInstallment()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 10000,
            Term = 12,
            YearlyIncome = 5000
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.InRange(result, 0, 1);
        Assert.True(result > 0 && result < 1); // Risk should be between 0 and 1
    }

    [Fact]
    public void CalculateRisk_ShouldReturnOne_WhenIncomeEqualsInstallment()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 12000,
            Term = 12,
            YearlyIncome = 1000
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.Equal(1, result); // Risk should be 1
    }

    [Fact]
    public void CalculateRisk_ShouldReturnMaxRisk_WhenIncomeIsLessThanInstallment()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 12000,
            Term = 12,
            YearlyIncome = 500
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.Equal(1, result); // Risk should be maxed out at 1
    }

    [Fact]
    public void CalculateRisk_ShouldReturnMinRisk_WhenIncomeIsMuchHigherThanInstallment()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 10000,
            Term = 12,
            YearlyIncome = 120000
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.Equal(0, result); // Risk should be minimized to 0
    }
    
    [Fact]
    public void CalculateRisk_ShouldReturnMaxRisk_WhenIncomeIsZero()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 10000,
            Term = 12,
            YearlyIncome = 0
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.Equal(1, result); // Risk should be maxed out at 1
    }

    [Fact]
    public void CalculateRisk_ShouldHandleSmallIncome()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            LoanAmount = 10000,
            Term = 12,
            YearlyIncome = 1 // Very small income
        };

        // Act
        var result = _service.CalculateRisk(request);

        // Assert
        Assert.Equal(1, result); // Risk should be maxed out at 1
    }
}
