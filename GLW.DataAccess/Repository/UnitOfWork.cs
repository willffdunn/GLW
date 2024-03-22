using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models;
using GLW.DataAccess.Data;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       
        private ApplicationDBContext _db;
        public IMemberRepository Member { get; private set; }
        public IGolfCourseRepository GolfCourse { get; private set; }
        public IGolfCourseHoleRepository GolfCourseHole { get; private set; }
        public IGolfGroupRepository GolfGroup { get; private set; }
        public IGolfPlayerRepository GolfPlayer { get; private set; }
        public IGolfRoundRepository GolfRound { get; private set; }
        public IEventRepository Event { get; private set; }
        public ILeagueRepository League { get; private set; }
        public IGolfPlayerScoreRepository GolfPlayerScore { get; private set; }
        public IEventParticipantRepository EventParticipant { get; private set; }



        public UnitOfWork(ApplicationDBContext db)  
            {
            _db = db;
            Member = new MemberRepository(_db);
            GolfCourse = new GolfCourseRepository(_db);
            GolfCourseHole = new GolfCourseHoleRepository(_db);
            GolfGroup = new GolfGroupRepository(_db);
            Event = new EventRepository(_db);
            EventParticipant = new EventParticipantRepository(_db);
            GolfRound = new GolfRoundRepository(_db);
            League = new LeagueRepository(_db); 
            GolfPlayerScore = new GolfPlayerScoreRepository(_db);
            GolfPlayer = new GolfPlayerRepository(_db);
          


        }
        public void ClearChangeTracker()
        {
            _db.ChangeTracker.Clear();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
