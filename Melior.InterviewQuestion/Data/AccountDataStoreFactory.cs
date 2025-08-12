namespace Melior.InterviewQuestion.Data
{
    internal class AccountDataStoreFactory
    {
        public static IAccountDataStore GetAccountDataStore(string dataStoreType)
        {
            if (dataStoreType == "Backup")
            {
                return new BackupAccountDataStore();
            }
            else
            {
                return new AccountDataStore();
            }
        }
    }
}
