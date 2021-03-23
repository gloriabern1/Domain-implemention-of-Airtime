using CommonComponents.Helpers;
using Domain.Airtime.DTOs;
using Domain.Airtime.RecurringAirtimeAndDataAggregate;
using System;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public interface IRecuringAirtimeandDataManager
    {
        Task InsertRecuringAirtimeandData(RecurringAirtimeAndData model);

        Task<bool> UpdateRecuringAirtimeandData(RecurringAirtimeAndData model);

        Task<RecurringAirtimeAndData> GetRecurringAirtimeandDataInfo(Guid Id);
        bool CheckRecuringAirtimeandData(RecurringAirtimeAndData model);
        bool CheckRecuringAirtimeandDataOldBeneficiary(long BeneficiaryPaymentId);
        PagedList<RecuringAirtimeandDataResponse> GetRecuringAirtimeandData(string Cif, int pageNumber, int pagesize, int? transactionType);
        bool CheckDeleteRecuringAirtimeandData(string RecurringId);
        int DeleteRecuringAirtimeandData(string RecurringId);
        PagedList<RecuringAirtimeandDataResponse> GetRecurringwithParam(string Cif, int pageNumber, int pagesize, string param);
        bool UpdateRecurringNickname(string Id, string NewNickname);
    }
}
