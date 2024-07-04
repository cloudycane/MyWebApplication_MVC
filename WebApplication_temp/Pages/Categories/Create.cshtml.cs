using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_temp.Data;
using WebApplication_temp.Models;

namespace WebApplication_temp.Pages.Categories
{

    [BindProperties]
    public class CreateModel : PageModel
    {
        //Constructor 

        private readonly ApplicationDbContext _db;

        public Category Category{ get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public ActionResult OnPost(Category obj) 
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully.";
            return RedirectToPage("Index");
        }
    }
}
