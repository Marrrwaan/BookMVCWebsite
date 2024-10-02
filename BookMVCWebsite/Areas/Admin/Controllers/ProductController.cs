using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMVCWebsite.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _uniteOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _uniteOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Product> categories = _uniteOfWork.Product.GetAll().ToList();
        return View(categories);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product obj)
    {
        if (ModelState.IsValid)
        {
            _uniteOfWork.Product.Add(obj);
            _uniteOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Product? productFromDb = _uniteOfWork.Product.Get(c => c.Id == id);

        if (productFromDb == null) { return NotFound(); }

        return View(productFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _uniteOfWork.Product.Update(obj);
            _uniteOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) { return NotFound(); }

        Product? productFromDb = _uniteOfWork.Product.Get(c => c.Id == id);

        if (productFromDb == null) { return NotFound(); }

        return View(productFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Product? productFromDb = _uniteOfWork.Product.Get(c => c.Id == id);

        if (productFromDb == null) { return NotFound(); }

        _uniteOfWork.Product.Remove(productFromDb);
        _uniteOfWork.Save();
        TempData["success"] = "Product deleted successfully";

        return RedirectToAction("Index");
    }
}
