using GLW.DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
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
