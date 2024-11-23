using Sims3.Gameplay.ObjectComponents;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using System;
using System.Reflection;


namespace Arro.MCR
{
    public class Face : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 4;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 4;

        [PersistableStatic(true)]
        public static float fVisibleSliders = 3;

        public static string currentLayout;
        public static string currentLayoutState;
        public static Button doneButton;
        public static bool shouldupdate = true;

        public override void Simulate()
        {
            try
            {
                if (CASFacialDetails.gSingleton != null)
                {
                    OnTick();
                    Main.canMCRFace = true;
                }
                else
                {
                    Main.canMCRFace = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerFace");
            }
        }
        public static void OnTick()
        {
            try
            {
                GetCurrentLayout();
                SetFaceItemGrid();
                SetCASFaceBackgroundSize();
                MoveDoneButton();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnTick_Face");
            }
        }
        public static void GetCurrentLayout()
        {
            if (CASHeadEars.gSingleton != null)
            {
                currentLayout = "CASHeadEars"; // Get the type of the instance
                currentLayoutState = CASHeadEars.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
            }
            else if (CASEyes.gSingleton != null)
            {
                currentLayout = "CASEyes";
                currentLayoutState = CASEyes.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
            }
            else if (CASNose.gSingleton != null)
            {
                currentLayout = "CASNose";
                currentLayoutState = CASNose.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
            }
            else if (CASMouth.gSingleton != null)
            {
                currentLayout = "CASMouth";
                currentLayoutState = CASMouth.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
            }
            else if (CASMoles.gSingleton != null)
            {
                currentLayout = "CASMoles";
                currentLayoutState = "basics";
            }
            else if (CASMakeup.gSingleton != null)
            {
                currentLayout = "CASMakeup";
                currentLayoutState = "basics";
            }
        }

        public static void SetFaceItemGrid()
        {
            try
            {
                switch (currentLayout)
                {
                    case "CASHeadEars":
                        var VisibleRows = CASHeadEars.gSingleton.mPresetsGrid.VisibleRows;
                        var VisibleColumns = CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns;
                        //var backgroundImage = CASHeadEars.gSingleton.GetChildByID(344323677U, true) as Drawmas;
                        Rect GridArea = CASHeadEars.gSingleton.mPresetsGrid.Area;
                        VisibleRows = (uint)fVisibleRows;
                        GridArea.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                        VisibleColumns = (uint)fVisibleColumns;
                        GridArea.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                        CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
                        CASHeadEars.gSingleton.mPresetsGrid.VisibleRows = VisibleRows;
                        CASHeadEars.gSingleton.mPresetsGrid.Area = GridArea;
                        break;
                    case "CASEyes":
                        var VisibleRows1 = CASEyes.gSingleton.mPresetsGrid.VisibleRows;
                        var VisibleColumns1 = CASEyes.gSingleton.mPresetsGrid.VisibleColumns;
                        Rect GridArea1 = CASEyes.gSingleton.mPresetsGrid.Area;
                        VisibleRows1 = (uint)fVisibleRows;
                        GridArea1.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                        VisibleColumns1 = (uint)fVisibleColumns;
                        GridArea1.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                        CASEyes.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns1;
                        CASEyes.gSingleton.mPresetsGrid.VisibleRows = VisibleRows1;
                        CASEyes.gSingleton.mPresetsGrid.Area = GridArea1;
                        break;
                    case "CASNose":
                        var VisibleRows2 = CASNose.gSingleton.mPresetsGrid.VisibleRows;
                        var VisibleColumns2 = CASNose.gSingleton.mPresetsGrid.VisibleColumns;
                        Rect GridArea2 = CASNose.gSingleton.mPresetsGrid.Area;
                        VisibleRows2 = (uint)fVisibleRows;
                        GridArea2.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                        VisibleColumns = (uint)fVisibleColumns;
                        GridArea2.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                        CASNose.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
                        CASNose.gSingleton.mPresetsGrid.VisibleRows = VisibleRows2;
                        CASNose.gSingleton.mPresetsGrid.Area = GridArea2;
                        break;
                    case "CASMouth":
                        var VisibleRows3 = CASMouth.gSingleton.mPresetsGrid.VisibleRows;
                        var VisibleColumns3 = CASMouth.gSingleton.mPresetsGrid.VisibleColumns;
                        Rect GridArea3 = CASMouth.gSingleton.mPresetsGrid.Area;
                        VisibleRows3 = (uint)fVisibleRows;
                        GridArea3.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                        VisibleColumns = (uint)fVisibleColumns;
                        GridArea3.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                        CASMouth.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
                        CASMouth.gSingleton.mPresetsGrid.VisibleRows = VisibleRows3;
                        CASMouth.gSingleton.mPresetsGrid.Area = GridArea3;
                        break;

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetFaceItemGrid");
            }
        }
        public static void SetCASFaceBackgroundSize()
        {
            try
            {
                Rect rect;
               // float backgroundWidth = (136.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
                //float backgroundHeight = (145.33f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();

                if (CASFacialDetails.gSingleton.mShortPanel != null && shouldupdate)
                {
                    if (currentLayoutState == "basics")
                    {
                        rect = CASFacialDetails.gSingleton.mShortPanel.Area;
                        rect.Width += 136;
                        //rect.Height = backgroundHeight;
                        CASFacialDetails.gSingleton.mShortPanel.Area = rect;
                    }
                    else if (currentLayoutState == "advanced")
                    {
                        rect = CASFacialDetails.gSingleton.mShortPanel.Area;
                        rect.Width += 136;
                        //rect.Height = backgroundHeight;
                        CASFacialDetails.gSingleton.mShortPanel.Area = rect;
                    }
                    shouldupdate = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetCASFaceBackgroundSize");
            }
        }

        public static void OnDoneClick(WindowBase sender, UIMouseEventArgs args)
        {
            if (args.MouseKey == MouseKeys.kMouseRight)
            {
                Simulator.AddObject(new OneShotFunctionTask(Configure.Face, StopWatch.TickStyles.Seconds, 0.1f));
                return;
            }
            CASTopState topState = CASTopState.CreateASim;
            if (Responder.Instance.CASModel.CASMode == CASMode.Mirror)
            {
                topState = CASTopState.Mirror;
            }
            else if (Responder.Instance.CASModel.CASMode == CASMode.Tattoo)
            {
                topState = CASTopState.Tattoo;
            }
            else if (Responder.Instance.CASModel.CASMode == CASMode.Stylist)
            {
                topState = CASTopState.Stylist;
            }
            CASController.Singleton.SetCurrentState(new CASState(topState, CASMidState.Summary, CASPhysicalState.None, CASClothingState.None));
            args.Handled = true;

        }
        public static void MoveDoneButton()
        {
            try
            {
                float startingPositionX;
                float startingPositionY;
                doneButton = CASFacialDetails.gSingleton.GetChildByID(98278400U, true) as Button;
                doneButton.Click -= CASFacialDetails.gSingleton.OnDoneButtonClick;
                startingPositionX = 353f;
                startingPositionY = 35f;
                doneButton.Position = new Vector2(startingPositionX + (100f * (fVisibleColumns - 3)), startingPositionY);
                doneButton.MouseUp += OnDoneClick;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "MoveDoneButton");
            }
        }
    }
}
//Vector2 donebuttonposition = CASFacialDetails.gSingleton.mDoneButton.Position;
//// create a message with the position
//string message = string.Format("donebutton position: x = {0}, y = {1}", donebuttonposition.x, donebuttonposition.y);
//// show the notification
//Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(message, StyledNotification.NotificationStyle.kGameMessageNegative));