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

namespace TBGCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GolfRoundController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GolfRoundController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<GolfRound> objGRList = _unitOfWork.GolfRound.GetAll
                (includeProperties: "GolfCourse").OrderBy(p => p.GCId).ToList();
            if (objGRList == null)
            {
                TempData["Success"] = "Golf Round Returned Null";
                return View();

            }
         
            return View(objGRList);
        }  


        public IActionResult Upsert(int? GRId)
        {
            if (GRId == 0 || GRId == null)
            {
                IEnumerable<SelectListItem> GolfCourseList = _unitOfWork.GolfCourse.GetAll().Select(u => new SelectListItem
                {
                    Text = u.GCName,
                    Value = u.GCId.ToString()
                });

                ViewBag.GolfCourseList = GolfCourseList;
                GolfRound golfRound = new GolfRound();
                return View(golfRound);
            }
            else
            {
                GolfRound uGolfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId, includeProperties: "GolfCourse");
                return View(uGolfRound);
            }
        }

        [HttpPost]
        public IActionResult Upsert(GolfRound obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "GolfRound data create failed";
                return View();
            }
            {
                GolfRound _obj = _unitOfWork.GolfRound.Get(u => u.GRId == obj.GRId, includeProperties: "GolfCourse");

                if (_obj== null)
                {
                    _unitOfWork.GolfRound.Add(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "GolfRound added successfully";
                    return RedirectToAction("Index"); 
                }
                else
                {
                    _unitOfWork.GolfRound.Update(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "GolfRound data edited successfully";
                    return RedirectToAction("Index");
                  
                }
            }
        }

        public IActionResult Delete(int? GRId)
        {
            {
                if (GRId == 0 || GRId == null)
                    return NotFound();
            }
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == GRId,
              includeProperties:"GolfCourse");
            {
                if (golfRound == null)
                    return NotFound();
            }
            ViewBag.GCName = golfRound.GolfCourse.GCName;
            ViewBag.GRDate = golfRound.GRDate.ToShortDateString();
            return View(golfRound);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(GolfRound obj)
        {
            GolfRound golfRound = _unitOfWork.GolfRound.Get(u => u.GRId == obj.GRId);
            if (golfRound == null)
            {
                return NotFound("");
            }
            _unitOfWork.GolfRound.Remove(golfRound);
            _unitOfWork.Save();
            TempData["Success"] = "GolfRound deleted";
            return RedirectToAction("Index");
        }
    }
}

