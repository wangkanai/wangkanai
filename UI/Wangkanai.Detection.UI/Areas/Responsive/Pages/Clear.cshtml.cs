using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.UI.Pages.Internal
{
    //[ResponsiveDefaultUI(typeof(ClearModel))]
    public class ClearModel : PageModel
    {
        private readonly IPreferenceService _preferenceService;

        public ClearModel(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            _preferenceService.Clear();

            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return RedirectToPage();
        }
    }
}