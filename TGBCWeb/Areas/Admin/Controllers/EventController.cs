using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using TBGC.DataAccess.Data;
using TBGC.DataAccess.Repository;
using TBGC.DataAccess.Repository.IRepository;
using TBGC.Models;

namespace TGBCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Event> objEventList = _unitOfWork.Event.GetAll().OrderBy(p => p.EvDate).ToList();
            return View(objEventList);
        }
        public IActionResult Upsert(int? EvId)
        {
            if (EvId == null)
            { 
                Event ev = new Event();
                return View(ev);
            }
            else
            {
                Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
                {
                    if (EventFromDB == null)
                        return NotFound();
                }
                return View(EventFromDB);
            }
        }
        public IActionResult EventEmail(int? EvId)
        {
            if (EvId == null)
            {
                Event ev = new Event();
                return View(ev);
            }

            Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
            {
                if (EventFromDB == null)
                    return NotFound();
            }

            IEnumerable<SelectListItem> AvailableRecepients = _unitOfWork.Member.GetAll().Select(u => new SelectListItem
            {
                Text = u.FullName,
                Value = u.Email
            });
                       
            EventEmailVM EVM = new EventEmailVM();
           
            string message = string.Empty;
            
 
            IEnumerable<SelectListItem> strings = Enumerable.Empty<SelectListItem>();
            EVM.Event = EventFromDB;
            EVM.EmailMessage = message;
            EVM.SelectedRecipients = new List<string>();
            EVM.AvailableRecipients = AvailableRecepients;
            return View(EVM);

        }
        public IActionResult Upsertx(int? EvId)
        {
            if (EvId == null)
            {
                Event ev = new Event();
                return View(ev);
            }
            else
            {
                Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
                {
                    if (EventFromDB == null)
                        return NotFound();
                }
                return View(EventFromDB);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Event obj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Event", "Invalid Event Model");
                return View(obj);
            }

                Event _obj = _unitOfWork.Event.Get(u => u.EvId == obj.EvId);
                if (_obj == null)
                {
                    _unitOfWork.Event.Add(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "Event added successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Event.Update(obj);
                    _unitOfWork.Save();
                    TempData["Success"] = "Event data edited successfully";
                    return RedirectToAction("Index");
                }
 
        }
        [HttpPost]
        public IActionResult EventEmail(EventEmailVM obj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Event", "Invalid Event Model");
                return View(obj);
            }

            Event _obj = _unitOfWork.Event.Get(u => u.EvId == obj.Event.EvId);
            return View(obj);
        }

        public IActionResult Browse(int? EvId)
        {
            {
                if (EvId == null)
                    return NotFound();
            }
            Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
            {
                if (EventFromDB == null)
                    return NotFound();
            }
            return View(EventFromDB);
        }
        public IActionResult Delete(int? EvId)
        {
            {
                if (EvId == null)
                    return NotFound();
            }
            Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
            {
                if (EventFromDB == null)
                    return NotFound();
            }
            return View(EventFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(Event obj)
        {
            Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == obj.EvId);
            if (EventFromDB == null)
            {
                return NotFound("");
            }
            _unitOfWork.Event.Remove(EventFromDB);
            _unitOfWork.Save();
            TempData["Success"] = "Event deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

