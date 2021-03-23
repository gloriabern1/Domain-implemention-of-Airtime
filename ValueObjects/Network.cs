using CommonComponents.Models;
using CSharpFunctionalExtensions;
using Domain.Airtime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Airtime.ValueObjects
{
    public   class Network : ValueObject<Network>
    {
        static NetworkVendors networkVendors;
        public string Value { get; set; }

        private Network()
        {

        }
        private Network(string value)
        {
            Value = value;
        }

        public static Result<Network> Create(string network)
        {

            network = (network ?? string.Empty).Trim();

            if (network.Length == 0)
                return Result.Failure<Network>("Network Provider should not be empty");
            if (network.Length < 3 )
                return Result.Failure<Network>("Network provider should be atleast three characters");

            if (!Enum.TryParse<NetworkVendors>(network, out networkVendors))
                return Result.Failure<Network>("Enter a valid Network Provider. e.g MTN, GLO, AIRTEL, 9MOBILE");
                
            return Result.Ok(new Network(network.ToUpper()));
        }

        public static bool ValidateNetworkProvider(string Network)
        {
            if (!Enum.TryParse<NetworkVendors>(Network, out networkVendors))
                return false;
            return true;
        }

        protected override bool EqualsCore(Network other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Network(string network)
        {
            return Create(network).Value;
        }

        public static implicit operator string(Network network)
        {

            return network.Value;
        }


    }

  
}
