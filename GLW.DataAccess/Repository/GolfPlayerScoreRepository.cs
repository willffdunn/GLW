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
    public class GolfPlayerScoreRepository : Repository<GolfPlayerScore>, IGolfPlayerScoreRepository
    {
        private ApplicationDBContext _db;
        public GolfPlayerScoreRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfPlayerScore obj)
        {
            _db.Update(obj);
        }

    }
}
