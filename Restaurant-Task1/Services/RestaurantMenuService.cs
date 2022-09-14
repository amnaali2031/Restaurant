using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public class RestaurantMenuService : IRestaurantMenuService
    {

        private readonly restaurantdbContext _db;
        private bool IgnoreFilter = false;

        public RestaurantMenuService(restaurantdbContext _db)
        {
            this._db = _db;
        }




        public async Task<List<RestaurantMenu>> GetAll()
        {
            return _db.RestaurantMenus.ToList();
        }


        public async Task<List<RestaurantMenu>> GetAllDeleted(bool c)
        {
            _db.IgnoreFilter = c;
            return _db.RestaurantMenus.ToList();
        }


        public async Task<RestaurantMenu> Get(int id)
        {
            return _db.RestaurantMenus.Find(id);
        }


        public async Task<bool> DeleteById(int Id)
        {
            var Rest = _db.RestaurantMenus.Find(Id);

            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                     _db.RestaurantMenus.Update(Rest);
                    _db.SaveChanges();
                    return true;
                }

            }

            return false;

        }


        public async Task<bool> DeleteByName(string name)
        {
            var Rest = _db.RestaurantMenus.FirstOrDefault(a => a.MealName.ToLower().Equals(name.ToLower()));

            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    var x = _db.RestaurantMenus.Update(Rest);
                    _db.SaveChanges();
                    return true;
                }

            }

            return false;

        }


        public async Task<bool> Create(RestaurantMenu restaurant)
        {
            bool flag = false;
            var c = IsFound(restaurant.MealName);

            if (c == false)
            {
                restaurant.PriceInUsd = restaurant.PriceInNis / 3.5;
                _db.RestaurantMenus.Add(restaurant);
                _db.SaveChanges();
                flag = true;
            }

            return flag;
        }


        public async Task<bool> Update(string name, RestaurantMenu restaurant)
        {
            bool flag = false;
            var c = IsFound(name);

            if (c == true)
            {
                var x = _db.RestaurantMenus.FirstOrDefault(a => a.MealName.ToLower().Equals(name.ToLower()));
                if (x.Archived == 0)
                {
                    x.MealName = restaurant.MealName;
                    x.PriceInNis = restaurant.PriceInNis;
                    x.PriceInUsd = restaurant.PriceInUsd;
                    x.Quantity = restaurant.Quantity;

                    _db.Update(x);
                    _db.SaveChanges();
                    flag = true;
                }

            }
            return flag;
        }


        private bool IsFound(string name)
        {
            bool found = false;
            var x = _db.RestaurantMenus.FirstOrDefault(a => a.MealName.ToLower().Equals(name.ToLower()));
            if (x != null)
                found = true;

            return found;
        }

    }
}
