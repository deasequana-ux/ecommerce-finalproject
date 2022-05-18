using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Models
{
    public class ShippingDetails //siparişle ilgili adres bilgileri
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the address header.")]
        public string AddressHeader { get; set; }

        [Required(ErrorMessage = "Please enter the address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select the city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select the town.")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Please enter the post code.")]
        public string PostCode { get; set; }

        

    }
}
