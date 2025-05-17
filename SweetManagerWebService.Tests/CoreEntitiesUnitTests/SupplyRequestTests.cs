using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class SuppliesRequestTests
{
    [Test]
    public void SuppliesRequest_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var id = 1;
        var paymentsOwnersId = 2;
        var suppliesId = 3;
        var count = 10;
        var amount = 250.00m;

        // Act
        var suppliesRequest = new SuppliesRequest(
            id,
            paymentsOwnersId,
            suppliesId,
            count,
            amount
        );

        // Assert
        Assert.That(suppliesRequest.Id, Is.EqualTo(id));
        Assert.That(suppliesRequest.PaymentsOwnersId, Is.EqualTo(paymentsOwnersId));
        Assert.That(suppliesRequest.SuppliesId, Is.EqualTo(suppliesId));
        Assert.That(suppliesRequest.Count, Is.EqualTo(count));
        Assert.That(suppliesRequest.Amount, Is.EqualTo(amount));
    }

    [Test]
    public void SuppliesRequest_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateSuppliesRequestCommand(
            PaymentsOwnersId: 5,
            SuppliesId: 8,
            Count: 15,
            Amount: 375.50m
        );

        // Act
        var suppliesRequest = new SuppliesRequest(command);

        // Assert
        Assert.That(suppliesRequest.PaymentsOwnersId, Is.EqualTo(command.PaymentsOwnersId));
        Assert.That(suppliesRequest.SuppliesId, Is.EqualTo(command.SuppliesId));
        Assert.That(suppliesRequest.Count, Is.EqualTo(command.Count));
        Assert.That(suppliesRequest.Amount, Is.EqualTo(command.Amount));
    }

    [Test]
    public void SuppliesRequest_AmountCalculation_ShouldMatchCountAndPrice()
    {
        // Arrange
        var id = 2;
        var paymentsOwnersId = 3;
        var suppliesId = 4;
        var count = 5;
        var pricePerUnit = 50.00m;
        var expectedAmount = count * pricePerUnit;

        // Act
        var suppliesRequest = new SuppliesRequest(
            id,
            paymentsOwnersId,
            suppliesId,
            count,
            expectedAmount
        );

        // Assert
        Assert.That(suppliesRequest.Amount, Is.EqualTo(expectedAmount));
        Assert.That(suppliesRequest.Count * pricePerUnit, Is.EqualTo(suppliesRequest.Amount));
    }

    
}