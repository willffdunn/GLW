// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web.Http;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Models;
using Newtonsoft.Json;
using NuGet.LibraryModel;
using Utility;
namespace GLWWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly Dictionary<string, ApplicationUser> _userDictionary = new Dictionary<string, ApplicationUser>();


        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public int? LId { get; set; }
            public LeagueListVM LeagueListVM { get; set; }
            public int? SelectedLeagueId { get; set; }
        }

        public IActionResult OnGet()
        {
            // Optionally, perform any necessary logic here before rendering the view

            return Page(); // Return the view associated with this page
        }
        public IActionResult OnPost(string provider, string returnUrl = null)
        {

           if (ModelState.IsValid)
            {
                // Store the input in TempData to pass it to the callback page
                TempData["Input"] = JsonConvert.SerializeObject(Input);

                // Request a redirect to the external login provider.
                var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl, ModelState });
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return new ChallengeResult(provider, properties);
            }
            else
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                TempData["Info"] = "Email is required";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });


        }
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            // Retrieve the input from TempData
            var inputJson = TempData["Input"] as string;
            if (string.IsNullOrEmpty(inputJson))
            {
                ErrorMessage = "Input data not found.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Deserialize the input
            Input = JsonConvert.DeserializeObject<InputModel>(inputJson);
            if (Input == null)
            {
                ErrorMessage = "Failed to deserialize input data.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Use the input as needed
            int? LId = Input.LId;
            if (LId == null || LId == 0)
            {
                ErrorMessage = "League Id is null or 0.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Use the email provided in the login page
            var email = Input.Email;
            if (string.IsNullOrEmpty(email))
            {
                ErrorMessage = "Email not provided.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Find the user by email address
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                ErrorMessage = "User not found.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var claimsEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (claimsEmail == null)
            {
                ErrorMessage = "Email claim not found.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            if (claimsEmail != email)
            {
                ErrorMessage = "Input email does not match browser session email.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Find the external login for the user
            var existingLogin = await _userManager.FindByLoginAsync(info.LoginProvider, user.Id);
            if (existingLogin == null)
            {
                await _userManager.AddLoginAsync(user, info);
            }

            // Get the league details
            LeagueListVM leagueListVM = await GetGolfLeague((int)LId, email);

            // Set the necessary data
            SD.LeagueId = leagueListVM.LId;
            SD.LeagueName = leagueListVM.LeagueName;
            SD.UserFirstName = leagueListVM.FirstName;
            SD.UserLastName = leagueListVM.LastName;
            SD.Email = email;

            // Sign in the user
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

                if (!result.Succeeded)
                {
                    ErrorMessage = "The external login was not added. External logins can only be associated with one account.";
                    return RedirectToPage();
                }

                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            ErrorMessage = "The external login was not added";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId, code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<LeagueListVM> GetGolfLeague(int LId, string email)
        {
            League league = _unitOfWork.League.Get(p => p.LId == LId);
            Member lM = _unitOfWork.Member.Get(p => p.Email == email & p.LId == LId);
            LeagueListVM leagueListVM = new LeagueListVM();
            leagueListVM.FirstName = lM.FirstName;
            leagueListVM.LastName = lM.LastName;
            leagueListVM.Registered = lM.Registered;
            leagueListVM.PhoneNumber = lM.PhoneNumber;
            leagueListVM.LId = lM.LId;
            leagueListVM.LeagueName = league.LeagueName;
            return leagueListVM;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}


