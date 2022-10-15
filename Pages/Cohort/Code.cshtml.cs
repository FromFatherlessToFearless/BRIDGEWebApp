using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace BRIDGEWebApp.Pages.Cohort
{
    public class CodeModel : PageModel
    {
        private readonly BRIDGEWebApp.Data.ApplicationDbContext _context;

        public CodeModel(BRIDGEWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Data.Models.Cohort Cohort { get; set; }

        public async Task<IActionResult> OnGetAsync(int? cohortId)
        {
            if (cohortId == null || _context.Cohorts == null)
            {
                return NotFound();
            }

            var cohort = await _context.Cohorts.FirstOrDefaultAsync(m => m.Id == cohortId);

            if (cohort == null)
            {
                return NotFound();
            }
            else
            {
                Cohort = cohort;
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            var imgType = Base64QRCode.ImageType.Png;
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
            ViewData["QRCodeImage"] = $"data:image/{imgType.ToString().ToLower()};base64," + qrCodeImageAsBase64;


          
            
            return Page();
        }
    }
}
