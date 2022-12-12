using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Refuntations_App.Pages
{
    public class DownloadModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public DownloadModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult OnGet(string fileName)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot", fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", fileName);
        }
    
    }
}
