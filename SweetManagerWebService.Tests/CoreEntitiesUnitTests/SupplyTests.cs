
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class SupplyTests
{
    [Test]
    public void Supply_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var id = 1;
        var providersId = 2;
        var name = "Azúcar";
        var price = 25.50m;
        var stock = 100;
        var state = "disponible";

        // Act
        var supply = new Supply(
            id,
            providersId,
            name,
            price,
            stock,
            state
        );

        // Assert
        Assert.That(supply.Id, Is.EqualTo(id));
        Assert.That(supply.ProvidersId, Is.EqualTo(providersId));
        Assert.That(supply.Name, Is.EqualTo(name.ToUpper())); // Verificamos que el nombre se convierta a mayúsculas
        Assert.That(supply.Price, Is.EqualTo(price));
        Assert.That(supply.Stock, Is.EqualTo(stock));
        Assert.That(supply.State, Is.EqualTo(state.ToUpper())); // Verificamos que el estado se convierta a mayúsculas
    }

    [Test]
    public void Supply_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateSupplyCommand(
            ProvidersId: 3,
            Name: "Harina",
            Price: 18.75m,
            Stock: 50,
            State: "activo"
        );

        // Act
        var supply = new Supply(command);

        // Assert
        Assert.That(supply.ProvidersId, Is.EqualTo(command.ProvidersId));
        Assert.That(supply.Name, Is.EqualTo(command.Name.ToUpper())); // Verificamos que el nombre se convierta a mayúsculas
        Assert.That(supply.Price, Is.EqualTo(command.Price));
        Assert.That(supply.Stock, Is.EqualTo(command.Stock));
        Assert.That(supply.State, Is.EqualTo(command.State.ToUpper())); // Verificamos que el estado se convierta a mayúsculas
    }

    [Test]
    public void Supply_Update_ShouldUpdateProperties()
    {
        // Arrange
        var supply = new Supply(
            id: 1,
            providersId: 2,
            name: "Azúcar",
            price: 25.50m,
            stock: 100,
            state: "disponible"
        );

        var updateCommand = new UpdateSupplyCommand(
            Id: 1, // Asumimos que UpdateSupplyCommand tiene un Id aunque no está en los archivos proporcionados
            ProvidersId: 4,
            Name: "Azúcar Refinada",
            Price: 28.75m,
            Stock: 75,
            State: "oferta"
        );

        // Act
        supply.Update(updateCommand);

        // Assert
        Assert.That(supply.ProvidersId, Is.EqualTo(updateCommand.ProvidersId));
        Assert.That(supply.Name, Is.EqualTo(updateCommand.Name.ToUpper()));
        Assert.That(supply.Price, Is.EqualTo(updateCommand.Price));
        Assert.That(supply.Stock, Is.EqualTo(updateCommand.Stock));
        Assert.That(supply.State, Is.EqualTo(updateCommand.State.ToUpper()));
    }

    
}