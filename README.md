# Pdf-Annotation-Replacer with iText7 .net C#
This will Replace an existing annotation  

This program especially get the file link path from the pdf and convert it to URI web link path with existing value and save the pdf.

It use Itext7 library .net c#

//Code//

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
 

Please Contribute

paypal.me/smuthus

Email : smuthus333@gmail.com


