using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.Room;
using SweetManagerWebService.Monitoring.Domain.Services.Room;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room;
using SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Room;

namespace SweetManagerWebService.Monitoring.Interfaces.REST
{
    [Route("api/[controller]")] // Base route: /api/rooms
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)] // Returns JSON responses 
    public class RoomsController
        (IRoomCommandService roomCommandService,
        IRoomQueryService roomQueryService) : ControllerBase
    {
        [HttpPost("create-room")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource resource)
        {
            var result = await roomCommandService.Handle
                (CreateRoomCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut("update-room-state")]
        public async Task<IActionResult> UpdateRoomState([FromBody] UpdateRoomStateResource resource)
        {
            var result = await roomCommandService.Handle
                (UpdateRoomStateCommandFromResourceAssembler
                .ToCommandFromResource(resource));

            if (result is false)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("get-all-rooms")]
        public async Task<IActionResult> AllRooms([FromQuery] int hotelId)
        {
            var rooms = await roomQueryService
                .Handle(new GetAllRoomsQuery(hotelId));

            var roomsResource = rooms.Select
                (RoomResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(roomsResource);
        }

        [HttpGet("get-room-by-id")]
        public async Task<IActionResult> RoomById([FromQuery] int  id)
        {
            var room = await roomQueryService
                .Handle(new GetRoomByIdQuery(id));

            if (room is null)
                return BadRequest();

            var roomResource = RoomResourceFromEntityAssembler
                .ToResourceFromEntity(room);

            return Ok(roomResource);
        }

        [HttpGet("get-room-by-type")]
        public async Task<IActionResult> RoomsByTypeRoomId([FromQuery] int typeRoomId)
        {
            var rooms = await roomQueryService
                .Handle(new GetRoomsByTypeRoomIdQuery
                (typeRoomId));

            var roomsResource = rooms.Select
                (RoomResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(roomsResource);
        }
    }
}
