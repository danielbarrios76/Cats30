using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Cats30.Entities;
using System.Linq;
using System;

namespace Cats30.SAL
{
    public class ServiceClient
    {
        string url = "https://ticapacitacion.com/cats";

        public async Task<List<Cat>> GetCats()
        {
            //return !useAzure ? await GetCatsRestful() : await GetCatsAzure();
            //return await GetCatsRestful();
            return await GetCatsAzure();
        }

        private async Task<List<Cat>> GetCatsRestful()
        {
            List<Cat> cats = null;

            using (var cliente = new HttpClient())
            {
                var json = await cliente.GetStringAsync(url);
                cats = JsonConvert.DeserializeObject<List<Cat>>(json);
            }

            return cats;
        }

        private async Task<List<Cat>> GetCatsAzure()
        {
            try
            {
                var service = new AzureServiceClient();
                List<Cats> cats = await service.GetCatsAsync();

                return cats.Select(cat => cat as Cat).ToList();
            }
            catch(Exception ex)
            {
                return default(List<Cat>);
            }
        }
    }
}
