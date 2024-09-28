using Book.DataAccess.Repository.IRepository;
using BookMVCWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMVCWebsite.Controllers;

public class CategoryController : Controller
{
    private readonly IUnitOfWork _uniteOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _uniteOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Category> categories = _uniteOfWork.Category.GetAll().ToList();
        return View(categories);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (ModelState.IsValid)
        {
            _uniteOfWork.Category.Add(obj);
            _uniteOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Category? categoryFromDb = _uniteOfWork.Category.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _uniteOfWork.Category.Update(obj);
            _uniteOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Category? categoryFromDb = _uniteOfWork.Category.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category? categoryFromDb = _uniteOfWork.Category.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        _uniteOfWork.Category.Remove(categoryFromDb);
        _uniteOfWork.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}
