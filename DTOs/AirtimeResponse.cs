using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    public class AirtimeServiceResponse
    {

        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }

        public string ProccedByVendorId { get; set; }

    }

    public class Response
    {

        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }

        public string TransactionReference { get; set; }
    }
}
