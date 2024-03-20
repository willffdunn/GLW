using Models;
using Models.ViewModels;
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
