using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
