using Microsoft.AspNetCore.Mvc;

namespace Presensia.Controllers
{
    public class LegalController : Controller
    {
        [HttpGet("/mentions-legales")] public IActionResult Mentions() => View();
        [HttpGet("/sla")] public IActionResult SLA() => View();
        [HttpGet("/politique-rgpd")] public IActionResult RGPD() => View();
    }
}