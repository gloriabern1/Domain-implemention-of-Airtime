using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Models
{
    public class PackagesResponse
    {
        public int Id { get; set; }
        public string NetworkProvider { get; set; }

        public IEnumerable<Packages> dataPackages { get; set;}
    }
    public class Packages
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal amount { get; set; }
        public string DataPlan { get; set; }
        public string Validity_Period { get; set; }

        public string Description { get; set; }
    }

}
