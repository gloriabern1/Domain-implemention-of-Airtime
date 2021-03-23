using CommonComponents.Helpers;
using Domain.Airtime.BeneficiaryAggregate;
using Domain.Airtime.DTOs;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public  interface IBeneficiaryManager
    {
        Task InsertBeneficiary(Beneficiary beneficiary);

        bool UpdateBeneficiary(UpdateBeneficiary beneficiary);

        bool Checkbeneficiary(Beneficiary beneficiary);
        PagedList<BeneficiaryResponse> GetBeneficiaries(string Cif, int pageNumber, int pagesize, int? transactionType);
        bool CheckDeletebeneficiary(string beneficiaryId);
        int Deletebeneficiary(string beneficiaryId);
        bool CheckbeneficiaryPhoneNumber(UpdateBeneficiary updateBeneficiary);
        PagedList<BeneficiaryResponse> GetBeneficiarieswithParam(string Cif, int pageNumber, int pagesize, string param);
        bool CheckOLDbeneficiary(long OldbeneficiaryId);
    }
}
