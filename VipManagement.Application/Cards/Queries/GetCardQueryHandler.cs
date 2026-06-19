using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using VipManagement.Application.Cards.DTOs;
using VipManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace VipManagement.Application.Cards.Queries
{

    public class GetCardQueryHandler : IRequestHandler<GetCardQuery, CreateCardInputDto>
    {
        private readonly VipManagementDbContext _context;

        public GetCardQueryHandler(VipManagementDbContext context)
        {
            _context = context;
        }

        public async Task<CreateCardInputDto> Handle(
            GetCardQuery request,
            CancellationToken cancellationToken)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (card == null)
                throw new Exception("Card not found");

            return new CreateCardInputDto
            {
                Number = card.Number,
                Name = card.Name,
                ExpirationDate = card.ExpirationDate
            };
        }
    }

}
