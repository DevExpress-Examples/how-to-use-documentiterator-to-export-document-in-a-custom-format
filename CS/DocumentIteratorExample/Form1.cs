using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Linq;

namespace DocumentIteratorExample {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        public Form1() {
            InitializeComponent();
            ribbonControl1.SelectedPage = pageIterator;
            richEditControl1.LoadDocument("Markdown.docx");                 
        }

        private void btnIteratorRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MarkdownVisitor visitor = new MarkdownVisitor();
            Iterate(visitor);
           
            memoEdit1.Text = visitor.Text;
        }
        private void Iterate(IDocumentVisitor visitor)
        {
            DocumentIterator iterator = new DocumentIterator(richEditControl1.Document, true);
            while (iterator.MoveNext())
                iterator.Current.Accept(visitor);
        }
    }

}
