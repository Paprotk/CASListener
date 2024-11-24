using Sims3.SimIFace;
using Sims3.UI.CAS;
using System;
//Currently unused
namespace Arro.MCR
{
    public class Hair : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 4;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 4;

        public override void Simulate()
        {
            try
            {
                if (CASPhysical.gSingleton != null)
                {
                    OnTick();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerHair");
            }
        }
        public static void OnTick()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnTick_Hair");
            }
        }
    }
}