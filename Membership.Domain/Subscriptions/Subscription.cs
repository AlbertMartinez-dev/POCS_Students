using Kernel.Domain.Primitives;
using Membership.Domain.Members.Entities;
using Membership.Domain.MembershipPlans;
using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Domain.Subscriptions
{
    public class Subscription : Entity <SubscriptionId>
    {

        public MemberId MemberId { get; set; }

        public MemberShipPlanId ShipPlanId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }



    }
}
