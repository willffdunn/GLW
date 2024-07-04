using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Models;

namespace GLWWeb.Areas.Identity.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterConfirmationModel> _logger;

        public RegisterConfirmationModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterConfirmationModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Verification Code")]
            public string SmsVerificationCode { get; set; }
        }

        public void OnGet(string email = null)
        {
            Input = new();
            Input = new InputModel
            {
                Email = email
            };
        }

        public async Task<IActionResult> OnPostVerifyPhoneNumberAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ApplicationUser au = await _userManager.FindByEmailAsync(Input.Email);
            if (au == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid user ID.");
                return Page();
            }

            var expectedCode = Input.SmsVerificationCode?.ToString();
            if (expectedCode == Input.SmsVerificationCode)
            {
                au.PhoneNumberConfirmed = true;
                var result = await _userManager.UpdateAsync(au);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(au, isPersistent: false);
                    return RedirectToPage("RegisterConfirmationSuccess");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid verification code.");
            }

            return Page();
        }
    }
}
