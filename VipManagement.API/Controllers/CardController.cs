using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;
using VipManagement.Application.Cards.DTOs;
using VipManagement.Application.Cards.Commands;
using MediatR;
using ErrorOr;
using Microsoft.IdentityModel.Tokens.Experimental;
namespace VipManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly VipManagementDbContext _context;
        private readonly IMediator _mediator;

        public CardController(VipManagementDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet("GetCards")]
        public async Task<IActionResult> Get()
        {
            var cards = await _context.Cards.ToListAsync();
            return Ok(cards);
        }


        [HttpGet("GetCards")]
        public async Task<IActionResult> Get()
        {
            var cards = await _context.Cards.ToListAsync();
            return Ok(cards);
        }



        [HttpPost("createCard")]
        public async Task<IActionResult> Create([FromBody] CreateCardInputDto dto)
        {
            var cmd = (CreateCardCommand)dto;

            var result = await _mediator.Send(cmd);

            return result.Match<IActionResult>(
                success => Ok(success),
                errors =>
                {
                    if (errors.Any(error => error.Type == ErrorType.Validation))
                    {
                        return BadRequest(errors);
                    }

                    if (errors.Any(error => error.Type == ErrorType.NotFound))
                    {
                        return NotFound(errors);
                    }

                    if (errors.Any(error => error.Type == ErrorType.Conflict))
                    {
                        return Conflict(errors);
                    }

                    if (errors.Any(error => error.Type == ErrorType.Unexpected))
                    {
                        return Problem(
                            detail: errors[0].Description,
                            statusCode: StatusCodes.Status500InternalServerError
                        );
                    }

                    return BadRequest(errors);
                }
            );
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(x => x.Id == new CardId(id));

            if (card is null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}