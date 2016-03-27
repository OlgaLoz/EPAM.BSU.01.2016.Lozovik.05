using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    public interface IRepository
    {
        IEnumerable<Book> LoadBooks(string path);
        void SaveBooks(IEnumerable<Book> books, string path);
    }

    public class BinaryRepository : IRepository
    {
        private const string nullStr = "nUlL";

        public IEnumerable<Book> LoadBooks(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            List<Book> books = new List<Book>();
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string title = CheckForRead(reader.ReadString());
                    string author = CheckForRead(reader.ReadString());
                    string year = CheckForRead(reader.ReadString());

                    Book book = new Book(title, author, year);
                    books.Add(book);
                }
            }
            return books;
        }

        public void SaveBooks(IEnumerable<Book> books, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (Book book in books)
                {
                    if (book == null) continue;
                    
                    writer.Write(CheckForWrite(book.Title));
                    writer.Write(CheckForWrite(book.Author));
                    writer.Write(CheckForWrite(book.Year));
                }
            }
        }

        private string CheckForWrite(string parametr) => parametr ?? nullStr;
        
        private string CheckForRead(string parametr) => parametr == nullStr ? null : parametr;
        
    }
}
