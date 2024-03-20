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
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        private ApplicationDBContext _db;
        public LeagueRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(League obj)
        {
            _db.Leagues.Update(obj);
        }
    }
}
