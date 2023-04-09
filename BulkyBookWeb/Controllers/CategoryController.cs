
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryRepository _db;

    public CategoryController(ICategoryRepository db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.GetAll();
        return View(objCategoryList);

    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly mactch the Name. !");
        }

        if (ModelState.IsValid)
        {
            _db.Add(obj);
            _db.Save();
            TempData["success"] = "Category created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.GetFirstOrDefault(c => c.Id == id);
        //var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Name == "id");
        //var categoryFromDbFirst = _db.Categories.SingleOrDefault(c => c.Id == id);
        //var categoryFromDb = _db.Categories.Find(id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly mactch the Name. !");
        }

        if (ModelState.IsValid)
        {
            _db.Update(obj);
            _db.Save();
            TempData["success"] = "Category updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var category = _db.Categories.FirstOrDefault(c => c.Id == id);
        // var categoryFromDbFirst = _db.Categories.SingleOrDefault(c => c.Id == id);
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDb = _db.GetFirstOrDefault(u =>u.Id == id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category obj)
    {
      
        _db.Remove(obj);
        _db.Save();
        TempData["success"] = "Category deleted Successfully";
        return RedirectToAction("Index");
     
    }
}
