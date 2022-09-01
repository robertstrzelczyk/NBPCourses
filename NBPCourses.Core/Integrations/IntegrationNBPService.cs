using NBPCourses.Core.Integrations.Interfaces;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NBPCourses.Core.Integrations.Models;
using NBPCourses.Core.Models;
using System.Globalization;

namespace NBPCourses.Core.Integrations
{
    public class IntegrationNBPService : IIntegrationNBPService
    {
        private readonly HttpClient Client = new HttpClient();

        private const string BaseUrl = "https://api.nbp.pl";
        private const string ApiEndpoint = "/api/exchangerates/tables/A/";

        public IntegrationNBPService()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.BaseAddress = new Uri(BaseUrl);
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CoursesDataTable> GetCoursesAsync()
        {
            try
            {
                var tableArray = await GetTableArray();
                var output = new CoursesDataTable();

                foreach (var table in tableArray)
                {
                    output.TotalCount = table.Rates.Count();
                    output.Items = table.Rates.Select(rate => new Course
                    {
                        CurrencyName = rate.Currency,
                        Code = rate.Code,
                        Mid = rate.Mid,
                        EffectiveDate = table.EffectiveDate
                    }).OrderBy(x => x.EffectiveDate).ToList();
                }

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<ExchangeRatesTable[]> GetTableArray()
        {
            using (var response = await Client.GetAsync(ApiEndpoint))
            {

                if (!response.IsSuccessStatusCode) return null;
                var str = await response.Content.ReadAsStringAsync();
                var tableArray = JsonConvert.DeserializeObject<ExchangeRatesTable[]>(str);
                return tableArray;
            }
        }

    }
}
