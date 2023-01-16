# Tika on .NET

## Description

This is an example repo to read word documents (e.g. doc, docx) and PDFs using C#.

This is basically a way to do what GroupDocs.Parser does but without any limitations. It also utilises the latest ikvm-revived to load in all the parsers, etc.

## Usage

Download the TikaOffice.jar file (a wrapper jar that drags in all the maven dependencies, and sorts out some referencing to ensure all the parsers get included- this does not happen if using MavenReference).

Then, add the following to your .csproj

```
  <ItemGroup>
    <PackageReference Include="IKVM.Maven.Sdk" Version="1.3.0-develop0029" />
    
    <IkvmReference Include="path/to/TikaOffice.jar">
      <AssemblyName>Tika</AssemblyName>
      <AssemblyVersion>2.6.0</AssemblyVersion>
      <AssemblyFileVersion>2.6.8</AssemblyFileVersion>
      <DisableAutoAssemblyName>true</DisableAutoAssemblyName>
      <DisableAutoAssemblyVersion>true</DisableAutoAssemblyVersion>
    </IkvmReference>
  </ItemGroup>

```

Finally, you can utilise this library as follows in your C# project:

```
// filename
            var model = "path/to/test.docx";

// open a java input stream inside of c#
            var fs = new FileStream(model, FileMode.Open);
            var stream = new ikvm.io.InputStreamWrapper(fs);

            BodyContentHandler handler = new BodyContentHandler();
            Parser parser = null;
            
            // autodetecting parser does not work as this is c# so we do a dirty fix
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

// now parse!
            parser.parse(stream, handler, metadata, context);
            
            // use the text version of the document somehow
            return (handler.toString());
```

## How it works

This uses IKVM-revived (a JVM for c#) to drag in all of org.apache.tika, compile to DLL, and then uses this to read in documents.

## Repo layout

The src folder contains the java source code for a wrapper piece of code to drag in the bits you need. The example folder has an example of a c# project using this.
