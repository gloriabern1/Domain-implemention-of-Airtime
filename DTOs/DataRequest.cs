using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Airtime.DTOs
{
   
    public class DataRequestWithPin
    {
       
        public string ClientTransactionReference { get; set; }
        public string AccountNumber { get; set; }

        public string PhoneNumber { get; set; }

        public int PackageCode { get; set; } 

        public string PIN { get; set; }

        public string ChannelId { get; set; }
        public string CIF { get; set; }

    }


    public class DataRequestPinOTP
    {
        public string ClientTransactionReference { get; set; }
        public string AccountNumber { get; set; }

        public string PhoneNumber { get; set; }

        public int PackageCode { get; set; }

        public string PIN { get; set; }

        public string OTP { get; set; }

        public string ChannelId { get; set; }
        public string CIF { get; set; }

    }

    public class DataRequestPinBiometric
    {
      
        public string ClientTransactionReference { get; set; }

        public string AccountNumber { get; set; }

        public string PhoneNumber { get; set; }

        public int PackageCode { get; set; }

        public string PIN { get; set; }

        public string BiometricToken { get; set; }

        public string BiometricPolicy { get; set; }

        public string ChannelId { get; set; }
        public string CIF { get; set; }

    }

    public class RequestDataAuth
    {

        public string ClientTransactionReference { get; set; }
        public string AccountNumber { get; set; }

        public string PhoneNumber { get; set; }

        public int PackageCode { get; set; }

        public ThirdPartyAuthOption AuthOption { get; set; }

        public string ChannelId { get; set; }
        public string CIF { get; set; }

    }

    public class CompleteDataRequest
    {
        public string ChannelId { get; set; }
        public string TransactionReference { get; set; }
        public string OTP { get; set; }
        public string CIF { get; set; }
    }

}
