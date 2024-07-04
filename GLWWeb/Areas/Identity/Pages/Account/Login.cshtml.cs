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

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [ValidateNever]
            public string UserName { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
            public int? LId { get; set; }

            public string? LeagueName { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> LeagueList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            // Clear model state to remove any previously bound values
            ModelState.Clear();

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Initialize Input to refresh its data
            Input = new InputModel
            {
                Email = string.Empty,
                Password = string.Empty,
                RememberMe = false,
                LId = null,
                LeagueName = null,
                LeagueList = Enumerable.Empty<SelectListItem>()
            };

            returnUrl ??= Url.Content("~/");

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
                Input.UserName = Input.Email;
                ApplicationUser au = await _userManager.FindByNameAsync(Input.Email);
                Member member = _unitOfWork.Member.Get(p => p.Email == Input.Email && p.LId == Input.LId);
                if (au == null)
                {
                    TempData["Info"] = "Member is not registered";
                    return RedirectToPage("./Register");
                }
                if (member != null)
                {
                    if (!member.Registered)
                    {
                        TempData["Info"] = "Member is not registered with league";
                        return RedirectToPage("./LeagueRegister", new { LId = Input.LId, Email = Input.Email });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt - League Member does not exist.");
                    return Page();
                }
                if (!au.EmailConfirmed)
                {
                    TempData["Info"] = "Member email not confirmed";
                    return RedirectToPage("./ResendEmailConfirmation");
                }
                if (!au.PhoneNumberConfirmed)
                {
                    TempData["Info"] = "Member phone number not confirmed. Member will not receive text messages until phone number confirmed.";
                }

                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

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
                    SD.MemberPlan = member.MemberPlan;

                    _logger.LogInformation(Input.Email + " logged in.");
                    TempData["Success"] = Input.Email + " logged in successfully";
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
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
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
                    LeagueListVM leagueListVM = new LeagueListVM
                    {
                        LId = lM.LId,
                        LeagueName = lList.Where(u => u.LId == lM.LId).FirstOrDefault()?.LeagueName
                    };
                    leagueListVMs.Add(leagueListVM);
                }
                return new JsonResult(leagueListVMs);
            }
            return new JsonResult(leagueListVMs);
        }
    }
}
