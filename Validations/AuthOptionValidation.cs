using Domain.Airtime.Base;
using PaymentSharedKernels.Models;
using PaymentSharedKernels.Models.Enums;
using SharedKernel.Infrastructure.CustomerProfile.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Airtime.Validations
{
    public class AuthOptionValidation
    {
        protected static ICustomerProfileService customerProfileService = new CustomerProfileService();

        public static string Authenticate(string CIF, AuthOptions transferAuth)
        {
            
            var profile = customerProfileService.GetCustomerProfile(CIF).Result;

            if (profile == null)
                return "Unable to get customer profile";

            if (profile.HasMessage)
                return profile.Message;

            if (profile.Data.IsPinBlocked)
                return "PIN BLOCKED";
            if (AuthenticationType.Pin == transferAuth.AuthenticationType)
            {
                var authResponse = customerProfileService.VerifyPin(profile.Data.ProfileId, transferAuth.PIN).Result?.Data;

                if (authResponse == null)
                    return AirtimeValidationMessages.ErrorMessages.PinAuthenticationFailed;

                if (!authResponse.Status)
                    return authResponse.Message;

                if (authResponse.Status)
                    return AirtimeValidationMessages.ConstantMessages.AuthenticationSuccessful;
            }
            else if (AuthenticationType.PinAndOtp == transferAuth.AuthenticationType)
            {
                return AirtimeValidationMessages.ConstantMessages.AuthenticationSuccessful;
            }
            else if (AuthenticationType.PinAndBiometrics == transferAuth.AuthenticationType)
            {
                return AirtimeValidationMessages.ConstantMessages.AuthenticationSuccessful;
            }
            return AirtimeValidationMessages.ErrorMessages.AuthenticationFailed;
        }

        public static string ThirdPartyAuthenticate(string CIF, ThirdPartyAuthOption thirdPartyAuthOption)
        {

            var profile = customerProfileService.GetCustomerProfile(CIF).Result;

            if (profile == null)
                return "Unable to get customer profile";

            if (profile.HasMessage)
                return profile.Message;

            if (profile.Data.IsPinBlocked)
                return "PIN BLOCKED";
            if (AuthenticationType.PinAndOtp == thirdPartyAuthOption.AuthenticationType)
            {
                var authResponse = customerProfileService.VerifyPin(profile.Data.ProfileId, thirdPartyAuthOption.PIN).Result?.Data;

                if (authResponse == null)
                    return AirtimeValidationMessages.ErrorMessages.PinAuthenticationFailed;

                if (!authResponse.Status)
                    return authResponse.Message;

                if (authResponse.Status)
                    return AirtimeValidationMessages.ConstantMessages.AuthenticationSuccessful;
                   
            }
           
            return AirtimeValidationMessages.ErrorMessages.AuthenticationFailed;
        }

        public static string ValidatePin(string pin, string Cif)
        {
           
            var authoptions = new AuthOptions()
            {
                AuthenticationType = AuthenticationType.Pin,
                PIN = pin
            };
            return Authenticate(Cif, authoptions);
        }
        public static string ValidatePinandOTP(string pin, string otp, string Cif)
        {
           
            var authoptions = new AuthOptions()
            {
                AuthenticationType = AuthenticationType.PinAndOtp,
                PIN = pin,
                OTP = otp
            };
            return Authenticate(Cif, authoptions);
        }
        public static bool ValidateOtp(string otp)
        {
            var errorCounter = Regex.Matches(otp, @"[a-zA-Z]").Count;
            if (errorCounter > 0)
                return false;
            if (otp.Length != 6)
                return false;
            return true;
        }
        public static bool ValidatePin(string pin)
        {
            var errorCounter = Regex.Matches(pin, @"[a-zA-Z]").Count;
            if (errorCounter > 0)
                return false;
            if (pin.Length != 4)
                return false;
            return true;
        }
        public static bool ValidateAuthType(ThirdPartyAuthOption authModel)
        {
            if (authModel == null)
                return false;
            if (authModel.AuthenticationType == AuthenticationType.Pin)
                return false;
            if (authModel.AuthenticationType == AuthenticationType.PinAndBiometrics)
                return true;
            if (authModel.AuthenticationType == AuthenticationType.PinAndOtp)
                return true;
            if (authModel.AuthenticationType == AuthenticationType.None)
                return false;
            return false;
        }
        public static string ValidatePinandBiometric(string pin, string biometricPolicy, string biometricToken, string Cif)
        {
           
            var authoptions = new AuthOptions()
            {
                AuthenticationType = AuthenticationType.PinAndBiometrics,
                PIN = pin,
                BiometricPolicy = biometricPolicy,
                BiometricToken= biometricToken
            };
            return Authenticate(Cif, authoptions);
        }
    }
}