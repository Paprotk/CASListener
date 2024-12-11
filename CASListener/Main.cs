using Sims3.Gameplay.Core;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Reflection;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;

namespace Arro.MCR
{
    public class Main
    {

        [Tunable]
#pragma warning disable CS0169 // Field is never used
        private static bool kInstantiator;
#pragma warning restore CS0169 // Field is never used

        static Main()
        {
            World.sOnStartupAppEventHandler += OnStartupApp;
            World.sOnWorldLoadFinishedEventHandler += OnWorldLoadFinished;
            World.sOnWorldQuitEventHandler += OnWorldQuit;
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
                    instance.GameStateChanging = (GameStateChangingDelegate)Delegate.Remove(instance.GameStateChanging, new GameStateChangingDelegate(OnGameStateChanged));
                    Sims3.Gameplay.UI.Responder instance2 = Sims3.Gameplay.UI.Responder.Instance;
                    instance2.GameStateChanging = (GameStateChangingDelegate)Delegate.Combine(instance2.GameStateChanging, new GameStateChangingDelegate(OnGameStateChanged));
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
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (CASHook != null) 
                {
                    Simulator.DestroyObject(CASHook);
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
                if (MCR.CASHook.isClothesProcessing)
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

        public static bool isNraasMcInstalled;
        public static Assembly nraasAssembly;

        private static void CheckForMods()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] assems = currentDomain.GetAssemblies();
            foreach (Assembly assembly in assems)
            {
                if (assembly.GetName().Name == "NRaasMasterController")
                {
                    isNraasMcInstalled = true;
                    nraasAssembly = assembly;
                    break;
                }
            }
        }

        internal static void OnGameStateChanged(Responder.GameSubState previousState, Responder.GameSubState newState)
        {
            if (newState == Responder.GameSubState.CASFullMode || newState == Responder.GameSubState.CASMirrorMode || newState == Responder.GameSubState.CASTackMode || newState == Responder.GameSubState.CASDresserMode || newState == Responder.GameSubState.CASTattooMode || newState == Responder.GameSubState.CASStylistMode || newState == Responder.GameSubState.CASCollarMode || newState == Responder.GameSubState.CASSurgeryFaceMode || newState == Responder.GameSubState.CASSurgeryBodyMode)
            {
                Cheats("register");
                CASHook = Simulator.AddObject(new CASHook());
            }
            else
            {
                Cheats("unregister");
                Simulator.DestroyObject(CASHook);
            }
        }
        public static ObjectGuid CASHook;

        private static void Cheats(string action)
        {
            if (action == "register")
            {
                Commands.sGameCommands.Register("mcr", "Usage: Type MCR to edit the number of rows and columns.", Commands.CommandType.General, (Configure_cheat));
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