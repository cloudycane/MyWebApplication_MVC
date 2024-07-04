using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        ICategoryRepository Category{ get; }

        void Save();
    }
}
