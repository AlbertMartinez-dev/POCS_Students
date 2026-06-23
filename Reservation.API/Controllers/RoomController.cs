using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Rooms.Commands;
using Reservation.Application.Rooms.DTOs;
using Reservation.Application.Rooms.Queries;

namespace Reservation.API.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(
            [FromBody] CreateRoomInputDTO input,
            CancellationToken cancellationToken)
        {
            var command = new CreateRoomCommand(Guid.NewGuid())
            {
                RoomtypeName = input.RoomtypeName,
                RoomtypeDescription = input.RoomtypeDescription,
                FloorNumber = input.FloorNumber,
                RoomNumber = input.RoomNumber
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoomById(
                    int id,
                    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetRoomByIdQuery(id),
                cancellationToken);

            if (result.IsError)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

    }
}