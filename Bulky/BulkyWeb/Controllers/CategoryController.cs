using AspNetCoreGeneratedDocument;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController (ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categorys.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create (Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categorys.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id) 
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            Category categoryFromDb = _db.Categorys.FirstOrDefault( x => x.Id.Equals(id));

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj) 
        {
            if (ModelState.IsValid)
            {
                _db.Categorys.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category categoryFromDb = _db.Categorys.FirstOrDefault(x => x.Id.Equals(id));

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _db.Remove(categoryFromDb);

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST (int? id)
        {
            Category? obj = _db.Categorys.Find(id);
            if (obj == null) 
            { 
                return NotFound();
            }
            _db.Categorys.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
