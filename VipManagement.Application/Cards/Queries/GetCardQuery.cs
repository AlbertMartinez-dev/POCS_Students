using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using VipManagement.Application.Cards.DTOs;

namespace VipManagement.Application.Cards.Queries
{
    public class GetCardQuery : IRequest<CreateCardInputDto>
    {
        public int Id { get; set; }

        public GetCardQuery(int id)
        {
            this.Id = id;
        }

    }
}
