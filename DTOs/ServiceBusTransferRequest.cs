using PaymentSharedKernels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.DTOs
{
    //public class ServiceBusAirtimeTransferRequest
    //{
    //    public string sourceAccountNumber { get; set; }
    //    public decimal amount { get; set; }
    //    public string narration { get; set; }
    //    public string destinationAccountNumber { get; set; }

    //    public string clientId { get; set; }
    //    public string transactionReference { get; set; }

    //    public TransactionTypes transactionTypes { get; set; }
    //    public TransferauthOptions authOptions { get; set; }

    //    public string ClientKey { get; set; }
    //    public string CodedString { get; set; }
    //    public string CorrelationalId { get; set; }
    //    public bool IsRecurring { get; set; }
    //    public Guid RecurringTransactionId { get; set; }

    //}

    public class TransferauthOptions
    {
        public int authenticationType { get; set; }

        public string pin { get; set; }

        public string otp { get; set; }
        public string biometricToken { get; set; }
        public string biometricPolicy { get; set; }
    }

    public class ServiceBusTransferReversalRequestDto
    {
        public bool IsReversal { get; set; }
        public string OriginalStan { get; set; }
        public string PlatformReference { get; set; }
        public string OriginalTransactionDateTime { get; set; }
    }
}