using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Model
{
    public abstract class KaomiPlugin
    {
        public abstract void Initialize(string callingAssembly);
    }
}
