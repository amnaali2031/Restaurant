using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<List<Customer>> GetAllDeleted(bool c);
        Task<Customer> Get(int id);
        Task<bool> DeleteById(int Id);
        Task<bool> DeleteByName(string name);
        Task<bool> Create(Customer restaurant);
        Task<bool> Update(string name, Customer restaurant);
    }
}
