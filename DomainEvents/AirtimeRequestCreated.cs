using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DomainEvents
{
    public class AirtimeRequestCreated : INotification
    {
        public AirtimeAggregate.Airtime Airtime { get; set; }
    }
}
