using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace simplecsharp.Pages;

public class PrivacyModel : PageModel
{
    public void OnGet()
    {
string dateTime = DateTime.Now.ToString("d", new CultureInfo("en-US"));
   ViewData["TimeStamp"] = dateTime;
    }
}

