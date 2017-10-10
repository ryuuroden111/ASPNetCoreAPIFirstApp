using DemoFirstAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFirstAPI
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDTO> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    ID = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<PointsOfInterestDTO>()
                    {
                        new PointsOfInterestDTO()
                        {
                            ID = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the United States."
                        },
                        new PointsOfInterestDTO()
                        {
                            ID = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skycraper located in Midtown Mahattan."
                        },
                    }
                },
                new CityDTO()
                {
                    ID = 2,
                    Name = "Antwerp",
                    Description = "The one with the catheral that was never really finished.",
                    PointsOfInterest = new List<PointsOfInterestDTO>()
                    {
                        new PointsOfInterestDTO()
                        {
                            ID = 3,
                            Name = "Cathedral of Our Lady",
                            Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans"
                        },
                        new PointsOfInterestDTO()
                        {
                            ID = 4,
                            Name = "Antwerp Central Station",
                            Description = "The finest example of railway architecture in Belgium."
                        }
                    }
                },
                new CityDTO()
                {
                    ID = 3,
                    Name = "Paris",
                    Description = "The one with that big tower",
                    PointsOfInterest = new List<PointsOfInterestDTO>()
                    {
                        new PointsOfInterestDTO()
                        {
                            ID = 5,
                            Name = "Eiffel Tower",
                            Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave..."
                        },
                        new PointsOfInterestDTO()
                        {
                            ID = 6,
                            Name = "The Louvre",
                            Description = "The world's largest museum."
                        },
                    }
                },
            };
        }
    }
}
