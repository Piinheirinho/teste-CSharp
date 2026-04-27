using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using simplecsharp.Models;
using System.Text.Json;

namespace MyApp.Namespace
{
    public class InfopaisModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InfopaisModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Pais InfoPais { get; set; }
        public string CodigoPais { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            CodigoPais = cod;
            var client = _httpClientFactory.CreateClient("apiRest");
            var response = await client.GetAsync($"countriesfile/{cod}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var artigoResponse = JsonSerializer.Deserialize<CountryApiResponse>(json, options);

            InfoPais = new Pais
            {
                OfficialName = artigoResponse.name?.official,
                Cca2 = artigoResponse.cca2,
                FlagUrl = artigoResponse.flags?.png
            };
            return Page();
        }
    }
}
