using Book.DataAccess.Repository.IRepository;
using BookMVCWebsite.DataAccess.Data;
using BookMVCWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMVCWebsite.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryController(ICategoryRepository db)
    {
        this._categoryRepo = db;
    }
    public IActionResult Index()
    {
        List<Category> categories = _categoryRepo.GetAll().ToList();
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
            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Category? categoryFromDb = _categoryRepo.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            TempData["success"]= "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Category? categoryFromDb = _categoryRepo.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category? categoryFromDb = _categoryRepo.Get(c => c.Id == id);

        if (categoryFromDb == null) { return NotFound(); }

        _categoryRepo.Remove(categoryFromDb);
        _categoryRepo.Save();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}
