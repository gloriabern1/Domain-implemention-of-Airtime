using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    public class UpdateBeneficiary
    {
        public string Id { get; set; }
        public string phoneNumber { get; set; }

        public string CIF { get; set; }
        public int transactiontype { get; set; }

        public string? Vendcode { get; set; }
        public string NickName { get; set; }
        public decimal? amount { get; set; }

        public int? datapackageId { get; set; }
    }
}
