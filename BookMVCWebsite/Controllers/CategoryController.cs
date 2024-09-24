﻿using BookMVCWebsite.Data;
using BookMVCWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMVCWebsite.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        this._db = db;
    }
    public IActionResult Index()
    {
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
    }
}
