using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Models
{
    public class SubscribeModel
    {
         
        [Required(ErrorMessage = "Please Provide Id ljzcnvefjbnvfsn")]
        public int Email { get; set; }
        
    }
}
