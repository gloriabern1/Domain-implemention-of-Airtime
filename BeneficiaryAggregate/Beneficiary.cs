using AutoMapper;
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

namespace Domain.Airtime.BeneficiaryAggregate
{
    public class Beneficiary : Entity<Guid>
    {
        public PhoneNumber phoneNumber { get; set; }

        public string CIF { get; set; }
        public string PIN { get; set; }

        public string vendCode { get; set; }

        public bool IsActive { get; set; }
        public TransactionTypes transactiontype { get; set; }

        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }

        public string NickName { get; set; }
        public decimal amount { get; set; }
        public long? OldBeneficiaryId { get; set; }

        public int datapackageId { get; set; }
        private IBeneficiaryManager beneficiaryManager;

        public Beneficiary() : base(Guid.NewGuid())
        {
        }

        public Beneficiary(IBeneficiaryManager _beneficiaryManager) : base(Guid.NewGuid())
        {
            beneficiaryManager = _beneficiaryManager;
        }

        public Result<Beneficiary> Create(AddBeneficiary addBeneficiary)
        {
            Mapper.Map(addBeneficiary, this);

            DateCreated = DateTime.Now;
            IsActive = true;

            return Result.Ok(this);
        }

        public Result<Beneficiary> ExecuteBeneficiary()

        {
            var BeneficiaryExist = beneficiaryManager.Checkbeneficiary(this);

            if (BeneficiaryExist)
            {
                this.Status = TransactionStatus.Failed.ToString();
                this.Message = "Beneficiary already exists";
                return Result.Ok(this);
            }
            else
            {
                this.Status = TransactionStatus.Success.ToString();
                this.Message = AirtimeValidationMessages.ConstantMessages.SaveBeneficiarySuccess;

                beneficiaryManager.InsertBeneficiary(this);
                return Result.Ok(this);
            }
        }

        public GlobalResponse DeleteBeneficiary(string BeneficiaryId)

        {
            var response = new GlobalResponse();

            var BeneficiaryExist = beneficiaryManager.CheckDeletebeneficiary(BeneficiaryId);
            if (BeneficiaryExist)
            {
                beneficiaryManager.Deletebeneficiary(BeneficiaryId);

                response.Status = TransactionStatus.Success.ToString();
                response.Message = AirtimeValidationMessages.ConstantMessages.DeleteBeneficiarySuccess;
            }
            else
            {
                response.Status = TransactionStatus.Failed.ToString();
                response.Message = "Invalid BeneficiaryId";
            }

            return response;
        }

        public GlobalResponse UpdateBeneficiary(UpdateBeneficiary updateBeneficiary)

        {
            var response = new GlobalResponse();

            var BeneficiaryExist = beneficiaryManager.CheckDeletebeneficiary(updateBeneficiary.Id);
            var PhoneNumberTransactionTypeCheck = beneficiaryManager.CheckbeneficiaryPhoneNumber(updateBeneficiary);
            if (BeneficiaryExist)
            {
                if (!PhoneNumberTransactionTypeCheck)
                {
                    response.Status = TransactionStatus.Failed.ToString();
                    response.Message = "Cannot update phoneNumber or transactionType";
                    return response;
                }
                if (!beneficiaryManager.UpdateBeneficiary(updateBeneficiary))
                {
                    response.Status = TransactionStatus.Failed.ToString();
                    response.Message = "Operation not successful";
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

        public PagedList<BeneficiaryResponse> GeBeneficiaries(RequestBeneficiary requestBeneficiary)
        {
            return beneficiaryManager.GetBeneficiaries(requestBeneficiary.CIF, requestBeneficiary.PageNumber, requestBeneficiary.PageSize, requestBeneficiary.transactiontype);
        }
    }
}