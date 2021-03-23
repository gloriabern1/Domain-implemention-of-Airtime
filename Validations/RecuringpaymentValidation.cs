using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.ValueObjects;
using FluentValidation;
using PaymentSharedKernels.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
    public class RecuringpaymentValidation : AbstractValidator<AddRecurringAirtimeandData>
    {
        public RecuringpaymentValidation()
        {
            RuleFor(x => x.phoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.NickName).NotNull().NotEmpty().MinimumLength(1);
            RuleFor(x => x.phoneNumber).Must(PhoneNumber.PhonNumberLength).WithMessage("Invalid PhoneNumber");
            RuleFor(x => x.CustomerAccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.CustomerAccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.CustomerAccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);

            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.transactiontype).GreaterThan(0);
            RuleFor(x => x.transactiontype).Must(TransactiontypeValidation.ValidateTransactiontype).WithMessage("Invalid Transaction type");
            RuleFor(x => x.StartDate).Must(ValidateDateTime.BeAValidDate).WithMessage("Invalid StartDate");
            RuleFor(x => x.StartDate).Must(ValidateDateTime.BeAValidDateGreaterThanToday).WithMessage("DateTime input must be greater than today");
            RuleFor(x => x.RecurringFrequency).Must(RecurringfrequencyValidation.Validate).WithMessage("Invalid Recurring Frequency");
            RuleFor(x => x.Duration).GreaterThan(0);

        }
    }

    public class UpdateBeneficiaryValidation : AbstractValidator<UpdateBeneficiary>
    {
        public UpdateBeneficiaryValidation()
        {
            RuleFor(x => x.phoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.NickName).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(x => x.Id).Must(GuidValidation.ValidateGuid).WithMessage("Invalid BeneficiaryId");

            RuleFor(x => x.phoneNumber).Must(PhoneNumber.PhonNumberLength).WithMessage("Invalid PhoneNumber");
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.transactiontype).GreaterThan(0);
            RuleFor(x => x.transactiontype).Must(TransactiontypeValidation.ValidateTransactiontype).WithMessage("Invalid Transaction type");
         

        }
    }
}
