using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task1
{
    public class BinarySerializationRepository : IRepository
    {
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();

        public IEnumerable<Book> LoadBooks(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            List<Book> result = new List<Book>();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                result = (List<Book>)binaryFormatter.Deserialize(fs);
            }
            return result;
        }

        public void SaveBooks(IEnumerable<Book> books, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                 binaryFormatter.Serialize(fs, books);
            }
        }
    }
}
