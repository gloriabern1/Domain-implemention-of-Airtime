using Domain.Airtime.AirtimeAggregate;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public interface ITransferManager
    {

        Task InsertTransferTransaction(TransferFunds transferFunds);

        Task UpdateTransferTransaction(TransferFunds transferFunds);

        Task<TransferFunds> GetTransferbyCorrelationid(string CorrelationId);
    }
}
