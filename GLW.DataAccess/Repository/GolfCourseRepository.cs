using GLW.DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repository
{
    public class GolfCourseRepository : Repository<GolfCourse>, IGolfCourseRepository
    {
        private ApplicationDBContext _db;
        public GolfCourseRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfCourse obj)
        {
            _db.GolfCourses.Update(obj);
        }

    }
}

