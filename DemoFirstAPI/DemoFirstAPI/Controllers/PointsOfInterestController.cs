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
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityID}/PointsOfInterest")]
        public IActionResult GetPointsOfInterest(int cityID)
        {
            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null) return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityID}/PointsOfInterest/{id}")]
        public IActionResult GetPointOfInterest(int cityID, int id)
        {
            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null) return NotFound();

            PointsOfInterestDTO pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == id);
            if (pointOfInterest == null) return NotFound();

            return Ok(pointOfInterest);
        }
    }
}