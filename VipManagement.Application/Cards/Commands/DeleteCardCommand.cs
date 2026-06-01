using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Domain.Cards.Entities;

namespace VipManagement.Application.Cards.Commands
{
    // Aquest command s'envia amb MediaTr i  amb Unit, no espera dades concretes ja que no retornem res.
    public record DeleteCardCommand(Guid IdempotencyKey) : IRequest<Unit>
    {

        public CardId CardId { get; set; }

    }
}
