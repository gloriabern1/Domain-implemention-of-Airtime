using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    public class RequestAirtimeAuth
    {
        public string ClientTransactionReference { get; set; }

        public string AccountNumber { get; set; }

        public string CIF { get; set; }

        public string Network { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Amount { get; set; } = default(decimal);

        public ThirdPartyAuthOption AuthOption { get; set; }

        public string ChannelId { get; set; }

    }
}
