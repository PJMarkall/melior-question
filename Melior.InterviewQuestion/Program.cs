using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Types.PaymentSchemeRules;
using System.Collections.Generic;
using System.Configuration;

namespace Melior.InterviewQuestion
{
    public class Program
    {
        public static void Run()
        {
            string dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            IAccountDataStore accountDataStore = AccountDataStoreFactory.GetAccountDataStore(dataStoreType);

            IDictionary<PaymentScheme, IPaymentSchemeRules> paymentSchemes = new Dictionary<PaymentScheme, IPaymentSchemeRules>()
            {
                { PaymentScheme.Bacs, new BacsPaymentSchemeRules() },
                { PaymentScheme.FasterPayments, new FasterPaymentsPaymentSchemeRules() },
                { PaymentScheme.Chaps, new ChapsPaymentSchemeRules() }
            };

            PaymentService paymentService = new PaymentService(accountDataStore, paymentSchemes);
        }
    }
}
