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
    public class FavouriteController : Controller
    {
        private readonly IProductsService _service;

        public FavouriteController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int catId) //ilk olarak tüm ürünlerin gözükmesi
        {
            var favItems = HttpContext.Session.GetString("favItems");

            if (!string.IsNullOrEmpty(favItems))
            {
                var itemIds = favItems.Split(',').ToList();
                var allProducts = await _service.GetAllAsync();

                var relatedProducts = allProducts.Where(p => itemIds.Any(i => i == p.Id.ToString())).ToList();

                return View(relatedProducts);
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
            var favItems = HttpContext.Session.GetString("favItems");

            if (string.IsNullOrEmpty(favItems))
            {
                return Json(new
                {
                    ResponseMessage = "can not found any product in fav list",
                    StatusCode = 301
                });
            }
            else
            {
                var itemList = favItems.Split(',').ToList();

                if (itemList.Any(i => i == itemId))
                {
                    itemList.RemoveAll(i => i == itemId);

                    var newItemList = string.Join(",", itemList);

                    HttpContext.Session.SetString("favItems", newItemList);
                }
                else
                {
                    return Json(new
                    {
                        ResponseMessage = "Product id not found",
                        StatusCode = 301
                    });
                }
            }

            return Json(new
            {
                StatusCode = 200,
            });
        }

        [AllowAnonymous]
        public JsonResult AddItemFav(string itemId)
        {
            var favItems = HttpContext.Session.GetString("favItems");
            var favItemsCookie = Request.Cookies["favItems"];

            if (string.IsNullOrEmpty(favItemsCookie))
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("favItems", itemId, option);

                var favItemsCookie1 = Request.Cookies["favItems"];
            }
            else
            {
                var itemList = favItems.Split(',').ToList();

                if (!itemList.Any(i=>i == itemId))
                {
                    favItems += "," + itemId.ToString();
                    HttpContext.Session.SetString("favItems", favItems);
                }
            }
            
            return Json(new
            {
                StatusCode = 200,
            });
        }

    }
}
