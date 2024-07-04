// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;


namespace GLWWeb.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public string FirstName { get; set; }
            public string LastName { get; set; }
            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }
            public string EmailConfirmed { get; set; }
            public string PhoneConfirmed { get; set; }
            [Phone]
            [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
                ErrorMessage = "Invalid phone number.")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public bool Registered { get; set; }
            public string PreferredNotification { get; set; }
            public string MemberStatus { get; set; }
            public string MemberPlan { get; set; }
            public string MemberType { get; set; }
            public string MemberTee { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var currentUser = await _userManager.GetUserAsync(User);
            ApplicationUser appUser = currentUser;
            Member member = _unitOfWork.Member.Get(u => u.Email == SD.Email & u.LId == SD.LeagueId);
            Username = userName;
            Input = new InputModel();
            Input.FirstName = appUser.FirstName;
            Input.LastName = appUser.LastName;
            Input.Email = appUser.UserName;
            if (appUser.EmailConfirmed == true)
            {
                Input.EmailConfirmed = "Yes";
            }
            else
            {
                Input.EmailConfirmed = "No";
            }
            if (appUser.PhoneNumberConfirmed == true)
            {
                Input.PhoneConfirmed = "Yes";
            }
            else
            {
                Input.PhoneConfirmed = "No";
            }
            Input.PhoneNumber = phoneNumber;
            Input.Street = appUser.Street;
            Input.City = appUser.City;
            Input.State = appUser.State;
            Input.PostalCode = appUser.PostalCode;
 //         Input.MemberPlan = member.MemberPlan;
            Input.MemberType = member.MemberType;
            Input.MemberTee = member.MemberTee;
            Input.MemberStatus = member.MemberStatus;
            Input.PreferredNotification = member.PreferredNotification;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                TempData["info"] = "Update failed - Invalid Model";
                await LoadAsync(user);
                return Page();
            }


            user.Street = Input.Street;
            user.City = Input.City;
            user.State = Input.State;
            user.PostalCode = Input.PostalCode;
            user.PhoneNumber = Input.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                Member member = _unitOfWork.Member.Get(u => u.Email == SD.Email & u.LId == SD.LeagueId);

                member.FirstName = Input.FirstName;
                member.LastName = Input.LastName;
                member.PhoneNumber = Input.PhoneNumber;
                member.PreferredNotification = Input.PreferredNotification;
                member.MemberTee = Input.MemberTee;
                member.MemberType = Input.MemberType;
                member.MemberStatus = Input.MemberStatus;
                member.FullName = Input.FirstName + " " + Input.LastName;
                _unitOfWork.Member.Update(member);
                _unitOfWork.Save();

                Input.MemberPlan = member.MemberPlan;
                if (user.EmailConfirmed == true)
                {
                    Input.EmailConfirmed = "Yes";
                }
                else
                {
                    Input.EmailConfirmed = "No";
                }
                if (user.PhoneNumberConfirmed == true)
                {
                    Input.PhoneConfirmed = "Yes";
                }
                else
                {
                    Input.PhoneConfirmed = "No";
                }
                await _signInManager.RefreshSignInAsync(user);
                TempData["info"] = "Your profile has been updated";

                return RedirectToPage();
            }
            foreach (var error in result.Errors)
            {
                TempData["info"] = "Update failed - Invalid Post";
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return Page();


        }
    }
}
