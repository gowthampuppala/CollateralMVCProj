using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Models
{
    public partial class Login
    {
        [Required(ErrorMessage ="Email must be entered")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
