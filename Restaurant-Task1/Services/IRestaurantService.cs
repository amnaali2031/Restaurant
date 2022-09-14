using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetAll();
        Task<List<Restaurant>> GetAllDeleted(bool c);
        Task<Restaurant> Get(int id);
        Task<bool> DeleteById(int Id);
        Task<bool> DeleteByName(string name);
        Task<bool> Create(Restaurant restaurant);
        Task<bool> Update(string name, Restaurant restaurant);


    }
}
