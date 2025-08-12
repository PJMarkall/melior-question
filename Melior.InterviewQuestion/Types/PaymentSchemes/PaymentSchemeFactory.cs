namespace Melior.InterviewQuestion.Types.PaymentSchemes
{
    public class PaymentSchemeFactory
    {
        public static IPaymentScheme GetPaymentScheme(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsPaymentScheme();

                case PaymentScheme.FasterPayments:
                    return new FasterPaymentsPaymentScheme();

                case PaymentScheme.Chaps:
                    return new ChapsPaymentScheme();
            }

            return null;
        }
    }
}
