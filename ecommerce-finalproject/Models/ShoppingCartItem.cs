using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ecommerce_finalproject.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Products Products { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }


    }
}
