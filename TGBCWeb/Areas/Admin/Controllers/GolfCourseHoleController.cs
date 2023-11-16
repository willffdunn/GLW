using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using TBGC.Models;
using Models;

namespace TBGCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GolfCourseHoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GolfCourseHoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index(int GCId )
        {
            int _gcId = GCId;
            if (_gcId == 0)
            {
                TempData["Success"] = "Golf Course Hole ID Equals 0";
                return View();

            }
            List<GolfCourseHole> objGHList = _unitOfWork.GolfCourseHole.GetAll
                (includeProperties:"GolfCourse").OrderBy(p => p.GCId).ToList();
            if (objGHList == null)
            {
                TempData["Success"] = "Golf Course Hole Index Returned Null";
                return View();

            }
            List<GolfCourseHole> ghQuery = objGHList.Where(p => p.GCId == _gcId).ToList();

            if (ghQuery.Count == 0)
            {
                GolfCourse _gc = _unitOfWork.GolfCourse.Get(p => p.GCId == _gcId);
                GolfCourseHole _ghh = new GolfCourseHole();
                _ghh.GCId = _gcId;
                _ghh.GCName = _gc.GCName;
                _ghh.GHHole = 0;
                _ghh.GHPar = 4;
                _ghh.GHHandicap = 0;
                _ghh.GHT1 = 0;
                _ghh.GHT2 = 0;
                _ghh.GHT3 = 0;
                _ghh.GHT4 = 0;
                _ghh.GHT5 = 0;

                if (_gc.GCNbrHoles == 18)
                {
                    for (int i = 0; i < 18; i++)
                    {
                        if (i < 10)
                            _ghh.GHSectName = "Front";
                        else
                            _ghh.GHSectName = "Back";
                        _ghh.GHHole += 1;
                        _ghh.GHId = 0;
                        _unitOfWork.GolfCourseHole.Add(_ghh);
                        _unitOfWork.Save();
                    };
                    List<GolfCourseHole> objGHList2 = _unitOfWork.GolfCourseHole.GetAll().OrderBy(p => p.GCId).ToList();
                    if (objGHList == null)
                    {
                        TempData["Success"] = "Golf Course Hole Index Returned Null";
                        return View();

                    }

                    List<GolfCourseHole> ghQuery2 = objGHList.Where(p => p.GCId == _gcId).ToList();
                    return View(ghQuery2);
                }
                else {
                    if (_gc.GCNbrHoles == 27)
                    {
                        for (int b = 1; b < 4; b++)
                        {
                            if (b == 1)
                                _ghh.GHSectName = _gc.GCName1;
                            if (b == 2)
                                _ghh.GHSectName = _gc.GCName2;
                            if (b == 3)
                                _ghh.GHSectName = _gc.GCName3;

                            _ghh.GHHole = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                _ghh.GHHole += 1;
                                _ghh.GHId = 0;
                                _unitOfWork.GolfCourseHole.Add(_ghh);
                                _unitOfWork.Save();
                            };
                        }
                        List<GolfCourseHole> objGHList2 = _unitOfWork.GolfCourseHole.GetAll().OrderBy(p => p.GCId).ToList();
                        if (objGHList == null)
                        {
                            TempData["Success"] = "Golf Course Hole Index Returned Null";
                            return View();

                        }

                        List<GolfCourseHole> ghQuery2 = objGHList2.Where(p => p.GCId == _gcId).ToList();
                        return View(ghQuery2);
                    }
                    else
                    {
                        return View(null);
                    };
                };
            
            }
            else
            {
                return View(ghQuery);
            }
        }      
        [HttpPost]
        public IActionResult Create(GolfCourseHoleVM GolfCourseHoleVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Success"] = "Golf Course Hole Add Failed - Invalid Model";
                return View("Create");
            }
            if (GolfCourseHoleVM == null)
            {
                TempData["Success"] = "Golf Course Hole Add Failed - no data";
                return View("Create");
            }

                var gCHView = new GolfCourseHoleVM();
          
            for (int i = 0; i < 9; i++)
            {

                GolfCourseHole _gch = gCHView.GolfCourseHoles[i];
                _unitOfWork.GolfCourseHole.Add(_gch);
                _unitOfWork.Save();

            };
      
                TempData["Success"] = "Golf Course Hole Section added successfully";
                return View();
           
            
        }
        public IActionResult Edit(int GHId)
        {
            {
                if (GHId == 0)
                    return NotFound();
            }
                int _ghId = (int)GHId;
  
            GolfCourseHole ghFromDB = _unitOfWork.GolfCourseHole.Get(u => u.GHId == _ghId);
            {
                if (ghFromDB == null)
                {
                    return NotFound();
                }
                return View(ghFromDB);
            }
        }
        public IActionResult Delete(int GHId)
        {
            {
                if (GHId == 0)
                    return NotFound();
            }
            int _ghId = (int)GHId;

            GolfCourseHole ghFromDB = _unitOfWork.GolfCourseHole.Get(u => u.GHId == _ghId);
            {
                if (ghFromDB == null)
                {
                    return NotFound();
                }
                return View(ghFromDB);
            }
        }
        [HttpPost]
        public IActionResult CreatePost(GolfCourseHole obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Add Hole Failed";
                return View(obj);
            }
    
            _unitOfWork.GolfCourseHole.Add(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Golf Course Hole added successfully";
            return View();
        }
        [HttpPost]

        public IActionResult Edit(GolfCourseHole obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Edit Failed";
                return View(obj);
            }
            GolfCourseHole _gcH = obj;
            int _gcId = _gcH.GCId;

            GolfCourse _gc = _unitOfWork.GolfCourse.Get(p => p.GCId == _gcId);

            _unitOfWork.GolfCourseHole.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Golf Course Hole edited successfully";

            if( _gcH.GHHole  < _gc.GCNbrHoles) 

            {
                _gcH.GHHole += 1;
                GolfCourseHole ghFromDB = _unitOfWork.GolfCourseHole.Get(u => u.GHHole == _gcH.GHHole & u.GCId == _gcH.GCId);
                {
                    if (ghFromDB == null)
                    {
                        return NotFound();
                    }
                    return View(ghFromDB);
                }
            }
            else
            {
                TempData["Info"] = "No more holes to edit";
                return View(obj);
            }
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(GolfCourseHole obj)
        {
            GolfCourseHole ghFromDB = _unitOfWork.GolfCourseHole.Get(u => u.GHId == obj.GHId);
            if (ghFromDB == null)
            {
                return NotFound("");
            }
            _unitOfWork.GolfCourseHole.Remove(ghFromDB);
            _unitOfWork.Save();
            TempData["Info"] = "Golf Hole deleted";
            return RedirectToAction("Index");
        }
    }
}

