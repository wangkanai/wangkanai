using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Areas.Detection.Controllers
{
    [Area("Detection")]
    public class PreferenceController : Controller
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public IActionResult Index()
        {
            return Content("Preference");
        }
        
        // GET
        public IActionResult Prefer(string returnUrl = null)
        {
            _preferenceService.Set(Device.Desktop);

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return Redirect("/");
        }
        
        // GET
        public IActionResult Clear(string returnUrl = null)
        {
            _preferenceService.Clear();
            
            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return Redirect("/");
        }
    }
}