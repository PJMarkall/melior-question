using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemes;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            MakePaymentResult makePaymentResult = new MakePaymentResult();

            if (account == null)
            {
                makePaymentResult.Success = false;
                return makePaymentResult;
            }

            makePaymentResult.Success = CheckPaymentIsValidForScheme(account, request);

            if (makePaymentResult.Success)
            {
                account.DeductFromAccount(request.Amount);
                _accountDataStore.UpdateAccount(account);
            }

            return makePaymentResult;
        }

        public bool CheckPaymentIsValidForScheme(Account account, MakePaymentRequest request)
        {
            IPaymentScheme paymentScheme = PaymentSchemeFactory.GetPaymentScheme(request.PaymentScheme);
            
            return paymentScheme.IsValidForPayment(account, request);
        }
    }
}
