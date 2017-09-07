using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnaghaEnterprises.DataAccess;

namespace AnaghaEnterprises.Models
{
   public class InvoiceLineModel
    {
       
            public long InvoiceLineID { get; set; }
            public long InvoiceID { get; set; }
            public string Description { get; set; }
            public decimal? Nos_Bag { get; set; }
            public string Uom { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? Discount { get; set; }
            public decimal? TaxableValue { get; set; }
            public decimal? CGST_Rate { get; set; }
            public decimal? CGST_Amount { get; set; }
            public decimal? SGST_Rate { get; set; }
            public decimal? SGST_Amount { get; set; }
            public decimal? IGST_Rate { get; set; }
            public decimal? IGST_Amount { get; set; }
            public decimal? Total { get; set; }

        public bool Save()
        {
            try
            {
                using (var context = new AnaghaEnterprisesEntities())
                {
                    context.InvoiceLines.Add(new InvoiceLine()
                    {

                        InvoiceLineID = InvoiceLineID,
                        InvoiceID = InvoiceID,
                        Description = Description,
                        Nos_Bag = Nos_Bag,
                        Uom = Uom,
                        Rate = Rate,
                        Amount = Amount,
                        Discount = Discount,
                        TaxableValue = TaxableValue,
                        CGST_Rate = CGST_Rate,
                        CGST_Amount = CGST_Amount,
                        SGST_Rate = SGST_Rate,
                        SGST_Amount = SGST_Amount,
                        IGST_Rate = IGST_Rate,
                        IGST_Amount = IGST_Amount,
                        Total = Total
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
               return true;
            }
        }
    }
}
