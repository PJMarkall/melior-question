namespace Melior.InterviewQuestion.Types
{
    public interface IPaymentScheme
    {
        bool IsValidForPayment(Account account, MakePaymentRequest request);
    }
}
