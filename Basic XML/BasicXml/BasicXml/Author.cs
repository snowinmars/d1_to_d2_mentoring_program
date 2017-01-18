using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;

namespace BasicXml
{
    public class Author
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public static Author GetNewOne()
        {
            return new Author
            {
                FirstName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                SecondName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
            };
        }
    }
}
