using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.BLL
{
    public interface ILogic<T> : ICRUD<T>
    {
        IEnumerable<T> GetAll(); // TODO redo with GetByFilter

    }
}
