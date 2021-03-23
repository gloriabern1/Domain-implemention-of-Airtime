

using Domain.Airtime.DataAggregate;
using Domain.Airtime.DTOs;
using Domain.Airtime.Models;
using System;
using System.Collections.Generic;

namespace Domain.Airtime.Interface
{
    public interface IAirtimeService
    {

        string AgentName();
        AirtimeServiceResponse BuyAirtime(Domain.Airtime.AirtimeAggregate.Airtime airtime);

        AirtimeServiceResponse BuyData(Data data);
        AirtimeServiceResponse CheckPending(Guid TransactionId, string TransactionReference);
       
    }
}
