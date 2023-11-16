using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.DataAccess.Data;
using TBGC.DataAccess.Repository;
using TBGC.DataAccess.Repository.IRepository;
using TBGC.Models;

namespace DataAccess.Repository
{
    public class GolfGroupRepository : Repository<GolfGroup>, IGolfGroupRepository
    {
        private ApplicationDBContext _db;
        public GolfGroupRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(GolfGroup obj)
        {
            _db.Update(obj);
        }

    }
}

