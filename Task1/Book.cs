using System;

namespace Task1
{
    [Serializable]
    public class Book :  IEquatable<Book>
    {
        public string Title { get; }

        public string Author { get; }

        public string Year { get; }

        public Book() { }

        public Book(string title, string author, string year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public bool Equals(Book book)
        {
            if (book == null)
            {
                return false;
            }

            return (Author == book.Author && Title == book.Title && Year == book.Year);
        }

        public override int GetHashCode()
        {
            int hash = Author?.GetHashCode() ?? 1;
            hash ^= Title?.GetHashCode() ?? 1;
            hash ^= Year?.GetHashCode() ?? 1;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType()!=typeof(Book))
            {
                return false;
            }

            return Equals((Book)obj);
        }
    }
}
