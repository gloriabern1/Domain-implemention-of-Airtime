namespace Domain.Airtime.Base
{
    public static class AirtimeValidationMessages
    {
        public static class ErrorMessages
        {
            public const string SourceAccountNotAllowed = "Source Account Not Allowed";
            public const string SourceAndDestinationSame = "Source Account and Destination Account Can't be Same";
            public const string NoChannelId = "ChannelId Can't be Empty";
            public const string NoTransactionRef = "Please Provide Transaction Reference for this Transaction";
            public const string AmountEqualZero = "Amount Can't be 0 or Less";
            public const string NoNarration = "Provide a Narration for this Transaction";
            public const string NoOtp = "Please Provide valid Otp for this Transaction";
            public const string NoPin = "Please Provide valid Pin for this Transaction";
            public const string NoBiometrics = "Please Provide valid Biometrics for this Transaction";
            public const string AccountInquiryFailed = "Account Inquiry Failed, Confirm That Account Number is valid";
            public const string AccountInquiryFailedForSource = "Account Inquiry Failed, Confirm That Account Number is valid for source Account";
            public const string AccountInquiryFailedForDestination = "Account Inquiry Failed, Confirm That Account Number is valid for destination";
            public const string WrongCurrency = "Currency On Both Accounts Didn't Match";
            public const string AccountNameMismatch = "Account Name Mismatch";
            public const string AuthenticationFailed = "Authentication Failed";
            public const string PinAuthenticationFailed = "PIN Authentication Failed";
            public const string DestinationAccountNotFound = "Destination Account Not Owned by Source Account";
            public const string ValidateAccountLength = "Account Number should be ten digit";
            public const string ValidateAccountNumericOnly = "Account number should contain numeric values alone";
            public const string ValidateAuthType = "Invalid authentication type";
            public const string InvalidTransactionType = "Invalid Transaction type";
            public const string InsufficientFund = "Insufficient Fund.";
            public const string InvalidService = "Invalid service called. Destination account is a wema account. Please use the IntraBank service ";
            public const string LimitExceeded = "Transfer limit for this account Type exceeded.";
            public const string SchemeCodeNotAvailable = "Scheme code not available.";
            public const string DifferentAccountOwner = "Self to Self Invalid - Source account and destination account does not have the same owner";

            public const string InvalidNetworkProvider = "Enter a valid Network Provider. e.g MTN, GLO, AIRTEL, 9MOBILE";
            public const string TransferError = "Account Debit was Unsuccessful";
            public const string InvalidDataPackageId = "Invalid PackageCode";
            public const string TransactionFailed = "Topup Transaction Failed";

        }

        public static class ConstantMessages
        {
            public const string WemaAlatName = "WEMA/ALAT";

            public const string TransactionProcessing = "Your transaction is processing";
            public const string AirtimeTransactionSuccessful = "Airtime Purchased Successfully";
            public const string DataTransactionSuccessful = "Data Purchased Successfully";
            public const string SaveBeneficiarySuccess = "Saved Beneficiary Successfully";
            public const string SaveRecurringSuccess = "Saved Recurring Payment Successfully";
            public const string DeleteBeneficiarySuccess = "Deleted Beneficiary Successfully";
            public const string DeleteRecurringSuccess = "Deleted Recurring Successfully";
            public const string GenericSuccess = "Operation Successfully";
            public const string AuthenticationSuccessful = "Authentication Successful";
            public const string TrasferSuccessul = "Transfer Successful. Sending to vendor";
        }
    }
}