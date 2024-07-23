using Microsoft.AspNetCore.Mvc;
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

        private readonly IWebHostEnvironment _webHostEnvironment; 
        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment) //Dependency Injection of IUnitofWork and IWebHostEnvironment
        {
            _unitOfWork = unitOfWork;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: CategoryController

        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // if file is not null then we want to get that file 

                if(file != null)
                {
                    // renaming the file 
                    // This means a random guid name + file extension name

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // navigate or routing to the product path in wwwroot (location)

                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // DELETE OLD IMAGE
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + filename; 

                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else 
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
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

        #region API CALLS

        [HttpGet]
        public ActionResult GetAll() 
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }


        #endregion

        }
    }

