using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using System;
using Xunit;

namespace Melior.InterviewQuestionTests.PaymentSchemeRules
{
    public class ChapsPaymentSchemeRulesTests
    {
        private MakePaymentRequest makePaymentRequest = new MakePaymentRequest()
        {
            CreditorAccountNumber = "123",
            DebtorAccountNumber = "456",
            Amount = 1.5m,
            PaymentDate = DateTime.Now,
            PaymentScheme = PaymentScheme.Chaps
        };

        [Fact]
        public void GivenValidAccount_IsValidForPaymentReturnsTrue()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 10m,
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };
            var chapsPaymentScheme = new ChapsPaymentSchemeRules();

            bool result = chapsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

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
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };
            var chapsPaymentScheme = new ChapsPaymentSchemeRules();

            bool result = chapsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.False(result);
        }

        [Fact]
        public void GivenAccountNotLive_IsValidForPaymentReturnsFalse()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 10m,
                Status = AccountStatus.InboundPaymentsOnly,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };
            var chapsPaymentScheme = new ChapsPaymentSchemeRules();

            bool result = chapsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.False(result);
        }
    }
}
