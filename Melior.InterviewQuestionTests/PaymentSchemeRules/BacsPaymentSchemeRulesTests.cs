using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using System;
using Xunit;

namespace Melior.InterviewQuestionTests.PaymentSchemeRules
{
    public class BacsPaymentSchemeRulesTests
    {
        private MakePaymentRequest makePaymentRequest = new MakePaymentRequest()
        {
            CreditorAccountNumber = "123",
            DebtorAccountNumber = "456",
            Amount = 1.5m,
            PaymentDate = DateTime.Now,
            PaymentScheme = PaymentScheme.Bacs
        };

        [Fact]
        public void GivenValidAccount_IsValidForPaymentReturnsTrue()
        {
            var account = new Account()
            {
                AccountNumber = "456",
                Balance = 10m,
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };
            var bacsPaymentScheme = new BacsPaymentSchemeRules();

            bool result = bacsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

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
            var bacsPaymentScheme = new BacsPaymentSchemeRules();

            bool result = bacsPaymentScheme.IsValidForPayment(account, makePaymentRequest);

            Assert.False(result);
        }
    }
}
