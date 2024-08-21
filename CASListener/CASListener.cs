﻿using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;

namespace Arro
{
    public class CASListener
    {
        //Tunables are found in Arro.CASListener.xml

        [Tunable]
        protected static bool kInstantiator = false;

        [Tunable]
        public static float fMainStateListenerSpeed;

        [Tunable]
        public static bool bDebugging = false;

        [Tunable]
        public static bool bHair = true;

        [Tunable]
        public static bool bFace = true;

        [Tunable]
        public static float fHairWindowSize;

        [Tunable]
        public static float fHairColorsPosition;

        [Tunable]
        public static float fFaceWindowSize;

        public static bool bShouldRepeat;

        public static string notificationMessage = "";

        static CASListener()
        {
            World.sOnWorldLoadFinishedEventHandler += new EventHandler(OnWorldLoadFinished);
        }
        private static void OnWorldLoadFinished(object sender, EventArgs e)
        {
            try
            {
                bool flag = Sims3.Gameplay.UI.Responder.Instance != null;
                if (flag)
                {
                    Sims3.Gameplay.UI.Responder instance = Sims3.Gameplay.UI.Responder.Instance;
                    instance.GameStateChanging = (GameStateChangingDelegate)Delegate.Remove(instance.GameStateChanging, new GameStateChangingDelegate(CASListener.OnGameStateChanged));
                    Sims3.Gameplay.UI.Responder instance2 = Sims3.Gameplay.UI.Responder.Instance;
                    instance2.GameStateChanging = (GameStateChangingDelegate)Delegate.Combine(instance2.GameStateChanging, new GameStateChangingDelegate(CASListener.OnGameStateChanged));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnWorldLoadFinished");
            }
        }
        internal static void OnGameStateChanged(Sims3.UI.Responder.GameSubState previousState, Sims3.UI.Responder.GameSubState newState)
        {
            try
            {
                if (newState == Sims3.UI.Responder.GameSubState.CASFullMode || newState == Sims3.UI.Responder.GameSubState.CASMirrorMode || newState == Sims3.UI.Responder.GameSubState.CASTackMode || newState == Sims3.UI.Responder.GameSubState.CASDresserMode || newState == Sims3.UI.Responder.GameSubState.CASTattooMode || newState == Sims3.UI.Responder.GameSubState.CASStylistMode || newState == Sims3.UI.Responder.GameSubState.CASCollarMode || newState == Sims3.UI.Responder.GameSubState.CASSurgeryFaceMode || newState == Sims3.UI.Responder.GameSubState.CASSurgeryBodyMode)
                {
                    Simulator.AddObject(new Sims3.Gameplay.OneShotFunctionTask(new Sims3.Gameplay.Function(CASListener.MainStateListener), StopWatch.TickStyles.Seconds, 1f));
                    bShouldRepeat = true;

                }
                else
                {
                    bShouldRepeat = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnGameStateChanged");
            }
        }
        public static void MainStateListener()
        {
            try
            {
                var PhysicalLayout = CASPhysical.sPhysicalLayout;
                var FacialDetailsLayout = CASFacialDetails.sPhysicalLayout;
                if (FacialDetailsLayout == null && PhysicalLayout == null && bShouldRepeat)
                {
                    Hair.StateListenerHair();
                    Face.StateListenerFace();
                    if (bDebugging)
                    {
                        notificationMessage = "Face and Hair are both absent.";
                    }
                }
                if (FacialDetailsLayout != null && PhysicalLayout == null && bFace && bShouldRepeat)
                {
                    Face.StateListenerFace();
                    if (bDebugging)
                    {
                        notificationMessage = "Face present, Hair absent.";
                    }
                }
                if (FacialDetailsLayout == null && PhysicalLayout != null && bHair && bShouldRepeat)
                {
                    Hair.StateListenerHair();
                    if (bDebugging)
                    {
                        notificationMessage = "Hair present, Face absent.";
                    }
                }
                Simulator.AddObject(new Sims3.Gameplay.OneShotFunctionTask(new Sims3.Gameplay.Function(CASListener.MainStateListener), StopWatch.TickStyles.Seconds, fMainStateListenerSpeed));
                if (bDebugging)
                {
                    Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(notificationMessage, StyledNotification.NotificationStyle.kGameMessageNegative));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "MainStateListener");
            }
        }
        public static class TinyUIFixForTS3Integration 
        {
            public delegate float FloatGetter();

            public static FloatGetter getUIScale = () => 1f;
        }
    }
}