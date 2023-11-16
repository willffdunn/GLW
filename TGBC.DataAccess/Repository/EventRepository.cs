using Models;
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
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private ApplicationDBContext _db;
        public EventRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Event obj)
        {
            _db.Update(obj);
        }

    }
}
