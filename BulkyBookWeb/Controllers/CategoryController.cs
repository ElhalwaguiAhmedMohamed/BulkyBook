using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) // injected from Program.cs 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //Get 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly math the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly math the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // entityframework core update statement
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }
            var toBeDeleted = _db.Categories.Find(id);
            return View(toBeDeleted);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult Delete(Category obj)
        {
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
