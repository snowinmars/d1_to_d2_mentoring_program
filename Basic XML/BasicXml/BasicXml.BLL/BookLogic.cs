using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.DAL;
using BasicXml.Entities;

namespace BasicXml.BLL
{
    public class BookLogic : IBookLogic
    {
        private IBookDao BookDao { get; } = new BookDao();

        public void Add(Book item)
        {
            this.BookDao.Add(item);
        }

        public Book Get(Guid id)
        {
            return this.BookDao.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return this.BookDao.GetAll().ToList();
        }
    }
}
