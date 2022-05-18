using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }

      
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
