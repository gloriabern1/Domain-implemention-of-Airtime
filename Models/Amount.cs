using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Models
{
    public class Amount : ValueObject<Amount>
    {
        public decimal Value { get; set; }

        private Amount()
        {

        }
        private Amount(decimal value)
        {
            Value = value;
        }

        public static Result<Amount> Create(decimal amount)
        {

            if (amount < 50)
                return Result.Failure<Amount>("Amount Should not be less than 50");
          

            return Result.Ok(new Amount(amount));
        }

        protected override bool EqualsCore(Amount other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        public static explicit operator Amount(decimal amount)
        {
            return Create(amount).Value;
        }

        public static implicit operator decimal(Amount _amount)
        {

            return _amount.Value;
        }
    }
}
