using Microsoft.AspNetCore.Routing.Tree;
using Restaurant_Task1.DTO;
using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly restaurantdbContext _db;
        private bool IgnoreFilter = false;


        public RestaurantService(restaurantdbContext _db)
        {
            this._db = _db;
        }


        public async Task<List<Restaurant>> GetAll()
        {
            return _db.Restaurants.ToList();
        }


        public async Task<List<Restaurant>> GetAllDeleted(bool c)
        {
            _db.IgnoreFilter = c;
            return _db.Restaurants.ToList();
        }


        public async Task<Restaurant> Get(int id)
        {
            return _db.Restaurants.Find(id);
        }


        public async Task<bool> DeleteById(int Id)
        {
            var Rest = _db.Restaurants.Find(Id);

            if(Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    var x = _db.Restaurants.Update(Rest);
                    _db.SaveChanges();

                    editDeletedRest(Rest.Id);

                    return true;
                }

            }
            
            return false;

        }


        public async Task<bool> DeleteByName(string name)
        {
            var Rest = _db.Restaurants.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
           
            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    var x = _db.Restaurants.Update(Rest);
                    _db.SaveChanges();


                    editDeletedRest(Rest.Id);

                    return true;
                }

            }

            return false;

        }


        public async Task<bool> Create(Restaurant restaurant)
        {
            bool flag = false;
            var c = IsFound(restaurant.Name);

            if (c == false )
            {
                _db.Restaurants.Add(restaurant);
                _db.SaveChanges();
                flag = true;
            }

            return flag;
        }


        public async Task<bool> Update(string name , Restaurant restaurant)
        {
            bool flag = false;
            var c = IsFound(name);

            if (c == true)
            {
                var x = _db.Restaurants.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                if (x.Archived == 0)
                {
                    x.Name = restaurant.Name;
                    x.PhoneNumber = restaurant.PhoneNumber;
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
            var x = _db.Restaurants.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
            if (x != null)
                found = true;
           
            return found;
        }

        private void editDeletedRest(int id)
        {
            List<RestaurantMenu> x = _db.RestaurantMenus.Where(a => a.RestaurantId == id).ToList();

            foreach (var menu in x)
            {
                
                _db.RestaurantMenus.Remove(menu);
                _db.SaveChanges();
            }
        }
    }

    
}
