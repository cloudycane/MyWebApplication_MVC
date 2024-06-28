using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }

        // GET: CategoryController

        public ActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }

            if (ModelState.IsValid) { 

            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully.";
            return RedirectToAction("Index");

            }

            return View();

        }

        // GET: CategoryController/Edit/5 // EDIT // WE NEED TO KNOW THE CATEGORY ID
        public ActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category obj)
        {
           if (ModelState.IsValid)
            {
               _db.Categories.Update(obj);
               _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
           return View();
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePOST (int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index"); 
        }
    }
}
