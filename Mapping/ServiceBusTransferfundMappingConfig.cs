using AutoMapper;
using Domain.Airtime.AirtimeAggregate;
using PaymentSharedKernels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Mapping
{


    public class ServiceBusTransferfundMappingConfig : Profile
    {
        public ServiceBusTransferfundMappingConfig()
        {
            CreateMap<TransferFunds, ClientIntrabankRequest>()
                .ForMember(x => x.OriginalStan, options => options.Ignore())
                    .ForMember(x => x.PlatformReference, options => options.Ignore())
                 .ForMember(x => x.OriginalTransactionDateTime, options => options.Ignore())

                .ForMember(x => x.CorrelationId, options => options.MapFrom(s => s.CorrelationalId));
           


        }


    }
}
