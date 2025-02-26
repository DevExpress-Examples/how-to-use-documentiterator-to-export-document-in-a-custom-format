using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Diagnostics;
using System.IO;

namespace DocumentIteratorExample {
    class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {

            using (RichEditDocumentServer wordProcessor = new RichEditDocumentServer())
            {
                wordProcessor.LoadDocument("Markdown.docx", DocumentFormat.OpenXml);
                Document document = wordProcessor.Document;
                MarkdownVisitor visitor = new MarkdownVisitor();
                Iterate(visitor, document);
                string newFilePath = "output.md";
                File.WriteAllText(newFilePath, visitor.Text);
            }

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"output.md")
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private static void Iterate(IDocumentVisitor visitor, Document document)
        {
            DocumentIterator iterator = new DocumentIterator(document, true);
            while (iterator.MoveNext())
                iterator.Current.Accept(visitor);
        }
    }
}
