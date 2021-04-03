using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAD._8392.DAL.DBO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAD._8392.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //controller for returning enums to the client side
    public class EnumsController : ControllerBase
    {
        //return conditions of the product
        [HttpGet("Conditions")]
        public ActionResult GetConditions()
        {
            var values = new List<object>();
            foreach (var item in Enum.GetValues(typeof(Condition)))
            {
                //return enum value and value converted to string
                values.Add(new
                {
                    condValue = (int)item,
                    condName = item.ToString()
                });
            }
            return Ok(values);
        }
        //return statuses of the product 
        [HttpGet("Statuses")]
        public ActionResult GetStatuses()
        {
            var values = new List<object>();
            foreach (var item in Enum.GetValues(typeof(Status)))
            {

                values.Add(new
                {
                    statusValue = (int)item,
                    statusName = item.ToString()
                });
            }
            return Ok(values);
        }
    }
}
