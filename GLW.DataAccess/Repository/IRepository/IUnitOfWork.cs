using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMemberRepository Member { get; }
        IGolfCourseRepository GolfCourse { get; }
        IGolfCourseHoleRepository GolfCourseHole { get; }
        IGolfGroupRepository GolfGroup { get; }
        IGolfRoundRepository GolfRound { get; }
        IGolfPlayerRepository GolfPlayer { get; }
        IGolfPlayerScoreRepository GolfPlayerScore { get; }
        IEventRepository Event { get; }
        ILeagueRepository League { get; }
        IEventParticipantRepository EventParticipant { get; }
        void ClearChangeTracker();
        void Save();
    }

        

}

