using Lmi3d.GoSdk;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Processes
{
    class Program
    {
        static void Main(string[] args)
        {
            var gocator = new Gocator();
            gocator.OnInitialize();
            gocator.OnIteration();
            gocator.OnFinalize();
        }
    }
}
