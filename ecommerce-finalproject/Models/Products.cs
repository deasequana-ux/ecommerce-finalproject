using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Models
{
    public class Products:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Display(Name = "Information of product")]
        public string description { get; set; }
        public double price { get; set; }

        [Display(Name = "Profile Picture URL")]
        public string imageURL { get; set; }
        public int stockCode { get; set; }
        public int ProductCategory { get; set; }
        //public string CategoryName { get; set; }
        //public ProductCategory ProductCategory { get; set; }

    }
}
