using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public interface IRestaurantMenuService
    {

        Task<List<RestaurantMenu>> GetAll();
        Task<List<RestaurantMenu>> GetAllDeleted(bool c);
        Task<RestaurantMenu> Get(int id);
        Task<bool> DeleteById(int Id);
        Task<bool> DeleteByName(string name);
        Task<bool> Create(RestaurantMenu restaurant);
        Task<bool> Update(string name, RestaurantMenu restaurant);
    }
}
