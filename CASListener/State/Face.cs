using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.SimIFace.CAS;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using System;

namespace Arro.MCR
{
    public class Face
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 3;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 3;

        [PersistableStatic(true)]
        public static float fVisibleSliders = 3;

        public static string currentState;
        public static string currentLayoutState;
        public static Button doneButton;
        public static bool shouldBoostrap;

        public static CASFacialDetails faceSingleton;

        public static void Hook()
        {
            try
            {
                faceSingleton = CASFacialDetails.gSingleton;
                faceSingleton.EffectFinished += OnFacialDetailsEffectFinished;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Face.Hook");
            }
        }
        public static void UpdateUI()
        {
            currentState = CASController.gSingleton.CurrentState.mPhysicalState.ToString();
            Bootstrap(currentState);
        }

        private static void OnFacialDetailsEffectFinished(WindowBase sender, UIHandledEventArgs eventArgs)
        {
            faceSingleton.EffectFinished -= OnFacialDetailsEffectFinished;
            currentState = CASController.gSingleton.CurrentState.mPhysicalState.ToString();
            Bootstrap(currentState);
            HookButtonClicks();
        }

        private static void Bootstrap(string currentstate)
        {
            
            if (currentstate == "HeadAndEars")
            {
                string mode = CASHeadEars.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
                HeadEars(mode);
                Button presets = CASHeadEars.gSingleton.GetChildByID(98284289U, true) as Button;
                Button advanced = CASHeadEars.gSingleton.GetChildByID(98284290U, true) as Button;
                presets.Click -= (clickSender, clickEventArgs) => HeadEars("basics");
                advanced.Click -= (clickSender, clickEventArgs) => HeadEars("advanced");
                presets.Click += (clickSender, clickEventArgs) => HeadEars("basics");
                advanced.Click += (clickSender, clickEventArgs) => HeadEars("advanced");
            }
        }

        private static void HookButtonClicks()
        {
            Button mHeadEarsButton = faceSingleton.GetChildByID(98278402U, true) as Button;
            mHeadEarsButton.Click += (sender, e) => HeadEarsButton_Click(sender, e);
        }

        private static void HeadEarsButton_Click(WindowBase sender, UIButtonClickEventArgs eventArgs)
        {
            string mode = CASHeadEars.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
            HeadEars(mode);
            Button presets = CASHeadEars.gSingleton.GetChildByID(98284289U, true) as Button;
            Button advanced = CASHeadEars.gSingleton.GetChildByID(98284290U, true) as Button;
            presets.Click += (clickSender, clickEventArgs) => HeadEars("basics");
            advanced.Click += (clickSender, clickEventArgs) => HeadEars("advanced");
        }

        private static void HeadEars(string mode)
        {
            if (mode == "basics")
            {
                var visibleRows = CASHeadEars.gSingleton.mPresetsGrid.VisibleRows;
                var visibleColumns = CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns;
                Rect GridArea = CASHeadEars.gSingleton.mPresetsGrid.Area;
                visibleRows = (uint)fVisibleRows;
                GridArea.Height = (103f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                visibleColumns = (uint)fVisibleColumns;
                GridArea.Width = (108f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns = visibleColumns;
                CASHeadEars.gSingleton.mPresetsGrid.VisibleRows = visibleRows;
                CASHeadEars.gSingleton.mPresetsGrid.Area = GridArea;
            }
            else if (mode == "advanced")
            {
             
            }
            MoveDoneButton(mode);
            SetCASFaceBackgroundSize(mode);
        }
        public static void MoveDoneButton(string mode)
        {
            try
            {
                doneButton = CASFacialDetails.gSingleton.GetChildByID(98278400U, true) as Button;
                float startingPositionX = 353f;
                float startingPositionY = 35f;
                if (mode == "basics")
                {
                    doneButton.Position = new Vector2(startingPositionX + (109f * fVisibleColumns), startingPositionY);
                }
                else if (mode == "advanced")
                {
                    doneButton.Position = new Vector2(startingPositionX, startingPositionY);
                }
                else if (mode == "long")
                {
                    doneButton.Position = new Vector2(startingPositionX, startingPositionY);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "MoveDoneButton");
            }
        }
        public static void SetCASFaceBackgroundSize(string mode)
        {
            try
            {
                Rect rect;

                if (mode == "basics")
                {
                }
                else if (mode == "advanced")
                {
                    rect = faceSingleton.mShortPanel.Area;
                    rect.Width = 409f;
                    rect.Height = 545f;
                    faceSingleton.mShortPanel.Area = rect;
                }
                else if (mode == "long")
                {

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetCASFaceBackgroundSize");
            }
        }
    }
}