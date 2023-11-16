using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TBGC.DataAccess.Repository.IRepository;
using TBGC.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace TBGC.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db; 
        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext db) 
        {
             _db = db;
            this.dbSet = _db.Set<T>();
            _db.GolfCourseHoles.Include(u => u.GolfCourse);
            _db.GolfRounds.Include(u => u.GolfCourse);
            _db.GolfGroups.Include(u => u.GolfRound);
            _db.GolfPlayers.Include(u => u.GolfGroup);
            _db.GolfPlayers.Include(u => u.Member);
            _db.GolfPlayerScores.Include(u => u.GolfCourseHole);
           






        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
           
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter); 
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return query.FirstOrDefault();

         
        }
        public IEnumerable<T> GetArray(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return query.ToList();


        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }

            return query.ToList();
            
        }
    }
}
