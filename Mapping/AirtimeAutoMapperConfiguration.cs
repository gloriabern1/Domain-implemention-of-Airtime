using AutoMapper;
using Domain.Airtime.BeneficiaryAggregate;
using Domain.Airtime.DTOs;
using Domain.Airtime.RecurringAirtimeAndDataAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Mapping
{
    public class AirtimeAutoMapperConfiguration
    {
    }

    public class AirtimeThirdpartyAutoMapperConfiguration : Profile
    {
        public AirtimeThirdpartyAutoMapperConfiguration()
        {
            CreateMap<RequestAirtimeAuth, Domain.Airtime.AirtimeAggregate.Airtime>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x._airtimeTransactionDate, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.transactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                    .ForMember(x => x.TransferReference, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                        .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitaccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.vendCode, options => options.MapFrom(s => s.Network))
                .ForMember(x => x.PIN, options => options.MapFrom(s => s.AuthOption.PIN))
                .ForMember(x => x.amount, options => options.MapFrom(s => s.Amount))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId));


        }
    }
    public class AirtimePINAutoMapperConfiguration : Profile
    {
        public AirtimePINAutoMapperConfiguration()
        {
            CreateMap<AirtimeRequestWithPIN, Domain.Airtime.AirtimeAggregate.Airtime>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x._airtimeTransactionDate, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.transactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                    .ForMember(x => x.TransferReference, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                        .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitaccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.vendCode, options => options.MapFrom(s => s.Network))
                .ForMember(x => x.PIN, options => options.MapFrom(s => s.PIN))
                .ForMember(x => x.amount, options => options.MapFrom(s => s.Amount))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId));


        }
    }

    public class AirtimeOTPAutoMapperConfiguration : Profile
    {
        public AirtimeOTPAutoMapperConfiguration()
        {
            CreateMap<AirtimeRequestWithPINAndOTP, Domain.Airtime.AirtimeAggregate.Airtime>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x._airtimeTransactionDate, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.transactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                    .ForMember(x => x.TransferReference, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                        .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitaccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.vendCode, options => options.MapFrom(s => s.Network))
                .ForMember(x => x.PIN, options => options.MapFrom(s => s.PIN))
                .ForMember(x => x.OTP, options => options.MapFrom(s => s.OTP))
                .ForMember(x => x.amount, options => options.MapFrom(s => s.Amount))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId));


        }
    }

    public class AirtimeBiometricAutoMapperConfiguration : Profile
    {
        public AirtimeBiometricAutoMapperConfiguration()
        {
            CreateMap<AirtimeRequestWithPINAndBiometric, Domain.Airtime.AirtimeAggregate.Airtime>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x._airtimeTransactionDate, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.transactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                    .ForMember(x => x.TransferReference, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                        .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitaccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.vendCode, options => options.MapFrom(s => s.Network))
                .ForMember(x => x.PIN, options => options.MapFrom(s => s.PIN))
                .ForMember(x => x.BiometricPolicy, options => options.MapFrom(s => s.BiometricPolicy))
                 .ForMember(x => x.BiometricToken, options => options.MapFrom(s => s.BiometricToken))
                .ForMember(x => x.amount, options => options.MapFrom(s => s.Amount))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId));


        }
    }

    public class BeneficiaryAutomapperconfiguration : Profile
    {
        public BeneficiaryAutomapperconfiguration()
        {
            CreateMap<AddBeneficiary, Beneficiary>()
                .ForMember(x => x.DateCreated, options => options.Ignore())
                    .ForMember(x => x.Id, options => options.Ignore())
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.phoneNumber));


        }



    }

    public class RecurringAutomapperconfiguration : Profile
    {
        public RecurringAutomapperconfiguration()
        {
            CreateMap<AddRecurringAirtimeandData, RecurringAirtimeAndData>()
                .ForMember(x => x.DateCreated, options => options.Ignore())
                    .ForMember(x => x.Id, options => options.Ignore())
                        .ForMember(x => x.Message, options => options.Ignore())
                            .ForMember(x => x.Status, options => options.Ignore())
                                .ForMember(x => x.IsActivated, options => options.Ignore())
                                  .ForMember(x => x.EndDate, options => options.Ignore())
                                .ForMember(x => x.IsProcessed, options => options.Ignore())
                                  .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.phoneNumber))
                                    .ForMember(x => x.CustomerAccountNumber, options => options.MapFrom(s => s.CustomerAccountNumber)); ;

        }


    }

    public class AirtimeRecordAutoMapperConfiguration : Profile
    {
        public AirtimeRecordAutoMapperConfiguration()
        {
            CreateMap<Domain.Airtime.AirtimeAggregate.Airtime, Domain.Airtime.AirtimeAggregate.Airtime>()

                .ForMember(x => x.Id, options => options.MapFrom(s => s.Id))
                .ForMember(x => x.ClientTransactionReference, options => options.MapFrom(s => s.ClientTransactionReference))
                  .ForMember(x => x.Vendors, options => options.Ignore());

        }
    }
}



