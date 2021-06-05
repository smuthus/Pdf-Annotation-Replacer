using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Borders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf_worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadFile location
            string readfile = "C:\\test.pdf";
            //Write File location
            string writefile = "C:\\testresult.pdf";

            var doc=ReadFile(readfile, new PdfWriter(writefile));
            Document document = new Document(doc);
            document.Close();
        }
        public static PdfDocument ReadFile(string pdfPath,PdfWriter pw)
        {
            
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfPath),pw))
            {
                var pageNumbers = pdfDocument.GetNumberOfPages();
                for (int i = 1; i <= pageNumbers; i++)
                {
                    var annotations = pdfDocument.GetPage(i).GetAnnotations();

                    for(int j=0; j<=annotations.Count-1; j++)
                    {
                        var anno = annotations[j];
                        //reading executeable file link value
                        PdfDictionary pdfDictionary= (PdfDictionary)anno.GetPdfObject().Get(PdfName.A);
                        var dic1 = ((PdfDictionary)pdfDictionary.Get(PdfName.F));
                            var value=dic1.Get(PdfName.F).ToString();
                        //remove the annotation if you want
                        //anno.Remove(PdfName.A);
                        //anno.Remove(PdfName.F);
                        PdfLinkAnnotation annotation = new PdfLinkAnnotation(anno.GetPdfObject().GetAsRectangle(PdfName.Rect));
                     //concat file path to uri path
                        PdfAction action = PdfAction.CreateURI("http://www.google.com/"+value);
                        annotation.SetAction(action);
                        annotation.SetBorder(new PdfAnnotationBorder(0, 0, 0));
                        // annotation.SetPage(anno.GetPage());
                        pdfDocument.GetPage(i).AddAnnotation(annotation);
                    }

                }

                return pdfDocument;


            }
            
        }
    }
}
