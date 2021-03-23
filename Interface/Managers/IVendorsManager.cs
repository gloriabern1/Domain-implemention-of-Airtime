
using Domain.Airtime.AirtimeAggregate;
using System.Collections.Generic;

namespace Domain.Airtime.Interface.Managers
{
    public  interface IVendorsManager 
    {

        List<Vendors> GetAllVendorAgents();
        Vendors GetsingleVendorAgents(int Id);
        void UpdateVendorAgents(Vendors vendoragents);
    }
}
