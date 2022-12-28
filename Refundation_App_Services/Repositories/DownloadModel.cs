using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components;

namespace Refuntations_App.Pages
{
    public class DownloadModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        [Inject]
        private NavigationManager _navigation { get; set; }
        public DownloadModel(IWebHostEnvironment env)
        {
            _env = env;
     
        }
        public IActionResult OnGet(string fileName)
        {
            if (fileName.Contains("?"))
                return Redirect("/sifarnici");
            else
            {
                fileName = fileName.Replace("?", "");
                var filePath = Path.Combine(_env.ContentRootPath, "wwwroot", fileName);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/force-download", fileName);
            }
            
            
           
        }
 

    }
}
