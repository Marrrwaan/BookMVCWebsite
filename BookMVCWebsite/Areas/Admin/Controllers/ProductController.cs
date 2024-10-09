using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        List<Product> productList = _uniteOfWork.Product.GetAll().ToList();
        return View(productList);
    }
    public IActionResult Upsert(int? id) //update + insert
    {
        ProductVM productVM = new()
        {
            CategoryList = _uniteOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
            Product = new Product()
        };
        if (id == null || id == 0)
        {
            // create
            return View(productVM);
        }
        else
        {
         //update
            productVM.Product = _uniteOfWork.Product.Get(p => p.Id == id);
            return View(productVM);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            _uniteOfWork.Product.Add(productVM.Product);
            _uniteOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        else
        {
            productVM.CategoryList = _uniteOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productVM);
        }
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
