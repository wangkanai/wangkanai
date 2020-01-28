using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.UI.Pages.Internal
{
    //[ResponsiveDefaultUI(typeof(PreferModel))]
    public class PreferModel : PageModel
    {
        private readonly IPreferenceService _preferenceService;

        public PreferModel(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            _preferenceService.Set(Device.Mobile);

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToPage();
        }
    }
}