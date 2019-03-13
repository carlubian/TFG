using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core.Model
{
    public class Sensor
    {
        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Puerto { get; set; }
        public string Tipo { get; set; }
        public string Pais { get; set; }
        public string Lugar { get; set; }
        public string Operaciones { get; set; }

        public string InternalID { get; set; }
    }
}
