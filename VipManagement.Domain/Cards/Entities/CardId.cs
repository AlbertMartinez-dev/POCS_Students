using System;
using System.Collections.Generic;
using System.Text;
using Kernel.Domain.Primitives;

namespace VipManagement.Domain.Cards.Entities
{
    public record CardId(int Value) : IValue<int>
    {


        public static implicit operator int(CardId self) => self.Value;


    }
}
