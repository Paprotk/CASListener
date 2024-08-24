using System;
using System.Collections;
using System.Collections.Generic;
using Sims3.Gameplay.Core;
using Sims3.SimIFace;
using Sims3.SimIFace.CAS;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.Store;

namespace CASListener
{
    public static class ClothesTask
    {
        private static ObjectGuid taskGuid;

        public static void Initialize()
        {
            World.sOnWorldLoadFinishedEventHandler = (EventHandler)Delegate.Combine(World.sOnWorldLoadFinishedEventHandler, new EventHandler(ClothesTask.OnWorldLoad));
            World.sOnWorldQuitEventHandler = (EventHandler)Delegate.Combine(World.sOnWorldQuitEventHandler, new EventHandler(ClothesTask.OnWorldQuit));
        }
        private static void OnWorldLoad(object sender, EventArgs e)
        {
            ClothesTask.taskGuid = Simulator.AddObject(new ClothingSizeTask());
        }
        private static void OnWorldQuit(object sender, EventArgs e)
        {
            Simulator.DestroyObject(ClothesTask.taskGuid);
        }
    }
}
