using System;
using System.Collections;
using System.Collections.Generic;

namespace Task1
{
    public interface IBook
    {
        string Title { get;  set; }
        string Author { get; set; }
        string Year { get; set; }
    }

    public interface ITagFinder
    {
        bool CompareByTag(IBook book);
    }

    public interface IComparator
    {
        bool Compare(IBook x, IBook y);
    }

    public class BookCollection : IEnumerable<IBook>
    {
        private readonly int capacity = 10;

        private readonly IBookStreamer streamer;

        private IBook[] items;

        private int count;

        public int Count => count;

        public BookCollection(IBookStreamer streamer)
        {
            if (streamer == null)
            {
                throw new ArgumentNullException(nameof(streamer));
            }

            this.streamer = streamer;
            items = new IBook[capacity];
            count = 0;
        }

        public IEnumerator<IBook> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SaveBooks(string path)
        {
            streamer.SaveBooks(items, count, path);    
        }

        public void LoadBooks(string path)
        {
            items =  streamer.LoadBooks(path);
            count = items.Length;
        }

        public void AddBook(IEquatable<IBook> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (CheckBook(value) != -1)
            {
                throw new ArgumentException("This book is already exist!");
            }

            CheckFreeSpace();
            items[count] = (IBook)value;
            count++;
        }

        public void RemoveBook(IEquatable<IBook> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int index = CheckBook(value);

            if (index == -1)
            {
                throw new ArgumentException("This book isn't exist!");
            }

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
        }

        public IEnumerable<IBook> FindByTag(ITagFinder tagFinder)
        {
            if (tagFinder == null)
            {
                throw new ArgumentNullException(nameof(tagFinder));
            }
            for (int i = 0; i < count; i++)
            {
                if (tagFinder.CompareByTag(items[i]))
                {
                    yield return items[i];
                }
            }
        }

        public void SortByTag(IComparator comparer )
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i; j < count; j++)
                {
                    if (comparer.Compare(items[i], items[j]))
                    {
                        IBook temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
        }

        public void Clear()
        {
            items = new IBook[0];
            count = 0;
        }

        private void CheckFreeSpace()
        {
            if (count == items.Length)
            {
                var tempItems = new IBook[Count + capacity];
                items.CopyTo(tempItems, 0);
                items = tempItems;
            }
        }

        private int CheckBook(IEquatable<IBook> book)
        {
            for (int i = 0; i < count; i++)
            {
                if (book.Equals(items[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}