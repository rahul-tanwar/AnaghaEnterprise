using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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
        public static bool PDFFromHTMLFile(string htmlFileName,string pdffile, string css)
        {
            byte[] pdf; // result will be here

            var cssText = File.ReadAllText(css);
            var html = htmlFileName;  //File.ReadAllText(htmlFileName);


            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.LETTER, 20, 20, 60, 60);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }

                document.Close();

                pdf = memoryStream.ToArray();
                File.WriteAllBytes(pdffile, pdf);
            }
            return true;
        }
    }
}