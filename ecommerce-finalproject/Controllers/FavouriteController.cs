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
    public class FavouriteController : Controller
    {
        private readonly IProductsService _service;
        private readonly AppDbContext _context;

        public FavouriteController(IProductsService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int catId) //ilk olarak tüm ürünlerin gözükmesi
        {

            var favItemsCookie = Request.Cookies["favItems"];

            if (!string.IsNullOrEmpty(favItemsCookie))
            {
                var itemIds = favItemsCookie.Split(',').ToList();
                var allProducts = await _service.GetAllAsync();

                var relatedProducts = allProducts.Where(p => itemIds.Any(i => i == p.Id.ToString())).ToList();
                var favList = new List<ProductVM>();
                var categoryList = _context.Category.ToList();

                foreach (var product in relatedProducts)
                {
                    favList.Add(new ProductVM()
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

                return View(favList);
            }
            else
            {
                // hiç favori yoksa
                return View();
            }
        }

        [AllowAnonymous]
        public JsonResult RemoveItemFav(string itemId)
        {
            try
            {
                var favItemsCookie = Request.Cookies["favItems"];

                if (!string.IsNullOrEmpty(favItemsCookie))
                {
                    var itemList = favItemsCookie.Split(',').ToList();
                    itemList.Remove(itemId);

                    var newList = string.Join(",", itemList);

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("favItems", newList, option);

                    return Json(new
                    {
                        StatusCode = 200,
                    });
                }
                else
                {
                    return Json(new
                    {
                        StatusCode = 300,
                    });
                }

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    StatusCode = 300,
                });
            }
        }

        [AllowAnonymous]
        public JsonResult AddItemFav(string itemId)
        {

            try
            {
                var favItemsCookie = Request.Cookies["favItems"];

                if (string.IsNullOrEmpty(favItemsCookie))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("favItems", itemId, option);

                }
                else
                {
                    var itemList = favItemsCookie.Split(',').ToList();

                    if (!itemList.Any(i => i == itemId))
                    {
                        favItemsCookie += "," + itemId.ToString();

                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Append("favItems", favItemsCookie, option);
                    }
                }
                return Json(new
                {
                    StatusCode = 200,
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    StatusCode = 300,
                });
            }

        }
    }
}
