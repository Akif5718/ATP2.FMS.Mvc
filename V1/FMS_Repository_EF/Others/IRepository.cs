using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Repository_EF
{
   public interface IRepository<T>
    {
        bool Insert(T entities);
        bool Update(T entities);
        bool Delete(int id);
        ICollection<T> GetAll();
        T GetbyId(int id);
    }
}
