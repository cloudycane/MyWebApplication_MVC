using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication_temp.Data;
using WebApplication_temp.Models;

namespace WebApplication_temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    
        {
            //Constructor 

            private readonly ApplicationDbContext _db;

            public Category Category { get; set; }

            public DeleteModel(ApplicationDbContext db)
            {
                _db = db;
            }
            public void OnGet(int? id)
            {

                if (id != null || id != 0)
                {
                   Category = _db.Categories.Find(id); 
                }
                   
            }
            public ActionResult OnPost()
            {
                Category obj = _db.Categories.Find(Category.Id);
                if (obj == null)
            {
                return NotFound();
            }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToPage("Index");
            }
            
        }
}
