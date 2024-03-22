using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.UI.Services;
using Models;

namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GolfPlayerController : Controller
    {
        private readonly IEmailSender _emailSender;

        private readonly IUnitOfWork _unitOfWork;
        public GolfPlayerController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;

            _emailSender = emailSender;
        }

        public IActionResult IndexR(int? GRId)
        {
            if (GRId == 0 || GRId == null)
            {
                TempData["Info"] = "Golf Round Id is 0";
                return View();
            }
            int gRId = (int)GRId;

            List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();
            if (objGGList == null)
            {
                TempData["Info"] = "No Golf Groups";
                return View();
            }

            List<GolfPlayer> objGPList = _unitOfWork.GolfPlayer.GetAll
                (includeProperties: "GolfGroup").OrderBy(p => p.GGId).ToList();
            if (objGPList == null)
            {
                TempData["Info"] = "No Golf Players";
                return View();
            }
            List<GolfPlayer> gpQuery = objGPList.Where
                (p => p.GolfGroup.GRId == gRId).ToList();
            if (gpQuery == null)
            {
                TempData["Info"] = "Group has no Players";
                return View();
            }
            GolfGroup ggg = _unitOfWork.GolfGroup.Get(u => u.GRId == gRId);
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == ggg.GRId,
                includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;

            return View(gpQuery);
        }
        public IActionResult Index(int? GGId)
        {
            if (GGId == 0 || GGId == null)
            {
                TempData["Info"] = "Golf Group Id is 0";
                return View();
            }

            List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();
            if (objGGList == null)
            {
                TempData["Info"] = "No Golf Groups";
                return View();
            }

            List<GolfPlayer> objGPList = _unitOfWork.GolfPlayer.GetAll
                (includeProperties: "GolfGroup").OrderBy(p => p.GGId).ToList();
            if (objGPList == null)
            {
                TempData["Info"] = "No Golf Players";
                return View();
            }
            List<GolfPlayer> gpQuery = objGPList.Where
                (p => p.GolfGroup.GGId == GGId).ToList();
            if (gpQuery == null)
            {
                TempData["Info"] = "Group has no Players";
                return View();
            }
            GolfGroup ggg = _unitOfWork.GolfGroup.Get(u => u.GGId == GGId);
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == ggg.GRId,
                includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;

            return View(gpQuery);
        }
        public IActionResult Index2(int? GRId, int? GGId)
        {
            if (GRId == 0 || GRId == null)
            {
                TempData["Info"] = "Golf Round Id is 0";
                return View();
            }
            int gRId = GRId.Value;
            List<Member> objMList = _unitOfWork.Member.GetAll().OrderBy(p => p.FullName).ToList();

            if (objMList == null)
            {
                TempData["Info"] = "No Members";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            List<GolfPlayer> objGPList = _unitOfWork.GolfPlayer.GetAll
                (includeProperties: "GolfGroup").OrderBy(p => p.GGId).ToList();
            if (objGPList == null)
            {
                TempData["Info"] = "No Golf Players";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            List<GolfPlayer> gpQuery = objGPList.Where(p => p.GolfGroup.GRId == GRId).ToList();
            if (gpQuery == null)
            {
                TempData["Info"] = "Round has no Players";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            List<Member> nPlayer = objMList.Where(m => !gpQuery.Any(d => d.MemberId == m.MemberId)).ToList();
            if (nPlayer == null || nPlayer.Count == 0)
            {
                TempData["Info"] = "Every Member has been selected";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();
            if (objGGList == null)
            {
                TempData["Info"] = "No Golf Groups";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            GolfGroup gGroup = _unitOfWork.GolfGroup.Get(u => u.GRId == GRId);
            if (gGroup == null)
            {
                TempData["Info"] = "Round has no Groups";
                return RedirectToAction("Index", new { GRId = gRId });
            }
            IEnumerable<SelectListItem> GroupList = objGGList.Where(p => p.GRId == gGroup.GRId).Select(u => new SelectListItem
            {
                Text = u.GGName + " " + u.GGTtime.ToString(),
                Value = u.GGId.ToString(),
            });
            ViewBag.GroupList = GroupList;
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId,
                        includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;


            List<GolfPlayerVM> gpVMs = new List<GolfPlayerVM>();
            foreach (var member in nPlayer)
            {
                GolfPlayerVM gpVM = new GolfPlayerVM();
                gpVM.Member = member;
                gpVM.GGId = 0;
                gpVM.GRId = golfRound.GRId;
                gpVMs.Add(gpVM);

            }
            return View(gpVMs);
        }
        public IActionResult Upsert(int? GPId, int? GRId)
        {
            if (GRId == 0 || GRId == null)
            {
                TempData["Info"] = "Round Id invalid";
                return View();
            }
            else
            {
                IEnumerable<SelectListItem> MemberList = _unitOfWork.Member.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.MemberId.ToString()
                });

                ViewBag.MemberList = MemberList;

                List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                    (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();
                if (objGGList == null)
                {
                    TempData["Info"] = "No Golf Groups";
                    return View();
                }

                IEnumerable<SelectListItem> GroupList = objGGList.Where(p => p.GRId == GRId).Select(u => new SelectListItem
                {
                    Text = u.GGName,
                    Value = u.GGId.ToString(),
                });

                ViewBag.GroupList = GroupList;
            }

            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId,
                includeProperties: "GolfCourse");


            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;



            if (GPId == 0 || GPId == null)
            {
                GolfPlayer GolfPlayer = new GolfPlayer();
                ViewBag.GGId = 0;
                return View(GolfPlayer);
            }
            else
            {

                GolfPlayer golfPlayer = _unitOfWork.GolfPlayer.Get(u => u.GPId == GPId, includeProperties: "GolfGroup");
                ViewBag.GGId = golfPlayer.GGId;
                return View(golfPlayer);
            }

        }
        [HttpPost]
        public IActionResult Upsert(GolfPlayer obj, int? GRId)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "GolfPlayer data ModelState Invalid";
                return View();
            }
            GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == obj.GGId);
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId,
                includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;
            ViewBag.GGName = golfGroup.GGName;
            ViewBag.GGTtime = golfGroup.GGTtime;
            ViewBag.GGId = golfGroup.GGId;

            if (obj.GPId == 0)
            {
                _unitOfWork.GolfPlayer.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Player added";



                return RedirectToAction("Index", new { golfGroup.GGId });
            }
            else
            {
                _unitOfWork.GolfPlayer.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Player updated";

                return RedirectToAction("Index", new { golfRound.GRId });
            }
        }
        [HttpPost]
        public IActionResult Index2(List<GolfPlayerVM> obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "GolfPlayer data ModelState Invalid";
                return View();
            }

            List<GolfPlayerVM> gpVMs = obj;

            if (gpVMs == null)
            {
                TempData["Info"] = "GolfPlayer List Null";
                return View();
            }
            int gRId = gpVMs[0].GRId;

            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == gRId,
                includeProperties: "GolfCourse");
            string GCName = golfRound.GolfCourse.GCName;
            string GRDate = golfRound.GRDate.ToShortDateString();


            foreach (var member in gpVMs)
            {
                if (member.GGId != 0)
                {

                    GolfPlayer gp = new GolfPlayer();
                    gp.MemberId = member.Member.MemberId;
                    gp.GPType = "Member";
                    gp.GGId = member.GGId;
                    gp.GPFName = member.Member.FirstName;
                    gp.GPLName = member.Member.LastName;
                    _unitOfWork.GolfPlayer.Add(gp);
                    _unitOfWork.Save();

                    // After adding the player, send a confirmation email
                    if (golfRound.GCId == 99)
                    {
                        GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == member.GGId);
                        string GGName = golfGroup.GGName;
                        TimeSpan GGTime = golfGroup.GGTtime;
                        var recipientEmail = member.Member.Email; // The email of the newly added member
                        var subject = "Golf Round Scheduled " + GRDate + " at " + GCName;
                        var message = "You are scheduled to Tee Off at " + GGTime + " in " + GGName;

                        _emailSender.SendEmailAsync(recipientEmail, subject, message);

                        string accountSid = "AC37f20763ac3b293f908e1e0290b3dcd1";
                        string authToken = "c4937d899e21f3e4df66b1d65c91eccb";

                        //TwilioClient.Init(accountSid, authToken);

                        //var amessage = MessageResource.Create(
                        //  body: "Golf Round Scheduled " + GRDate + " at " + GCName +
                        // "You are scheduled to Tee Off at " + GGTime + " in " + GGName,
                        //from: new Twilio.Types.PhoneNumber("+18445130861"),
                        //to: new Twilio.Types.PhoneNumber("+14848857000")
                        //);
                        //Console.WriteLine(amessage.Sid);
                    }
                }


            }
            TempData["Success"] = "GolfPlayer added successfully";


            return RedirectToAction("IndexR", new { GRId = gRId });
        }

        public IActionResult Delete(int? GPId)
        {
            {
                if (GPId == null)
                    return NotFound();
            }
            GolfPlayer golfPlayer = _unitOfWork.GolfPlayer.Get(u => u.GPId == GPId, includeProperties: "Member");
            {
                if (golfPlayer == null)
                    return NotFound();
            }
            GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == golfPlayer.GGId);
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId ==
                   golfGroup.GRId, includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GGId = golfGroup.GGId;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GGName = golfGroup.GGName;
            ViewBag.GGTtime = golfGroup.GGTtime;
            return View(golfPlayer);
        }
        #region API CALLS

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(GolfPlayer obj)
        {
            GolfPlayer golfPlayer = _unitOfWork.GolfPlayer.Get(u => u.GPId == obj.GPId,
            includeProperties: "GolfGroup");
            if (golfPlayer == null)
            {
                return Json(new { success = false, message = "Golf Player not found" });
            }
            _unitOfWork.GolfPlayer.Remove(golfPlayer);
            _unitOfWork.Save();
            TempData["Success"] = "GolfPlayer deleted";
            return RedirectToAction("Index", new { obj.GGId });
        }

    }
}

#endregion