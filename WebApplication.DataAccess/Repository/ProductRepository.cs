using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DataAccess.Data;
using WebApplication.DataAccess.Repository.IRepository;
using WebApplication.Models;

namespace WebApplication.DataAccess.Repository

// Dependency Injection 

{
    // implement the ICategory interface 
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;

        // Add a constructor 

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        //public void Update(Category obj)
        //{
        //    _db.Categories.Update(obj);
        //}

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
