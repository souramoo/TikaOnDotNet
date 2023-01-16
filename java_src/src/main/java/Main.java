import org.apache.tika.exception.TikaException;
import org.apache.tika.metadata.Metadata;
import org.apache.tika.parser.AutoDetectParser;
import org.apache.tika.parser.microsoft.OfficeParser;
import org.apache.tika.parser.microsoft.ooxml.OOXMLParser;
import org.apache.tika.sax.BodyContentHandler;
import org.xml.sax.SAXException;

import java.io.IOException;
import java.io.InputStream;

public class Main {

    public static void main(String[] args) throws IOException, TikaException, SAXException {
        OfficeParser parser = new OfficeParser();
        OOXMLParser p2 = new OOXMLParser();
        BodyContentHandler handler = new BodyContentHandler();
        Metadata metadata = new Metadata();
        try (InputStream stream = Main.class.getResourceAsStream("test.doc")) {
            parser.parse(stream, handler, metadata);
            System.out.println(handler.toString());
        }
    }
}
