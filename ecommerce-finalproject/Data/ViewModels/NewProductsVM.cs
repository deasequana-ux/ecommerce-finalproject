using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Models
{
    public class NewProductsVM
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product picture URL")]
        [Required(ErrorMessage = "Product picture is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Stock code")]
        [Required(ErrorMessage = "Stock code is required")]
        public int StockCode { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public int ProductCategory { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
