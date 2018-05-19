Imports Microsoft.VisualBasic
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Linq

Namespace DocumentIteratorExample
	Partial Public Class Form1
		Inherits DevExpress.XtraBars.Ribbon.RibbonForm
		Public Sub New()
			InitializeComponent()
			ribbonControl1.SelectedPage = pageIterator
			richEditControl1.LoadDocument("Markdown.docx")
		End Sub

		Private Sub btnIteratorRun_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIteratorRun.ItemClick
			Dim visitor As New MarkdownVisitor()
			Iterate(visitor)

			memoEdit1.Text = visitor.Text
		End Sub
		Private Sub Iterate(ByVal visitor As IDocumentVisitor)
			Dim iterator As New DocumentIterator(richEditControl1.Document, True)
			Do While iterator.MoveNext()
				iterator.Current.Accept(visitor)
			Loop
		End Sub
	End Class

End Namespace
