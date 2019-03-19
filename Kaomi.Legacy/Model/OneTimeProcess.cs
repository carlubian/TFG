using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Represents a process that will run a single
    /// iteration on a Kaomi Task Host.
    /// </summary>
    public abstract class OneTimeProcess : KaomiProcess
    {
        public override void OnIteration() => DoWork();

        public abstract void DoWork();
    }
}
