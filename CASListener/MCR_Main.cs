using Sims3.Gameplay.Core;
using Sims3.SimIFace;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;
using System;
using System.Reflection;
using Sims3.UI.CAS;
using Sims3.UI;

namespace Arro.MCR
{
    public class Main
    {
        static Main()
        {
            World.sOnStartupAppEventHandler += new EventHandler(OnStartupApp);
            World.sOnWorldLoadFinishedEventHandler += new EventHandler(OnWorldLoadFinished);
            World.sOnWorldQuitEventHandler += new EventHandler(OnWorldQuit);
        }

        private static void OnStartupApp(object sender, EventArgs e) 
        {
            CheckForMods();
        }

        private static void OnWorldLoadFinished(object sender, EventArgs e)
        {
            try
            {
                bool responder = Sims3.Gameplay.UI.Responder.Instance != null;
                if (responder)
                {
                    Sims3.Gameplay.UI.Responder instance = Sims3.Gameplay.UI.Responder.Instance;
                    instance.GameStateChanging = (GameStateChangingDelegate)Delegate.Remove(instance.GameStateChanging, new GameStateChangingDelegate(Main.OnGameStateChanged));
                    Sims3.Gameplay.UI.Responder instance2 = Sims3.Gameplay.UI.Responder.Instance;
                    instance2.GameStateChanging = (GameStateChangingDelegate)Delegate.Combine(instance2.GameStateChanging, new GameStateChangingDelegate(Main.OnGameStateChanged));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnWorldLoadFinished");
            }
        }
        private static void OnWorldQuit(object sender, EventArgs e)
        {
            try
            {
                if (Main.ClothesGuid != null) // && Main.HairGuid != null && Main.FaceGuid != null
                {
                    Simulator.DestroyObject(Main.ClothesGuid);
                    //Simulator.DestroyObject(Main.HairGuid);
                    //Simulator.DestroyObject(Main.FaceGuid);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnWorldQuit");
            }
        }

        [Tunable]
        private static bool kInstantiator;

        public static bool CanMCRClothes = false;
        public static bool CanMCRFace = false;
        public static bool CanMCRHair = false;

        public static bool IsNraasMCInstalled = false;
        public static bool IsSmoothPatchInstalled = false;

        public static Assembly nraasAssembly;
        public static Assembly smoothpatchAssembly;
        private static int Configure_cheat(object[] parameters)
        {
            try
            {
                Simulator.AddObject(new OneShotFunctionTask(Config.Configure, StopWatch.TickStyles.Seconds, 1f));
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Configure_cheat");
                return 0;
            }
        }
        private static int RefreshGrid(object[] parameters)
        {
            try
            {
                CASClothingCategory.gSingleton.PopulateGrid();
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "RefreshGrid");
                return 0;
            }
        }
        private static void CheckForMods()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assems = currentDomain.GetAssemblies();
            foreach (Assembly assembly in assems)
            {
                if (assembly.GetName().Name == "NRaasMasterController")
                {
                    IsNraasMCInstalled = true;
                    nraasAssembly = assembly;
                    break;
                }
                if (assembly.GetName().Name == "LazyDuchess.SmoothPatch")
                {
                    IsSmoothPatchInstalled = true;
                    smoothpatchAssembly = assembly;
                    break;
                }
            }
        }
        internal static void OnGameStateChanged(Sims3.UI.Responder.GameSubState previousState, Sims3.UI.Responder.GameSubState newState)
        {
            if (newState == Sims3.UI.Responder.GameSubState.CASFullMode || newState == Sims3.UI.Responder.GameSubState.CASMirrorMode || newState == Sims3.UI.Responder.GameSubState.CASTackMode || newState == Sims3.UI.Responder.GameSubState.CASDresserMode || newState == Sims3.UI.Responder.GameSubState.CASTattooMode || newState == Sims3.UI.Responder.GameSubState.CASStylistMode || newState == Sims3.UI.Responder.GameSubState.CASCollarMode || newState == Sims3.UI.Responder.GameSubState.CASSurgeryFaceMode || newState == Sims3.UI.Responder.GameSubState.CASSurgeryBodyMode)
            {
                Cheats("register");
                Main.ClothesGuid = Simulator.AddObject(new Clothes());
                //Main.HairGuid = Simulator.AddObject(new Hair());
                //Main.FaceGuid = Simulator.AddObject(new Face());
            }
            else if (Main.ClothesGuid != null) // && Main.HairGuid != null && Main.FaceGuid != null
            {
                Cheats("unregister");
                Simulator.DestroyObject(Main.ClothesGuid);
                //Simulator.DestroyObject(Main.HairGuid);
                //Simulator.DestroyObject(Main.FaceGuid);
            }
        }
        private static ObjectGuid ClothesGuid;
        //private static ObjectGuid HairGuid;
        //private static ObjectGuid FaceGuid;
        private static void Cheats(string register)
        {
            if (register == "register")
            {
                Commands.sGameCommands.Register("mcr", "Usage: Type MCR to edit the number of rows and columns.", Commands.CommandType.General, new CommandHandler(Configure_cheat));
                Commands.sGameCommands.Register("refreshgrid", "Usage: Type refreshgrid to refresh the grid", Commands.CommandType.General, new CommandHandler(RefreshGrid));
            }
            else
            {
                Commands.sGameCommands.Unregister("refreshgrid");
                CommandSystem.UnregisterCommand("mcr");
            }

        }
    }
    public static class TinyUIFixForTS3Integration
    {
        public delegate float FloatGetter();

        public static FloatGetter getUIScale = () => 1f;
    }

}
//CTRL+K+C COMMENT CLTR+K+U UNCOMMENT