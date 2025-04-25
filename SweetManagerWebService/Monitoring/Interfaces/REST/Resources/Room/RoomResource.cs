namespace SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room
{
    public record RoomResource
        (int Id, int TypeRoomId,
        int HotelId, string RoomState);
}