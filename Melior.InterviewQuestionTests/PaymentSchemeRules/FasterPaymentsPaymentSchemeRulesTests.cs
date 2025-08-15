using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using System;
using Xunit;

namespace Melior.InterviewQuestionTests.PaymentSchemeRules
{
    public class FasterPaymentsPaymentSchemeRulesTests
    {
        private MakePaymentRequest makePaymentRequest = new MakePaymentRequest()
        {
            CreditorAccountNumber = "123",
            DebtorAccountNumber = "456",
            Amount = 5m,
            PaymentDate = DateTime.Now,
            PaymentScheme = PaymentScheme.FasterPayments
        };

        [Fact]
        public void GivenValidAccount_IsValidForPaymentReturnsTrue()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 10m,
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };
            var fasterPaymentsPaymentScheme = new FasterPaymentsPaymentSchemeRules();

            bool result = fasterPaymentsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.True(result);
        }

        [Fact]
        public void GivenInvalidPaymentScheme_IsValidForPaymentReturnsFalse()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 10m,
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };
            var fasterPaymentsPaymentScheme = new FasterPaymentsPaymentSchemeRules();

            bool result = fasterPaymentsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.False(result);
        }

        [Fact]
        public void GivenInsufficientBalance_IsValidForPaymentReturnsFalse()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 2m,
                Status = AccountStatus.InboundPaymentsOnly,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };
            var fasterPaymentsPaymentScheme = new FasterPaymentsPaymentSchemeRules();

            bool result = fasterPaymentsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.False(result);
        }
    }
}
