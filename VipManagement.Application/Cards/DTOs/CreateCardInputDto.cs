using System;
using System.Collections.Generic;
using System.Text;

namespace VipManagement.Application.Cards.DTOs
{

    public class CreateCardInputDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

}
