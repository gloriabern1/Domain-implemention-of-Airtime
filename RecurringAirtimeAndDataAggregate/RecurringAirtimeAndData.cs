using AutoMapper;
using AzureServiceBusUtil;
using CommonComponents.Helpers;
using CSharpFunctionalExtensions;
using Domain.Airtime.Base;
using Domain.Airtime.DTOs;
using Domain.Airtime.Interface.Managers;
using Domain.Airtime.Validations;
using Domain.Airtime.ValueObjects;
using PaymentSharedKernels.Models;
using PaymentSharedKernels.Models.Enums;
using System;

namespace Domain.Airtime.RecurringAirtimeAndDataAggregate
{
    public class RecurringAirtimeAndData : Entity<Guid>
    {
        public string vendCode { get; set; }
        public NubanAccountNumber CustomerAccountNumber { get; set; }
        public string CIF { get; set; }
        public string PIN { get; set; }
        public string NickName { get; set; }
        public DateTime StartDate { get; set; }
        public TransactionTypes Transactiontype { get; set; }
        public int DatapackageId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActivated { get; set; }
        public PhoneNumber phoneNumber { get; set; }
        public RecurringState RecurringState { get; set; }

        public string Status { get; set; }
        public string Message { get; set; }
        public int Duration { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public bool IsProcessed { get; set; }
        public long? OldCustomerId { get; set; }
        public long? OldBeneficiaryBillerSavedPaymentId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public RecurringFrequency RecurringFrequency { get; set; }

        public DateTime DateCreated { get; set; }

        private IRecuringAirtimeandDataManager recuringAirtimeandDataManager;
        public RecurringAirtimeAndData() : base(Guid.NewGuid())
        {

        }
        public RecurringAirtimeAndData(IRecuringAirtimeandDataManager _recuringAirtimeandDataManager) : base(Guid.NewGuid())
        {
            recuringAirtimeandDataManager = _recuringAirtimeandDataManager;
        }

        public Result<RecurringAirtimeAndData> Create(AddRecurringAirtimeandData model)
        {

            Mapper.Map(model, this);

            DateCreated = DateTime.Now;
            IsActivated = true;
            IsProcessed = false;
            NextDueDate = model.StartDate;
            UpdatedDate = DateTime.Now;

            return Result.Ok(this);

        }

        public Result<RecurringAirtimeAndData> ExecuteRecurring()
        {
            
            recuringAirtimeandDataManager.InsertRecuringAirtimeandData(this);
            this.Status = TransactionStatus.Success.ToString();
            this.Message = AirtimeValidationMessages.ConstantMessages.SaveRecurringSuccess;

            return Result.Ok(this);

        }

        public PagedList<RecuringAirtimeandDataResponse> GetRecuringPayemnt(RequestBeneficiary requestBeneficiary)
        {
            return recuringAirtimeandDataManager.GetRecuringAirtimeandData(requestBeneficiary.CIF, requestBeneficiary.PageNumber, requestBeneficiary.PageSize, requestBeneficiary.transactiontype);
        }

        public GlobalResponse DeleteRecurring(string Id)

        {
            var response = new GlobalResponse();

            var BeneficiaryExist = recuringAirtimeandDataManager.CheckDeleteRecuringAirtimeandData(Id);
            if (BeneficiaryExist)
            {
                recuringAirtimeandDataManager.DeleteRecuringAirtimeandData(Id);

                response.Status = TransactionStatus.Success.ToString();
                response.Message = AirtimeValidationMessages.ConstantMessages.DeleteRecurringSuccess;

            }
            else
            {
                response.Status = TransactionStatus.Failed.ToString();
                response.Message = "Invalid Recurring PaymentId";


            }

            return response;

        }

        public GlobalResponse UpdateRecurringName(string Id, string NickName)

        {
            var response = new GlobalResponse();

            var RecurringExist = recuringAirtimeandDataManager.CheckDeleteRecuringAirtimeandData(Id);
            if (RecurringExist)
            {

                if (!recuringAirtimeandDataManager.UpdateRecurringNickname(Id, NickName))
                {
                    response.Status = TransactionStatus.Failed.ToString();
                    response.Message = "Operation not Successful";
                    return response;
                }
                else
                {
                    response.Status = TransactionStatus.Success.ToString();
                    response.Message = AirtimeValidationMessages.ConstantMessages.GenericSuccess;

                }

            }
            else
            {
                response.Status = TransactionStatus.Failed.ToString();
                response.Message = "Invalid BeneficiaryId";


            }

            return response;

        }


        public Result<RecurringAirtimeAndData> SendReccuringRequest(string queueName, string servicebusConnectionstring)
        {
            var scheduleRecurringObject = new ScheduleRecurringObject
            {
                CIF = CIF,
                TransactionType = (TransactionTypes)Transactiontype,
                RecurringId = this.Id
            };

            var serviceBusCall = new ServiceBusManagement(servicebusConnectionstring).PushToQueue(scheduleRecurringObject, queueName);

            return Result.Ok(this);
        }
    }
}
