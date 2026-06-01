using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;
using VipManagement.Application.Cards.DTOs;

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



        [HttpPost]
        public async Task<ActionResult> Create(CreateCardInputDto dto)
        {
            var card = new Card(dto.Number, dto.Name, dto.ExpirationDate);

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return Ok(card);
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
