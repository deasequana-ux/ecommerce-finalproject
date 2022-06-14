using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Models
{
    public class NewOrdersVM
    {
        public int Id { get; set; } //OrderId
        public string Email { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public EnumOrderState OrderState { get; set; }

        //ShippinDetails
        public string UserName { get; set; }
        public string AddressHeader { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        //End
    }
}
