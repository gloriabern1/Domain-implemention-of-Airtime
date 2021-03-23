using Domain.Airtime.DataAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public interface IDataManager
    {
        Task InsertData(Data data);

        Task UpdateData(Data data);

        Task<Data> GetDatabyCorrelationid(string CorrelationId);
        Task<Data> GetDataByTransRefAndChannelId(string transactionReference, string channelId);
    }
}
