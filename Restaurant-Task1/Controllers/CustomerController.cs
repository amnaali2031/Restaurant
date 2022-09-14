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
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(IMapper _mapper, ICustomerService _service)
        {
            this._mapper = _mapper;
            this._service = _service;
        }



        // GET: api/<RestaurantController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res1 = await _service.GetAll();
            var RestList = _mapper.Map<List<CustomerViewModel>>(res1);
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
                var RestList = _mapper.Map<CustomerViewModel>(res1);
                response.status = true;
                response.message = "Customer Is Founded";
                response.objectRes = RestList;
            }
            else
            {
                response.status = false;
                response.message = "Customer Is NOT FOUND";
                response.objectRes = res1;
            }


            return Ok(response);
        }


        [HttpGet("Get All Deleted")]
        public async Task<IActionResult> GetDeletedCustomer(bool With_Deleted)
        {
            var res1 = await _service.GetAllDeleted(With_Deleted);
            var RestList = _mapper.Map<List<CustomerViewModel>>(res1);
            return Ok(RestList);
        }


        // POST api/<RestaurantController>
        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] CustomerCreateDto rest)
        {
            var restEtity = _mapper.Map<Customer>(rest);
            var flag = await _service.Create(restEtity);
            var response = new ResponseViewModel();

            if (flag == true)
            {
                response.status = true;
                response.message = "Resturant Is Added Successfully";
                response.objectRes = rest;
            }
            else
            {
                response.status = false;
                response.message = "The restaurant is already available";
                response.objectRes = rest;
            }

            return Ok(response);
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] CustomerCreateDto rest)
        {
            var restEtity = _mapper.Map<Customer>(rest);
            bool flag = await _service.Update(name, restEtity);
            var response = new ResponseViewModel();

            if (flag == true)
            {
                response.status = true;
                response.message = "Customer Is Updated Successfully";
                response.objectRes = rest;
            }
            else
            {
                response.status = false;
                response.message = "The Customer is NOT FOUND";
                response.objectRes = rest;
            }

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
                msg = "The Customer Deleted Successfully";
            else
                msg = "The Customer Is NOT FOUND";


            return Ok(msg);
        }

        



    }
}
