using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoFirstAPI.Models;

namespace DemoFirstAPI.Controllers
{
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        //[HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == id);
            if (city == null) return NotFound();
            return Ok(city);
        }
    }
}