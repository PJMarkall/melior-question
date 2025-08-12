using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using System.Configuration;

namespace Melior.InterviewQuestion
{
    public class Program
    {
        public static void Run()
        {
            string dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            IAccountDataStore accountDataStore = AccountDataStoreFactory.GetAccountDataStore(dataStoreType);

            PaymentService paymentService = new PaymentService(accountDataStore);
        }
    }
}
