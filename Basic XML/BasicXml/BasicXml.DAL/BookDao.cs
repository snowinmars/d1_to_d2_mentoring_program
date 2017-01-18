using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using BasicXml.Common;
using BasicXml.Entities;

namespace BasicXml.DAL
{
    public class BookDao : IBookDao
    {
        static XmlWriterSettings Settings = new XmlWriterSettings
        {
            ConformanceLevel = ConformanceLevel.Fragment,
            CloseOutput = false,
        };

        public void Add(Book item)
        {
            using (var stream = File.Open(Constant.DataFileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var writter = XmlWriter.Create(stream, Settings))
                {
                    item.WriteAsXmlTo(writter);
                }
            }
        }

        public Book Get(Guid id)
        {
            return this.GetAll().Where(item => item.Id == id).FirstOrDefault(); // TODO
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
