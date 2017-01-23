using BasicXml.Common;
using System;
using System.Collections.Generic;
using BasicXml.Generators;

namespace BasicXml.UI
{
    internal class Program
    {
       

        private static void Main(string[] args)
        {
            Library.AddAll(new List<LibraryItem> { EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper() });

            var collection = Library.GetAll();
        }
    }
}