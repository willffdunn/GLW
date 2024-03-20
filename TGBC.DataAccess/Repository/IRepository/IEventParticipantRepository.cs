using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;


namespace TBGC.DataAccess.Repository.IRepository
{
    public interface IEventParticipantRepository : IRepository<EventParticipant>
    {
        void Update(EventParticipant obj);

    }
}