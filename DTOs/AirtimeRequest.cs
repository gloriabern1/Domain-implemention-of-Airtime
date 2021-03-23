using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Airtime.DTOs
{

    public class AirtimeRequestWithPIN
    {

        public string ClientTransactionReference { get; set; }

        public string AccountNumber { get; set; }

        public string CIF { get; set; }

        public string Network { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Amount { get; set; } = default(decimal);

        public string PIN { get; set; }

        public string ChannelId { get; set; }

    }

    public class AirtimeRequestWithPINAndOTP
    {

        public string ClientTransactionReference { get; set; }

        public string AccountNumber { get; set; }

        public string Network { get; set; }
        public string CIF { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Amount { get; set; } = default(decimal);

        public string PIN { get; set; }

        public string ChannelId { get; set; }

        public string OTP { get; set; }
    }

    public class AirtimeRequestWithPINAndBiometric
    {

        public string ClientTransactionReference { get; set; }

        public string AccountNumber { get; set; }

        public string Network { get; set; }

        public string CIF { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Amount { get; set; } = default(decimal);

        public string PIN { get; set; }

        public string ChannelId { get; set; }

        public string BiometricToken { get; set; }

        public string BiometricPolicy { get; set; }
    }
}
