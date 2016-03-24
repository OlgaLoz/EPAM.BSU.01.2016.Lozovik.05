using System;
using System.Collections.Generic;
using Task1;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookStreamer bookStreamer = new BinaryStreamer<Book>();
            BookCollection bookCollection = new BookCollection(bookStreamer);

            bookCollection.AddBook(new Book());
            bookCollection.AddBook(new Book("book1","author1","2000"));
            bookCollection.AddBook(new Book("book2", "author2", "1995"));
            bookCollection.AddBook(new Book("book3", "author3", "2012"));
            bookCollection.AddBook(new Book("book4", "author4", "2001"));
            bookCollection.AddBook(new Book("book5", "author5", "2004"));
            bookCollection.AddBook(new Book("book6", "author6", "2004"));
            bookCollection.AddBook(new Book("book7", "author7", "2004"));
            bookCollection.AddBook(new Book("book0", null, ""));
            bookCollection.AddBook(new Book("book8", "author8", "2000"));
            // bookCollection.AddBook(new Book()); Exception: This book is already exist!
            ShowBooks(bookCollection);

            bookCollection.RemoveBook(new Book("book8", "author8", "2000"));
            bookCollection.RemoveBook(new Book());
            // bookCollection.RemoveBook(new Book());Exception: This book isn't exist!

            IEnumerable<IBook> booksByTag = bookCollection.FindByTag(new TagFinder());
            ShowBooks(booksByTag);
            
            bookCollection.SortByTag(new Comparer());
            ShowBooks(bookCollection);

            bookCollection.SaveBooks("library");

            BookCollection newBookCollection = new BookCollection(bookStreamer);
            newBookCollection.LoadBooks("library");
            
            ShowBooks(newBookCollection);

            Console.ReadKey();
        }

        public static void ShowBooks(IEnumerable<IBook> bookCollection)
        {
            foreach (var book in bookCollection)
            {
                Console.WriteLine(book.Author + "; " + book.Title + "; " + book.Year);
            }
            Console.WriteLine("---------------------");
        }
    }
}
