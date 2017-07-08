using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Cats30.SAL
{
    public class AzureServiceClient
    {
        string url = "http://catsandroid.azurewebsites.net";

        MobileServiceClient Client;
        IMobileServiceTable<Cats> CatsTable;

        public AzureServiceClient()
        {
            Client = new MobileServiceClient(url);
            CatsTable = Client.GetTable<Cats>();
        }

        public async Task<List<Cats>> GetCatsAsync()
        {
            return await CatsTable.ToListAsync();
        }
    }
}