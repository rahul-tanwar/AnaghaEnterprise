using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnaghaEnterprises.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnaghaEnterprises.Models
{
   
    public class InvoiceModel
    {
        public long InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PO_No { get; set; }
        public string TransportationMode { get; set; }
        public string Veh_No { get; set; }
        public string LR_No { get; set; }
        public DateTime? LR_Date { get; set; }
        public DateTime? DateTimeSupply { get; set; }
        public string CustomerName { get; set; }
        public string GST_No { get; set; }
        public string PAN_No { get; set; }
        public string CompanyAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal? Total_Nos_Kg { get; set; }
        public decimal? Total_Nos_Bag { get; set; }
        public decimal? TotalAmountBeforeTax { get; set; }
        public decimal? Total_CGST { get; set; }
        public decimal? Total_SGST { get; set; }
        public decimal? Total_IGST { get; set; }
        public decimal? Total_GST { get; set; }
        public decimal? TotalAmountAfterTax { get; set; }
        public decimal? TotalAfterRoundOff { get; set; }

        public bool save()
        {
            try
            {
                using (var context = new AnaghaEnterprisesEntities())
                {
                    context.Invoices.Add(new Invoice
                    {
                        InvoiceID = InvoiceID,
                        InvoiceDate = InvoiceDate,
                        PO_No = PO_No,
                        TransportationMode = TransportationMode,
                        Veh_No = Veh_No,
                        LR_No = LR_No,
                        LR_Date = LR_Date,
                        DateTimeSupply = DateTimeSupply,
                        CustomerName = CustomerName,
                        GST_No = GST_No,
                        PAN_No = PAN_No,
                        CompanyAddress = CompanyAddress,
                        ShippingAddress = ShippingAddress,
                        Total_Nos_Kg = Total_Nos_Kg,
                        Total_Nos_Bag = Total_Nos_Bag,
                        TotalAmountBeforeTax = TotalAmountBeforeTax,
                        Total_CGST = Total_CGST,
                        Total_SGST = Total_SGST,
                        Total_IGST = Total_IGST,
                        Total_GST = Total_GST,
                        TotalAmountAfterTax = TotalAmountAfterTax,
                        TotalAfterRoundOff = TotalAfterRoundOff
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            


        }

        public List<InvoiceModel> GetAllInvoice()
        {
            try
            {
                using (var context = new AnaghaEnterprisesEntities())
                {
                    return context.Invoices.Select(m => new InvoiceModel
                    {
                        InvoiceID = m.InvoiceID,
                        InvoiceDate = m.InvoiceDate,
                        PO_No = m.PO_No,
                        TransportationMode = m.TransportationMode,
                        Veh_No = m.Veh_No,
                        LR_No = m.LR_No,
                        LR_Date = m.LR_Date,
                        DateTimeSupply = m.DateTimeSupply,
                        CustomerName = m.CustomerName,
                        GST_No = m.GST_No,
                        PAN_No = m.PAN_No,
                        CompanyAddress = m.CompanyAddress,
                        ShippingAddress = m.ShippingAddress,
                        Total_Nos_Kg = m.Total_Nos_Kg,
                        Total_Nos_Bag = m.Total_Nos_Bag,
                        TotalAmountBeforeTax = m.TotalAmountBeforeTax,
                        Total_CGST = m.Total_CGST,
                        Total_SGST = m.Total_SGST,
                        Total_IGST = m.Total_IGST,
                        Total_GST = m.Total_GST,
                        TotalAmountAfterTax = m.TotalAmountAfterTax,
                        TotalAfterRoundOff = m.TotalAfterRoundOff

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get invoice list", ex);
            }
           
        } 

        public bool Delete()
        {
            try
            {
                var context = new AnaghaEnterprisesEntities();
                var invoice = new Invoice() { InvoiceID = this.InvoiceID };
                context.Invoices.Attach(invoice);
                context.Invoices.Remove(invoice);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete invoice", ex);
            }
        }
   }
}
