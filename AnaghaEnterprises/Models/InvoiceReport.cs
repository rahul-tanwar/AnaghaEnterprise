using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnaghaEnterprises.Models
{
    public class InvoiceReport
    {
        public List<Invoice> InvoiceList { get; set;  }
        public Decimal? TotalIGST { get; set; }
        public Decimal? TotalSGST { get; set; }
        public Decimal? TotalCGST { get; set; }
        public Decimal? TotalGST { get; set; }
        public Decimal? TotalAmountBefore { get; set; }
        public Decimal? TotalAmountAfter { get; set; }
    }
}