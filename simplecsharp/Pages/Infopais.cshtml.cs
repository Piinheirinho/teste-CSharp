using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using simplecsharp.Models;
using System.Linq;
using System.Text.Json;

namespace simplecsharp.Pages;

public class InfopaisModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InfopaisModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string CodigoPais { get; set; }
    public Pais InfoPais { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string cod)
    {
        CodigoPais = cod;

        var client = _httpClientFactory.CreateClient("RestCountries");
        var response = await client.GetAsync($"https://restcountries.com/v3.1/alpha?codes={cod}&fields=name,capital,currencies,cca2,flags");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var countryResponse = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options)?.FirstOrDefault();

            if (countryResponse != null)
            {
                InfoPais = new Pais
                {
                    OfficialName = countryResponse.name?.official,
                    Cca2 = countryResponse.cca2,
                    FlagUrl = countryResponse.flags?.png
                };
            }
        }

        return Page();
    }
}
