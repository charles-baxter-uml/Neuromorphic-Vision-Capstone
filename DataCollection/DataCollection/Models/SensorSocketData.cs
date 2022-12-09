using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Models
{
    public sealed class SensorSocketData
    {
        public double[] Values { get; set; }
        public int Accuracy { get; set; }
        public long Timestamp { get; set; }

        public void Print()
        {
            Console.WriteLine($"X: {Values[0]:000.000} Y: {Values[1]:000.000} Z: {Values[2]:000.000}");
        }
    }
}
