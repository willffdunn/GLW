// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GLWWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SelectLeagueModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SelectLeagueModel> _logger;
        private readonly IEmailSender _emailSender;

        public SelectLeagueModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<SelectLeagueModel> logger,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public List<LeagueListVM> LeagueListVMs { get; set; }

            [Required(ErrorMessage = "Please select a league.")]
            public int SelectedLeagueId { get; set; }
        }

        public void OnGet(string leagueListJson)
        {
            if (!string.IsNullOrEmpty(leagueListJson))
            {
                Input = new InputModel();
                Input.LeagueListVMs = JsonConvert.DeserializeObject<List<LeagueListVM>>(leagueListJson);
            }
        }
        public IActionResult OnPost()
        {
            // Validate SelectedLeagueId
            if (Input == null || Input.SelectedLeagueId == 0)
            {
                // Redirect back to the same page with an error message if no league is selected
                TempData["ErrorMessage"] = "Please select a league.";
                return RedirectToPage();
            }
            var selectedLeagueId = Input.SelectedLeagueId;

            if (selectedLeagueId == 0)
            {
                // Handle the error or return an appropriate response
                return NotFound(); // Example: return a 404 Not Found response
            }
            return RedirectToPage("ExternalLogin", pageHandler: "Callback", new { selectedLeagueId = selectedLeagueId });
        }

    }
}

