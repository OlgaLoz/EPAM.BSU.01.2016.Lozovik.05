using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task1
{
    public class XmlRepository : IRepository
    {
        private const string nullStr = "nUlL";

        public IEnumerable<Book> LoadBooks(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            XDocument xDocument = XDocument.Load(path);
            List<Book> result = xDocument.Element("root").Elements(nameof(Book))
                .Select(xe => new Book(
                    CheckForRead(xe.Attribute(nameof(Book.Title)).Value),
                    CheckForRead(xe.Element(nameof(Book.Author)).Value),
                    CheckForRead(xe.Element(nameof(Book.Year)).Value)))
                    .ToList();

            return result;
        }

        public void SaveBooks(IEnumerable<Book> books, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            XDocument xDocument = new XDocument();
            XElement root = new XElement("root");

            foreach (var book in books)
            {
                if (book == null) continue;

                XElement bookElement = new XElement(nameof(Book));
                bookElement.Add(new XAttribute(nameof(Book.Title), CheckForWrite(book.Title)));
                bookElement.Add(new XElement(nameof(Book.Author), CheckForWrite(book.Author)));
                bookElement.Add(new XElement(nameof(Book.Year), CheckForWrite(book.Year)));
                root.Add(bookElement);
            }

            xDocument.Add(root);
            xDocument.Save(path);
        }

        private string CheckForWrite(string parametr) => parametr ?? nullStr;

        private string CheckForRead(string parametr) => parametr == nullStr ? null : parametr;
    }
}
