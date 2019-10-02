namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Represents a process that will run a single
    /// iteration on a Kaomi Task Host.
    /// </summary>
    public abstract class OneTimeProcess : KaomiProcess
    {
        public override void OnIteration() => this.DoWork();

        public abstract void DoWork();
    }
}
