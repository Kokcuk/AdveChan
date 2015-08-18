using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor
{
    //Due to Repository Desing Pattern i create interface with my repository methods
    interface IRepository<T> : IDisposable where T:class
    {
        IEnumerable<T> GetDocumentList();
        T GetDocument(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
