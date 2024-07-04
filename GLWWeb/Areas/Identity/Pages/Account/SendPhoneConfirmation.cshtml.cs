using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace GLWWeb.Areas.Identity.Pages.Account
{
    public class SendPhoneConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISmsSender _smsSender;

        public SendPhoneConfirmationModel(UserManager<ApplicationUser> userManager, ISmsSender smsSender)
        {
            _userManager = userManager;
            _smsSender = smsSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid user ID / email.");
                    return Page();
                }

                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Input.PhoneNumber);
                var result = await _userManager.ChangePhoneNumberAsync(user, Input.PhoneNumber, code);

                if (result.Succeeded)
                {
                    // Send SMS confirmation code
                    await _smsSender.SendSmsAsync(Input.PhoneNumber, $"Your phone confirmation code is: {code}");
                    TempData["SmsVerificationCode"] = code;
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
