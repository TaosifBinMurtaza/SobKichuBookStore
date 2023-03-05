using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using SobKichuBookStore.Models;
using SobKichuBookStore.Models.ViewModels;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SobKichuBookStore.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unit;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork unit, IWebHostEnvironment webHostEnvironment)
        {
            this.unit = unit;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id=0)
        {
            Product product= new();
            IEnumerable<SelectListItem> categoryList = unit.CategoryRepo.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }
                );

            if (id == 0)
            {
                //create
                ViewBag.Category = categoryList;
                return View(product);
            }
            else
            {
                //update
                product = unit.ProductRepo.GetFisrtOrDefault(u => u.Id == id);
                ViewBag.Category = categoryList;
                return View(product);
            }

        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj,IFormFile? file)
        {
           if(ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;

                if(file!=null)
				{
					var pathName = Path.Combine(wwwRootPath, @"images\products");
					string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.ImageUrl!= null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(pathName, fileName + extension),FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
					obj.ImageUrl = @"\images\products\" + fileName + extension;
				}
                if(obj.Id==0)
                {
                    unit.ProductRepo.Add(obj);
                    TempData["Success"] = "Category has been created";
                }
                else
                {
                    unit.ProductRepo.Update(obj);
                    TempData["Success"] = "Category has been Updated";
                }
                			
                unit.Save();
				return RedirectToAction("Index");
			}
           else
            {
				return View(obj);
			}
           

        }

        
        public IActionResult Delete(int? id=0)
        {
            var obj = unit.ProductRepo.GetFisrtOrDefault(u => u.Id == id);

            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            unit.ProductRepo.Remove(obj);
            unit.Save();
            return RedirectToAction("Index");

        }


        #region Api call
        [HttpGet]
		public IActionResult GetAll()
		{
			var productlist=unit.ProductRepo.GetAll(includeProperties:"category");
            return Json(new { data=productlist });
	    }

        

        #endregion



    }

}
