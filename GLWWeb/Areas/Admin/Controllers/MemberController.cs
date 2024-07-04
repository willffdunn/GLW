using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;
namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Member> objMemberList = _unitOfWork.Member.GetAll().OrderBy(p => p.LastName).ToList();
            if (SD.LeagueId != 0)
            {
                List<Member> leagueList = objMemberList.Where(p => p.LId == Convert.ToInt32(SD.LeagueId)).ToList(); ;
                return View(leagueList);
            }
            else
            {
                TempData["Message"] = "League not selected - LogIn";
                return Redirect("/Identity/Account/Login");

            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member obj)

        {

            obj.MemberPlan = "Not Registered";
            obj.LId = SD.LeagueId;

            Member _obj = _unitOfWork.Member.Get(u => u.FullName == obj.FullName &
            u.LId == SD.LeagueId);
            if (_obj == null)
            {
                _unitOfWork.Member.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Member added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("LastName", "Duplicate names are not allowed.");
                return View("Create");
            }

        }
        public IActionResult Edit(int? memberId)
        {
            {
                if (memberId == null)
                    return NotFound();
            }
            Member memberFromDB = _unitOfWork.Member.Get(u => u.MemberId == memberId);
            {
                if (memberFromDB == null)
                    return NotFound();
            }
            return View(memberFromDB);
        }
        public IActionResult Browse(int? memberId)
        {
            {
                if (memberId == null)
                    return NotFound();
            }
            Member memberFromDB = _unitOfWork.Member.Get(u => u.MemberId == memberId);
            {
                if (memberFromDB == null)
                    return NotFound();
            }
            return View(memberFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Member obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            {
                obj.FullName = obj.FirstName + " " + obj.LastName;
                _unitOfWork.Member.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Member data edited successfully";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int? memberId)
        {
            {
                if (memberId == null)
                    return NotFound();
            }
            Member memberFromDB = _unitOfWork.Member.Get(u => u.MemberId == memberId);
            {
                if (memberFromDB == null)
                    return NotFound();
            }
            return View(memberFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(Member obj)
        {
            Member memberFromDB = _unitOfWork.Member.Get(u => u.MemberId == obj.MemberId);
            if (memberFromDB == null)
            {
                return NotFound("");
            }
            _unitOfWork.Member.Remove(memberFromDB);
            _unitOfWork.Save();
            TempData["Success"] = "Member deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

