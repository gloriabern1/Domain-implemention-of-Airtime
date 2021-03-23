using PaymentSharedKernels.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Airtime.Base
{
    public class AirtimeDomainAppsettingManager
    {
      
        public AirtimeDomainAppsettingManager()
        {

        }
        public static bool UseServiceBus()
        {
            try
            {
                return bool.Parse(AppsettingsManager.GetConfig("OtherSettings:UseServiceBus"));
            }
            catch
            {
                return false;
            }
        }

        public static string GetServiceBusConnectionString()
        {
            return AppsettingsManager.GetConfig("OtherSettings:ServiceBusConnectionString");
        }
        public static string GetC24IntraBankTopicConnectionString()
        {
            return AppsettingsManager.GetConfig("OtherSettings:C24IntraBankTopicConnectionString");
        }
        public static string GetAirtimeTransferQueueConnectionString()
        {
            return AppsettingsManager.GetConfig("OtherSettings:AirtimeTransferQueueConnectionString");
        }
        public static string GetRecurringQueueConnectionString()
        {
            return AppsettingsManager.GetConfig("OtherSettings:RecurringQueueConnectionString");
        }
        public static string GetSettlementQueueConnectionString()
        {
            return AppsettingsManager.GetConfig("OtherSettings:SettlementQueueConnectionString");
        }
        public static string GetAirtimeQueueName()
        {
            return AppsettingsManager.GetConfig("OtherSettings:AirtimeTransferQueueName");
        }
        public static string GetSettlementQueueName()
        {
            return AppsettingsManager.GetConfig("OtherSettings:SettlementQueueName");
        }
    }
}

