using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfExample
{
    public class SensorOrientation
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}
