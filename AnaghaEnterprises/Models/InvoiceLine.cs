using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnaghaEnterprises.Models
{
    public class InvoiceLine
    {
        public long InvoiceLineID { get; set; }
        [Required]
        public string Description { get; set; }
       [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? Nos_Bag { get; set; }
        public string Uom { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? Discount { get; set; }
        public decimal? TaxableValue { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? CGST_Rate { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? CGST_Amount { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? SGST_Rate { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? SGST_Amount { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? IGST_Rate { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? IGST_Amount { get; set; }
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Enter only number")]
        public decimal? Total { get; set; }
    }
}