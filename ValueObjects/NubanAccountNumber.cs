using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Airtime.ValueObjects

{
    public class NubanAccountNumber : ValueObject<NubanAccountNumber>
    {

        public string Value { get; set; }

        private NubanAccountNumber()
        {

        }
        private NubanAccountNumber(string value)
        {
            Value = value;
        }

        public static Result<NubanAccountNumber> Create(string nubanAccount)
        {
          
            nubanAccount = (nubanAccount ?? string.Empty).Trim();

            if (nubanAccount.Length == 0)
                return Result.Failure<NubanAccountNumber>("Account Number should not be empty");
            if (nubanAccount.Length != 10 && nubanAccount.Length != 11)
                return Result.Failure<NubanAccountNumber>("Account Number should be ten or eleven digits");

            if (!Regex.IsMatch(nubanAccount, "^[0-9]+$"))

                return Result.Failure<NubanAccountNumber>("Account number should contain numeric values alone");

            return Result.Ok(new NubanAccountNumber(nubanAccount));
        }

        public static bool ValidateNumericAccountNumber(string accountNumber)
        {
            accountNumber = (!string.IsNullOrEmpty(accountNumber) ? accountNumber : "").Trim();
            if (!Regex.IsMatch(accountNumber, "^[0-9]+$"))
                return false;
            return true;
        }

        public static bool ValidateAccountNumberLength(string accountNumber)
        {
            if (accountNumber.Length == 10 || accountNumber.Length == 11)
                return true;
            return false;
        }

        protected override bool EqualsCore(NubanAccountNumber other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator NubanAccountNumber(string nubanAccountNumber)
        {
            return Create(nubanAccountNumber).Value;
        }

        public static implicit operator string(NubanAccountNumber nubanAccountNumber)
        {

            return nubanAccountNumber.Value;
        }
    }


}
