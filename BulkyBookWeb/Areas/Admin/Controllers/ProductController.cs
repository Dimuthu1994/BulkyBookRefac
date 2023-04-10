using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);

    }


    //GET
    public IActionResult Upsert(int? id)
    {
        Product product = new();
        if (id == null || id == 0)
        {
            //create product
            return View(product);
        }
        else
        {
            //update product
        }
        

        return View(product);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType updated Successfully";
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
        var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (coverTypeFromDb == null)
        {
            return NotFound();
        }

        return View(coverTypeFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(CoverType obj)
    {

        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted Successfully";
        return RedirectToAction("Index");

    }
}
