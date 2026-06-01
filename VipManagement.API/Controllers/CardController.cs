using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;
using VipManagement.Application.Cards.DTOs;
using VipManagement.Application.Cards.Commands;
using MediatR;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cards = await _context.Cards.ToListAsync();
            return Ok(cards);
        }

        [HttpPost("createCard")]
        public async Task<ActionResult> Create([FromBody] CreateCardInputDto dto)
        {
            var cmd = (CreateCardCommand)dto;

            var result = await _mediator.Send(cmd);

            return Ok(result);
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