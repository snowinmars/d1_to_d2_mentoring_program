using BasicXml.Common;
using System;
using System.Collections.Generic;
using System.IO;
using BasicXml.Generators;

namespace BasicXml.UI
{
    internal class Program
    {
       

        private static void Main(string[] args)
        {
            if (File.Exists(Constants.FullPathToDataFile))
            {
                File.Delete(Constants.FullPathToDataFile);
            }

            using (var stream = File.OpenWrite(Constants.FullPathToDataFile))
            {    Library.AddAll(new List<LibraryItem>
                {
                    EntityGenerator.GetNewBook(),
                    EntityGenerator.GetNewNewspaper(),
                    EntityGenerator.GetNewPatent(),
                    EntityGenerator.GetNewPatent(),
                    EntityGenerator.GetNewBook(),
                    EntityGenerator.GetNewNewspaper()
                }, stream);}

            using (var stream = File.OpenRead(Constants.FullPathToDataFile))
            {
                var collection = Library.GetAll(stream);
            }
        }
    }
}