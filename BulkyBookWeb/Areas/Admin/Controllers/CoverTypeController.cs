using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);

    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType created Successfully";
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
        var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        

        if (coverTypeFromDb == null)
        {
            return NotFound();
        }

        return View(coverTypeFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
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
