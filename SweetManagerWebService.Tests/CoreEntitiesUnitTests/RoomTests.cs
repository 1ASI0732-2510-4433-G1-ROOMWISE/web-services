using NUnit.Framework;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class RoomTests
{
    [Test]
    public void Room_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var typeRoomId = 1;
        var hotelId = 2;
        var roomState = ERoomState.LIBRE;

        // Act
        var room = new Room(typeRoomId, hotelId, roomState);

        // Assert
        Assert.That(room.TypesRoomsId, Is.EqualTo(typeRoomId));
        Assert.That(room.HotelsId, Is.EqualTo(hotelId));
        Assert.That(room.State, Is.EqualTo(roomState.ToString()));
        Assert.That(room.Bookings, Is.Not.Null);
        Assert.That(room.Bookings, Is.Empty);
    }

    [Test]
    public void Room_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateRoomCommand(
            TypeRoomId: 3,
            HotelId: 4,
            RoomState: ERoomState.OCUPADO
        );

        // Act
        var room = new Room(command);

        // Assert
        Assert.That(room.TypesRoomsId, Is.EqualTo(command.TypeRoomId));
        Assert.That(room.HotelsId, Is.EqualTo(command.HotelId));
        Assert.That(room.State, Is.EqualTo(command.RoomState.ToString()));
    }

    [Test]
    public void Room_State_ShouldFailWithIncorrectValue()
    {
        // Arrange
        var typeRoomId = 5;
        var hotelId = 6;
        var roomState = ERoomState.OCUPADO;

        // Act
        var room = new Room(typeRoomId, hotelId, roomState);

        // Assert - Esta prueba fallará intencionalmente
        Assert.That(room.State, Is.EqualTo("LIBRE"));
        // La prueba fallará porque el estado real es "OCUPADO"
    }
}