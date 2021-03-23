using PaymentSharedKernels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Validations
{
   public static class RecurringfrequencyValidation
    {
        public static bool Validate(int recurringFrequency)
        {
            bool response = false;
            switch (recurringFrequency)
            {
                case (int)RecurringFrequency.Daily:
                    response = true;
                    break;

                case (int)RecurringFrequency.Monthly:
                    response = true;
                    break;

                case (int)RecurringFrequency.One_Off:
                    response = true;
                    break;

                case (int)RecurringFrequency.Quarterly:
                    response = true;
                    break;

                case (int)RecurringFrequency.Weekly:
                    response = true;
                    break;
            }
            return response;
        }
    }
}
