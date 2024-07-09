﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.DataAccess.Repository.IRepository;
using WebApplication.Models;
using WebApplication.Models.ViewModels;


namespace WebApplication1.Areas.Admin.Controllers
{
    // To tell the machine that this belongs to a specific area 

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        // In order to insert Images and Files we need to depend inject or insert a constructor of IWebHostEnvironment
        // 

        private readonly IWebHostEnvironment webHostEnvironment; 
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment) //Dependency Injection of IUnitofWork and IWebHostEnvironment
        {
            _unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
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
        public ActionResult Upsert(int? id)
        {
            // Select Options 
            // The Variable ViewBag.CategoryList is equivalent to CategoryList
            // ViewBag.CategoryList = CategoryList;
           

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if(id == null || id == 0)
            {
                // create

                return View(productVM);
            }
            else
            {
               // update
               productVM.Product = _unitOfWork.Product.Get(u=> u.Id == id);
                return View(productVM);
            }

            
        }
        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;

                // if file is not null then we want to get that file 

                if(file != null)
                {
                    // renaming the file 
                    // This means a random guid name + file extension name

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // navigate or routing to the product path in wwwroot (location)

                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }


                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully.";
                return RedirectToAction("Index");

            }
            else
            {

                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            return View(productVM);
            }
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
