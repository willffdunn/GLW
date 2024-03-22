using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Utility;

namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeagueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;



        public LeagueController(IUnitOfWork unitOfWork, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _emailStore = GetEmailStore();

        }

        public IActionResult Index()
        {
            List<League> leagueList = _unitOfWork.League.GetAll().OrderBy(p => p.LeagueName).ToList();
            return View(leagueList);
        }
        public IActionResult Upsert(int? LId)
        {
            if (LId == 0 || LId == null)
            {
                League league = new League();
                return View(league);
            }
            else
            {
                League league = _unitOfWork.League.Get(u => u.LId == LId);
                return View(league);
            }
        }
        public IActionResult Browse(int? LId)
        {
            {
                if (LId == null)
                    return NotFound();
            }
            League league = _unitOfWork.League.Get(u => u.LId == LId);
            {
                if (league == null)
                    return NotFound();
            }
            return View(league);
        }
        public IActionResult Delete(int? LId)
        {
            {
                if (LId == null)
                    return NotFound();
            }
            League league = _unitOfWork.League.Get(u => u.LId == LId);
            {
                if (league == null)
                    return NotFound();
            }
            return View(league);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(League obj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("League", "Invalid League Model");
                TempData["Error"] = "Create League failed - Model Error";
                return View(obj);
            }

            League aLeague = _unitOfWork.League.Get(u => u.LId == obj.LId);
            if (aLeague == null)
            {
                _unitOfWork.League.Add(obj);
                _unitOfWork.Save();

                //Add League Contact as first League Member
                League bLeague = _unitOfWork.League.Get(u => u.LeagueName == obj.LeagueName);
                var member = new Member();
                member.Email = bLeague.LContactEmail;
                member.FirstName = bLeague.LContactFName;
                member.LastName = bLeague.LContactLName;
                member.MemberStatus = "Active";
                member.Registered = true;
                member.PreferredNotification = "Both";
                member.EmailConfirmed = false;
                member.FullName = bLeague.LContactFName + " " + bLeague.LContactLName;
                member.Handicap = 0;
                member.MemberTee = "Black";
                member.LId = bLeague.LId;
                member.MemberType = "Golfer";
                member.PhoneNumber = "999.999.9999";
                _unitOfWork.Member.Add(member);
                _unitOfWork.Save();

                Member bMember = _unitOfWork.Member.Get(u =>
                    u.FullName == bLeague.LContactFName + " " + bLeague.LContactLName &
                    u.LId == bLeague.LId);

                if (bMember == null)
                {
                    DeleteLeague(bLeague);

                }
                //Register League Admin
                var user = new ApplicationUser();
                await _userStore.SetUserNameAsync(user, bLeague.LContactEmail, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, bLeague.LContactEmail, CancellationToken.None);
                user.Street = bLeague.LStreet;
                user.FirstName = bLeague.LContactFName;
                user.LastName = bLeague.LContactLName;
                user.Email = bLeague.LContactEmail;
                user.City = bLeague.LCity;
                user.Name = bLeague.LContactFName + " " + bLeague.LContactLName;
                user.State = bLeague.LState;
                user.PostalCode = bLeague.LZip;
                user.PhoneNumber = bLeague.LContactPhoneNumber;
                user.UserName = bLeague.LContactEmail + bLeague.LId;
                var result = await _userManager.CreateAsync(user, bLeague.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_League_Admin);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    DeleteLeague(bLeague);
                }
                League league = _unitOfWork.League.Get(u => u.LeagueName == obj.LeagueName);
                TempData["Success"] = "League processed successfully";
                return View(league);

            }
            else
            {
                _unitOfWork.League.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "League modified successfully";
                return View(obj);
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
        private IActionResult DeleteLeague(League obj)
        {
            League leagueFromDB = _unitOfWork.League.Get(u => u.LId == obj.LId);
            if (leagueFromDB == null)
            {
                return NotFound("");
            }
            _unitOfWork.League.Remove(leagueFromDB);
            _unitOfWork.Save();
            TempData["Error"] = "Create League failed adding League Admin";
            return View(obj);
        }

        #region API CALLS

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(League obj)
        {
            League league = _unitOfWork.League.Get(u => u.LId == obj.LId);
            if (league == null)
            {
                return NotFound("");
            }
            _unitOfWork.League.Remove(league);
            _unitOfWork.Save();
            TempData["Success"] = "League deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
#endregion