using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DataAccess.Data;
using WebApplication.DataAccess.Repository.IRepository;

namespace WebApplication.DataAccess.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        // private and public constructor
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }
       

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
