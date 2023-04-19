<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/134077062/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T632549)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# WinForms Rich Text Editor - Use DocumentIterator to Export a Document in a Custom Format

This example demonstrates how to use a [DocumentIterator](https://docs.devexpress.com/OfficeFileAPI/DevExpress.XtraRichEdit.API.Native.DocumentIterator) object and a [DocumentVisitor](https://docs.devexpress.com/OfficeFileAPI/DevExpress.XtraRichEdit.API.Native.IDocumentVisitor) implementation to export a document in a custom format. The `DocumentIterator` navigates through document elements. The document visitor is used to export document elements to a lightweight markup language ([Markdown](https://en.wikipedia.org/wiki/Markdown)).

## Implementation

The document visitor is implemented according to the <strong>Visitor pattern</strong>. In this example, the custom document visitor is inherited from the <strong>DocumentVisitorBase</strong> class. The <strong>DocumentVisitorBase</strong> class implements the [IDocumentVisitor](https://docs.devexpress.com/OfficeFileAPI/DevExpress.XtraRichEdit.API.Native.IDocumentVisitor) interface. Thus, you can override only required methods of the <strong>DocumentVisitorBase</strong> class to process documents.

The document iterator moves over each document structure item and passes a custom document visitor object to the current item. The custom document visitor builds a string with lightweight markup for the currently processed document item.

## Files to Review

* [Form1.cs](./CS/DocumentIteratorExample/Form1.cs) (VB: [Form1.vb](./VB/DocumentIteratorExample/Form1.vb))
* [MarkdownVisitor](./CS/DocumentIteratorExample/MarkdownVisitor.cs) (VB: [MarkdownVisitor.vb](./VB/DocumentIteratorExample\MarkdownVisitor.vb))

## Documentation

* [How to: Retrieve the List of Document Fonts using the Visitor-Iterator Pattern](https://docs.devexpress.com/WindowsForms/116746/controls-and-libraries/rich-text-editor/examples/automation/how-to-retrieve-the-list-of-document-fonts-using-the-visitor-iterator-pattern)
