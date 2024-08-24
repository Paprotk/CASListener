using Sims3.SimIFace;

namespace Arro
{
    // Token: 0x02000003 RID: 3
    public class CASTaskCreator
    {
        // Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
        public static void Initialize()
        {
            Simulator.AddObject(new Clothes());
            Simulator.AddObject(new Hair());
            Simulator.AddObject(new Face());
        }
    }
}
