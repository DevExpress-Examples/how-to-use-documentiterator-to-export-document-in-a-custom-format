Imports Microsoft.VisualBasic
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Linq
Imports System.Text

Namespace DocumentIteratorExample
	Public MustInherit Class BufferedDocumentVisitor
		Inherits DocumentVisitorBase
		Private ReadOnly buffer_Renamed As StringBuilder
		Protected Sub New()
			Me.buffer_Renamed = New StringBuilder()
		End Sub
		Protected ReadOnly Property Buffer() As StringBuilder
			Get
				Return buffer_Renamed
			End Get
		End Property
	End Class
	Public Class MarkdownVisitor
		Inherits BufferedDocumentVisitor
		Private Const lastLowSpecial As Char = ChrW(&H1f)
		Private Const firstHighSpecial As Char = ChrW(&Hffff)
		Public ReadOnly Property Text() As String
			Get
				Return Buffer.ToString()
			End Get
		End Property

		Public Overrides Sub Visit(ByVal text As DocumentText)
			Dim prefix As String = GetPrefix(text.TextProperties)
			Buffer.Append(prefix)

			Dim count As Integer = text.Length
			For i As Integer = 0 To count - 1
				Dim ch As Char = text.Text(i)
				If ch > lastLowSpecial AndAlso ch < firstHighSpecial Then
					Buffer.Append(ch)
				ElseIf ch = ChrW(&H9) OrElse ch = ChrW(&HA) OrElse ch = ChrW(&HD) Then
					Buffer.Append(ch)
				End If
			Next i

			Buffer.Append(prefix)
		End Sub
		Public Overrides Sub Visit(ByVal paragraphStart As DocumentParagraphStart)
			If paragraphStart.ParagraphProperties.ParagraphStyle.Name = "heading 1" Then
				Buffer.Append("#")
			End If
		End Sub
		Public Overrides Sub Visit(ByVal inlinePicture As DocumentInlinePicture)
			InsertImageUri(inlinePicture.Uri)
		End Sub
		Public Overrides Sub Visit(ByVal picture As DocumentPicture)
			InsertImageUri(picture.Uri)
		End Sub
		Public Overrides Sub Visit(ByVal paragraphEnd As DocumentParagraphEnd)
			AppendLineOnNonEmptyContent()
		End Sub
		Public Overrides Sub Visit(ByVal sectionEnd As DocumentSectionEnd)
			AppendLineOnNonEmptyContent()
		End Sub
		Public Overrides Sub Visit(ByVal hyperlinkStart As DocumentHyperlinkStart)
			If (Not String.IsNullOrEmpty(hyperlinkStart.NavigateUri)) Then
				Buffer.Append("[")
			End If
		End Sub
		Public Overrides Sub Visit(ByVal hyperlinkEnd As DocumentHyperlinkEnd)
			If (Not String.IsNullOrEmpty(hyperlinkEnd.NavigateUri)) Then
				Buffer.Append(String.Format("]({0})", hyperlinkEnd.NavigateUri))
			End If
		End Sub
		Private Sub InsertImageUri(ByVal uri As String)
			If (Not String.IsNullOrEmpty(uri)) Then
				Buffer.Append(String.Format("![]({0})", uri))
			Else
				Buffer.Append("[[img src=attached-image.jpg alt=foobar]]")
			End If
		End Sub
		Private Function GetPrefix(ByVal properties As ReadOnlyTextProperties) As String
			Dim prefix As String = String.Empty
			If properties.FontBold Then
				prefix = "**"
			End If
			If properties.FontItalic Then
				prefix &= "*"
			End If
			If properties.StrikeoutType = StrikeoutType.Single Then
				prefix &= "~~"
			End If
			Return prefix
		End Function
		Private Sub AppendLineOnNonEmptyContent()
			If Buffer.Length > 0 Then
				Buffer.AppendLine()
			End If
		End Sub
	End Class
End Namespace
