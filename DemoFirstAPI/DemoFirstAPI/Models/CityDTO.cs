﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFirstAPI.Models
{
    public class CityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get { return PointsOfInterest.Count; }
        }
        public ICollection<PointsOfInterestDTO> PointsOfInterest { get; set; } = new List<PointsOfInterestDTO>();
    }
}
