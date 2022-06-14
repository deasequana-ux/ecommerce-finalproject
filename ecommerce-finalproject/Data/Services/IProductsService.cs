using ecommerce_finalproject.Data.Base;
using ecommerce_finalproject.Models;
using ecommerce_finalproject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Data.Services
{
    public interface IProductsService:IEntityBaseRepository<Products>
    {

        Task<Products> GetProductsByIdAsync(int id);

        Task AddNewProductsAsync(NewProductsVM data);
        Task UpdateProductsAsync(NewProductsVM data);
    }
}
