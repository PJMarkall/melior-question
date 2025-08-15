using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IDictionary<PaymentScheme, IPaymentSchemeRules> _paymentSchemeRules;

        public PaymentService(IAccountDataStore accountDataStore, IDictionary<PaymentScheme, IPaymentSchemeRules> paymentSchemeRules)
        {
            _accountDataStore = accountDataStore;
            _paymentSchemeRules = paymentSchemeRules;
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

            IPaymentSchemeRules rules = _paymentSchemeRules[request.PaymentScheme];
            makePaymentResult.Success = rules.IsValidForPayment(account, request);

            if (makePaymentResult.Success)
            {
                account.DeductFromAccount(request.Amount);
                _accountDataStore.UpdateAccount(account);
            }

            return makePaymentResult;
        }
    }
}
