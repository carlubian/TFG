namespace Kaomi.Processes
{
    class Program
    {
        /// <summary>
        /// Este código no debería ejecutarse
        /// desde aquí, sino como proceso de
        /// Kaomi a través de Gocator.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var gocator = new Gocator();
            gocator.OnInitialize();
            gocator.OnIteration();
            gocator.OnFinalize();
        }
    }
}
