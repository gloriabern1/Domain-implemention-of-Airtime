using CommonComponents.Models;
using Domain.Airtime.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
    public class AirtimeAmountValidation
    {

        public static bool CheckNetworkAmount(decimal Amount, string VendCode)
        {
            if (Enum.TryParse<NetworkVendors>(VendCode, out NetworkVendors networkVendors))
            {
                if (networkVendors == NetworkVendors.AIRTEL || networkVendors == NetworkVendors._9MOBILE
                    || networkVendors == NetworkVendors.GLO)
                {
                    if (Amount >= 50)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return true;
                }

            }
            return false;
        }

       
    }
}
