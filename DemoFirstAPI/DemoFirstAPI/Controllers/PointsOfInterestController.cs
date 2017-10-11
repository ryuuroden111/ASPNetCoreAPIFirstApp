using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoFirstAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpGet("{cityID}/PointsOfInterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityID, int id)
        {
            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null) return NotFound();

            PointsOfInterestDTO pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == id);
            if (pointOfInterest == null) return NotFound();

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityID}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityID, 
            [FromBody] PointsOfInterestForCreationDTO pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Provided description should be different from the name.");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null) return NotFound();

            int maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.ID);
            PointsOfInterestDTO finalPointOfInterest = new PointsOfInterestDTO()
            {
                ID = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityID = cityID, id = finalPointOfInterest.ID },
                finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointsOfInterestForUpdateDTO pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The Provided description should be different from the name.");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityId);
            if (city == null) return NotFound();

            PointsOfInterestDTO pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.ID == id);
            if (pointOfInterestFromStore == null) return NotFound();

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{cityID}/pointsofinterest/{id}")]
        public IActionResult PartuallyUpdatePointOfInterest(int cityID, int id,
            [FromBody] JsonPatchDocument<PointsOfInterestForUpdateDTO> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            bool hasCity = (city != null);
            if (!hasCity) return NotFound();

            PointsOfInterestDTO pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == id);
            bool hasPointOfInterest = (pointOfInterestFromStore != null);
            if (!hasPointOfInterest) return NotFound();

            PointsOfInterestForUpdateDTO pointOfInterestToPatch = new PointsOfInterestForUpdateDTO()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }
            TryValidateModel(pointOfInterestToPatch);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityID}/pointsofinterest/{id}")]
        public IActionResult DeletePointsOfInterest(int cityID, int id)
        {
            CityDTO city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            bool hasCity = (city != null);
            if (!hasCity) return NotFound();

            PointsOfInterestDTO pointsOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == id);
            bool hasPointOfInterest = (pointsOfInterestFromStore != null);
            if (!hasPointOfInterest) return NotFound();

            city.PointsOfInterest.Remove(pointsOfInterestFromStore);
            return NoContent();
        }
    }
}