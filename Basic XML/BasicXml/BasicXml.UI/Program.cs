using BasicXml.BLL;
using BasicXml.Entities;
using SandS.Algorithm.Library.GeneratorNamespace;
using System;

namespace BasicXml.UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            

            IBookLogic bookLogic = new BookLogic();

            Book book = Book.GetNewOne();

            bookLogic.Add(book);

            book = Book.GetNewOne();

            bookLogic.Add(book);

            book = Book.GetNewOne();

            bookLogic.Add(book);
        }
    }
}