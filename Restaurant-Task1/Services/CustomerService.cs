using Restaurant_Task1.Extensions;
using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly restaurantdbContext _db;
        private bool IgnoreFilter = false;

        public CustomerService(restaurantdbContext _db)
        {
            this._db = _db;
        }



        public async Task<List<Customer>> GetAll()
        {
            return _db.Customers.ToList();
        }


        public async Task<List<Customer>> GetAllDeleted(bool c)
        {
            _db.IgnoreFilter = c;
            return _db.Customers.ToList();
        }


        public async Task<Customer> Get(int id)
        {
            return _db.Customers.Find(id);
        }


        public async Task<bool> DeleteById(int Id)
        {
            var Rest = _db.Customers.Find(Id);

            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    var x = _db.Customers.Update(Rest);
                    _db.SaveChanges();


                    return true;
                }

            }

            return false;

        }


        public async Task<bool> DeleteByName(string name)
        {
            var Rest = _db.Customers.FirstOrDefault(a => (a.FirstName.ToLower()+ ' '+ a.FirstName.ToLower()).Equals(name.ToLower()));

            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    var x = _db.Customers.Update(Rest);
                    _db.SaveChanges();


                    return true;
                }

            }

            return false;

        }


        public async Task<bool> Create(Customer restaurant)
        {
            bool flag = false;
            var c = IsFound(restaurant.FirstName+' '+restaurant.LastName);

            if (c == false)
            {
                restaurant.FirstName.Capitalize();
                restaurant.LastName.Capitalize();   
                _db.Customers.Add(restaurant);
                _db.SaveChanges();
                flag = true;
            }

            return flag;
        }


        public async Task<bool> Update(string name, Customer restaurant)
        {
            bool flag = false;
            var c = IsFound(name);

            if (c == true)
            {
                var x = _db.Customers.FirstOrDefault(a => (a.FirstName+' '+a.LastName).ToLower().Equals(name.ToLower()));
                if (x.Archived == 0)
                {
                    x.FirstName = restaurant.FirstName;
                    x.LastName = restaurant.LastName;
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
    }
}
