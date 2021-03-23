using Domain.Airtime.DataAggregate;
using Domain.Airtime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Airtime.Interface.Managers
{
    public interface IDatapackageManager
    {
       Task<DataPackages> GetDataPackage(int Id);


       Task<List<PackagesResponse>> GetallDataPackages();

        string[] GetPackageVendors(int packageid);
    }
}
