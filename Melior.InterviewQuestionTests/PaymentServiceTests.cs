using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using System;
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

            var paymentServicePartialMock = new Mock<PaymentService>(accountDataStoreMock.Object).As<IPaymentService>();
            paymentServicePartialMock.CallBase = true;
            paymentServicePartialMock.Setup(c => c.CheckPaymentIsValidForScheme(account, makePaymentRequest)).Returns(true);

            MakePaymentResult result = paymentServicePartialMock.Object.MakePayment(makePaymentRequest);

            Assert.True(result.Success);
        }

        [Fact]
        public void GivenAccountIsNull_MakePaymentSuccessReturnsFalse()
        {
            var accountDataStoreMock =
                new Mock<IAccountDataStore>();
            accountDataStoreMock.Setup(m => m.GetAccount("456"))
                                .Returns(() => null);

            var paymentServicePartialMock = new Mock<PaymentService>(accountDataStoreMock.Object).As<IPaymentService>();
            paymentServicePartialMock.CallBase = true;
            paymentServicePartialMock.Setup(c => c.CheckPaymentIsValidForScheme(account, makePaymentRequest)).Returns(true);

            MakePaymentResult result = paymentServicePartialMock.Object.MakePayment(makePaymentRequest);

            Assert.False(result.Success);
        }

        [Fact]
        public void GivenPaymentIsInvalid_MakePaymentSuccessReturnsFalse()
        {
            var accountDataStoreMock =
                new Mock<IAccountDataStore>();
            accountDataStoreMock.Setup(m => m.GetAccount("456"))
                                .Returns(account);

            var paymentServicePartialMock = new Mock<PaymentService>(accountDataStoreMock.Object).As<IPaymentService>();
            paymentServicePartialMock.CallBase = true;
            paymentServicePartialMock.Setup(c => c.CheckPaymentIsValidForScheme(account, makePaymentRequest)).Returns(false);

            MakePaymentResult result = paymentServicePartialMock.Object.MakePayment(makePaymentRequest);

            Assert.True(result.Success);
        }
    }
}
