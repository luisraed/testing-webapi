using InventoryManagementDto;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagementAcceptanceTests.Helpers
{
    public class InventoryManagementClient
    {
        private const string BaseUrl = "http://localhost:8888";
        private JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    public async Task<InventoryResponse> GetAsync(int productId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseUrl}/inventory/{productId}");

                 string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<InventoryResponse>(responseBody, Options);
            };
        }
        public async Task<InventoryResponse> PutAsync(InventoryUpdateRequest request)
        {
            using (var client = new HttpClient())
            {
                var serializedRequest = JsonSerializer.Serialize(request);
                var requestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{BaseUrl}/inventory", requestContent);

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<InventoryResponse>(responseBody, Options);
            };
        }

        public async Task<InventoryResponse> PostAsync(InventoryPostRequest request)
        {
            using (var client = new HttpClient())
            {
                var serializedRequest = JsonSerializer.Serialize(request);
                var requestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{BaseUrl}/inventory", requestContent);

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<InventoryResponse>(responseBody, Options);
            };
        }
    }
}
