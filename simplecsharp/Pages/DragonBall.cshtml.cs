using Microsoft.AspNetCore.Mvc.RazorPages;
using simplecsharp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace simplecsharp.Pages;
public class DragonBallModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DragonBallModel(IHttpClientFactory httpClientFactory)

    {
        _httpClientFactory = httpClientFactory;
    }

    public List<DragonBall> DragonBalls { get; set; } = new();
    public async Task OnGetAsync()

    {
        var client = _httpClientFactory.CreateClient("dragonball");
        var response = await client.GetAsync("https://dragonball-api.com/api/characters");
        

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options);

            DragonBalls = dados.Select(d => new DragonBall
            {
                Nome = d.items?.name,
                Imagem = d.items?.image

            }).ToList();
        }
    }
}
