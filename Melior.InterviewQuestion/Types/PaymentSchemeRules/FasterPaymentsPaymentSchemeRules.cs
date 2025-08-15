namespace Melior.InterviewQuestion.Types.PaymentSchemeRules
{
    public class FasterPaymentsPaymentSchemeRules : IPaymentSchemeRules
    {
        public bool IsValidForPayment(Account account, MakePaymentRequest request)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }
            else if (account.Balance < request.Amount)
            {
                return false;
            }

            return true;
        }
    }
}
