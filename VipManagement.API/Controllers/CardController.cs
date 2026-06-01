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
        // Llegeix el dbcontext
        private readonly VipManagementDbContext _context;



        public CardController(VipManagementDbContext context)
        {
            // es crea una copia del contexte actual
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var Cards = await _context.Cards.ToListAsync();
            return Ok(Cards);
        }



        [HttpPost("createCard")]
        public async Task<ActionResult> Create([FromBody]CreateCardInputDto dto)
        {
            var cmd = (CreateCardCommand)dto;

            var result = await Mediator.Send(cmd);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (Card card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
            

          
        }




       
    }
}
