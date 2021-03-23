using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    public class AddBeneficiary
    {
        public string phoneNumber { get; set; }
        public string? vendCode { get; set; }
        public string CIF { get; set; }
        public string PIN { get; set; }
        public int transactiontype { get; set; }

        public string NickName { get; set; }
        public decimal? amount { get; set; }

        public int?  datapackageId { get; set; }

        //  public string fields { get; set; }
    }

 
}
