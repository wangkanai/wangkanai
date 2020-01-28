using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wangkanai.Detection.Hosting;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Pages.Internal
{
    [ResponsiveDefaultUI(typeof(PreferModel))]
    public class PreferModel : PageModel
    {
        private readonly  IPreferenceService _preferenceService;
        public            string             ReturnUrl    { get; set; }
        [TempData] public string             ErrorMessage { get; set; }

        public PreferModel(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public void OnGet(string returnUrl = null)
        {
            if(!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl = returnUrl ?? Url.Content("~/");

            _preferenceService.Clear();
            
            ReturnUrl = returnUrl;
        }

        public IActionResult OnPost(Device prefer, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                _preferenceService.Set(prefer);
                return LocalRedirect(returnUrl);
            }

            return Page();
        }
    }
}