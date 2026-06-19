using Kernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;


namespace Membership.Domain.MembershipPlans
{
    public class MembershipPlan : Entity<MemberShipPlanId>
    {

        public string MemberShipName { get; set; }

        public decimal Price { get; set; }

        public int DurationInMonths { get; set; }

        public bool IsActive { get; set; }


    }
}
