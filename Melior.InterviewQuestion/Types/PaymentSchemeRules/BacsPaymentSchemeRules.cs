namespace Melior.InterviewQuestion.Types.PaymentSchemeRules
{
    public class BacsPaymentSchemeRules : IPaymentSchemeRules
    {
        public bool IsValidForPayment(Account account, MakePaymentRequest request)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return false;
            }

            return true;
        }
    }
}
