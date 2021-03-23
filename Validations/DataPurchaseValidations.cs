using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.Models;
using Domain.Airtime.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
    public class DataRequestAuthValidation : AbstractValidator<RequestDataAuth>
    {
        public DataRequestAuthValidation()
        {
            RuleFor(x => x.PackageCode).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();

            RuleFor(x => x.AuthOption).NotNull();
            When(x => x.AuthOption != null, () =>
            {
                RuleFor(x => x.AuthOption.PIN).NotNull().NotEmpty().Must(AuthOptionValidation.ValidatePin).WithMessage(AirtimeValidationMessages.ErrorMessages.NoPin);
                RuleFor(x => x.AuthOption.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 4 digit");
                RuleFor(x => x.AuthOption).Must(AuthOptionValidation.ValidateAuthType).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAuthType);
            });

        }

    }

    public class CompleteDataRequestValidation : AbstractValidator<CompleteDataRequest>
    {
        public CompleteDataRequestValidation()
        {
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.TransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.OTP).NotNull().NotEmpty();
            RuleFor(x => x.OTP).Matches("^[0-9]*$").WithMessage("Otp Must be Numeric Only");
            RuleFor(x => x.OTP).NotEmpty().NotNull().Must(AuthOptionValidation.ValidateOtp).WithMessage(AirtimeValidationMessages.ErrorMessages.NoOtp);

        }
    }

    public class DataPurchaseValidationsWithPIN : AbstractValidator<DataRequestWithPin>
    {
        public DataPurchaseValidationsWithPIN()
        {
            RuleFor(x => x.PackageCode).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();

        }
    }

    public class DataPurchaseValidationsWithPINAndOTP : AbstractValidator<DataRequestPinOTP>
    {
        public DataPurchaseValidationsWithPINAndOTP()
        {
            RuleFor(x => x.PackageCode).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.OTP).NotNull().NotEmpty();
            RuleFor(x => x.OTP).Matches("^[0-9]*$").MaximumLength(6).MinimumLength(6).WithMessage("OTP Must be only Numeric and not more or less than 6 digit");

        }
    }

    public class DataPurchaseValidationsWithPINAndBiometric : AbstractValidator<DataRequestPinBiometric>
    {
        public DataPurchaseValidationsWithPINAndBiometric()
        {
            RuleFor(x => x.PackageCode).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(5).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.BiometricPolicy).NotNull().NotEmpty();
            RuleFor(x => x.BiometricToken).NotNull().NotEmpty();
        }
    }
}
