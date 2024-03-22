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
