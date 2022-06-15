using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Models
{
    public class Category:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

    }
}
