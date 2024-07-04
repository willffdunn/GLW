// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace GLWWeb.Areas.Identity.Pages.Account
{
    public class LeagueRegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public LeagueRegisterModel(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            Input = new InputModel(); // Initialize Input here

        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress]
            public string Email { get; set; }
            public int LId { get; set; }
            public string LeagueName { get; set; }
            public string MemberPlan { get; set; }

        }
        public void OnGet(int LId, string Email)
        {
            Input.LId = LId;
            Input.Email = Email;

            League aLeague = _unitOfWork.League.Get(u => u.LId == Input.LId);
            if (aLeague == null)
            {
                TempData["info"] = "League not found";
                return;
            }
            Input.LeagueName = aLeague.LeagueName;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string userName = Input.Email;
                Member member = _unitOfWork.Member.Get(u => u.Email == Input.Email &
                u.LId == Input.LId);

                if (member == null)
                {
                    TempData["info"] = "Member not found in League";
                    return Page();
                }

                if (Input.MemberPlan == null)
                {
                    TempData["info"] = "Select Member Plan";
                    return Page();
                }

                member.MemberPlan = Input.MemberPlan;
                member.Registered = true;

                _unitOfWork.Member.Update(member);
                _unitOfWork.Save();
                TempData["Success"] = "Member Plan updated successfully";
                return RedirectToPage("./Login");


            }
            TempData["Info"] = "Member Plan update model error";
            return Page();
        }
    }
}
