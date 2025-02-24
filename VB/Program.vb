Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System.IO

Namespace DocumentIteratorExample
    Friend Class Program
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main(ByVal args As String())

            Using wordProcessor As New RichEditDocumentServer()
                wordProcessor.LoadDocument("Markdown.docx", DocumentFormat.OpenXml)
                Dim document As Document = wordProcessor.Document
                Dim visitor As New MarkdownVisitor()
                Iterate(visitor, document)
                Dim newFilePath As String = "output.md"
                File.WriteAllText(newFilePath, visitor.Text)
            End Using

            Dim p As New Process()
            p.StartInfo = New ProcessStartInfo("output.md") With {
                .UseShellExecute = True
            }
            p.Start()
        End Sub

        Private Shared Sub Iterate(ByVal visitor As IDocumentVisitor, ByVal document As Document)
            Dim iterator As New DocumentIterator(document, True)
            While iterator.MoveNext()
                iterator.Current.Accept(visitor)
            End While
        End Sub
    End Class
End Namespace
