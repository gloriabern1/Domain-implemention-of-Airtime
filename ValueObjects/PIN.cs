using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Airtime.ValueObjects
{
    public class PIN : ValueObject<PIN>
    {
        public string Value { get; set; }

        private PIN()
        {

        }
        private PIN(string value)
        {
            Value = value;
        }

        public static Result<PIN> Create(string pin)
        {

            pin = (pin ?? string.Empty).Trim();

            if (pin.Length == 0)
                return Result.Failure<PIN>("PIN should not be empty");
            if (pin.Length < 4 )
                return Result.Failure<PIN>("Pin should not be less than four characters");
            if (pin.Length > 6)
                return Result.Failure<PIN>("Pin should not be more than six characters");

            if (!Regex.IsMatch(pin, "^[0-9]+$"))

                return Result.Failure<PIN>("PIN should contain numeric values alone");

            return Result.Ok(new PIN(pin));
        }


        protected override bool EqualsCore(PIN other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator PIN(string pin)
        {
            return Create(pin).Value;
        }

        public static implicit operator string(PIN pin)
        {

            return pin.Value;
        }
    }

    public class OTP : ValueObject<OTP>
    {
        public string Value { get; set; }

        private OTP()
        {

        }
        private OTP(string value)
        {
            Value = value;
        }

        public static Result<OTP> Create(string pin)
        {

            pin = (pin ?? string.Empty).Trim();

            if (pin.Length == 0)
                return Result.Failure<OTP>("OTP should not be empty");
            if (pin.Length < 4)
                return Result.Failure<OTP>("OTP should not be less than four characters");
            if (pin.Length > 6)
                return Result.Failure<OTP>("OTP should not be more than six characters");

            if (!Regex.IsMatch(pin, "^[0-9]+$"))

                return Result.Failure<OTP>("OTP should contain numeric values alone");

            return Result.Ok(new OTP(pin));
        }


        protected override bool EqualsCore(OTP other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator OTP(string pin)
        {
            return Create(pin).Value;
        }

        public static implicit operator string(OTP pin)
        {

            return pin.Value;
        }
    }
}
