using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Application.Cards.Commands;

namespace VipManagement.Application.Cards.DTOs
{

    public class CreateCardInputDto
    {
        public Guid IdempotencyKey { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }




        public static explicit operator CreateCardCommand(CreateCardInputDto dto)
        {

            return new CreateCardCommand(dto.IdempotencyKey)
            {
                Number = dto.Number,
                Name = dto.Name,
                ExpirationDate = dto.ExpirationDate

            };


        }
    }

}
