using AutoMapper;
using AzureServiceBusUtil;
using CommonComponents.Helpers;
using CSharpFunctionalExtensions;
using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.Interface;
using Domain.Airtime.Interface.Managers;
using Domain.Airtime.Models;
using Domain.Airtime.RecurringAirtimeAndDataAggregate;
using Domain.Airtime.ValueObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentSharedKernels.Extensions;
using PaymentSharedKernels.Models;
using PaymentSharedKernels.Models.Enums;
using SharedKernel.Infrastructure.Transfer.Infrastructure.Core;
using SharedKernel.Infrastructure.Transfer.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Airtime.AirtimeAggregate
{
    public class Airtime : Entity<Guid>
    {
        public DateTime _airtimeTransactionDate { get; set; }
        public string ClientTransactionReference { get; set; }
        public string transactionReference { get; set; }
        public NubanAccountNumber DebitaccountNumber { get; set; }
        public string vendCode { get; set; }
        public string vendorId { get; set; }
        public PhoneNumber phoneNumber { get; set; }
        public string OTP { get; set; }
        public string BiometricToken { get; set; }
        public string BiometricPolicy { get; set; }
        public int authenticationType { get; set; }
        public bool airtimeRequestStatus { get; set; }
        public decimal amount { get; set; } = default(decimal);
        public string PIN { get; set; }
        public string Narration { get; set; }
        public string ResponseStatus { get; set; }
        public string TransactionStan { get; set; }
        public string ChannelId { get; set; }
        public string TransferReference { get; set; }
        public string ResponseMessage { get; set; }
        public string TransferNarration { get; set; }
        public bool IsRecurringService { get; set; }
        public int TrialCount { get; set; }
        public string cif;
        public Guid RecurringTransactionId { get; set; }

        public Airtime() : base(Guid.NewGuid())
        {
        }

        public IEnumerable<IAirtimeService> airtimeServices;
        public ITransferService transferService;
        public ITransferManager transferManager;
        public Vendors Vendors;
        public ILogger<Airtime> _logger;
        public Airtime(IVendorsManager _vendorsManager,
            IEnumerable<IAirtimeService> _airtimeServices,
            ITransferService _transferService,
            ITransferManager _transferManager, ILogger<Airtime> Alogger) : base(Guid.NewGuid())
        {
            Vendors = new Vendors(_vendorsManager);
            airtimeServices = _airtimeServices;
            transferService = _transferService;
            transferManager = _transferManager;
            _logger = Alogger;
        }

        public Result<Airtime> Create(AirtimeRequestWithPIN airtimeRequestWithPIN)
        {
            Mapper.Map(airtimeRequestWithPIN, this);

            transactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{airtimeRequestWithPIN.Network}-{airtimeRequestWithPIN.Amount.ToString()}";
            _airtimeTransactionDate = DateTime.Now;
            authenticationType = (int)Authtype.PIN;
            IsRecurringService = false;
            cif = airtimeRequestWithPIN.CIF;
            return Result.Ok(this);
        }

        public Result<Airtime> Createthirdparty(RequestAirtimeAuth requestAirtimeAuth)
        {
            Mapper.Map(requestAirtimeAuth, this);

            transactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{requestAirtimeAuth.Network}-{requestAirtimeAuth.Amount.ToString()}";
            _airtimeTransactionDate = DateTime.Now;
            authenticationType = (int)requestAirtimeAuth.AuthOption.AuthenticationType;
            IsRecurringService = false;
            cif = requestAirtimeAuth.CIF;
            return Result.Ok(this);
        }

        public Result<Airtime> Create(AirtimeRequestWithPINAndOTP airtimeRequestWithPINAndOTP)
        {
            Mapper.Map(airtimeRequestWithPINAndOTP, this);

            transactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{airtimeRequestWithPINAndOTP.Network}-{airtimeRequestWithPINAndOTP.Amount.ToString()}";

            _airtimeTransactionDate = DateTime.Now;
            authenticationType = (int)Authtype.PIN_OTP;
            IsRecurringService = false;
            cif = airtimeRequestWithPINAndOTP.CIF;
            return Result.Ok(this);
        }

        public Result<Airtime> Create(AirtimeRequestWithPINAndBiometric requestWithPINAndBiometric)
        {
            Mapper.Map(requestWithPINAndBiometric, this);

            transactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{requestWithPINAndBiometric.Network}-{requestWithPINAndBiometric.Amount.ToString()}";

            _airtimeTransactionDate = DateTime.Now;
            authenticationType = (int)Authtype.PIN_BIOMETRIC;
            IsRecurringService = false;
            cif = requestWithPINAndBiometric.CIF;
            return Result.Ok(this);
        }

        public Result<Airtime> CreateRecurring(RecurringAirtimeAndData model, Guid RecurringTransactionId)
        {
            this.amount = model.Amount;
            this.DebitaccountNumber = model.CustomerAccountNumber;
            this.phoneNumber = model.phoneNumber;
            this.vendCode = model.vendCode;
            this.ClientTransactionReference = model.Id.ToString();
            this.ChannelId = "ALAT";
            transactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{model.vendCode}-{model.Amount.ToString()}";
            _airtimeTransactionDate = DateTime.Now;
            this.IsRecurringService = true;
            this.RecurringTransactionId = RecurringTransactionId;
            return Result.Ok(this);
        }

        public async Task<Result<Airtime>> BuyAirtime()
        {
            _logger.LogInformation($"Airtime log is working");

            string GlobalAccount = AppsettingsManager.GetConfig("GlobalAccount");
            // string Clientid = AppsettingsManager.GetConfig("ClientId");
            string Clientkey = AppsettingsManager.GetConfig("ClientKey");

            this.Narration = $"Airtime Recharge {this.ChannelId} {this.vendCode} {this.phoneNumber.Value}";

            var transferFunds = new TransferFunds(transferService, transferManager).Create(this, Narration, cif, GlobalAccount, DebitaccountNumber);

            await transferManager.InsertTransferTransaction(transferFunds);

            if (AirtimeDomainAppsettingManager.UseServiceBus())
            {
                var serviceBusTransferRequest = transferFunds.MapTo<ClientIntrabankRequest>();
                serviceBusTransferRequest.ClientKey = Clientkey;

                var serviceBusCall = new ServiceBusManagement(AirtimeDomainAppsettingManager.GetAirtimeTransferQueueConnectionString()).PushToQueue(serviceBusTransferRequest, AirtimeDomainAppsettingManager.GetAirtimeQueueName());
                ResponseStatus = TransactionStatus.Processing.ToString();
                ResponseMessage = AirtimeValidationMessages.ConstantMessages.TransactionProcessing;
            }
            else
            {
                var transferResponse = transferFunds.ExecuteTransfer();

                //var transferResponse = new TransferResponse<TransferResult>()
                //{
                //    result = new TransferResult()
                //};

                //transferResponse.hasError = false;
                //transferResponse.result.Status = TransactionStatus.Success.ToString();

                if (transferResponse == null)
                {
                    ResponseStatus = TransactionStatus.Failed.ToString();
                    ResponseMessage = AirtimeValidationMessages.ErrorMessages.TransferError;
                }
                else if (transferResponse.hasError == true)
                {
                    ResponseStatus = TransactionStatus.Failed.ToString();

                    if (transferResponse?.errorMessage?.ToString() == null)
                    {
                        string response = "";
                        foreach (var err in transferResponse.errorMessages)
                        {
                            response += err;
                        }
                        ResponseMessage = response;
                    }
                    else
                    {
                        ResponseMessage = transferResponse.errorMessage.ToString();
                    }
                }
                else
                {
                    if (transferResponse.result.Status == TransactionStatus.Success.ToString() || transferResponse.result.Status == TransactionStatus.Pending.ToString())
                    {
                        TransferReference = transferResponse?.result?.PlatformTransactionReference;
                        TransactionStan = transferResponse?.result?.TransactionStan;
                        TransferNarration = transferResponse?.result?.Narration;

                        var AirtimeserviceResponse = SwitchAirtimeVendors(airtimeServices);

                        this.vendorId = AirtimeserviceResponse.ProccedByVendorId;

                        if (AirtimeserviceResponse.ResponseStatus == TransactionStatus.Success.ToString())
                        {
                            ResponseStatus = TransactionStatus.Success.ToString();
                            ResponseMessage = AirtimeValidationMessages.ConstantMessages.AirtimeTransactionSuccessful;
                            airtimeRequestStatus = true;
                            transferFunds.IsSettlement = true;
                            var vendorDetails = this.Vendors.getSingleVendorAgent(int.Parse(AirtimeserviceResponse.ProccedByVendorId));

                            string SettlementNarration = $"Airtime Recharge {this.ChannelId} {this.vendCode} {this.phoneNumber.Value} {transferResponse.result.TransactionStan}";

                            TransferFunds.VendorSettlement(transferFunds, transferFunds.DateCreated.ToString(), vendorDetails.suspenseAccount, SettlementNarration);
                        }
                        else if (AirtimeserviceResponse.ResponseStatus == TransactionStatus.Pending.ToString())
                        {
                            ResponseStatus = TransactionStatus.Pending.ToString();
                            ResponseMessage = AirtimeValidationMessages.ConstantMessages.TransactionProcessing;

                        }
                        else
                        {
                            ResponseStatus = TransactionStatus.Failed.ToString();
                            ResponseMessage = AirtimeserviceResponse?.ResponseMessage;
                            airtimeRequestStatus = false;
                            transferFunds.IsReversal = true;
                            TransferFunds.InitiateTransferReversal(transferFunds.DateCreated.ToString(), transferResponse.result.TransactionStan, transferResponse.result.PlatformTransactionReference);
                        }
                    }
                    else
                    {
                        ResponseStatus = transferResponse?.result?.Status.ToString();
                        ResponseMessage = transferResponse?.result?.Message?.ToString();
                        airtimeRequestStatus = false;
                    }
                }
            }
            return Result.Ok(this);
        }

        public AirtimeServiceResponse SwitchAirtimeVendors(IEnumerable<IAirtimeService> airtimeServices)
        {
            AirtimeServiceResponse airtimeServiceResponse = new AirtimeServiceResponse();

            try
            {
                _logger.LogInformation($"Inside switch airtime vendors method");
                var _Vendoragents = this.Vendors.AllVendors();


                if (_Vendoragents?.Count > 0)
                {
                    _logger.LogInformation($"Vendor agents is greater than zero");
                    var ActiveVendor = _Vendoragents.FirstOrDefault(X => X.commissioned == true && X.Active);
                    if (ActiveVendor == null)
                        ActiveVendor = _Vendoragents[0];

                    _logger.LogInformation($"about to call first vendor");

                    //airtimeServiceResponse = airtimeServices
                    //.FirstOrDefault(x => x.AgentName().ToLower() == "CWG".ToLower())
                    //.BuyAirtime(this);

                    airtimeServiceResponse = airtimeServices
                       .FirstOrDefault(x => x.AgentName().ToLower() == ActiveVendor.Name.ToLower())
                       .BuyAirtime(this);
                    //airtimeServiceResponse.ProccedByVendorId = "1";

                    airtimeServiceResponse.ProccedByVendorId = ActiveVendor.Id.ToString();

                    if (airtimeServiceResponse.ResponseStatus == TransactionStatus.Failed.ToString())
                    {

                        int vendorcount = _Vendoragents.Count() - 1;

                        List<Vendors> Othervendors = _Vendoragents.ToList<Vendors>();
                        Othervendors.Remove(ActiveVendor);

                        var vendorsarray = Othervendors.ToArray<Vendors>();

                        ActiveVendor.Active = false;
                        ActiveVendor.DateUpdated = DateTime.Now;

                        this.Vendors.UpdateVendorAgent(ActiveVendor);

                        for (int i = 0; i < vendorcount; i++)
                        {
                            Vendors NewActiveVendor = (Vendors)vendorsarray[i];

                            airtimeServiceResponse = airtimeServices
                           .FirstOrDefault(x => x.AgentName().ToLower() == vendorsarray[i].Name.ToLower() &&
                           x.AgentName().ToLower() != ActiveVendor.Name.ToLower())
                           .BuyAirtime(this);

                            if (airtimeServiceResponse.ResponseStatus == TransactionStatus.Success.ToString())
                            {
                                NewActiveVendor.Active = true;
                                NewActiveVendor.DateUpdated = DateTime.Now;
                                this.Vendors.UpdateVendorAgent(NewActiveVendor);
                                airtimeServiceResponse.ProccedByVendorId = NewActiveVendor.Id.ToString();
                                break;
                            }

                            if (airtimeServiceResponse.ResponseStatus == TransactionStatus.Pending.ToString())
                            {
                                NewActiveVendor.Active = false;
                                NewActiveVendor.DateUpdated = DateTime.Now;
                                if (_Vendoragents?.Count > 1)
                                {
                                    var unusedactive = _Vendoragents.FirstOrDefault(x => x.Id != NewActiveVendor.Id);
                                    if (unusedactive != null)
                                    {
                                        unusedactive.Active = true;
                                        unusedactive.DateUpdated = DateTime.Now;
                                        this.Vendors.UpdateVendorAgent(unusedactive);
                                    }
                                
                                }
                                this.Vendors.UpdateVendorAgent(NewActiveVendor);
                                airtimeServiceResponse.ProccedByVendorId = NewActiveVendor.Id.ToString();
                                break;
                            }

                            if (airtimeServiceResponse.ResponseStatus == TransactionStatus.Failed.ToString())
                            {
                                airtimeServiceResponse.ProccedByVendorId = NewActiveVendor.Id.ToString();
                                if (_Vendoragents?.Count > 1 && i == vendorcount - 1)
                                {
                                    var unusedactive = _Vendoragents.FirstOrDefault(x => x.Id != NewActiveVendor.Id);
                                    if (unusedactive != null)
                                    {
                                        unusedactive.Active = true;
                                        unusedactive.DateUpdated = DateTime.Now;
                                        this.Vendors.UpdateVendorAgent(unusedactive);
                                    }

                                }
                            }
                        }
                    }

                    if (airtimeServiceResponse.ResponseStatus == TransactionStatus.Pending.ToString())
                    {
                        ActiveVendor.Active = false;
                        ActiveVendor.DateUpdated = DateTime.Now;

                        if (_Vendoragents?.Count > 1)
                        {
                            var unusedactive = _Vendoragents.FirstOrDefault(x => x.Id != ActiveVendor.Id);
                            if (unusedactive != null)
                            {
                                unusedactive.Active = true;
                                unusedactive.DateUpdated = DateTime.Now;
                                this.Vendors.UpdateVendorAgent(unusedactive);
                            }

                        }

                        this.Vendors.UpdateVendorAgent(ActiveVendor);
                    }

                    return airtimeServiceResponse;
                }
                else
                {
                    _logger.LogInformation($"No vendor agent found");
                    airtimeServiceResponse.ResponseMessage = AirtimeValidationMessages.ErrorMessages.TransactionFailed;
                    airtimeServiceResponse.ResponseStatus = TransactionStatus.Failed.ToString();
                    return airtimeServiceResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"error when inside switching to vendor --{this?.phoneNumber?.Value}--: {ex?.Message}");
                _logger.LogInformation($"error when inside switching to vendor --{this?.phoneNumber?.Value}--: {ex?.InnerException?.Message}");
                _logger.LogInformation($"error when inside switching to vendor stack trace --{this?.phoneNumber?.Value}--: {ex?.StackTrace}");
                airtimeServiceResponse.ResponseMessage = AirtimeValidationMessages.ErrorMessages.TransactionFailed;
                airtimeServiceResponse.ResponseStatus = TransactionStatus.Failed.ToString();
                return airtimeServiceResponse;
            }



        }


    }
}