using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core.Model.SensorProperties
{
    public class NumericProperty
    {
        public int Min { get; set; }
        public int Value { get; set; }
        public int Max { get; set; }
        public int Tick { get; set; }
        public string Caption { get; set; }
    }
}
