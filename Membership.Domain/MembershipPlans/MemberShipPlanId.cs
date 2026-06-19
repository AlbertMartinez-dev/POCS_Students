using Kernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Membership.Domain.MembershipPlans
{
    public record MemberShipPlanId(int Value) : IValue<int>
    {

        public static implicit operator int(MemberShipPlanId self) => self.Value;
    }
}
