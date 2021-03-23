using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
   public class RecuringAirtimeandDataResponse
    {
        public string Id { get; set; }
        public string vendCode { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CIF { get; set; }
        public string PIN { get; set; }
        public string NickName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public int transactiontype { get; set; }
        public int DatapackageId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public string phoneNumber { get; set; }
        public int RecurringState { get; set; }

        public int Duration { get; set; }
        public int RecurringFrequency { get; set; }
    }
}
