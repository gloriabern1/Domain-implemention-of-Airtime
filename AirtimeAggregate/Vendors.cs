using Domain.Airtime.Interface.Managers;
using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;

namespace Domain.Airtime.AirtimeAggregate
{
    public class Vendors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string suspenseAccount { get; set; }
        public bool Active { get; set; }
        public bool commissioned { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Date { get; set; }

        private IVendorsManager vendorsManager;

       

        public IVendorsManager VendorsManager;

        public Vendors()
        {

        }

        public Vendors(IVendorsManager _vendorsManager)
        {
            vendorsManager = _vendorsManager;
        }

        public Vendors getSingleVendorAgent(int vendorid)
        {
            return AllVendors().Find(x => x.Id == vendorid);
        }

        public Vendors getActiveVendorAgent()
        {
            return AllVendors().Find(x => x.Active == true && x.commissioned == true);
        }

        public List<Vendors> AllVendors()
        {
            return vendorsManager.GetAllVendorAgents();
        }

        public void UpdateVendorAgent(Vendors vendors)
        {
            vendorsManager.UpdateVendorAgents(vendors);
        }


    }
}
