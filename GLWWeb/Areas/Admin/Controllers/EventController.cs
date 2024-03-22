using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using Utility;


namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public EventController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;

        }

        public IActionResult Index()
        {
            List<Event> objEventList = _unitOfWork.Event.GetAll().OrderByDescending(p => p.EvDate).ToList();
            return View(objEventList);
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
        public IActionResult EvCalendar()
        {
            List<Event> objEventList = _unitOfWork.Event.GetAll().OrderByDescending(p => p.EvDate).ToList();
            List<EventCal> events = new List<EventCal>();

            for (int i = 0; i < objEventList.Count; i++)
            {
                EventCal evC = new EventCal();
                evC.StartDate = objEventList[i].EvDate.ToString("yyyy-MM-dd");
                evC.EndDate = objEventList[i].EvEndDate.ToString("yyyy-MM-dd");
                evC.Title = objEventList[i].EvShortDescription;
                evC.Id = objEventList[i].EvId;

                events.Add(evC);
            };

            return View(events);
        }
        public IActionResult Upsert(int? EvId)
        {
            Event ev = new Event();

            if (EvId != null)
            {
                ev = _unitOfWork.Event.Get(u => u.EvId == EvId);
                {
                    if (ev == null)
                        return NotFound();
                }
            }
            return View(ev);

        }
        public IActionResult EventParticipant(int? EvId)
        {
            if (EvId == 0)
            {
                TempData["Info"] = "Event is 0";
                return View();
            }

            List<Member> mList = _unitOfWork.Member.GetAll().OrderBy(p => p.FullName).ToList();

            if (mList == null)
            {
                TempData["Info"] = "No Members";
                return View();
            }
            List<EventParticipant> ePListAll = _unitOfWork.EventParticipant.GetAll
                (includeProperties: "Event").OrderBy(p => p.EvId).ToList();
            if (ePListAll == null)
            {
                TempData["Info"] = "No Event Participants";
                return View();
            }
            List<EventParticipant> yPart = ePListAll.Where(p => p.EvId == EvId).ToList();
            if (yPart == null)
            {
                TempData["Info"] = "Event has no Participants";
            }
            else
            {
                List<Member> nPart = mList.Where(m => !yPart.Any(d => m.MemberId == d.MemberId)).ToList();
                if (nPart == null || nPart.Count == 0)
                {
                    TempData["Info"] = "Every Member is participating";
                }
            }
            IEnumerable<SelectListItem> nParticipant = mList.Where(m => !yPart.Any(d => d.MemberId == m.MemberId)).Select(u => new SelectListItem
            {
                Text = u.FullName,
                Value = u.MemberId.ToString()
            });

            Event EventFromDB = _unitOfWork.Event.Get(u => u.EvId == EvId);
            {
                if (EventFromDB == null)
                    return NotFound();
            }

            EventVM EVM = new EventVM();

            string message = string.Empty;

            EVM.Event = EventFromDB;
            EVM.EmailSender = "admin.tropicbaygolf.com";
            EVM.EmailMessage = message;
            EVM.EmailRecipients = new List<string>();
            EVM.AvailableRecipients = nParticipant;
            EVM.Participants = yPart;


            return View(EVM);
        }
        public IActionResult EventEmail(int? EvId)
        {
            ; if (EvId == null)
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

            EventVM EVM = new EventVM();

            string message = string.Empty;


            IEnumerable<SelectListItem> strings = Enumerable.Empty<SelectListItem>();
            EVM.Event = EventFromDB;
            EVM.EmailSender = "admin.tropicbaygolf.com";
            EVM.EmailMessage = message;
            EVM.EmailRecipients = new List<string>();
            EVM.AvailableRecipients = AvailableRecepients;

            return View(EVM);
        }
        [HttpPost]
        public IActionResult Upsert(Event obj)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Event", "Invalid Event Model");
                return View(obj);
            }

            Event ev = _unitOfWork.Event.Get(u => u.EvId == obj.EvId);
            if (ev == null)
            {
                _unitOfWork.Event.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Event added successfully";

            }
            else
            {
                _unitOfWork.Event.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Event data edited successfully";

            }

            return View(ev);
        }
        [HttpPost]

        public IActionResult EventParticipant(EventVM obj)
        {
            EventVM eVM = obj;



            if (eVM.EventParticipants == null)
            {
                if (eVM.GuestFName == null)
                {
                    ModelState.AddModelError("Event", "No Participants Selected");
                    return View(obj);
                }
                else
                {
                    EventParticipant guest = new EventParticipant();
                    guest.EPEmail = eVM.GuestEmail;
                    guest.EvId = eVM.Event.EvId;
                    guest.EPFName = eVM.GuestFName;
                    guest.EPLName = eVM.GuestLName;
                    guest.EPPhoneNumber = eVM.GuestPhone;
                    guest.EPType = "Guest";
                    _unitOfWork.EventParticipant.Add(guest);
                    _unitOfWork.Save();

                }
            }
            else
            {
                for (int i = 0; i < eVM.EventParticipants.Count; i++)
                {
                    EventParticipant part = new EventParticipant();

                    Member m = _unitOfWork.Member.Get(u => u.MemberId == int.Parse(eVM.EventParticipants[i]));

                    part.EPEmail = m.Email;
                    part.EvId = eVM.Event.EvId;
                    part.EPFName = m.FirstName;
                    part.EPLName = m.LastName;
                    part.EPPhoneNumber = m.PhoneNumber;
                    part.EPType = "Member";
                    part.MemberId = m.MemberId;
                    _unitOfWork.EventParticipant.Add(part);
                    _unitOfWork.Save();
                }
                if (eVM.GuestFName != null)
                {
                    EventParticipant guest = new EventParticipant();
                    guest.EPEmail = eVM.GuestEmail;
                    guest.EvId = eVM.Event.EvId;
                    guest.EPFName = eVM.GuestFName;
                    guest.EPLName = eVM.GuestLName;
                    guest.EPPhoneNumber = eVM.GuestPhone;
                    guest.EPType = "Guest";
                    _unitOfWork.EventParticipant.Add(guest);
                    _unitOfWork.Save();

                }
            }
            TempData["Success"] = "Participants added successfully";

            return RedirectToAction("EventParticipant", new { eVM.Event.EvId });
        }

        [HttpPost]
        public IActionResult EventEmail(EventVM obj)
        {
            if (obj.EmailRecipients == null)
            {
                ModelState.AddModelError("Event", "No Email Recepients");
                return View(obj);
            }

            EventVM eVM = new EventVM();
            eVM = obj;

            Event evnt = _unitOfWork.Event.Get(u => u.EvId == obj.Event.EvId);

            EventVM EVM = new EventVM();

            List<Member> AllEmailRecepients = _unitOfWork.Member.GetAll().ToList();

            List<string> allEmails = new List<string>();

            if (eVM.EmailRecipients.Count > 0)
            {

                if (eVM.EmailRecipients[0] == "[all]")
                {
                    for (int i = 0; i < AllEmailRecepients.Count; i++)
                    {
                        allEmails.Add(AllEmailRecepients[i].Email);
                    }
                }
                else
                {

                    for (int i = 0; i < eVM.EmailRecipients.Count; i++)
                    {
                        allEmails.Add(eVM.EmailRecipients[i]);
                    }
                }
            }
            else
            {
                TempData["Info"] = "No email recepients selected";
                return View(eVM);
            }

            IEnumerable<SelectListItem> AvailableRecepients = _unitOfWork.Member.GetAll().Select(u => new SelectListItem
            {
                Text = u.FullName,
                Value = u.Email
            });

            int eventID = eVM.Event.EvId;

            Uri URL = new Uri(new Uri(SD.APIBase), $"Admin/Event/Browse?EvId={eventID}");

            EVM.Event = eVM.Event;
            EVM.Event.EvShortDescription = eVM.Event.EvShortDescription;
            EVM.EmailSender = "admin.tropicbaygolf.com";

            StringBuilder sb = new StringBuilder(eVM.EmailMessage);
            sb.AppendLine("<br />");
            sb.AppendLine(obj.Event.EvShortDescription);
            sb.AppendLine("<br />");

            sb.AppendLine("Where: " + obj.Event.EvLocation + "-- " +
                "When: " + obj.Event.EvDate.ToShortDateString() + " " +
                obj.Event.EvDate.ToString(@"hh\:mm"));
            sb.AppendLine("<br />");
            sb.AppendLine("Click on the link to register online: " + URL);
            sb.AppendLine("<br />");
            sb.AppendLine(obj.Event.EvDescription);

            EVM.EmailMessage = sb.ToString();
            EVM.EmailRecipients = allEmails;
            EVM.AvailableRecipients = AvailableRecepients;

            for (int i = 0; i < allEmails.Count; i++)
            {
                var recipientEmail = allEmails[i];
                var subject = EVM.Event.EvShortDescription;
                var message = EVM.EmailMessage;

                _emailSender.SendEmailAsync(recipientEmail, subject, message);

                string accountSid = "AC37f20763ac3b293f908e1e0290b3dcd1";
                string authToken = "c4937d899e21f3e4df66b1d65c91eccb";
            }

            TempData["Success"] = "Email Sent";
            return View("Browse", EVM.Event);
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

        public IActionResult DeleteParticipant(int? EPId)
        {
            EventParticipant eP = _unitOfWork.EventParticipant.Get(u => u.EPId == EPId);
            if (eP == null)
            {
                return NotFound("");
            }
            _unitOfWork.EventParticipant.Remove(eP);
            _unitOfWork.Save();
            TempData["Success"] = "EventParticipant deleted successfully";
            return RedirectToAction("EventParticipant", new { eP.EvId });
        }
    }


}

