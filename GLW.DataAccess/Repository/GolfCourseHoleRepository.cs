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
    public class GolfCourseHoleRepository : Repository<GolfCourseHole>, IGolfCourseHoleRepository
    {
        private ApplicationDBContext _db;
        public GolfCourseHoleRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfCourseHole obj)
        {
            _db.Update(obj);
        }

    }
}


