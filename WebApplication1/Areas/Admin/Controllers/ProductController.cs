using Microsoft.AspNetCore.Mvc;
using WebApplication.DataAccess.Repository.IRepository;
using WebApplication.Models;


namespace WebApplication1.Areas.Admin.Controllers
{
    // To tell the machine that this belongs to a specific area 

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public ProductController(IUnitofWork unitOfWork) //Dependency Injection
        {
            _unitOfWork = unitOfWork;
        }

        // GET: CategoryController

        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
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
        public ActionResult Create(Product obj)
        {
           
            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully.";
                return RedirectToAction("Index");

            }

            return View();

        }

        // GET: CategoryController/Edit/5 // EDIT // WE NEED TO KNOW THE CATEGORY ID
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully.";
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
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePOST(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
