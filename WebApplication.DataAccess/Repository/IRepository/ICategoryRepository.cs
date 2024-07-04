using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.DataAccess.Repository.IRepository
{   
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();


        //Here we will have void update 
        void Update(Category obj);

    }
}
