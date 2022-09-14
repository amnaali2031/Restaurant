using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Task1.DTO;
using Restaurant_Task1.Model;
using Restaurant_Task1.ModelView;
using Restaurant_Task1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant_Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IMapper _mapper, IOrderService _service)
        {
            this._mapper = _mapper;
            this._service = _service;
        }



        // GET: api/<RestaurantController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res1 = await _service.GetAll();
            var RestList = _mapper.Map<List<OrderModelView>>(res1);
            return Ok(RestList);
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res1 = await _service.Get(id);
            var response = new ResponseViewModel();

            if (res1 != null)
            {
                var RestList = _mapper.Map<OrderModelView>(res1);
                response.status = true;
                response.message = "Order Is Founded";
                response.objectRes = RestList;
            }
            else
            {
                response.status = false;
                response.message = "Order Is NOT FOUND";
                response.objectRes = res1;
            }


            return Ok(response);
        }


        [HttpGet("Get All Deleted")]
        public async Task<IActionResult> GetDeletedOrder(bool With_Deleted)
        {
            var res1 = await _service.GetAllDeleted(With_Deleted);
            var RestList = _mapper.Map<List<OrderModelView>>(res1);
            return Ok(RestList);
        }


        // POST api/<RestaurantController>
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto rest)
        {

            // flag 1 => no object
            // flag 2 => quantity
            // flag 3 => no customer
            // flag 0 => correct


            var restEtity = _mapper.Map<Order>(rest);
            var flag = await _service.Create(restEtity);
            var response = new ResponseViewModel();
            response.status = true;
            response.objectRes = rest;

            if (flag == 1)
                response.message = "There iS NO Restaurant Menu ";
            else if (flag == 2)
                response.message = "Required quantity is not available";
            else if (flag == 3)
                response.message = "There Is No Customer available";
            else
                response.message = "Order Is Updated Successfully";


            return Ok(response);
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderCreateDto rest)
        {

            // flag 1 => no object
            // flag 2 => quantity
            // flag 3 => no customer
            // flag 0 => correct
            // flag 5 => order not found


            var restEtity = _mapper.Map<Order>(rest);
            int flag = await _service.Update(id, restEtity);
            var response = new ResponseViewModel();
            response.status = true;
            response.objectRes = rest;

            if (flag == 1)
                response.message = "There iS NO Restaurant Menu ";
            else if (flag == 2)
                response.message = "Required quantity is not available";
            else if (flag == 3)
                response.message = "There Is No Customer available";
            else
                response.message = "Order Is Updated Successfully";


            return Ok(response);
        }

        /*
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var IsDeleted = await _service.DeleteByName(name);
            string msg;

            if (IsDeleted == true)
                msg = "The Customer Deleted Successfully";
            else
                msg = "The Customer Is NOT FOUND";


            return Ok(msg);
        }

        */


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            var IsDeleted = await _service.DeleteById(Id);
            string msg;

            if (IsDeleted == true)
                msg = "The Order Deleted Successfully";
            else
                msg = "The Customer Is NOT FOUND";


            return Ok(msg);
        }





    }
}
