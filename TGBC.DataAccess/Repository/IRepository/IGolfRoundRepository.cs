using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;

namespace TBGC.DataAccess.Repository.IRepository
{
    public interface IGolfRoundRepository : IRepository<GolfRound>
    {
        void Update(GolfRound obj);

    }
}