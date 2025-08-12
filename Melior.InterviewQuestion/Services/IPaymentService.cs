using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
        bool CheckPaymentIsValidForScheme(Account account, MakePaymentRequest request);
    }
}
