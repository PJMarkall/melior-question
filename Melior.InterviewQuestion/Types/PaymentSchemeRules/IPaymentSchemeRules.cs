namespace Melior.InterviewQuestion.Types.PaymentSchemeRules
{
    public interface IPaymentSchemeRules
    {
        bool IsValidForPayment(Account account, MakePaymentRequest request);
    }
}
