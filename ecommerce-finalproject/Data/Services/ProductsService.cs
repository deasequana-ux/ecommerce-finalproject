using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_finalproject.Data.Base;
using ecommerce_finalproject.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_finalproject.Data.Services
{
    public class ProductsService : EntityBaseRepository<Products>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task AddNewProductsAsync(NewProductsVM data)
        {
            var newProduct = new Products()
            {
                name = data.Name,
                description = data.Description,
                stockCode = data.StockCode,
                imageURL = data.ImageURL,
                price = data.Price,
                ProductCategory = data.ProductCategory
                
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

        }

        //public async Task DeleteProductsAsync(NewProductsVM data)
        //{
        //    var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);
        //    if (dbProduct != null)
        //    {
        //        _context.Products.Remove(dbProduct);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<Products> GetProductsByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
                
        }

        public async Task<List<Products>> GetProductsByCategoryId(ProductCategory cat)
        {

            var relatedProducts =  _context.Products.Where(p => p.ProductCategory == cat).ToList();


            return relatedProducts;
        }

        public async Task UpdateProductsAsync(NewProductsVM data)
        {

            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbProduct != null)
            {
                dbProduct.name = data.Name;
                dbProduct.description = data.Description;
                dbProduct.stockCode = data.StockCode;
                dbProduct.imageURL = data.ImageURL;
                dbProduct.price = data.Price;
                dbProduct.ProductCategory = data.ProductCategory;

                await _context.SaveChangesAsync();
            }

            
        }
    }
}
