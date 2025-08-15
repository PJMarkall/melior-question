using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Melior.InterviewQuestion.Tests
{
    public class PaymentServiceTests
    {
        private MakePaymentRequest makePaymentRequest = new MakePaymentRequest()
        {
            CreditorAccountNumber = "123",
            DebtorAccountNumber = "456",
            Amount = 1.5m,
            PaymentDate = DateTime.Now,
            PaymentScheme = PaymentScheme.Bacs
        };

        private Account account = new Account()
        {
            AccountNumber = "456",
            Balance = 10m,
            Status = AccountStatus.Live,
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
        };

        [Fact]
        public void GivenValidRequest_MakePaymentSuccessReturnsTrue()
        {
            var accountDataStoreMock =
                new Mock<IAccountDataStore>();
            accountDataStoreMock.Setup(m => m.GetAccount("456"))
                                .Returns(account);

            var bacsPaymentSchemeRulesMock =
                new Mock<IPaymentSchemeRules>();
            bacsPaymentSchemeRulesMock.Setup(m => m.IsValidForPayment(account, makePaymentRequest))
                                .Returns(true);
            IDictionary<PaymentScheme, IPaymentSchemeRules> paymentSchemes = new Dictionary<PaymentScheme, IPaymentSchemeRules>()
            {
                { PaymentScheme.Bacs, new BacsPaymentSchemeRules() },
            };


            var paymentService = new PaymentService(accountDataStoreMock.Object, paymentSchemes);

            MakePaymentResult result = paymentService.MakePayment(makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void GivenAccountIsNull_MakePaymentSuccessReturnsFalse()
        {
            var accountDataStoreMock =
                new Mock<IAccountDataStore>();
            accountDataStoreMock.Setup(m => m.GetAccount("456"))
                                .Returns(() => null);

            var bacsPaymentSchemeRulesMock =
                new Mock<IPaymentSchemeRules>();
            bacsPaymentSchemeRulesMock.Setup(m => m.IsValidForPayment(account, makePaymentRequest))
                                .Returns(true);
            IDictionary<PaymentScheme, IPaymentSchemeRules> paymentSchemes = new Dictionary<PaymentScheme, IPaymentSchemeRules>()
            {
                { PaymentScheme.Bacs, new BacsPaymentSchemeRules() },
            };


            var paymentService = new PaymentService(accountDataStoreMock.Object, paymentSchemes);

            MakePaymentResult result = paymentService.MakePayment(makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void GivenPaymentIsInvalid_MakePaymentSuccessReturnsFalse()
        {
            var accountDataStoreMock =
                new Mock<IAccountDataStore>();
            accountDataStoreMock.Setup(m => m.GetAccount("456"))
                                .Returns(account);

            var bacsPaymentSchemeRulesMock =
                new Mock<IPaymentSchemeRules>();
            bacsPaymentSchemeRulesMock.Setup(m => m.IsValidForPayment(account, makePaymentRequest))
                                .Returns(false);
            IDictionary<PaymentScheme, IPaymentSchemeRules> paymentSchemes = new Dictionary<PaymentScheme, IPaymentSchemeRules>()
            {
                { PaymentScheme.Bacs, new BacsPaymentSchemeRules() },
            };


            var paymentService = new PaymentService(accountDataStoreMock.Object, paymentSchemes);

            MakePaymentResult result = paymentService.MakePayment(makePaymentRequest);

            Assert.True(result.Success);
        }
    }
}
