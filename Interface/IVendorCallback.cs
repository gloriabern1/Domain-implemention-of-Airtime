using Domain.Airtime.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Interface
{
    public interface IVendorCallback
    {
        AirtimeServiceResponse VendorCallback(string VendorObject);
    }
}
