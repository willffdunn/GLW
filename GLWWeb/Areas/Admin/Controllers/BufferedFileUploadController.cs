using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace GLWWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BufferedFileUploadController : Controller
    {
        private readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly IUnitOfWork _unitOfWork;
        public BufferedFileUploadController(IBufferedFileUploadService bufferedFileUploadService, IUnitOfWork unitOfWork)
        {
            _bufferedFileUploadService = bufferedFileUploadService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var stream = file.OpenReadStream())
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            Member member = new Member();
                            {
                                member.FirstName = worksheet.Cells[row, 1].Text.TrimEnd();
                                member.LastName = worksheet.Cells[row, 2].Text.TrimEnd();
                                member.Email = worksheet.Cells[row, 3].Text.TrimEnd();
                                member.MemberPlan = "Not Registered";
                                member.PhoneNumber = worksheet.Cells[row, 4].Text.TrimEnd();
                                member.MemberStatus = worksheet.Cells[row, 5].Text.TrimEnd();
                                member.MemberType = worksheet.Cells[row, 6].Text.TrimEnd();
                                member.Handicap = int.Parse(worksheet.Cells[row, 7].Text);
                                member.PreferredNotification = "Both";
                                member.FullName = member.FirstName + " " + member.LastName;
                                member.MemberTee = "White";
                                member.LId = 1;
                                // Set other properties as needed
                            };
                            // Update the database with the data
                            _unitOfWork.Member.Add(member);
                            _unitOfWork.Save();
                        }

                        ViewBag.Message = "Member Data loaded to Database";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Member Data failed to load to Database";
                }
                return View();
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UploadX(IFormFile file)
        {
            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }
    }
}
