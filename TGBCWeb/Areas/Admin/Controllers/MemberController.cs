﻿using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TBGC.DataAccess.Data;
using DataAccess;
using TBGC.DataAccess.Repository;
using DataAccess.Repository;
using TBGC.DataAccess.Repository.IRepository;
using Models;
using System;

namespace TBGCWeb.Areas.Admin.Controllers
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
            return View(objMemberList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member obj)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            {

                Member _obj = _unitOfWork.Member.Get(u => u.FullName == obj.FullName);
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
                obj.FullName = obj.FirstName + obj.LastName;
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
