using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Services;
using ecommerce_finalproject.Data.Static;
using ecommerce_finalproject.Data.ViewModels;
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
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly AppDbContext _context;

        public ProductsController(IProductsService service,AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index() //ilk olarak tüm ürünlerin gözükmesi
        {
            var allProducts = await _service.GetAllAsync();
            var productList = new List<ProductVM>();
            var categoryList =  _context.Category.ToList();

            foreach (var product in allProducts)
            {
                productList.Add(new ProductVM()
                {
                    Id = product.Id,
                    description = product.description,
                    CategoryName = categoryList.FirstOrDefault(c => c.Id == product.ProductCategory).CategoryName,
                    imageURL = product.imageURL,
                    name = product.name,
                    price = product.price,
                    stockCode = product.stockCode,
                    ProductCategory = product.ProductCategory
                });
            }

            return View(productList);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString) //ürünlerde filtreleme işlemi
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.name.ToLower().Contains(searchString.ToLower()) || n.description.ToLower().Contains(searchString.ToLower())).ToList();

                var productList = new List<ProductVM>();
                var categoryList = _context.Category.ToList();

                foreach (var product in filteredResult)
                {
                    productList.Add(new ProductVM()
                    {
                        Id = product.Id,
                        description = product.description,
                        CategoryName = categoryList.FirstOrDefault(c => c.Id == product.ProductCategory).CategoryName,
                        imageURL = product.imageURL,
                        name = product.name,
                        price = product.price,
                        stockCode = product.stockCode,
                        ProductCategory = product.ProductCategory
                    });
                }


                return View("Index", productList);
            }

            return View("Index", allProducts);
        }

        //GET: Products/Details/1 
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)  //bir ürünün show details butonuna tıklayınca detay sayfasının gelmesi
        {
            var productDetail = await _service.GetProductsByIdAsync(id);
            var category = _context.Category.FirstOrDefault(cl => cl.Id == productDetail.ProductCategory)?.CategoryName;
            var product = new ProductVM()
            {
                Id = productDetail.Id,
                description = productDetail.description,
                CategoryName = category,
                imageURL = productDetail.imageURL,
                name = productDetail.name,
                price = productDetail.price,
                stockCode = productDetail.stockCode,
                ProductCategory = productDetail.ProductCategory
            };
            return View(product);
        }

        //GET: Products/Create 
        public IActionResult Create() //Add new butonu ile yeni ürün ekleme
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is the store description.";

            var model = new NewProductsVM();
            model.Categories = _context.Category.ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductsVM products)
        {
            if (!ModelState.IsValid)
            {
                return View(products);
            }
            await _service.AddNewProductsAsync(products);
            return RedirectToAction(nameof(Index));
        }


        //GET: Products/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductsByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductsVM()
            {
                Id = productDetails.Id,
                Name = productDetails.name,
                Description = productDetails.description,
                Price = productDetails.price,
                ImageURL = productDetails.imageURL,
                StockCode = productDetails.stockCode,
                ProductCategory = productDetails.ProductCategory,
            };

            response.Categories = _context.Category.ToList();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductsVM products)
        {
            if (id != products.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                return View(products);
            }
            await _service.UpdateProductsAsync(products);
            return RedirectToAction(nameof(Index));
        }

        //Get Products/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
