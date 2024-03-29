﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDB_Project.DataReading;

namespace RDB_Project
{
    /// <summary>
    /// db_log, devices, measurements, mtypes, points
    /// </summary>
    class DatabaseObjects
    {
        public List<Device> Devices { get; set; }
        public List<Measurement> Measurements { get; set; }
        public List<MType> MTypes { get; set; }
        public List<Point> Points { get; set; }

        public void Clear()
        {
            Devices = new List<Device>();
            Measurements = new List<Measurement>();
            MTypes = new List<MType>();
            Points = new List<Point>();
        }
    }
}
