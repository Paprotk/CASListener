using Sims3.SimIFace;

namespace Arro.CASListener
{
    public class Main
    {
        static Main()
        {
            Simulator.AddObject(new Hair());
            Simulator.AddObject(new Face());
            //Currently there is no support for columns and rows, but it's in works!
        }

        [Tunable]
        private static bool kInstantiator;
    }
    public static class TinyUIFixForTS3Integration
    {
        public delegate float FloatGetter();

        public static FloatGetter getUIScale = () => 1f;
    }
}
