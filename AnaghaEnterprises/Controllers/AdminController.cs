using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AnaghaEnterprises.Models;
using AnaghaEnterprises.Helper;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace AnaghaEnterprises.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            Invoice invoiceObj = new Invoice()
            {
                InvoiceLineList = new List<InvoiceLine>()
                {
                    new InvoiceLine()
                }
            };
            return View(invoiceObj);
        }

        public ActionResult Search()
        {
            var obj = new Invoice();
            return View(obj.GetAllInvoice());
        }

        public ActionResult Report()
        {
            var obj = new Invoice();
            return View();
        }

        public PartialViewResult AddInvoiceLine(int number)
        {
            return PartialView("_AddInvoiceLine", number);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult SaveInvoice(Invoice model)
        {
            var response = new JsonResponse() {Success = false,Message = "Please try again"};
            if (ModelState.IsValid)
            {
                if (model.Save())
                {
                    response.Success = true;
                    response.Message = "Successfully save";
                    response.ReturnUrl = Url.Action("Index", "Admin");
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteInvoice(long InvoiceId)
        {
            var modelobj = new Invoice() { InvoiceID = InvoiceId };
            if (modelobj.DeleteInoice())
            {
                return View("Search", modelobj.GetAllInvoice());
            }
            return null;
        }
        public ActionResult GetReport(string dateRange)
        {
            var report = new InvoiceReport();
            var modelobj = new Invoice() { InvoiceDate = dateRange };
            var result = modelobj.GetAllInvoice();
            if (result != null && result.Count() > 0)
            {
                report.InvoiceList = result;
                report.TotalIGST = result.Select(m => m.Total_IGST).Sum();
                report.TotalSGST = result.Select(m => m.Total_SGST).Sum();
                report.TotalCGST = result.Select(m => m.Total_CGST).Sum();
                report.TotalGST = result.Select(m => m.Total_GST).Sum();
                report.TotalAmountBefore = result.Select(m => m.TotalAmountBeforeTax).Sum();
                report.TotalAmountAfter = result.Select(m => m.TotalAfterRoundOff).Sum();
                return View("Report", report);
            }
            return null;
        }

       public bool generate(string htmlstring)
        {

          //  StringBuilder sb = new StringBuilder();
           //Generate Invoice (Bill) Header.
         //   string strHtml = string.Empty;
            //   //HTML File path -http://aspnettutorialonline.blogspot.com/
          //    string htmlFileName = Request.PhysicalApplicationPath + "\\files\\" + "ConvertHTMLToPDF.htm";
            //string htmlFileName =    Server.MapPath("~/files/ConvertHTMLToPDF.html");
            //pdf file path. -http://aspnettutorialonline.blogspot.com/
            string pdfFileName = Request.PhysicalApplicationPath + "\\files\\" + "ConvertHTMLToPDF.pdf";
            string cssFileName = Server.MapPath("~/Content/style.css");
            //reading html code from html file
            //FileStream fsHTMLDocument = new FileStream(htmlFileName, FileMode.Open, FileAccess.Read);
            //StreamReader srHTMLDocument = new StreamReader(fsHTMLDocument);
            //strHtml = srHTMLDocument.ReadToEnd();
            //srHTMLDocument.Close();
            //StringReader sr = new StringReader(sb.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Write(pdfDoc);
            //Response.End();
            //return File(Response.OutputStream, "application/pdf", "attachment;filename=Invoice_" + ".pdf");
            //strHtml = strHtml.Replace("\r\n", "");
            //strHtml = strHtml.Replace("\0", "");
            //if (Utility.CreatePDFFromHTMLFile(sb.ToString(), pdfFileName, cssFileName))
            //{
            //    return true;
            //}
            Utility.PDFFromHTMLFile(htmlstring, pdfFileName, cssFileName);
            return false;
          
       }

        [HttpGet]
        public virtual ActionResult Download()
        {
            string fullPath = Path.Combine(Server.MapPath("~/files"), "ConvertHTMLToPDF.pdf");
            return File(fullPath, "application/pdf", "ConvertHTMLToPDF.pdf");
        }
     }
}