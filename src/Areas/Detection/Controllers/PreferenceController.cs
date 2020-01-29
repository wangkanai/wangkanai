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
        
        // GET
        public IActionResult Prefer(string returnUrl = null)
        {
            _preferenceService.Set(Device.Mobile);

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToRoute("/");
        }
        
        // GET
        public IActionResult Clear(string returnUrl = null)
        {
            _preferenceService.Clear();
            
            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToRoute("/");
        }
    }
}