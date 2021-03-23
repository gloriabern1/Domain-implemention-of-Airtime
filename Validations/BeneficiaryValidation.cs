using Domain.Airtime.DTOs;
using Domain.Airtime.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
   public class BeneficiaryValidation : AbstractValidator<AddBeneficiary>
    {
        public BeneficiaryValidation()
        {
            RuleFor(x => x.phoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.NickName).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.phoneNumber).Must(PhoneNumber.PhonNumberLength).WithMessage("Invalid PhoneNumber");
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.transactiontype).GreaterThan(0);
            RuleFor(x => x.transactiontype).Must(TransactiontypeValidation.ValidateTransactiontype).WithMessage("Invalid Transaction type");


        }
    }

    public class BeneficiaryRequestValidation : AbstractValidator<RequestBeneficiary>
    {
        public BeneficiaryRequestValidation()
        {
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        

        }
    }

    public class BeneficiaryFiletrValidation : AbstractValidator<FilterBeneficiary>
    {
        public BeneficiaryFiletrValidation()
        {
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.Filterparam).NotNull().NotEmpty();
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);


        }
    }
}
