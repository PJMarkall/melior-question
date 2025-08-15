namespace Melior.InterviewQuestion.Types.PaymentSchemeRules
{
    public class ChapsPaymentSchemeRules : IPaymentSchemeRules
    {
        public bool IsValidForPayment(Account account, MakePaymentRequest request)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }
            else if (account.Status != AccountStatus.Live)
            {
                return false;
            }

            return true;
        }
    }
}
