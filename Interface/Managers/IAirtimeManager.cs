using Domain.Airtime.AirtimeAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public interface IAirtimeManager 
    {

        Task InsertAirtime(Domain.Airtime.AirtimeAggregate.Airtime airtime);

        Task UpdateAirtime(Domain.Airtime.AirtimeAggregate.Airtime airtime);

        Task<Domain.Airtime.AirtimeAggregate.Airtime> GetAirtimebyCorrelationid(string CorrelationId);
        Task<List<AirtimeAggregate.Airtime>> AllPendingAirtimeTransaction(int count);
        Task<AirtimeAggregate.Airtime> GetAirtimebyTransrefAndChannelId(string transactionReference, string channelId);
    }
}
