using Restaurant_Task1.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Task1.Services
{
    public class OrderService : IOrderService
    {
        private readonly restaurantdbContext _db;
        private bool IgnoreFilter = false;

        public OrderService(restaurantdbContext _db)
        {
            this._db = _db;
        }



        public async Task<List<Order>> GetAll()
        {
            return _db.Orders.ToList();
        }


        public async Task<List<Order>> GetAllDeleted(bool c)
        {
                   _db.IgnoreFilter = c;
            return _db.Orders.ToList();
        }


        public async Task<Order> Get(int id)
        {
            return _db.Orders.Find(id);
        }


        public async Task<bool> DeleteById(int Id)
        {
            var Rest = _db.Orders.Find(Id);

            if (Rest != null)
            {
                if (Rest.Archived == 0)
                {
                    Rest.Archived = 1;
                    _db.Orders.Update(Rest);
                    _db.SaveChanges();


                    return true;
                }

            }

            return false;

        }



        public async Task<int> Create(Order restaurant)
        {
            var c = IsAvailable((double)restaurant.Quantity, restaurant.RestaurantMenuId, restaurant.CustomerId);
           RestaurantMenu menu =  _db.RestaurantMenus.Find(restaurant.RestaurantMenuId);
           

            if (c == 0) {
                          restaurant.TotalPrice = restaurant.Quantity * menu.PriceInUsd;
                          _db.Orders.Add(restaurant);
                          _db.SaveChanges();
                 }

            return c;
        }


        public async Task<int> Update(int id, Order restaurant)
        {
            var c = -1;
            var order1 = _db.Orders.Find(id);

            if (order1 == null)
                c = 5;
            else { 
             c = IsAvailable((double)restaurant.Quantity, restaurant.RestaurantMenuId, restaurant.CustomerId);
            var orderEnt = _db.Orders.Find(id);


            if (c == 0)
            {
                orderEnt.Quantity = restaurant.Quantity;
                orderEnt.CustomerId = restaurant.CustomerId;
                orderEnt.RestaurantMenuId = restaurant.RestaurantMenuId;
                orderEnt.TotalPrice = restaurant.TotalPrice;
                _db.Update(orderEnt);
                _db.SaveChanges();
                
            }}
            return c;
        }


        private int IsAvailable(double Quantity,int menuId, int custId)
        {
            // flag 1 => no object
            // flag 2 => quantity
            // flag 3 => no customer
            // flag 0 => correct



           RestaurantMenu menu =  _db.RestaurantMenus.Find(menuId);
           Customer customer =  _db.Customers.Find(custId);
            if (menu == null || menu.Archived == 1)
                return 1;

            if (customer == null || customer.Archived == 1)
                return 3;


            if (menu.Quantity > Quantity)
                return 2;

            return 0;
        }


    }
}
