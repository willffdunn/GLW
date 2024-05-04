// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;

namespace GLWWeb.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [ValidateNever]
            public string UserName { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
            public int? LId { get; set; }
            //
            public string? LeagueName { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> LeagueList { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                Input.UserName = Input.Email;
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var currentUser = await _userManager.FindByNameAsync(Input.Email);
                ApplicationUser au = currentUser;

                if (result.Succeeded)
                {
                    League league = _unitOfWork.League.Get(u => u.LId == Input.LId);
                    if (league != null)
                    {
                        SD.LeagueName = league.LeagueName;
                    }
                    else
                    {
                        SD.LeagueName = "Sample League";
                    }

                    SD.LeagueId = Convert.ToInt32(Input.LId);
                    SD.UserFirstName = au.FirstName;
                    SD.UserLastName = au.LastName;
                    SD.Email = au.Email;

                    _logger.LogInformation("User logged in.");
                    TempData["Success"] = "User logged in successfully";
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)

                {
                    TempData["Info"] = "User account locked out.";
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    if (au == null & Input.LId != null)
                    {
                        TempData["Info"] = "Member is not registered with league";
                        return RedirectToPage("./Register");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public IActionResult OnGetGolfLeagues(string userEmail)
        {
            List<League> lList = _unitOfWork.League.GetAll().ToList();
            List<Member> mList = _unitOfWork.Member.GetAll(includeProperties: "League").OrderBy(p => p.Email).ToList();
            List<LeagueListVM> leagueListVMs = new List<LeagueListVM>();
            if (mList != null)
            {
                List<Member> memberLeague = mList.Where(u => u.Email == userEmail).ToList();
                foreach (Member lM in memberLeague)
                {
                    LeagueListVM leagueListVM = new LeagueListVM();
                    League league = lList.Where(u => u.LId == lM.LId).FirstOrDefault();
                    leagueListVM.LId = lM.LId;
                    leagueListVM.LeagueName = league.LeagueName;
                    leagueListVMs.Add(leagueListVM);
                }
                return new JsonResult(leagueListVMs);
            }
            return new JsonResult(leagueListVMs);
        }

    }
}
