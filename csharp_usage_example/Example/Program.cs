using System.Threading;
using System;
using System.Linq;
using System.IO;
using System.Net;
using org.apache.tika;
using org.apache.tika.parser;
using org.apache.tika.sax;
using org.apache.tika.metadata;
using org.apache.commons.io;
using java.nio.charset;
using System.Reflection;
using java.lang;

namespace Example
{
    class Program
    {
        static string ReadFile(string model)
        {

            var fs = new FileStream(model, FileMode.Open);
            var stream = new ikvm.io.InputStreamWrapper(fs);

            BodyContentHandler handler = new BodyContentHandler();
            Parser parser = null;
            if (model.EndsWith(".docx"))
            {
                parser = new org.apache.tika.parser.microsoft.ooxml.OOXMLParser();
            }
            if (model.EndsWith(".doc"))
            {
                parser = new org.apache.tika.parser.microsoft.OfficeParser();
            }
            if (model.EndsWith(".pdf"))
            {
                parser = new org.apache.tika.parser.pdf.PDFParser();
            }
            Metadata metadata = new Metadata();
            ParseContext context = new ParseContext();

            parser.parse(stream, handler, metadata, context);
            return (handler.toString());
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Printing text contents of word document below.\n\n\n");
            ReadFile("Document.docx");
        }
    }
}