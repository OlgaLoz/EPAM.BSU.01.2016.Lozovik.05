using System;
using System.Collections.Generic;
using Task1;
namespace ConsoleUI
{
    class Program
    {
        public class TagFinder : ITagFinder
        {
            public bool CompareByTag(Book book)
            {
                return book.Year == "2004";
            }
        }

        public class Comparer : IComparer<Book>
        {
            public int Compare(Book x, Book y) => string.CompareOrdinal(x.Year, y.Year);
        }

        static void Main(string[] args)
        {
            IRepository repository = new BinaryRepository();
            IRepository xmlRepository = new XmlRepository();

            BookService bookService = new BookService();

            bookService.AddBook(new Book());
            bookService.AddBook(new Book("book1","author1","2000"));
            bookService.AddBook(new Book("book2", "author2", "1995"));
            bookService.AddBook(new Book("book3", "author3", "2012"));
            bookService.AddBook(new Book("book4", "author4", "2001"));
            bookService.AddBook(new Book("book5", "author5", "2004"));
            bookService.AddBook(new Book("book6", "author6", "2004"));
            bookService.AddBook(new Book("book7", "author7", "2004"));
            bookService.AddBook(new Book("book0", null, ""));
            bookService.AddBook(new Book("book8", "author8", "2000"));
            // bookCollection.AddBook(new Book()); Exception: This book is already exist!
            ShowBooks(bookService.GetBooks());

            bookService.RemoveBook(new Book("book8", "author8", "2000"));
            bookService.RemoveBook(new Book());
            // bookCollection.RemoveBook(new Book());Exception: This book isn't exist!

            IEnumerable<Book> booksByTag = bookService.FindByTag(new TagFinder());
            ShowBooks(booksByTag);
            
            bookService.SortByTag(new Comparer());
            ShowBooks(bookService.GetBooks());

            bookService.SaveBooks(xmlRepository, "xmlLibrary");
            bookService.SaveBooks(repository, "library");
            

            BookService newBookService = new BookService();
          
            // newBookService.LoadBooks(repository, "library");
            newBookService.LoadBooks(xmlRepository, "xmlLibrary");

            ShowBooks(newBookService.GetBooks());

            Console.ReadKey();
        }

        public static void ShowBooks(IEnumerable<Book> bookCollection)
        {
            foreach (var book in bookCollection)
            {
                Console.WriteLine(book.Author + "; " + book.Title + "; " + book.Year);
            }
            Console.WriteLine("---------------------");
        }
    }
}
