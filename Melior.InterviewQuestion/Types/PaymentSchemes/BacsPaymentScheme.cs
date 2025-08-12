namespace Melior.InterviewQuestion.Types.PaymentSchemes
{
    public class BacsPaymentScheme : IPaymentScheme
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
