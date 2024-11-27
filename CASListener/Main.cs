using Sims3.Gameplay.Core;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using System;
using System.Reflection;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;

namespace Arro.MCR
{
    public class Main
    {

        [Tunable]
        private static bool kInstantiator;

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
                if (Main.CASHook != null)
                {
                    Simulator.DestroyObject(Main.CASHook);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnWorldQuit");
            }
        }

        public static string casState = "none";
        private static int Configure_cheat(object[] parameters)
        {
            try
            {
                if (MCR.CASHook.isClothesProcessing == true)
                {
                    Simulator.AddObject(new OneShotFunctionTask(Configure.Clothes, StopWatch.TickStyles.Milliseconds, 1f));
                }
                else if (MCR.CASHook.isFaceProcessing)
                {
                    //Simulator.AddObject(new OneShotFunctionTask(Configure.Face, StopWatch.TickStyles.Milliseconds, 1f));
                }
                else if (MCR.CASHook.isHairProcessing)
                {
                    //Simulator.AddObject(new OneShotFunctionTask(Configure.Hair, StopWatch.TickStyles.Milliseconds, 1f));
                }
                else
                {
                    string NotificationInfo = Localization.LocalizeString("Arro/MCR/Local:EnterCASSubcategoryToEditGrid", new object[0]);
                    StyledNotification.Show(new StyledNotification.Format(NotificationInfo, StyledNotification.NotificationStyle.kGameMessageNegative));
                }
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Configure_cheat");
                return 0;
            }
        }

        public static bool isNraasMCInstalled = false;
        public static bool isSmoothPatchInstalled = false;
        public static Assembly nraasAssembly;
        public static Assembly smoothpatchAssembly;

        private static void CheckForMods()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assems = currentDomain.GetAssemblies();
            foreach (Assembly assembly in assems)
            {
                if (assembly.GetName().Name == "NRaasMasterController")
                {
                    isNraasMCInstalled = true;
                    nraasAssembly = assembly;
                    break;
                }
                if (assembly.GetName().Name == "LazyDuchess.SmoothPatch")
                {
                    isSmoothPatchInstalled = true;
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
                Main.CASHook = Simulator.AddObject(new CASHook());
            }
            else if (Main.CASHook != null)
            {
                Cheats("unregister");
                Simulator.DestroyObject(Main.CASHook);
            }
        }
        public static ObjectGuid CASHook;

        private static void Cheats(string action)
        {
            if (action == "register")
            {
                Commands.sGameCommands.Register("mcr", "Usage: Type MCR to edit the number of rows and columns.", Commands.CommandType.General, new CommandHandler(Configure_cheat));
                return;
            }
            Commands.sGameCommands.Unregister("mcr");
        }
    }
    public static class TinyUIFixForTS3Integration
    {
        public delegate float FloatGetter();

        public static FloatGetter getUIScale = () => 1f;
    }

}