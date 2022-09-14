using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public interface IOrderService
    {

        Task<List<Order>> GetAll();
        Task<List<Order>> GetAllDeleted(bool c);
        Task<Order> Get(int id);
        Task<bool> DeleteById(int Id);
        Task<int> Create(Order restaurant);
        Task<int> Update(int id, Order restaurant);
    }
}
