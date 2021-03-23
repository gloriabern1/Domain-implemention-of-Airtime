using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    public class RequestBeneficiary
    {
        const int MaxPageSize = 20;
        public string CIF { get; set; }
        public int? transactiontype { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _PageSize = 20;

      //  public string fields { get; set; }
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }

    public class FilterBeneficiary
    {
        const int MaxPageSize = 20;
        public string CIF { get; set; }
        public string Filterparam { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _PageSize = 20;

        //  public string fields { get; set; }
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}
