using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class SubscriptionTests
{
    [Test]
    public void Subscription_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var name = "Premium";
        var description = "Acceso completo a todas las funcionalidades";
        var price = 99.99m;
        var state = "Active";

        // Act
        var subscription = new Subscription(name, description, price, state);

        // Assert
        Assert.That(subscription.Name, Is.EqualTo(name.ToUpper()));
        Assert.That(subscription.Description, Is.EqualTo(description));
        Assert.That(subscription.Price, Is.EqualTo(price));
        Assert.That(subscription.State, Is.EqualTo(state.ToUpper()));
    }
    
    [Test]
    public void Subscription_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateSubscriptionCommand("Premium", "Acceso completo a todas las funcionalidades", 99.99m, "Active");

        // Act
        var subscription = new Subscription(command.Name, command.Description, command.Price, command.State);

        // Assert
        Assert.That(subscription.Name, Is.EqualTo(command.Name.ToUpper()));
        Assert.That(subscription.Description, Is.EqualTo(command.Description));
        Assert.That(subscription.Price, Is.EqualTo(command.Price));
        Assert.That(subscription.State, Is.EqualTo(command.State.ToUpper()));
    }
}