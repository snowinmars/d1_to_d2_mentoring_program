using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Library.AddAll(new[] {Book.GetNewOne(), Book.GetNewOne() , Book.GetNewOne() , Book.GetNewOne() , Book.GetNewOne() });
        }
    }
}
