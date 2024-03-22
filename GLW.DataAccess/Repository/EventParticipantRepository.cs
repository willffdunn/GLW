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
    public class EventParticipantRepository : Repository<EventParticipant>, IEventParticipantRepository
    {
        private ApplicationDBContext _db;
        public EventParticipantRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(EventParticipant obj)
        {
            _db.Update(obj);
        }

    }
}
