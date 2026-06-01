using System;
using System.Collections.Generic;
using System.Text;
using Kernel.Domain.Primitives;

namespace VipManagement.Domain.Cards.Entities
{

    public class Card : Aggregate<CardId>
    {
        
        private Card()  { } 

        public Card(string number, string name, DateTime expirationDate)
        {
            Number = number;
            Name = name;
            ExpirationDate = expirationDate;
        }

        public string Number { get; private set; }
        public string Name { get; private set; }
        public DateTime ExpirationDate { get; private set; }

    }

}
