namespace Melior.InterviewQuestion.Types.PaymentSchemes
{
    public class ChapsPaymentScheme : IPaymentScheme
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
