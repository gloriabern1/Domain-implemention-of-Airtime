using CSharpFunctionalExtensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.ValueObjects

{

    public class PhoneNumber : PaymentSharedKernels.Models.ValueObject<PhoneNumber>
    {

        public string Value { get; set; }

        private PhoneNumber()
        {

        }
        public PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string phonenumber)
        {
            phonenumber = (phonenumber ?? string.Empty).Trim();

            if (phonenumber.Length == 0)
                return Result.Failure<PhoneNumber>("PhoneNumber should not be empty");
            if (phonenumber.Length < 11)
                return Result.Failure<PhoneNumber>("PhoneNumber should not be less than ten digit");

                return Result.Ok(new PhoneNumber(getNonFormattedPhoneNumberTrimPlusSign(phonenumber)));
        }
        public static bool PhonNumberLength(string phonenumber)
        {
            if (phonenumber.Length <11)
                return false;
            return true;
        }



        public static explicit operator PhoneNumber(string phonenumber)
        {
            return Create(phonenumber).Value;
        }

        public static implicit operator string(PhoneNumber phoneNumber)
        {

            return phoneNumber.Value;
        }

        public static String getNonFormattedPhoneNumberTrimPlusSign(String FormattedPhoneNumber)
        {
            String MobilePhone = FormattedPhoneNumber + "";
            if (MobilePhone.StartsWith("+"))
            {
                MobilePhone = MobilePhone.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Trim();
            }
            else
            {
                MobilePhone = MobilePhone.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                if (MobilePhone.Trim().Length == 11)
                {
                    MobilePhone = MobilePhone.StartsWith("0") ? "234" + MobilePhone.Remove(0, 1) : MobilePhone;
                }
            }
            return MobilePhone;
        }
    }
}
