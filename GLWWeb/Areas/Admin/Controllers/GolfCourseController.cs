using Microsoft.AspNetCore.Mvc;

namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GolfCourseController : Controller
    {
        private readonly DataAccess.Repository.IRepository.IUnitOfWork _unitOfWork;
        public GolfCourseController(DataAccess.Repository.IRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<GolfCourse> objGCList = _unitOfWork.GolfCourse.GetAll().OrderBy(p => p.GCName).ToList();
            return View(objGCList);
        }

        public IActionResult Upsert(int? GCId)
        {
            if (GCId == 0 || GCId == null)
            {
                GolfCourse golfCourse = new GolfCourse();
                return View(golfCourse);
            }
            else
            {
                GolfCourse uGolfCourse = _unitOfWork.GolfCourse.Get(u => u.GCId == GCId);
                return View(uGolfCourse);
            }
        }

        [HttpPost]
        public IActionResult Upsert(GolfCourse obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Golf Course edit failed";
                return View(obj);
            }
            {

                if (obj.GCId == 0)
                {
                    _unitOfWork.GolfCourse.Add(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "Golf Course added";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.GolfCourse.Update(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "Golf Course data edited";
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Delete(int? GCID)
        {
            {
                if (GCID == null)
                    return NotFound();
            }
            GolfCourse gcFromDB = _unitOfWork.GolfCourse.Get(u => u.GCId == GCID);
            {
                if (gcFromDB == null)
                    return NotFound();
            }
            return View(gcFromDB);
        }
        public IActionResult Browse(int? GCID)
        {
            {
                if (GCID == null)
                    return NotFound();
            }
            GolfCourse gcFromDB = _unitOfWork.GolfCourse.Get(u => u.GCId == GCID);
            {
                if (gcFromDB == null)
                    return NotFound();
            }
            return View(gcFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(GolfCourse obj)
        {
            GolfCourse gcFromDB = _unitOfWork.GolfCourse.Get(u => u.GCId == obj.GCId);
            if (gcFromDB == null)
            {
                return NotFound("");
            }
            _unitOfWork.GolfCourse.Remove(gcFromDB);
            _unitOfWork.Save();
            TempData["Success"] = "Golf Course deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

