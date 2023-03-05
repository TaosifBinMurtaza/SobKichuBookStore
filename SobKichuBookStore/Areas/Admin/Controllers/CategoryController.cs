using Microsoft.AspNetCore.Mvc;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using SobKichuBookStore.Models;
using SobKichuBookStore.Models.ViewModels;

namespace SobKichuBookStore.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unit;
        public CategoryController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> obj = _unit.CategoryRepo.GetAll();
            return View(obj);
        }
        public IActionResult Create()
        {      
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _unit.CategoryRepo.Add(obj);
                _unit.Save();
                TempData["Success"] = "Category has been created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var obj = _unit.CategoryRepo.GetFisrtOrDefault(u => u.Id == id);
                return View(obj);
            }         
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unit.CategoryRepo.Update(obj);
                _unit.Save();
                TempData["Success"] = "Category has been updated";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var obj = _unit.CategoryRepo.GetFisrtOrDefault(u => u.Id == id);
                return View(obj);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            
                _unit.CategoryRepo.Remove(obj);
                _unit.Save();
                TempData["Success"] = "Category has been deleted";
                return RedirectToAction("Index");
            
            

        }
    }
}
