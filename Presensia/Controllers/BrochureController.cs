using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Presensia.Controllers
{
    public class BrochureController : Controller
    {
        [HttpGet("/brochure.pdf")]
        public IActionResult Pdf()
        {
            var turquoise = "#16c7d1";
            var petrol = "#0e2a35";

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    //page.Background().Image("wwwroot/img/bg-gradient.png", ImageScaling.Stretch);


                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Text("Presensia").FontSize(22).Bold().FontColor(petrol);
                        row.ConstantItem(80).Height(20).Background(turquoise);
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(8);
                        col.Item().Text("AG fluide, présence validée, PV en 24h.").FontSize(18).Bold().FontColor(petrol);
                        col.Item().Text("• Check-in en 2 gestes • Quorum live • Procurations en ligne • eIDAS/RGPD • SSO/API");                        
                        col.Item().Border(1).Padding(10).Background(Colors.White).Text("Packs: Essentiel • Pro Compliance • Enterprise DSI");
                    });

                    page.Footer().AlignCenter().Text("© " + DateTime.UtcNow.Year + " Presensia — Satellisoft").FontSize(10).FontColor(Colors.Grey.Medium);
                });
            });

            var bytes = doc.GeneratePdf();
            return File(bytes, "application/pdf", "Presensia-Fiche-Produit.pdf");
        }
    }
}