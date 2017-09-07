using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AnaghaEnterprises.Helper
{
    public static class Utility
    {



        public static bool CreatePDFFromHTMLFile(string HtmlStream, string FileName)
        {
            try
            {
                object TargetFile = FileName;
                string ModifiedFileName = string.Empty;
                string FinalFileName = string.Empty;

                /* To add a Password to PDF -http://aspnettutorialonline.blogspot.com/ */
                HtmlToPdfBuilder builder = new HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);
                HtmlPdfPage first = builder.AddPage();
                first.AppendHtml(HtmlStream);
                byte[] file = builder.RenderPdf();
                File.WriteAllBytes(TargetFile.ToString(), file);

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
                ModifiedFileName = TargetFile.ToString();
                ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

                string password = "";
                iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, password, "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
                ////http://aspnettutorialonline.blogspot.com/
                reader.Close();
                if (File.Exists(TargetFile.ToString()))
                    File.Delete(TargetFile.ToString());
                FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
                File.Copy(ModifiedFileName, FinalFileName);
                if (File.Exists(ModifiedFileName))
                    File.Delete(ModifiedFileName);
                return true;
               
            }
            catch (Exception)
            {
              
            }
            return false;
        }
    }
}