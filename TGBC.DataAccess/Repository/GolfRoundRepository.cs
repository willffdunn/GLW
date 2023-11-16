using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.DataAccess.Data;
using TBGC.DataAccess.Repository;
using TBGC.DataAccess.Repository.IRepository;
using TBGC.Models;

namespace TBGC.DataAccess.Repository
{
    public class GolfRoundRepository : Repository<GolfRound>, IGolfRoundRepository
    {
        private ApplicationDBContext _db;
        public GolfRoundRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfRound obj)
        {
            _db.Update(obj);
        }

    }
}
