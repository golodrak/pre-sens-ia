using Microsoft.AspNetCore.Mvc;
using Presensia.Services;
using System.ComponentModel.DataAnnotations;

namespace Presensia.Controllers
{
    public class ContactModel
    {
        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string Subject { get; set; } = string.Empty;

        [Required, StringLength(4000)]
        public string Message { get; set; } = string.Empty;
    }

    public class ContactController : Controller
    {
        private readonly IEmailService _email;
        public ContactController(IEmailService email) => _email = email;

        [HttpGet("/contact")]
        public IActionResult Index() => View(new ContactModel());

        [ValidateAntiForgeryToken]
        [HttpPost("/contact")]
        public async Task<IActionResult> Index(ContactModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _email.SendAsync($"[Presensia] {model.Subject}", $"<p><b>De :</b> {model.Name} ({model.Email})</p><p>{System.Net.WebUtility.HtmlEncode(model.Message)}</p>", model.Email);
            TempData["ok"] = "Merci, votre message a bien été envoyé.";
            return RedirectToAction(nameof(Index));
        }
    }
}