using ConsoleAppFinallyProject.Data;
using ConsoleAppFinallyProject.Enums;
using ConsoleAppFinallyProject.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Managers
{
    public class BookManager : IManager<Book>, IEnumerable<Book>
    {
        Book[] data = new Book[0];
        public void Add(Book item)
        {
            int len = data.Length;
            Array.Resize(ref data, len + 1);
            data[len] = item;
        }

        public void Edit(Book item)
        {
            var index = Array.IndexOf(data, item);

            if (index == -1)
                return;

            var found = data[index];

            found.Name = item.Name;
            found.AuthorId = item.AuthorId;
            found.GenrEnum = item.GenrEnum;
            found.PageCount = item.PageCount;
            found.Price = item.Price;
        }
        public void Remove(Book item)
        {
            int index = Array.IndexOf(data, item);

            if (index == -1)
                return;
            int len = data.Length - 1;
            for (int i = index; i < len; i++)
            {
                data[i] = data[i + 1];
            }
            Array.Resize(ref data, len);
        }

        public Book this[int index]
        {
            get
            {
                return data[index];
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
            foreach (var item in data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Book GetById(int id)
        {
            return Array.Find(data, item => item.Id == id);
        }

        public Book[] FindByName(string name)
        {
            return Array.FindAll(data, item => item.Name.ToLower().StartsWith(name.ToLower()));
        }

    }
}