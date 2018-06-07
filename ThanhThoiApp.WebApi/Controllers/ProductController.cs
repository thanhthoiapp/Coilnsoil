using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThanhThoiApp.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        ICategoryervice _Categoryervice;
        public ProductController(ICategoryervice Categoryervice)
        {
            _Categoryervice = Categoryervice;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_Categoryervice.GetAll());
        }

    }
}
