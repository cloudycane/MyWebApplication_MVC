using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_temp.Data;
using WebApplication_temp.Models;

namespace WebApplication_temp.Pages.Categories
{
    public class IndexModel : PageModel
    {

        //Constructor 

        private readonly ApplicationDbContext _db; 

        public List<Category> CategoryList {  get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
