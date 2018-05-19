using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Linq;
using System.Text;

namespace DocumentIteratorExample
{
    public abstract class BufferedDocumentVisitor : DocumentVisitorBase
    {
        readonly StringBuilder buffer;
        protected BufferedDocumentVisitor()
        {
            this.buffer = new StringBuilder();
        }
        protected StringBuilder Buffer { get { return buffer; } }
    }
    public class MarkdownVisitor : BufferedDocumentVisitor
    {
        const char lastLowSpecial = '\x1f';
        const char firstHighSpecial = '\xffff';
        public string Text { get { return Buffer.ToString(); } }

        public override void Visit(DocumentText text)
        {
            string prefix = GetPrefix(text.TextProperties);
            Buffer.Append(prefix);

            int count = text.Length;
            for (int i = 0; i < count; i++)
            {
                char ch = text.Text[i];
                if (ch > lastLowSpecial && ch < firstHighSpecial)
                    Buffer.Append(ch);
                else if (ch == '\x9' || ch == '\xA' || ch == '\xD')
                    Buffer.Append(ch);
            }

            Buffer.Append(prefix);
        }
        public override void Visit(DocumentParagraphStart paragraphStart)
        {
            if (paragraphStart.ParagraphProperties.ParagraphStyle.Name == "heading 1")
                Buffer.Append("#");
        }
        public override void Visit(DocumentInlinePicture inlinePicture)
        {
            InsertImageUri(inlinePicture.Uri);
        }
        public override void Visit(DocumentPicture picture)
        {
            InsertImageUri(picture.Uri);
        }
        public override void Visit(DocumentParagraphEnd paragraphEnd)
        {
            AppendLineOnNonEmptyContent();
        }
        public override void Visit(DocumentSectionEnd sectionEnd)
        {
            AppendLineOnNonEmptyContent();
        }
        public override void Visit(DocumentHyperlinkStart hyperlinkStart)
        {
            if (!String.IsNullOrEmpty(hyperlinkStart.NavigateUri))
                Buffer.Append("[");
        }
        public override void Visit(DocumentHyperlinkEnd hyperlinkEnd)
        {
            if (!String.IsNullOrEmpty(hyperlinkEnd.NavigateUri))
                Buffer.Append(String.Format("]({0})", hyperlinkEnd.NavigateUri));
        }
        void InsertImageUri(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
                Buffer.Append(String.Format("![]({0})", uri));
            else
                Buffer.Append("[[img src=attached-image.jpg alt=foobar]]");
        }
        string GetPrefix(ReadOnlyTextProperties properties)
        {
            string prefix = string.Empty;
            if (properties.FontBold)
                prefix = "**";
            if (properties.FontItalic)
                prefix += "*";
            if (properties.StrikeoutType == StrikeoutType.Single)
                prefix += "~~";
            return prefix;
        }
        void AppendLineOnNonEmptyContent()
        {
            if (Buffer.Length > 0)
                Buffer.AppendLine();
        }
    }
}
