using Domain.Airtime.Interface.Managers;
using Domain.Airtime.Models;
using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;

namespace Domain.Airtime.DataAggregate
{
    public  class DataPackages 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal amount { get; set; }
        public string DataPlan { get; set; }
        public string Validity_Period { get; set; }

        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int billerid { get; set; }

        public virtual Biller Biller { get; set; }

      //  public List<PackagesResponse> GetAllDataPlans => AllDataPackages();

       

        IDatapackageManager datapackageManager { get; set; }

        public DataPackages()
        {

        }
        public DataPackages(IDatapackageManager _datapackageManager)
        {
            datapackageManager = _datapackageManager;
        }

        public DataPackages getDatapackage(int id)
        {
            return datapackageManager.GetDataPackage(id).Result;
        }
    
        public List<PackagesResponse> AllDataPackages()
        {
            return datapackageManager.GetallDataPackages().Result;
        }

        public string[] GetAllPackageVendors(int id)
        {
            return datapackageManager.GetPackageVendors(id);
        }
    }
}
