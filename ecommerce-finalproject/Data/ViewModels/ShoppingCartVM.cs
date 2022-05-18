using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using ecommerce_finalproject.Data.Cart;
using ecommerce_finalproject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }   
    }
}
