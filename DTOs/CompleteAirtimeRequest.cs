using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
   public class CompleteAirtimeRequest
    {
        public string ChannelId { get; set; }
        public string TransactionReference { get; set; }
        public string OTP { get; set; }
        public string CIF { get; set; }
    }
}
