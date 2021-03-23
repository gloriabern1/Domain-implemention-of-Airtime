using Domain.Airtime.Interface.Managers;
using PaymentSharedKernels.Extensions;
using PaymentSharedKernels.Models;
using PaymentSharedKernels.Models.Enums;
using SharedKernel.Infrastructure.Transfer.Infrastructure.Core;
using System;
using SharedKernel.Infrastructure.Transfer.Infrastructure.DTOs;
using Domain.Airtime.DTOs;
using AzureServiceBusUtil;
using Domain.Airtime.Base;
using Newtonsoft.Json;

namespace Domain.Airtime.AirtimeAggregate
{
    public class TransferFunds : Entity<Guid>
    {
        public string sourceAccountNumber { get; set; }
        public decimal amount { get; set; }
        public string narration { get; set; }
        public string destinationAccountNumber { get; set; }

        public string ChannelId { get; set; }
        public string transactionReference { get; set; }

        public int authType { get; set; }

        public string PIN { get; set; }

        public string OTP { get; set; }

        public string BiometricToken { get; set; }
        public string BiometricPolicy { get; set; }
        public string CIF { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsReversal { get; set; }

        public bool IsSettlement { get; set; }

        public bool IsSuccessful { get; set; }

        public string CodedString { get; set; }

        public string CorrelationalId { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionStan { get; set; }

        public string ClientKey { get; set; }
        public string ResponseTransferReference { get; set; }
        public bool IsRecurring { get; set; }
        public Guid RecurringTransactionId { get; set; }

        public TransactionTypes TransactionType { get; set; }
        public TransferauthOptions authOptions { get; set; }

        private ITransferService transferService;

        private ITransferManager transferManager;

        public TransferFunds() : base(Guid.NewGuid())
        {
        }

        public TransferFunds(ITransferService _transferService, ITransferManager _transferManager)
        {
            transferService = _transferService;
            transferManager = _transferManager;
        }

        public TransferFunds Create(Domain.Airtime.AirtimeAggregate.Airtime airtime, string _narration, string cif, string destinationAccount, string sourcAccount)
        {
            sourceAccountNumber = sourcAccount;
            amount = airtime.amount;
            narration = _narration;
            destinationAccountNumber = destinationAccount;
            ChannelId = airtime.ChannelId;
            transactionReference = airtime.transactionReference;
            TransactionType = TransactionTypes.Airtime;
            CorrelationalId = airtime.transactionReference;
            //CodedString = ServceBusManagementCodedEncryption.GetCodedString(sourceAccountNumber, destinationAccountNumber);
            CodedString = "SampleCodedstring";

            authOptions = new TransferauthOptions()
            {
                authenticationType = airtime.authenticationType,
                pin = airtime.PIN,
                otp = airtime.OTP,
                biometricPolicy = airtime.BiometricPolicy,
                biometricToken = airtime.BiometricToken,
                platformTransactionReference=airtime.transactionReference
            };

            authType = airtime.authenticationType;
            PIN = airtime.PIN;
            OTP = airtime.OTP;
            BiometricPolicy = airtime.BiometricPolicy;
            BiometricToken = airtime.BiometricToken;
            DateCreated = DateTime.Now;
            IsRecurring = airtime.IsRecurringService;
            RecurringTransactionId = airtime.RecurringTransactionId;
             CIF = cif;

            //log transfer transaction here
            return this;
        }

        public TransferFunds Create(Domain.Airtime.DataAggregate.Data data, string _narration, string cif, string destinationAccount, string sourcAccount)
        {
            sourceAccountNumber = sourcAccount;
            amount = data.amount;
            narration = _narration;
            destinationAccountNumber = destinationAccount;
            ChannelId = data.ChannelId;
            transactionReference = data.TransactionReference;
            TransactionType = TransactionTypes.Data;
            CorrelationalId = data.TransactionReference;
            //CodedString = ServceBusManagementCodedEncryption.GetCodedString(sourceAccountNumber, destinationAccountNumber);
            CodedString = "SampleCodedstring";
            authOptions = new TransferauthOptions()
            {
                authenticationType = data.authenticationType,
                pin = data.Pin,
                otp = data.OTP,
                biometricPolicy = data.BiometricPolicy,
                biometricToken = data.BiometricToken,
                platformTransactionReference = data.TransactionReference
            };

            authType = data.authenticationType;
            PIN = data.Pin;
            OTP = data.OTP;
            BiometricPolicy = data.BiometricPolicy;
            BiometricToken = data.BiometricToken;
            IsRecurring = data.IsRecurringServices;
            RecurringTransactionId = data.RecurringTransactionId;
            DateCreated = DateTime.Now;
            CIF = cif;

            //log transfer transaction here
            return this;
        }

        public TransferResponse<TransferResult> ExecuteTransfer()
        {
            var transferrequest = this.MapTo<TransferFundRequest>();

            var transferresponse = transferService.TransferFund(transferrequest).Result;

            ResponseTransferReference = transferresponse?.result?.PlatformTransactionReference;
            ResponseStatus = transferresponse?.result?.Status;
            ResponseMessage = transferresponse?.result?.Message;
            TransactionStan = transferresponse?.result?.TransactionStan;

            transferManager.UpdateTransferTransaction(this);

            return transferresponse;
        }

        public static void InitiateTransferReversal(string transDateTime, string TransactionStan, string TransferReference)
        {
           
            var serviceBusTransferRequest = new ServiceBusTransferReversalRequestDto()
            {
                IsReversal = true,
                OriginalStan = TransactionStan,
                PlatformReference = TransferReference,
                OriginalTransactionDateTime = transDateTime
            };

            Console.WriteLine($"{DateTime.Now}--reversalobject--{JsonConvert.SerializeObject(serviceBusTransferRequest)}");

            var serviceBusCall = new ServiceBusManagement(AirtimeDomainAppsettingManager.GetAirtimeTransferQueueConnectionString()).PushToQueue(serviceBusTransferRequest, AirtimeDomainAppsettingManager.GetAirtimeQueueName());
        }

        public static void VendorSettlement(TransferFunds transferFunds, string OriTxnDate, string suspenseAcct, string Narration)
        {
            var serviceBusTransferRequest = new SettlementRequest()
            {
                Amount = transferFunds.amount,
                ChannelId = transferFunds.ChannelId,
                ClientKey = transferFunds.ClientKey,
                CodedString = transferFunds.CodedString,
                DestinationAccountNumber = suspenseAcct,
                Narration = Narration,
                OriginalStan = transferFunds.TransactionStan,
                OriginalTransactionDateTime = OriTxnDate,
                PlatformReference = transferFunds.ResponseTransferReference,
                SourceAccountNumber = transferFunds.destinationAccountNumber,
                TransactionReference = transferFunds.transactionReference,
                TransactionType = transferFunds.TransactionType
            };

            var serviceBusCall =  new ServiceBusManagement(AirtimeDomainAppsettingManager.GetSettlementQueueConnectionString()).PushToQueue(serviceBusTransferRequest, AirtimeDomainAppsettingManager.GetSettlementQueueName());
        }
    }

    public class TransferauthOptions
    {
        public int authenticationType { get; set; }

        public string pin { get; set; }

        public string otp { get; set; }
        public string biometricToken { get; set; }
        public string biometricPolicy { get; set; }
        public string platformTransactionReference { get; set; }
    }
}