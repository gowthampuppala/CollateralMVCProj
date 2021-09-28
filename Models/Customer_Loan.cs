using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Models { 
    public class Customer_Loan
    {
        public int Id { get; set; }
        public int LoanProductId { get; set; }
        public int CustomerId { get; set; }
        public int LoanPrincipal { get; set; }
        public int Tenure { get; set; }
        public int Interest { get; set; }
        public int EMI { get; set; }
        public int CollateralId { get; set; }

    }
}
