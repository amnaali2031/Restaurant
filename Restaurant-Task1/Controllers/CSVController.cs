using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Task1.Extensions;
using Restaurant_Task1.Model;
using Restaurant_Task1.ModelView;
using Restaurant_Task1.Services;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurant_Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {

        private readonly restaurantdbContext _db;
        private readonly IMapper _mapper;
        private bool IgnoreFilter = false;



        public CSVController(IMapper _mapper, restaurantdbContext db)
        {
            this._mapper = _mapper;
            this._db = db;  
        }


        // GET: api/<CSVController>
        [HttpGet]
        public IActionResult Get()
        {
            _db.IgnoreFilter = IgnoreFilter;
            var ViewList = _db.RestaurantViews.ToList();
            foreach (var view in ViewList)
            {

              var xx =   view.Name.Capitalize();
             var cc =    view.Expr1.Capitalize();
             var vv =    view.TheBestSellingMeal.Capitalize();
                
            }
            var r = _mapper.Map<List<RestaurantViewModel>>(ViewList);

            


            using (var writer = new StreamWriter("D:\\ItemDB.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(r);
                }
            }

            return Ok(r);
        }
        /*
        // GET api/<CSVController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CSVController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CSVController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CSVController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        */
    }
}
