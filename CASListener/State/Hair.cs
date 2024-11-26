using Sims3.SimIFace;
using System;
//Currently unused
namespace Arro.MCR
{
    public class Hair
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 3;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 3;

        public static void Hook()
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