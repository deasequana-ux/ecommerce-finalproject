using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Services;
using ecommerce_finalproject.Data.Static;
using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() 
        {
            var allCategories =  _context.Category.ToList();
            return View(allCategories);
        }

        public IActionResult CategoryDetail(int id)
        {
            var categories = _context.Products.Where(x=>x.ProductCategory == id).ToList();
            return View(categories);
        }

        //GET: Category/Create 
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //GET: Category/Edit
        public IActionResult Edit(int id)
        {
            var category = _context.Category.FirstOrDefault(x => x.Id == id);
            if (category == null) return View("NotFound");

            var response = new Category()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            };

            return View(response);
        }

        [HttpPost]
        public  IActionResult Edit(int id, Category category)
        {
            if (id != category.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Update(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var categoryId = _context.Category.FirstOrDefault(x => x.Id == id);
            if (categoryId == null) return View("NotFound");
            return View(categoryId);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var categoryId =  _context.Category.FirstOrDefault(x=>x.Id == id);
            if (categoryId == null) return View("NotFound");
            _context.Remove(categoryId);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
