using PaymentSharedKernels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
    public class TransactiontypeValidation
    {
        static TransactionTypes transactionTypes;
        public static bool ValidateTransactiontype(int _transactiontype)
        {

            if (Enum.TryParse<TransactionTypes>(_transactiontype.ToString(), out transactionTypes))
            {
                if(transactionTypes != TransactionTypes.Airtime && transactionTypes != TransactionTypes.Data)
                {
                    return false;
                }
                return true;
            }
                return false;

        }
    }
}
