using GLW.DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repository
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private ApplicationDBContext _db;
        public MemberRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Member obj)
        {
            _db.Members.Update(obj);
        }

    }
}
