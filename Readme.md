<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/134077062/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T632549)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to use DocumentIterator to export a rich edit document in a custom document format


<p>This example demonstrates how to export RichEditControl's document in a custom document format using a <a href="http://help.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditAPINativeDocumentIteratortopic">DocumentIterator</a> object and a custom <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraRichEdit.API.Native.IDocumentVisitor.class">document visitor</a>. The DocumentIterator is created to iterate through document elements. The custom document visitor is used to export document elements to a lightweight markup language (<a href="https://en.wikipedia.org/wiki/Markdown">Markdown</a>)/<br><br>The document visitor is implemented according to the <strong>Visitor pattern </strong>(by implementing the <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraRichEdit.API.Native.IDocumentVisitor.class">IDocumentVisitor</a> interface). In this example, the custom document visitor is inherited from the <strong>DocumentVisitorBase </strong>class. The <strong>DocumentVisitorBase</strong> class implements the <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraRichEdit.API.Native.IDocumentVisitor.class">IDocumentVisitor</a> interface by default. Thus, you can override only required methods of the <strong>DocumentVisitorBase</strong> class for custom document processing.<br><br>The document iterator moves over each document structure item and passes a custom document visitor object to the current item. The custom document visitor builds a string with lightweight markup for the currently processed document item.</p>
<br>See also: <a href="https://documentation.devexpress.com/WindowsForms/116746/Controls-and-Libraries/Rich-Text-Editor/Traversing-the-Document">Traversing the Document</a>

<br/>


