using AutoMapper;
using AzureServiceBusUtil;
using CSharpFunctionalExtensions;
using Domain.Airtime.AirtimeAggregate;
using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.Interface;
using Domain.Airtime.Interface.Managers;
using Domain.Airtime.Models;
using Domain.Airtime.RecurringAirtimeAndDataAggregate;
using Domain.Airtime.ValueObjects;
using PaymentSharedKernels.Extensions;
using PaymentSharedKernels.Models;
using PaymentSharedKernels.Models.Enums;
using SharedKernel.Infrastructure.Transfer.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel.Infrastructure.Transfer.Infrastructure.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using CommonComponents.Helpers;

namespace Domain.Airtime.DataAggregate
{
    public class Data : Entity<Guid>
    {
        public string ClientTransactionReference { get; set; }

        public string TransactionReference { get; set; }
        public NubanAccountNumber DebitAccountNumber { get; set; }
        public string Pin { get; set; }
        public string OTP { get; set; }
        public string BiometricToken { get; set; }
        public string BiometricPolicy { get; set; }
        public string CreditAccount { get; set; }
        public bool DataRequestStatus { get; set; }
        public int PackageId { get; set; }
        public string ChannelId { get; set; }
        public int vendorId { get; set; }
        public PhoneNumber phoneNumber { get; set; }
        public int authenticationType { get; set; }
        public bool airtimeRequestStatus { get; private set; }
        public decimal amount { get; set; } = default(decimal);
        public DateTime DateTimeCreated { get; set; }
        public string ResponseStatus { get; set; }
        public string Narration { get; set; }
        public string ResponseMessage { get; set; }

        public string TransactionStan { get; set; }
        public string TransferReference { get; set; }
        public string TransferNarration { get; set; }
        public int TrialCount { get; set; }

        public bool IsRecurringServices { get; set; }

        public DataPackages _DataPackages { get; set; }

        public Vendors Vendors { get; set; }

        public Guid RecurringTransactionId { get; set; }

        public string cif;

        public ITransferService transferService;

        public IEnumerable<IAirtimeService> airtimeServices;

        public ITransferManager transferManager;

        public Data() : base(Guid.NewGuid())
        {
        }

        public Data(IDatapackageManager datapackageManager,
            ITransferService _transferService,
            IVendorsManager vendorsManager,
        IEnumerable<IAirtimeService> _airtimeServices, ITransferManager _transferManager) : base(Guid.NewGuid())
        {
            transferService = _transferService;
            _DataPackages = new DataPackages(datapackageManager);
            Vendors = new Vendors(vendorsManager);
            airtimeServices = _airtimeServices;
            transferManager = _transferManager;
        }

        public Result<Data> Create(DataRequestWithPin dataRequestWithPin)
        {
            Mapper.Map(dataRequestWithPin, this);

            TransactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{dataRequestWithPin.PackageCode}";

            DateTimeCreated = DateTime.Now;

            authenticationType = (int)Authtype.PIN;
            cif = dataRequestWithPin.CIF;
            IsRecurringServices = false;
            return Result.Ok(this);
        }

        public Result<Data> Create(DataRequestPinOTP dataRequestPinOTP)
        {
            Mapper.Map(dataRequestPinOTP, this);

            TransactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{dataRequestPinOTP.PackageCode}";

            DateTimeCreated = DateTime.Now;

            authenticationType = (int)Authtype.PIN_OTP;

            IsRecurringServices = false;
            cif = dataRequestPinOTP.CIF;

            return Result.Ok(this);
        }

        public Result<Data> Create(DataRequestPinBiometric dataRequestPinBiometric)
        {
            Mapper.Map(dataRequestPinBiometric, this);

            TransactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{dataRequestPinBiometric.PackageCode}";

            DateTimeCreated = DateTime.Now;
            authenticationType = (int)Authtype.PIN_BIOMETRIC;
            IsRecurringServices = false;
            cif = dataRequestPinBiometric.CIF;

            return Result.Ok(this);
        }

        public Result<Data> Createthirdparty(RequestDataAuth requestDataAuth)
        {
            Mapper.Map(requestDataAuth, this);

            TransactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{requestDataAuth.PackageCode}";
            DateTimeCreated = DateTime.Now;
            authenticationType = (int)requestDataAuth.AuthOption.AuthenticationType;
            IsRecurringServices = false;
            cif = requestDataAuth.CIF;
            return Result.Ok(this);
        }


        public Result<Data> CreateRecurring(RecurringAirtimeAndData model, Guid recurringTransactionId)
        {
            this.amount = model.Amount;
            this.DebitAccountNumber = model.CustomerAccountNumber;
            this.phoneNumber = model.phoneNumber;
            this.PackageId = model.DatapackageId;
            this.ClientTransactionReference = model.Id.ToString();
            this.ChannelId = "ALAT";
            TransactionReference = $"Ref{DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss").Replace("/", "").Replace(":", "")}{GetRandomDigits.GetTwoDigit()}-{model.DatapackageId}";
            DateTimeCreated = DateTime.Now;
            IsRecurringServices = true;
            RecurringTransactionId = recurringTransactionId;

            return Result.Ok(this);
        }

        public async Task<Result<Data>> BuyData()
        {
            var dataPackage = _DataPackages.getDatapackage(this.PackageId);

            if (dataPackage != null)
            {
                string GlobalAccount = AppsettingsManager.GetConfig("GlobalAccount");
                // string Clientid = AppsettingsManager.GetConfig("AirtimeClientID");
                string Clientkey = AppsettingsManager.GetConfig("ClientKey");

                amount = dataPackage.amount;

                this.Narration = $"Data Recharge {this.ChannelId} {dataPackage.Biller.Name} {this.phoneNumber.Value}";

                var transferFunds = new TransferFunds(transferService, transferManager).Create(this, Narration, cif, GlobalAccount, DebitAccountNumber);

                var transferfundjson = new StringContent(JsonConvert.SerializeObject(transferFunds).ToString());

                await transferManager.InsertTransferTransaction(transferFunds);

                if (AirtimeDomainAppsettingManager.UseServiceBus())
                {
                    var serviceBusTransferRequest = transferFunds.MapTo<ClientIntrabankRequest>();
                    serviceBusTransferRequest.ClientKey = Clientkey;

                    var serviceBusCall = new ServiceBusManagement(AirtimeDomainAppsettingManager.GetAirtimeTransferQueueConnectionString()).PushToQueue(serviceBusTransferRequest, AirtimeDomainAppsettingManager.GetAirtimeQueueName());
                    ResponseStatus = PaymentSharedKernels.Models.Enums.TransactionStatus.Processing.ToString();
                    ResponseMessage = AirtimeValidationMessages.ConstantMessages.TransactionProcessing;
                }
                else
                {
                   var transferResponse = transferFunds.ExecuteTransfer();


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
                        if (transferResponse.result.Status == TransactionStatus.Success.ToString() || transferResponse.result.Status == PaymentSharedKernels.Models.Enums.TransactionStatus.Pending.ToString())
                        {
                            TransferReference = transferResponse?.result?.PlatformTransactionReference;
                            TransactionStan = transferResponse?.result?.TransactionStan;
                            TransferNarration = transferResponse?.result?.Narration;

                            var AirtimeserviceResponse = SwitchDataVendors(airtimeServices);
                            this.vendorId = int.Parse(AirtimeserviceResponse.ProccedByVendorId);

                            if (AirtimeserviceResponse.ResponseStatus == TransactionStatus.Success.ToString())
                            {
                                ResponseStatus = TransactionStatus.Success.ToString();
                                ResponseMessage = AirtimeValidationMessages.ConstantMessages.DataTransactionSuccessful;
                                DataRequestStatus = true;
                                transferFunds.IsSettlement = true;

                                var vendorDetails = this.Vendors.getSingleVendorAgent(int.Parse(AirtimeserviceResponse.ProccedByVendorId));

                                string SettlementNarration = $"Data Recharge {this.ChannelId} {this._DataPackages.Biller.Name} {this.phoneNumber.Value} {transferResponse.result.TransactionStan}";

                                TransferFunds.VendorSettlement(transferFunds, transferFunds.DateCreated.ToString(), vendorDetails.suspenseAccount, SettlementNarration);

                            }
                            else
                            {
                                ResponseStatus = TransactionStatus.Failed.ToString();
                                ResponseMessage = AirtimeserviceResponse?.ResponseMessage;
                                DataRequestStatus = false;
                                transferFunds.IsReversal = true;
                                TransferFunds.InitiateTransferReversal(transferFunds.DateCreated.ToString(), transferResponse.result.TransactionStan, transferResponse.result.PlatformTransactionReference);
                            }
                        }
                        else
                        {
                            ResponseStatus = transferResponse?.result?.Status?.ToString();
                            ResponseMessage = transferResponse?.result?.Message?.ToString();
                            DataRequestStatus = false;
                        }
                    }
                }
            }
            else
            {
                ResponseStatus = TransactionStatus.Failed.ToString();
                ResponseMessage = AirtimeValidationMessages.ErrorMessages.InvalidDataPackageId;
                vendorId = 0;

            }

            return Result.Ok(this);
        }

        public AirtimeServiceResponse SwitchDataVendors(IEnumerable<IAirtimeService> airtimeServices)
        {
            AirtimeServiceResponse airtimeServiceResponse = new AirtimeServiceResponse();

            var _Vendoragents = Vendors.AllVendors();
            string[] allActiveVendors = this._DataPackages.GetAllPackageVendors(this.PackageId);

            if (allActiveVendors.Length != 0)
            {
                for (int i = 0; i < allActiveVendors.Length; i++)
                {
                    Vendors NewActiveVendor = _Vendoragents.Where(x => x.Name.ToLower() == allActiveVendors[i].ToLower()).FirstOrDefault();
                    if (NewActiveVendor != null)
                    {
                        airtimeServiceResponse = airtimeServices
                            .FirstOrDefault(x => x.AgentName().ToLower() == allActiveVendors[i].ToLower())
                            .BuyData(this);

                        //airtimeServiceResponse = airtimeServices
                        //    .FirstOrDefault(x => x.AgentName().ToLower() == "CWG".ToLower())
                        //    .BuyData(this);
                        //airtimeServiceResponse.ProccedByVendorId = "1";

                        NewActiveVendor.DateUpdated = DateTime.Now;

                        if (airtimeServiceResponse?.ResponseStatus == TransactionStatus.Success.ToString())
                        {
                            vendorId = NewActiveVendor.Id;
                            airtimeServiceResponse.ProccedByVendorId = vendorId.ToString();
                            break;
                        }

                        if (airtimeServiceResponse?.ResponseStatus == TransactionStatus.Pending.ToString())
                        {
                            airtimeServiceResponse.ProccedByVendorId = NewActiveVendor.Id.ToString();
                            vendorId = NewActiveVendor.Id;
                            break;
                        }

                        if (airtimeServiceResponse?.ResponseStatus == TransactionStatus.Failed.ToString())
                        {
                            airtimeServiceResponse.ProccedByVendorId = NewActiveVendor.Id.ToString();
                            vendorId = NewActiveVendor.Id;
                        }

                    }
                    else
                    {
                        airtimeServiceResponse.ResponseStatus = TransactionStatus.Failed.ToString();
                        airtimeServiceResponse.ResponseMessage = AirtimeValidationMessages.ErrorMessages.InvalidDataPackageId;
                        airtimeServiceResponse.ProccedByVendorId = "0";
                    }
                }
            }
            else
            {
                airtimeServiceResponse.ResponseStatus = TransactionStatus.Failed.ToString();
                airtimeServiceResponse.ResponseMessage = AirtimeValidationMessages.ErrorMessages.InvalidDataPackageId;
                airtimeServiceResponse.ProccedByVendorId = "0";
            }

            return airtimeServiceResponse;
        }


        public List<PackagesResponse> AllDataPlans()
        {
            return _DataPackages.AllDataPackages();
        }
    }
}