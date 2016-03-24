using System;
using Task1;

namespace ConsoleUI
{
    public class TagFinder : ITagFinder
    {
        public bool CompareByTag(IBook book)
        {
            return book.Year == "2004";
        }
    }

    public class Comparer : IComparator
    {
        public bool Compare(IBook x, IBook y) => string.CompareOrdinal(x.Year, y.Year) == 1;

    }

    public class Book : IBook, IEquatable<IBook>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Year { get; set; }

        public Book() { }

        public Book(string title, string author, string year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public bool Equals(IBook book)
        {
            if (book == null)
            {
                return false;
            }

            return (Author == book.Author && Title == book.Title && Year == book.Year);
        }
    }
}
