using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.DAL
{
    public interface IDao<T> : ICRUD<T>
    {
        IEnumerable<T> GetAll(); // TODO redo with GetByFilter
    }
}
