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
