using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using GLW.DataAccess.Data;

namespace DataAccess.Repository
{
    public class GolfPlayerRepository : Repository<GolfPlayer>, IGolfPlayerRepository
    {
        private ApplicationDBContext _db;
        public GolfPlayerRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfPlayer obj)
        {
            _db.Update(obj);
        }

    }
}
