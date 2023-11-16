using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using static DataAccess.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using Models;
using Humanizer;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System;
using TBGC.Models;

namespace TBGCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GolfGroupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GolfGroupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index(int GRId)
        {
            int _gRId = GRId;
            if (_gRId == 0)
            {
                TempData["Info"] = "Golf Round ID Equals 0";
                return View();

            }
            List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();

            if (objGGList == null)
            {
                return View();
            }
            List<GolfGroup> ggQuery = objGGList.Where(p => p.GRId == _gRId).ToList();
            {
                if (ggQuery.Count == 0)
                {
                    TempData["Info"] = "Golf Round Has no Golf Groups";
                    return RedirectToAction("Upsert", new { gRId = _gRId });
                }
                GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == _gRId);
                GolfCourse golfCourse = _unitOfWork.GolfCourse.Get(u => u.GCId == golfRound.GCId);
                ViewBag.GCName = golfCourse.GCName;
                ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
                return View(ggQuery);
            }
        }
        public IActionResult Index2(int GGId )
        {
            if (GGId == 0 )
            {
                TempData["Info"] = "Golf Group Id is 0";
                return View();
            }
            int gGId = GGId;
            List<Member> objMList = _unitOfWork.Member.GetAll().OrderBy(p => p.FullName).ToList();

            if (objMList == null)
            {
                TempData["Info"] = "No Members";
                return RedirectToAction("Index", new { GGId = gGId });
            }
            List<GolfPlayer> objGPList = _unitOfWork.GolfPlayer.GetAll
                (includeProperties: "GolfGroup").OrderBy(p => p.GGId).ToList();
            if (objGPList == null)
            {
                TempData["Info"] = "No Golf Players";
                return RedirectToAction("Index", new { GGId = gGId });
            }
            List<GolfPlayer> gpQuery = objGPList.Where(p => p.GolfGroup.GGId == gGId).ToList();
            if (gpQuery == null)
            {
                TempData["Info"] = "Group has no Players";
                return RedirectToAction("Index", new { GGId = gGId });
            }
            List<Member> nPlayer = objMList.Where(m => !gpQuery.Any(d => d.MemberId == m.MemberId)).ToList();
            if (nPlayer == null || nPlayer.Count == 0)
            {
                TempData["Info"] = "Every Member has been selected";
                return RedirectToAction("Index", new { GGId = gGId });
            }
            List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();
            if (objGGList == null)
            {
                TempData["Info"] = "No Golf Groups";
                return RedirectToAction("Index", new { GRId = gGId });
            }
            GolfGroup gGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == gGId);
            if (gGroup == null)
            {
                TempData["Info"] = "Round has no Groups";
                return RedirectToAction("Index", new { GGId = gGId });
            }

            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == gGroup.GRId,
                        includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;
            List<GolfGroupVM> ggVMs = new List<GolfGroupVM>();
            foreach (GolfPlayer gp in gpQuery)
            {
                List<GolfPlayerVM> gpVMs = new List<GolfPlayerVM>();
                foreach (var member in nPlayer)
                {
                    GolfPlayerVM ggVM = new GolfPlayerVM();
                    ggVM.Member = member;
                    ggVM.GGId = 0;
                    ggVM.GRId = golfRound.GRId;


                }
            }
            return View(ggVMs);
        }
        public IActionResult Upsert(int? GRId, int? GGId)
        {
            if (GRId == 0 || GRId == null)
            {
                TempData["Info"] = "Golf Round Id is 0";
                return View();
            }
            if (GGId == 0 || GGId == null)
            {
                GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId, includeProperties: "GolfCourse");
                ViewBag.GCName = golfRound.GolfCourse.GCName;
                ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
                GolfGroup golfGroup = new GolfGroup();
                golfGroup.GRId = golfRound.GRId;
                return View(golfGroup);
            }
            else
            {
                GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId, includeProperties: "GolfCourse");
                GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == GGId, includeProperties: "GolfRound");
                ViewBag.GCName = golfRound.GolfCourse.GCName;
                ViewBag.GRDate = golfGroup.GolfRound.GRDate.ToShortDateString(); 
                return View(golfGroup);
            }
        }
        public IActionResult Edit(int GGId)
        {
            if (GGId == 0)
            {
                TempData["Info"] = "Golf Group is null";
                return View();
            }
            else
            {
                GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == GGId, includeProperties: "GolfRound");
                GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == 
                        golfGroup.GolfRound.GRId, includeProperties: "GolfCourse");
                ViewBag.GCName = golfRound.GolfCourse.GCName;
                ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
                return View(golfGroup);
            }
        }
        public IActionResult Browse(int? GGId)
        {
            {
                if (GGId == null)
                    return NotFound();
            }
            GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == GGId);
            {
                if (golfGroup == null)
                    return NotFound();
            }

            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == golfGroup.GRId,
                includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;

            return View(golfGroup);
        }
        public IActionResult Delete(int? GGId)
        {
            {
                if (GGId == null)
                    return NotFound();
            }
            GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == GGId);
            {
                if (golfGroup == null)
                    return NotFound();
            }

            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == golfGroup.GRId,
                includeProperties: "GolfCourse");
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            ViewBag.GRId = golfRound.GRId;

            return View(golfGroup);
        }
        [HttpPost]
        public IActionResult Upsert(GolfGroup obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "GolfGroup data create failed";
                return View();
            }

            GolfGroup _obj = _unitOfWork.GolfGroup.Get(u => u.GGId == obj.GGId, includeProperties: "GolfRound");

            if (_obj== null)
            {
                _unitOfWork.GolfGroup.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "GolfGroup added successfully";
                GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId ==
                        obj.GRId, includeProperties: "GolfCourse");
                ViewBag.GCName = golfRound.GolfCourse.GCName;
                ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
                return RedirectToAction("Index", new { GRId = obj.GRId });
            }
            else
            {
                _unitOfWork.GolfGroup.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "GolfGroup edited";

                List<GolfGroup> objGGList = _unitOfWork.GolfGroup.GetAll
                (includeProperties: "GolfRound").OrderBy(p => p.GGId).ToList();

                if (objGGList == null)
                {
                    return View();
                }
                return RedirectToAction("Index", new { GRId = obj.GRId });
            }
        }
        

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(GolfGroup obj)
        {
            GolfGroup golfGroup = _unitOfWork.GolfGroup.Get(u => u.GGId == obj.GGId);
            if (golfGroup == null)
            {
                return NotFound("");
            }
            _unitOfWork.GolfGroup.Remove(golfGroup);
            _unitOfWork.Save();
            TempData["Success"] = "GolfGroup deleted successfully";
            return RedirectToAction("Index", new { GRId = golfGroup.GRId });
        }
    }
}

