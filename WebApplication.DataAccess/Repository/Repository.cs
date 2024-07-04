using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DataAccess.Data;
using WebApplication.DataAccess.Repository.IRepository;

namespace WebApplication.DataAccess.Repository
{ // USING SCOPE DEPENDENCY INJECTION PATTERN
    public class Repository<T> : IRepository<T> where T : class
    {
        // Dependency Injection of the Database 

        private readonly ApplicationDbContext _db; // db is variable

        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // _db.Categories == _db.Set<T>
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);            
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet; 
            query = query.Where(filter);
            return query.FirstOrDefault();
        
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList(); 
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
