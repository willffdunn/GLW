using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;

namespace TBGC.DataAccess.Repository.IRepository
{
    public interface IGolfGroupRepository : IRepository<GolfGroup>
    {
        void Update(GolfGroup obj);

    }
}