using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    public interface IBookStreamer
    {
        IBook[] LoadBooks(string path);
        void SaveBooks(IBook[] books,int count, string path);
    }

    public class BinaryStreamer<T> : IBookStreamer where T : IBook , new()
    {
        private const string nullStr = "nUlL";

        public IBook[] LoadBooks(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            List<IBook> books = new List<IBook>();
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    IBook book = new T();
                    book.Title = CheckForRead(reader.ReadString());
                    book.Author = CheckForRead(reader.ReadString());
                    book.Year = CheckForRead(reader.ReadString());
                    books.Add(book);
                }
            }
            return books.ToArray();
        }

        public void SaveBooks(IBook[] books,int count, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < count; i++)
                {
                    if (books[i] == null) continue;
                    
                    writer.Write(CheckForWrite(books[i].Title));
                    writer.Write(CheckForWrite(books[i].Author));
                    writer.Write(CheckForWrite(books[i].Year));
                }
            }
        }

        private string CheckForWrite(string parametr) => parametr ?? nullStr;
        
        private string CheckForRead(string parametr) => parametr == nullStr ? null : parametr;
        
    }
}
