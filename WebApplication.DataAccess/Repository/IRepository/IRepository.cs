using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.DataAccess.Repository.IRepository
{
    // This needs to be a public interface so that when we pass the T, which is the variable for class IRepository in its backend or the Repository.class 
    // We are able to get T without error and implement its properties...
    public interface IRepository <T> where T : class
    {
        // T- Generic Model or Category
        // Retrieve Data 
        // GetAll the Data from the T or the Category or the generic model...
        IEnumerable<T> GetAll (string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); // Linq operation
        void Add(T entity); // void method to add similar to Create New Category 
                            // void Update(T entity); we like to use them outside the repository in CategoryController
        void Remove(T entity); // void delete method similar to Delete Category 
        void RemoveRange(IEnumerable<T> entity); // void delete range method... 

        // Imagine a model interface for the Repository backend...that uses Category.cs Model and the database.
       
    }
}
