using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public string LoanProducts { get; set; }
        public int MaxLoanEligible { get; set; }
        public int Interest { get; set; }
        public int Tenure { get; set; }
        public string TypeOfCollateralAccepted { get; set; }
    }
}
