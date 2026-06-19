using Kernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Domain.Subscriptions
{
    public record SubscriptionId(int Value) : IValue<int>
    {

        public static implicit operator int(SubscriptionId self) => self.Value;
    }
}
