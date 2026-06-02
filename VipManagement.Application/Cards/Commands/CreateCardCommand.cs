using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;

namespace VipManagement.Application.Cards.Commands
{





    public record CreateCardCommand(Guid IdempotencyKey): IRequest<ErrorOr<int>>
    {
       

        public string Number { get;set; }
        public string Name { get;  set; }
        public DateTime ExpirationDate { get;set; }


    }
}
