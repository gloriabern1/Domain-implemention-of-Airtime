using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.Models;
using Domain.Airtime.ValueObjects;
using FluentValidation;
using PaymentSharedKernels.Models.Enums;
using SharedKernel.Infrastructure.GeneralHelper;

namespace Domain.Airtime.Validations
{
    public class CompleteAirtimeRequestValidation : AbstractValidator<CompleteAirtimeRequest>
    {
        public CompleteAirtimeRequestValidation()
        {
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.TransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.OTP).NotNull().NotEmpty();
            RuleFor(x => x.OTP).Matches("^[0-9]*$").WithMessage("Otp Must be Numeric Only");
            RuleFor(x => x.OTP).NotEmpty().NotNull().Must(AuthOptionValidation.ValidateOtp).WithMessage(AirtimeValidationMessages.ErrorMessages.NoOtp);

        }
    }

    public class AirtimeRequestAuthValidation : AbstractValidator<RequestAirtimeAuth>
    {
        public AirtimeRequestAuthValidation()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.Network).NotNull().NotEmpty();
            RuleFor(x => x.Network).Must(Network.ValidateNetworkProvider).WithMessage(AirtimeValidationMessages.ErrorMessages.InvalidNetworkProvider);
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
    public class AirtimePurchaseValidationsWithPIN : AbstractValidator<AirtimeRequestWithPIN>
    {
        public AirtimePurchaseValidationsWithPIN()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.Network).NotNull().NotEmpty();
            RuleFor(x => x.Network).Must(Network.ValidateNetworkProvider).WithMessage(AirtimeValidationMessages.ErrorMessages.InvalidNetworkProvider);

            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();



        }
    }

    public class AirtimePurchaseValidationsWithPINAndOTP : AbstractValidator<AirtimeRequestWithPINAndOTP>
    {
        public AirtimePurchaseValidationsWithPINAndOTP()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("PIN Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.Network).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.Network).Must(Network.ValidateNetworkProvider).WithMessage(AirtimeValidationMessages.ErrorMessages.InvalidNetworkProvider);
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.OTP).NotNull().NotEmpty();
            RuleFor(x => x.OTP).Matches("^[0-9]*$").MaximumLength(6).WithMessage("OTP Must be only Numeric and not more than 6 digit");

        }
    }


    public class AirtimePurchaseValidationsWithPINAndBiometric : AbstractValidator<AirtimeRequestWithPINAndBiometric>
    {
        public AirtimePurchaseValidationsWithPINAndBiometric()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateAccountNumberLength).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountLength);
            RuleFor(x => x.AccountNumber).Must(NubanAccountNumber.ValidateNumericAccountNumber).WithMessage(AirtimeValidationMessages.ErrorMessages.ValidateAccountNumericOnly);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.PIN).NotNull().NotEmpty();
            RuleFor(x => x.CIF).NotNull().NotEmpty();
            RuleFor(x => x.PIN).Matches("^[0-9]*$").MaximumLength(4).WithMessage("Pin Must be only Numeric and not more than 5 digit");
            RuleFor(x => x.Network).NotNull().NotEmpty();
            RuleFor(x => x.Network).Must(Network.ValidateNetworkProvider).WithMessage(AirtimeValidationMessages.ErrorMessages.InvalidNetworkProvider);
            RuleFor(x => x.ChannelId).NotNull().NotEmpty();
            RuleFor(x => x.ClientTransactionReference).NotNull().NotEmpty();
            RuleFor(x => x.BiometricPolicy).NotNull().NotEmpty();
            RuleFor(x => x.BiometricToken).NotNull().NotEmpty();
        }
    }
}
