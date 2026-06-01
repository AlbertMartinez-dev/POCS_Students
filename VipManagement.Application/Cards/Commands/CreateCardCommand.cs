using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VipManagement.Application.Cards.Commands
{





    public record CreateCardCommand(Guid IdempotencyKey): IRequest<int>
    {
       

        public string Number { get;set; }
        public string Name { get;  set; }
        public DateTime ExpirationDate { get;set; }


    }
}
