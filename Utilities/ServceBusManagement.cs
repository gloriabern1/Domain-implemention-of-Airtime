using AzureServiceBusUtil;
using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Microsoft.ServiceBus;
using System.Threading.Tasks;

namespace Domain.Airtime.Models
{
    public class ServceBusManagementCodedEncryption
    {
        //public async Task PushTransaction(ServiceBusAirtimeTransferRequest serviceBusAirtimeTransferRequest)
        //{
        //    var xkey = "xxYYYkkk@123";
        //    //var encrtSAccount = Utilities.StoredProcedure.Services.EncryptStringData(serviceBusTransferRequest.SourceAccount);
        //    //var encrtDacct = Utilities.StoredProcedure.Services.EncryptStringData(serviceBusTransferRequest.DestinationAccount);
        //    //var encrtKey = Utilities.StoredProcedure.Services.EncryptStringData(xkey);

        //    var QueueName = "airtimetransfer";
        //    var TopicName = "Transfer";
        //    var SubscriptionName = "TransferFund";

        //    var serviceBusConnectionString = AirtimeDomainAppsettingManager.GetServiceBusConnectionString();

        //    var busManager = new QueueBusManager(serviceBusConnectionString);

        //    //var Topicresult = busManager.CreateTopic(TopicName);
        //    //var result = busManager.CreateSubscription(TopicName, SubscriptionName);
        //    // var result = busManager.CreateQueueAsync(QueueName, false, false, 3, true);
        //    //var result = busManager.GetSubscriptionsforTopic(TopicName).Result;

        //    // send data to bus
        //    ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Https;

        //    await busManager.SendObjectToQueue(serviceBusAirtimeTransferRequest, QueueName);
        //}

        public static string GetCodedString(string SourceAccount, string DestinationAccount)
        {
            var xkey = "xxYYYkkk@123";
            string codedString = Utilities.StoredProcedure.Services.EncryptStringData(SourceAccount) + Utilities.StoredProcedure.Services.EncryptStringData(DestinationAccount)
                + Utilities.StoredProcedure.Services.EncryptStringData(xkey);

            return codedString;
        }
    }
}