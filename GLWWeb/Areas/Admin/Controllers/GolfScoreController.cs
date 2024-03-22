using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using DataAccess.Repository;

namespace GLWWeb.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class GolfScoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GolfScoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int GGId)
        {
            int _ggId = GGId;
            if (_ggId == 0)
            {

                return View();

            }
            ViewBag.GGId = GGId;
            return View();



        }
        public IActionResult GPScoreM(int GPId)
        {
            int gPId = GPId;
            if (gPId == 0)
            {
                TempData["Info"] = "Player ID = 0";
                return View();

            }
            GPScoreVM gPSVM = GetGPScore(gPId);
            GolfPlayerScore gPS = gPSVM.GPS[0];
            ViewBag.GGId = gPSVM.GG.GGId;
            ViewBag.GGName = gPSVM.GG.GGName;
            TimeSpan gGtTimes = gPSVM.GG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gPSVM.GG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gPSVM.GC.GCName;
            ViewBag.GRDate = gPSVM.GG.GolfRound.GRDate.ToShortDateString();
            ViewBag.PlayerName = gPSVM.GP.GPFName + ' ' + gPSVM.GP.GPLName;
            ViewBag.Tot = gPSVM.GP.Tot3;

            return View(gPS);
        }
        public IActionResult GPScoreP(int GPSId, int GPId)
        {
            if (GPSId == 0 || GPId == 0)
            {
                TempData["Info"] = "Golf Score Model Invalid";
                return View();
            }

            GPScoreVM gPSVM = GetGPScore(GPId);

            int gPSHID = 0;

            for (int i = 0; i < 18; i++)
            {
                if (gPSVM.GPS[i].GPSId == GPSId)
                {
                    gPSHID = i;
                }
            }

            GolfPlayerScore gPS = new GolfPlayerScore();

            if (gPSHID != 0)
            {
                gPS = gPSVM.GPS[gPSHID - 1];
                gPS.GolfCourseHole = gPSVM.GPS[gPSHID - 1].GolfCourseHole;
            }
            else
            {
                gPS = gPSVM.GPS[gPSHID];
                gPS.GolfCourseHole = gPSVM.GPS[gPSHID].GolfCourseHole;
                TempData["Info"] = "First Golf Hole";
            }
            ViewBag.GGId = gPSVM.GG.GGId;
            ViewBag.GGName = gPSVM.GG.GGName;
            TimeSpan gGtTimes = gPSVM.GG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gPSVM.GG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gPSVM.GC.GCName;
            ViewBag.GRDate = gPSVM.GG.GolfRound.GRDate.ToShortDateString();
            ViewBag.PlayerName = gPSVM.GP.GPFName + ' ' + gPSVM.GP.GPLName;
            ViewBag.Tot = gPSVM.GP.Tot3;


            ModelState.Clear();

            return View("GPScoreM", gPS);
        }
        public IActionResult GPScore(int GPId)
        {
            int _gpId = GPId;
            if (_gpId == 0)
            {
                TempData["Info"] = "Player ID = 0";
                return View();

            }
            GPScoreVM gPScoreVM = GetGPScore(GPId);
            ViewBag.HoleNbr = 0;
            ViewBag.GGId = gPScoreVM.GG.GGId;
            return View(gPScoreVM);

        }
        public IActionResult GGScore(int GGId)
        {
            int _ggId = GGId;
            if (_ggId == 0)
            {
                TempData["Info"] = "Golf Group Has no Players";
                return RedirectToAction("GolfPlayer.Upsert", new { GGId = _ggId });

            }
            GolfGroup gG = _unitOfWork.GolfGroup.Get(p => p.GGId == _ggId,
               includeProperties: "GolfRound");
            if (gG == null)
            {
                TempData["Info"] = "Group doesn't exist";
                return View();

            }
            List<GolfPlayer> gPs = _unitOfWork.GolfPlayer.GetArray(p => p.GGId == _ggId,
                includeProperties: "Member").ToList();
            if (gPs.Count == 0)
            {
                TempData["Info"] = "Group " + gG.GGName + " has no Players";
                return RedirectToAction("Index2", "GolfPlayer", new { gG.GRId, GGId = _ggId });

            }
            GGScoreVM gGs = new GGScoreVM();
            GPScoreVM gPScoreVM = new GPScoreVM();
            List<GPScoreVM> gPScoreVMs = new List<GPScoreVM>();



            ViewBag.GGId = _ggId;
            foreach (GolfPlayer player in gPs)
            {
                gPScoreVM = GetGPScore(player.GPId);
                gPScoreVMs.Add(gPScoreVM);
            }

            gGs.GGS = gPScoreVMs;
            return View(gGs);



        }
        public IActionResult GGScoreZ(int GGId)
        {
            int _ggId = GGId;
            if (_ggId == 0)
            {
                TempData["Info"] = "Golf Group Has no Players";
                return RedirectToAction("GolfPlayer.Upsert", new { GGId = _ggId });

            }
            GolfGroup gG = _unitOfWork.GolfGroup.Get(p => p.GGId == _ggId,
               includeProperties: "GolfRound");
            if (gG == null)
            {
                TempData["Info"] = "Group doesn't exist";
                return View();

            }
            GolfRound gR = _unitOfWork.GolfRound.Get(p => p.GRId == gG.GRId,
                includeProperties: "GolfCourse");
            if (gG == null)
            {
                TempData["Info"] = "Group doesn't exist";
                return View();

            }
            List<GolfPlayer> gP = _unitOfWork.GolfPlayer.GetArray(p => p.GGId == _ggId,
                includeProperties: "Member").ToList();
            if (gP.Count == 0)
            {
                TempData["Info"] = "Group " + gG.GGName + " has no Players";
                return RedirectToAction("Index2", "GolfPlayer", new { gG.GRId, GGId = _ggId });

            }
            GolfPlayerScore gPs = new GolfPlayerScore();
            GPScoreVM gPScore = new GPScoreVM();

            List<GGScoreVP> gPScores = new List<GGScoreVP>();

            for (int i = 0; i < gP.Count; i++)
            {
                GGScoreVP gGS = new GGScoreVP();
                gPScore = GetGPScore(gP[i].GPId);
                gGS.GPS = gPScore.GPS[0];
                gGS.GP = gP[i];
                gGS.GG = gG;
                gPScores.Add(gGS);
            }

            ViewBag.GGId = gG.GGId;
            ViewBag.GGName = gG.GGName;
            TimeSpan gGtTimes = gG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gR.GolfCourse.GCName;
            ViewBag.GRDate = gG.GolfRound.GRDate.ToShortDateString();

            ModelState.Clear();

            return View("GGScoreZ", gPScores);

        }
        public IActionResult GGScoreP(int GHId, int GGId)
        {
            int _ggId = GGId;
            int _ghId = GHId;
            if (_ggId == 0)
            {
                TempData["Info"] = "Golf Group Has no Players";
                return RedirectToAction("GolfPlayer.Upsert", new { GGId = _ggId });

            }
            if (_ghId == 0)
            {
                TempData["Info"] = "Golf Group Hole = 0";
                return RedirectToAction("GolfPlayer.Upsert", new { GGId = _ggId });

            }
            GolfGroup gG = _unitOfWork.GolfGroup.Get(p => p.GGId == _ggId,
               includeProperties: "GolfRound");
            if (gG == null)
            {
                TempData["Info"] = "Group doesn't exist";
                return View();

            }
            GolfRound gR = _unitOfWork.GolfRound.Get(p => p.GRId == gG.GRId,
                includeProperties: "GolfCourse");
            if (gG == null)
            {
                TempData["Info"] = "Group doesn't exist";
                return View();

            }
            List<GolfPlayer> gP = _unitOfWork.GolfPlayer.GetArray(p => p.GGId == _ggId,
                includeProperties: "Member").ToList();
            if (gP.Count == 0)
            {
                TempData["Info"] = "Group " + gG.GGName + " has no Players";
                return RedirectToAction("Index2", "GolfPlayer", new { gG.GRId, GGId = _ggId });

            }
            GolfPlayerScore gPs = new GolfPlayerScore();
            GPScoreVM gPScore = new GPScoreVM();

            List<GGScoreVP> gPScores = new List<GGScoreVP>();

            int holeIndex = 0;

            if (_ghId > 1)
            {
                holeIndex = _ghId - 2;
            }
            else
            {
                holeIndex = 0;
            }
            for (int i = 0; i < gP.Count; i++)
            {
                GGScoreVP gGS = new GGScoreVP();
                gPScore = GetGPScore(gP[i].GPId);
                gGS.GPS = gPScore.GPS[holeIndex];
                gGS.GP = gP[i];
                gGS.GG = gG;
                gPScores.Add(gGS);
            }

            ViewBag.GGId = gG.GGId;
            ViewBag.GGName = gG.GGName;
            TimeSpan gGtTimes = gG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gR.GolfCourse.GCName;
            ViewBag.GRDate = gG.GolfRound.GRDate.ToShortDateString();

            ModelState.Clear();

            return View("GGScoreZ", gPScores);

        }
        public IActionResult GRScore(int GRId)
        {
            int _grId = GRId;
            if (_grId == 0)
            {
                TempData["Info"] = "Round ID = 0";
                return View();
            }
            GolfRound gR = _unitOfWork.GolfRound.Get(p => p.GRId == _grId, includeProperties: "GolfCourse");

            List<GolfGroup> ggs = _unitOfWork.GolfGroup.GetArray(p => p.GRId == _grId,
               includeProperties: "GolfRound").ToList();

            if (ggs == null)
            {
                TempData["Info"] = "Round has no Groups";
                return View();
            }

            ViewBag.GCName = gR.GolfCourse.GCName;
            ViewBag.GRDate = gR.GRDate.ToShortDateString();


            List<GRScoreVM> gRScoreVMs = new List<GRScoreVM>();
            foreach (GolfGroup gG in ggs)
            {
                List<GolfPlayer> gPs = _unitOfWork.GolfPlayer.GetArray(p => p.GGId == gG.GGId,
                    includeProperties: "Member").ToList();
                if (gPs == null)
                {
                    TempData["Info"] = "Group has no Players";
                    return View();
                }


                foreach (GolfPlayer player in gPs)
                {
                    GRScoreVM gRs = new GRScoreVM();
                    gRs.GPId = player.GPId;
                    gRs.MemberId = player.MemberId;
                    gRs.GPType = player.GPType;
                    gRs.GScore = player.Tot3;
                    gRs.GPLName = player.GPLName;
                    gRs.GPFName = player.GPFName;
                    gRs.GGName = gG.GGName;
                    gRs.GGTime = gG.GGTtime;

                    gRScoreVMs.Add(gRs);
                }
            }
            return View(gRScoreVMs);
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult GPScoreM(GolfPlayerScore obj)
        {
            if (obj == null)
            {
                TempData["Info"] = "Golf Score update failed";
                return View();
            }
            GolfPlayerScore gPScore = obj;

            GPScoreVM gPSVM = GetGPScore(gPScore.GPId);


            int gPSHID = 0;

            for (int i = 0; i < 18; i++)
            {
                if (gPSVM.GPS[i].GPSId == gPScore.GPSId)
                {
                    gPSVM.GPS[i] = gPScore;
                    gPSHID = i;
                }
            }

            GPScoreVM gPScoreVM = PostGPScore(gPSVM);
            GolfPlayerScore gPS = new GolfPlayerScore();

            if (gPSHID <= 16)
            {
                gPS = gPScoreVM.GPS[gPSHID + 1];
                gPS.GolfCourseHole = gPScoreVM.GPS[gPSHID + 1].GolfCourseHole;
            }
            else
            {
                gPS = gPScoreVM.GPS[gPSHID];
                gPS.GolfCourseHole = gPScoreVM.GPS[gPSHID].GolfCourseHole;
                TempData["Info"] = "Last Golf Hole";
            }
            ViewBag.GGId = gPScoreVM.GG.GGId;
            ViewBag.GGName = gPScoreVM.GG.GGName;
            TimeSpan gGtTimes = gPScoreVM.GG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gPScoreVM.GG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gPScoreVM.GC.GCName;
            ViewBag.GRDate = gPScoreVM.GG.GolfRound.GRDate.ToShortDateString();
            ViewBag.PlayerName = gPScoreVM.GP.GPFName + ' ' + gPScoreVM.GP.GPLName;

            GolfPlayer gP = _unitOfWork.GolfPlayer.Get(P => P.GPId == gPSVM.GP.GPId);
            ViewBag.Tot = gP.Tot3;

            ModelState.Clear();

            return View(gPS);
        }
        [HttpPost]
        public IActionResult GPScore(GPScoreVM obj)
        {
            if (obj.GPS == null)
            {
                TempData["Info"] = "Golf Player Score failed";
                return View();
            }
            GPScoreVM gPScoreVM = PostGPScore(obj);
            return RedirectToAction("GPScore", new { gPScoreVM.GPS[0].GPId });
        }


        [HttpPost]
        public IActionResult GGScore(GGScoreVM obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Golf Group Score failed";
                return View();
            }
            if (obj == null)
            {
                TempData["Info"] = "Golf Group Score failed";
                return View();
            }
            GGScoreVM gGs = obj;

            for (int g = 0; g < gGs.GGS.Count; g++)
            {
                GPScoreVM gPScoreVM = PostGPScore(gGs.GGS[g]);
            }
            return RedirectToAction("GGScore", new { gGs.GGS[0].GG.GGId });
        }
        [HttpPost]
        public IActionResult GGScoreZ(List<GGScoreVP> obj)
        {
            if (obj.Count == 0)
            {
                TempData["Info"] = "Golf Group Score update failed";
                return View();
            }
            List<GGScoreVP> gGScoreVPs = obj;

            int gPSHID = 0;

            GPScoreVM gPScoreVM = new GPScoreVM();
            GolfRound gRR = new GolfRound();
            GolfPlayer gPR = new GolfPlayer();

            List<GGScoreVP> gGScoreVML = new List<GGScoreVP>();

            for (int z = 0; z < gGScoreVPs.Count; z++)
            {

                GPScoreVM gPSVM = GetGPScore(gGScoreVPs[z].GPS.GPId);

                for (int i = 0; i < 18; i++)
                {
                    if (gPSVM.GPS[i].GPSId == gGScoreVPs[z].GPS.GPSId)
                    {
                        gPSVM.GPS[i] = gGScoreVPs[z].GPS;
                        gPSHID = i;
                    }
                }

                gPScoreVM = PostGPScore(gPSVM);

                GolfPlayerScore gPS = new GolfPlayerScore();

                if (gPSHID <= 16)
                {
                    gPS = gPScoreVM.GPS[gPSHID + 1];
                    gPSHID = gPSHID + 1;
                }
                else
                {
                    gPS = gPScoreVM.GPS[gPSHID];
                    gPS.GolfCourseHole = gPScoreVM.GPS[gPSHID].GolfCourseHole;
                    TempData["Info"] = "Last Golf Hole";
                }

                gPScoreVM.GPS[gPSHID] = gPS;



                gPR = _unitOfWork.GolfPlayer.Get(P => P.GPId == gPSVM.GP.GPId,
                    includeProperties: "GolfGroup");
                GGScoreVP gGScoreVP = new GGScoreVP();

                gRR = _unitOfWork.GolfRound.Get(P => P.GRId == gPR.GolfGroup.GRId,
                includeProperties: "GolfCourse");
                gGScoreVP.GPS = gPS;
                gGScoreVP.GP = gPR;
                gGScoreVP.GG = gPR.GolfGroup;
                gGScoreVP.GC = gRR.GolfCourse;

                gGScoreVML.Add(gGScoreVP);
            }

            ViewBag.GGId = gGScoreVML[0].GG.GGId;
            ViewBag.GGName = gGScoreVML[0].GG.GGName;
            TimeSpan gGtTimes = gGScoreVML[0].GG.GGTtime;
            TimeOnly gTime = TimeOnly.FromDateTime(DateTime.Today.Add(gGScoreVML[0].GG.GGTtime));
            ViewBag.GGTtime = gTime.ToShortTimeString();
            ViewBag.GCName = gGScoreVML[0].GC.GCName;
            ViewBag.GRDate = gRR.GRDate.ToShortDateString();


            ModelState.Clear();

            return View(gGScoreVML);
        }

        [HttpPost]
        public IActionResult GRScore(List<GRScoreVM> obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["Info"] = "Golf Course edit failed";
                return View();
            }

            List<GRScoreVM> gRs = obj;


            foreach (GRScoreVM gPs in gRs)
            {
                GolfPlayer gP = _unitOfWork.GolfPlayer.Get(p => p.GPId == gPs.GPId, includeProperties: "Member");
                gP.Tot3 = gPs.GScore;

                _unitOfWork.GolfPlayer.Update(gP);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index", "GolfRound");
        }

        private GPScoreVM PostGPScore(GPScoreVM obj)
        {
            GPScoreVM gpsVM = obj;
            List<GolfPlayerScore> gps = gpsVM.GPS;
            GolfPlayer gp = _unitOfWork.GolfPlayer.Get(p => p.GPId == gps[1].GPId);
            gp.Tot1 = 0;
            gp.Tot2 = 0;
            gp.Tot3 = 0;
            gp.PTot1 = 0;
            gp.PTot2 = 0;
            gp.PTot3 = 0;
            gp.GTot1 = 0;
            gp.GTot2 = 0;
            gp.GTot3 = 0;
            gp.FTot1 = 0;
            gp.FTot2 = 0;
            gp.FTot3 = 0;
            for (int i = 0; i < 9; i++)
            {
                gp.Tot1 = gp.Tot1 + gps[i].GPSStrokes;
                gp.PTot1 = gp.PTot1 + gps[i].GPSPutts;
                if (gps[i].GPSGIY == "Yes")
                {
                    gp.GTot1 = gp.GTot1 + 1;
                }
                if (gps[i].GPSFW == "Yes")
                {
                    gp.FTot1 = gp.FTot1 + 1;
                }

                _unitOfWork.GolfPlayerScore.Update(gps[i]);
                _unitOfWork.Save();
                _unitOfWork.ClearChangeTracker();

            };
            for (int i = 9; i < 18; i++)
            {
                gp.Tot2 = gp.Tot2 + gps[i].GPSStrokes;
                gp.PTot2 = gp.PTot2 + gps[i].GPSPutts;
                if (gps[i].GPSGIY == "Yes")
                {
                    gp.GTot2 = gp.GTot2 + 1;
                }
                if (gps[i].GPSFW == "Yes")
                {
                    gp.FTot2 = gp.FTot2 + 1;
                }

                _unitOfWork.GolfPlayerScore.Update(gps[i]);
                _unitOfWork.Save();
            };

            gp.Tot3 = gp.Tot1 + gp.Tot2;
            gp.PTot3 = gp.PTot1 + gp.PTot2;
            gp.GTot3 = gp.GTot1 + gp.GTot2;
            gp.FTot3 = gp.FTot1 + gp.FTot2;

            _unitOfWork.GolfPlayer.Update(gp);
            _unitOfWork.Save();

            return gpsVM;
        }

        private GPScoreVM GetGPScore(int GPId)
        {
            int _gpId = GPId;
            GPScoreVM gpsVM = new GPScoreVM();
            if (_gpId == 0)
            {
                return gpsVM;
            }
            GolfPlayer gp = _unitOfWork.GolfPlayer.Get(p => p.GPId == _gpId,
            includeProperties: "Member");
            if (gp == null)
            {
                return gpsVM;
            }
            GolfGroup gg = _unitOfWork.GolfGroup.Get(p => p.GGId == gp.GGId, includeProperties: "GolfRound");
            if (gg == null)
            {
                return gpsVM;
            }
            GolfRound gr = _unitOfWork.GolfRound.Get(p => p.GRId == gg.GRId,
            includeProperties: "GolfCourse");
            if (gr == null)
            {
                return gpsVM;

            }
            List<GolfCourseHole> gchList = _unitOfWork.GolfCourseHole.GetAll().ToList();
            if (gchList == null)
            {
                return gpsVM;
            }
            ViewBag.GGTtime = gg.GGTtime;
            ViewBag.GRId = gr.GRId;
            ViewBag.GRDate = gr.GRDate.ToShortDateString();
            ViewBag.GCName = gr.GolfCourse.GCName;
            double d1 = gp.GTot1;
            double perc = d1 / 9;
            ViewBag.GIYPerc1 = string.Format("{0:0.0%}", perc);
            d1 = gp.GTot2;
            perc = d1 / 9;
            ViewBag.GIYPerc2 = string.Format("{0:0.0%}", perc);
            d1 = gp.GTot3;
            perc = d1 / 18;
            ViewBag.GIYPerc3 = string.Format("{0:0.0%}", perc);
            d1 = gp.FTot1;
            perc = d1 / 9;
            ViewBag.GIYPerc4 = string.Format("{0:0.0%}", perc);
            d1 = gp.FTot2;
            perc = d1 / 9;
            ViewBag.GIYPerc5 = string.Format("{0:0.0%}", perc);
            d1 = gp.FTot3;
            perc = d1 / 18;
            ViewBag.GIYPerc6 = string.Format("{0:0.0%}", perc);
            List<GolfPlayerScore> gps = _unitOfWork.GolfPlayerScore.GetArray(p => p.GPId == _gpId, includeProperties: "GolfCourseHole").ToList();
            if (gps.Count == 0 || gps == null)
            {
                GolfPlayerScore gPs = new GolfPlayerScore();
                for (int i = 0; i < 18; i++)
                {
                    GolfCourseHole gch = _unitOfWork.GolfCourseHole.
                        Get(p => p.GCId == gr.GCId & p.GHHole == i + 1);

                    gPs.GPSId = 0;
                    gPs.GPId = _gpId;
                    gPs.GPSStrokes = 0;
                    gPs.GPSFW = " ";
                    gPs.GPSGIY = " ";
                    gPs.GPSPutts = 0;
                    gPs.GHId = gch.GHId;


                    _unitOfWork.GolfPlayerScore.Add(gPs);
                    _unitOfWork.Save();
                };
            }
            GPScoreVM gPScoreVM = new GPScoreVM();
            List<GolfPlayerScore> gPS = _unitOfWork.GolfPlayerScore.GetArray(p => p.GPId == _gpId,
                  includeProperties: "GolfCourseHole").ToList();
            gPScoreVM.GPS = gPS;
            gPScoreVM.GC = gr.GolfCourse;
            gPScoreVM.GG = gg;
            gPScoreVM.GP = gp;
            return gPScoreVM;

        }
    }
}


