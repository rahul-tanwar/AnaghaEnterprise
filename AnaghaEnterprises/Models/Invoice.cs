using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnaghaEnterprises.Models
{
    public class Invoice
    {
        public long InvoiceID { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        public string PO_No { get; set; }
        public string TransportationMode { get; set; }
        public string Veh_No { get; set; }
        public string LR_No { get; set; }
        [Required]
        public string LR_Date { get; set; }
        [Required]
        public string DateTimeSupply { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string GST_No { get; set; }
        [Required]
        public string PAN_No { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
        [Required]
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
        public List<InvoiceLine> InvoiceLineList { get; set; }


        public bool Save()
        {
            var invoiceObj = new InvoiceModel()
            {
                InvoiceID = InvoiceID,
                InvoiceDate = Convert.ToDateTime(InvoiceDate),
                PO_No = PO_No,
                TransportationMode = TransportationMode,
                Veh_No = Veh_No,
                LR_No = LR_No,
                LR_Date = Convert.ToDateTime(LR_Date),
                DateTimeSupply = Convert.ToDateTime(DateTimeSupply),
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
            };
            if (invoiceObj.save())
            {
                foreach (var item in InvoiceLineList)
                {
                    var inviceLineObj = new InvoiceLineModel()
                    {
                        InvoiceLineID = item.InvoiceLineID,
                        InvoiceID = InvoiceID,
                        Description = item.Description,
                        Nos_Bag = item.Nos_Bag,
                        Uom = item.Uom,
                        Rate = item.Rate,
                        Amount = item.Amount,
                        Discount = item.Discount,
                        TaxableValue = item.TaxableValue,
                        CGST_Rate = item.CGST_Rate,
                        CGST_Amount = item.CGST_Amount,
                        SGST_Rate = item.SGST_Rate,
                        SGST_Amount = item.SGST_Amount,
                        IGST_Rate = item.IGST_Rate,
                        IGST_Amount = item.IGST_Amount,
                        Total = item.Total
                    };
                    inviceLineObj.Save();
                }
                return true;
            }
            return false;
        }

        public List<Invoice> GetAllInvoice()
        {
            var invoiceObj = new InvoiceModel();
            List<Invoice> invoiceList = new List<Invoice>();
            var result = invoiceObj.GetAllInvoice();
            if (result != null && result.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.InvoiceDate))
                {
                    string[] daterange = InvoiceDate.Split('|');
                    result = result.Where(m => m.InvoiceDate >= Convert.ToDateTime(daterange[0]) && m.InvoiceDate <= Convert.ToDateTime(daterange[1])).ToList();
                }
                if (result != null && result.Count > 0)
                {
                    invoiceList = result.Select(m => new Invoice
                    {
                        InvoiceID = m.InvoiceID,
                        InvoiceDate = Convert.ToString(m.InvoiceDate),
                        PO_No = m.PO_No,
                        TransportationMode = m.TransportationMode,
                        Veh_No = m.Veh_No,
                        LR_No = m.LR_No,
                        LR_Date = Convert.ToString(m.LR_Date),
                        DateTimeSupply = Convert.ToString(m.DateTimeSupply),
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
            return invoiceList;

        }

        public bool DeleteInoice()
        {
            if (this.InvoiceID > 0)
            {
                var InvoiceModel = new InvoiceModel() { InvoiceID = this.InvoiceID };
                if (InvoiceModel.Delete())
                {
                    return true;
                }
               
            }
            return false;
        }

     
    }
}

   
