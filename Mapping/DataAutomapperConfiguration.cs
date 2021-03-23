using AutoMapper;
using Domain.Airtime.AirtimeAggregate;
using Domain.Airtime.DTOs;
using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Mapping
{
    public class DataAutomapperConfiguration
    {
    }

    public class DataThirdpartyAutoMapperConfiguration : Profile
    {
        public DataThirdpartyAutoMapperConfiguration()
        {
            CreateMap<RequestDataAuth, DataAggregate.Data>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x.DateTimeCreated, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.TransactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                        .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitAccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.PackageId, options => options.MapFrom(s => s.PackageCode))
                .ForMember(x => x.Pin, options => options.MapFrom(s => s.AuthOption.PIN))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId));
        }
    }


    public class DataPINAutomapperConfiguration : Profile
    {
        public DataPINAutomapperConfiguration()
        {
            CreateMap<DataRequestWithPin, Domain.Airtime.DataAggregate.Data>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x.DateTimeCreated, options => options.Ignore())
                       .ForMember(x => x.DataRequestStatus, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.TransactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                 .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitAccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.Pin, options => options.MapFrom(s => s.PIN))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId))
             .ForMember(x => x.PackageId, options => options.MapFrom(s => s.PackageCode));
        }
    }

    public class DataPINOTPAutomapperConfiguration : Profile
    {
        public DataPINOTPAutomapperConfiguration()
        {
            CreateMap<DataRequestPinOTP, Domain.Airtime.DataAggregate.Data>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x.DateTimeCreated, options => options.Ignore())
                      .ForMember(x => x.DataRequestStatus, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                        .ForMember(x => x.TransactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                 .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitAccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.Pin, options => options.MapFrom(s => s.PIN))
                 .ForMember(x => x.OTP, options => options.MapFrom(s => s.OTP))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId))
             .ForMember(x => x.PackageId, options => options.MapFrom(s => s.PackageCode));
        }
    }

    public class DataPINBiometricAutomapperConfiguration : Profile
    {
        public DataPINBiometricAutomapperConfiguration()
        {
            CreateMap<DataRequestPinBiometric, Domain.Airtime.DataAggregate.Data>()
                .ForMember(x => x.Vendors, options => options.Ignore())
                    .ForMember(x => x.DateTimeCreated, options => options.Ignore())
                 .ForMember(x => x.airtimeRequestStatus, options => options.Ignore())
                   .ForMember(x => x.DataRequestStatus, options => options.Ignore())
                        .ForMember(x => x.TransactionReference, options => options.Ignore())
                    .ForMember(x => x.vendorId, options => options.Ignore())
                 .ForMember(x => x.OTP, options => options.Ignore())
                        .ForMember(x => x.BiometricToken, options => options.Ignore())
                    .ForMember(x => x.BiometricPolicy, options => options.Ignore())
                 .ForMember(x => x.authenticationType, options => options.Ignore())
                        .ForMember(x => x.ResponseStatus, options => options.Ignore())
                 .ForMember(x => x.ResponseMessage, options => options.Ignore())
                 .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.DebitAccountNumber, options => options.MapFrom(s => s.AccountNumber))
                .ForMember(x => x.Pin, options => options.MapFrom(s => s.PIN))
                 .ForMember(x => x.BiometricPolicy, options => options.MapFrom(s => s.BiometricPolicy))
                 .ForMember(x => x.BiometricToken, options => options.MapFrom(s => s.BiometricToken))
              .ForMember(x => x.phoneNumber, options => options.MapFrom(s => s.PhoneNumber))
               .ForMember(x => x.ChannelId, options => options.MapFrom(s => s.ChannelId))
             .ForMember(x => x.PackageId, options => options.MapFrom(s => s.PackageCode));
        }
    }

    public class DataRecordAutoMapperConfiguration : Profile
    {
        public DataRecordAutoMapperConfiguration()
        {
            CreateMap<Domain.Airtime.DataAggregate.Data, Domain.Airtime.DataAggregate.Data>()

                .ForMember(x => x.Id, options => options.MapFrom(s => s.Id))
                .ForMember(x => x.ClientTransactionReference, options => options.MapFrom(s => s.ClientTransactionReference))
                  .ForMember(x => x.Vendors, options => options.Ignore());
        }
    }

    public class TransferFundAutoMapperConfiguration : Profile
    {
        public TransferFundAutoMapperConfiguration()
        {
            CreateMap<TransferFunds, ClientIntrabankRequest>();
        }
    }
}