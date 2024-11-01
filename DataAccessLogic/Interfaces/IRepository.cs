using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T tempObj); // Create
        List<T> GetAll(); // Read 
        void Update(int id, T obj); // Update 
        void DeleteById(int id); // Delete

        T GetById(int id);
        T GetByEmail(string email);

    }
}
