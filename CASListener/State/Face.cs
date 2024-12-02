//using Sims3.SimIFace;
//using Sims3.UI;
//using Sims3.UI.CAS;
//using System;


//namespace Arro.MCR
//{
//    public class Face
//    {
//        [PersistableStatic(true)]
//        public static float fVisibleRows = 3;

//        [PersistableStatic(true)]
//        public static float fVisibleColumns = 3;

//        [PersistableStatic(true)]
//        public static float fVisibleSliders = 3;

//        public static string currentLayout;
//        public static string currentLayoutState;
//        public static Button doneButton;

//        public static void Hook()
//        {
//            try
//            {
//                GetCurrentLayout();
//                SetFaceItemGrid();
//                SetCASFaceBackgroundSize();
//                MoveDoneButton();
//            }
//            catch (Exception ex)
//            {
//                ExceptionHandler.HandleException(ex, "OnTick_Face");
//            }
//        }
//        public static void GetCurrentLayout()
//        {
//            if (CASHeadEars.gSingleton != null)
//            {
//                currentLayout = "CASHeadEars";
//                currentLayoutState = CASHeadEars.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
//            }
//            else if (CASEyes.gSingleton != null)
//            {
//                currentLayout = "CASEyes";
//                currentLayoutState = CASEyes.gSingleton.mBasicsPanel.Visible ? "other" : "advanced";
//            }
//            else if (CASNose.gSingleton != null)
//            {
//                currentLayout = "CASNose";
//                currentLayoutState = CASNose.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
//            }
//            else if (CASMouth.gSingleton != null)
//            {
//                currentLayout = "CASMouth";
//                currentLayoutState = CASMouth.gSingleton.mBasicsPanel.Visible ? "basics" : "advanced";
//            }
//            else if (CASMoles.gSingleton != null)
//            {
//                currentLayout = "CASMoles";
//                currentLayoutState = "other";
//            }
//            else if (CASMakeup.gSingleton != null)
//            {
//                currentLayout = "CASMakeup";
//                currentLayoutState = "other";
//            }
//            else if (CASTattoo.gSingleton != null)
//            {
//                currentLayout = "CASTattoo";
//                currentLayoutState = "other";
//            }
//        }

//        public static void SetFaceItemGrid()
//        {
//            try
//            {
//                switch (currentLayout)
//                {
//                    case "CASHeadEars":
//                        if (currentLayoutState == "basics")
//                        {
//                            var VisibleRows = CASHeadEars.gSingleton.mPresetsGrid.VisibleRows;
//                            var VisibleColumns = CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns;
//                            //var backgroundImage = CASHeadEars.gSingleton.GetChildByID(98278400U, 255510366U, true) as Window;
//                            //backgroundImage.Visible = false;
//                            Rect GridArea = CASHeadEars.gSingleton.mPresetsGrid.Area;
//                            VisibleRows = (uint)fVisibleRows;
//                            GridArea.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
//                            VisibleColumns = (uint)fVisibleColumns;
//                            GridArea.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
//                            CASHeadEars.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
//                            CASHeadEars.gSingleton.mPresetsGrid.VisibleRows = VisibleRows;
//                            CASHeadEars.gSingleton.mPresetsGrid.Area = GridArea;
//                        }
//                        else if (currentLayoutState == "advanced")
//                        {
//                            if (CASHeadEars.gSingleton.mMiscGrid != null)
//                            {
//                            }
//                            else if (CASHeadEars.gSingleton.mChinGrid != null)
//                            {
//                            }
//                            else if (CASHeadEars.gSingleton.mJawGrid != null)
//                            {
//                            }
//                            else if (CASHeadEars.gSingleton.mCheekGrid != null)
//                            {
//                            }
//                            else if (CASHeadEars.gSingleton.mEarGrid != null)
//                            {
//                            }
//                        }
//                        break;

//                    case "CASEyes":
//                        if (currentLayoutState == "basics")
//                        {
//                            //var VisibleRows = CASEyes.gSingleton.mPresetsGrid.VisibleRows;
//                            //var VisibleColumns = CASEyes.gSingleton.mPresetsGrid.VisibleColumns;
//                            //Rect GridArea = CASEyes.gSingleton.mPresetsGrid.Area;
//                            //VisibleRows = (uint)fVisibleRows;
//                            //GridArea.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
//                            //VisibleColumns = (uint)fVisibleColumns;
//                            //GridArea1.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
//                            //CASEyes.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
//                            //CASEyes.gSingleton.mPresetsGrid.VisibleRows = VisibleRows;
//                            //CASEyes.gSingleton.mPresetsGrid.Area = GridArea;
//                        }
//                        else if (currentLayoutState == "advanced")
//                        {
//                            if (CASEyes.gSingleton.mMiscGrid != null)
//                            {
//                            }
//                            else if (CASEyes.gSingleton.mEyeShapeGrid != null)
//                            {
//                            }
//                            else if (CASEyes.gSingleton.mEyeLidGrid != null)
//                            {
//                            }
//                            else if (CASEyes.gSingleton.mBrowGrid != null)
//                            {
//                            }
//                        }
//                        break;
//                    case "CASNose":
//                        if (currentLayoutState == "basics")
//                        {
//                            var VisibleRows = CASNose.gSingleton.mPresetsGrid.VisibleRows;
//                            var VisibleColumns = CASNose.gSingleton.mPresetsGrid.VisibleColumns;
//                            Rect GridArea = CASNose.gSingleton.mPresetsGrid.Area;
//                            VisibleRows = (uint)fVisibleRows;
//                            GridArea.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
//                            VisibleColumns = (uint)fVisibleColumns;
//                            GridArea.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
//                            CASNose.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
//                            CASNose.gSingleton.mPresetsGrid.VisibleRows = VisibleRows;
//                            CASNose.gSingleton.mPresetsGrid.Area = GridArea;
//                        }
//                        else if (currentLayoutState == "advanced")
//                        {
//                            if (CASNose.gSingleton.mMiscGrid != null)
//                            {
//                            }
//                            else if (CASNose.gSingleton.mNostrilGrid != null)
//                            {
//                            }
//                            else if (CASNose.gSingleton.mTipGrid != null)
//                            {
//                            }
//                            else if (CASNose.gSingleton.mBridgeGrid != null)
//                            {
//                            }
//                        }
//                        break;
//                    case "CASMouth":
//                        if (currentLayoutState == "basics")
//                        {
//                            var VisibleRows = CASMouth.gSingleton.mPresetsGrid.VisibleRows;
//                            var VisibleColumns = CASMouth.gSingleton.mPresetsGrid.VisibleColumns;
//                            Rect GridArea = CASMouth.gSingleton.mPresetsGrid.Area;
//                            VisibleRows = (uint)fVisibleRows;
//                            GridArea.Height = (104f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
//                            VisibleColumns = (uint)fVisibleColumns;
//                            GridArea.Width = (106.33f * fVisibleColumns) * TinyUIFixForTS3Integration.getUIScale();
//                            CASMouth.gSingleton.mPresetsGrid.VisibleColumns = VisibleColumns;
//                            CASMouth.gSingleton.mPresetsGrid.VisibleRows = VisibleRows;
//                            CASMouth.gSingleton.mPresetsGrid.Area = GridArea;
//                        }
//                        else if (currentLayoutState == "advanced")
//                        {
//                            if (CASMouth.gSingleton.mMiscGrid != null)
//                            {
//                            }
//                            else if (CASMouth.gSingleton.mLowerGrid != null)
//                            {
//                            }
//                            else if (CASMouth.gSingleton.mUpperGrid != null)
//                            {
//                            }
//                        }
//                        break;

//                }
//            }
//            catch (Exception ex)
//            {
//                ExceptionHandler.HandleException(ex, "SetFaceItemGrid");
//            }
//        }
//        public static void SetCASFaceBackgroundSize()
//        {
//            try
//            {
//                Rect rect;
//                if (CASFacialDetails.gSingleton.mShortPanel != null)
//                {
//                    float baseWidth = 409f;
//                    float backgroundWidth = (baseWidth + (109f * (fVisibleColumns - 3))) * TinyUIFixForTS3Integration.getUIScale();
//                    float backgroundHeight = (100f * fVisibleRows + 148f) * TinyUIFixForTS3Integration.getUIScale();
//                    float sliderBackgroundHeight = (100f * fVisibleSliders + 210f) * TinyUIFixForTS3Integration.getUIScale();

//                    if (currentLayoutState == "basics")
//                    {
//                        rect = CASFacialDetails.gSingleton.mShortPanel.Area;
//                        rect.Width = backgroundWidth;
//                        rect.Height = backgroundHeight;
//                        CASFacialDetails.gSingleton.mShortPanel.Area = rect;
//                    }
//                    else if (currentLayoutState == "advanced")
//                    {
//                        rect = CASFacialDetails.gSingleton.mShortPanel.Area;
//                        rect.Width = 409f;
//                        rect.Height = sliderBackgroundHeight;
//                        CASFacialDetails.gSingleton.mShortPanel.Area = rect;
//                    }
//                    else if (currentLayout == "CASMoles")
//                    {
//                        rect = CASFacialDetails.gSingleton.mShortPanel.Area;
//                        rect.Width = 409f;
//                        rect.Height = 448f;
//                        CASFacialDetails.gSingleton.mShortPanel.Area = rect;
//                    }
//                }
//                else if (CASFacialDetails.gSingleton.mLongPanel != null)
//                {
//                    float backgroundWidth = (100f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
//                    if (currentLayout == "CASEyes")
//                    {
//                        rect = CASFacialDetails.gSingleton.mLongPanel.Area;
//                        rect.Width = backgroundWidth;
//                        CASFacialDetails.gSingleton.mLongPanel.Area = rect;
//                    }
//                    else if (currentLayout == "CASMakeup")
//                    {
//                        rect = CASFacialDetails.gSingleton.mLongPanel.Area;
//                        rect.Width = backgroundWidth;
//                        CASFacialDetails.gSingleton.mLongPanel.Area = rect;
//                    }
//                    else if (currentLayout == "CASTatto")
//                    {
//                        rect = CASFacialDetails.gSingleton.mLongPanel.Area;
//                        rect.Width = backgroundWidth;
//                        CASFacialDetails.gSingleton.mLongPanel.Area = rect;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ExceptionHandler.HandleException(ex, "SetCASFaceBackgroundSize");
//            }
//        }
//        public static void MoveDoneButton()
//        {
//            try
//            {
//                doneButton = CASFacialDetails.gSingleton.GetChildByID(98278400U, true) as Button;
//                doneButton.Click -= CASFacialDetails.gSingleton.OnDoneButtonClick;
//                doneButton.MouseUp += OnDoneClick;
//                float startingPositionX = 353f;
//                float startingPositionY = 35f;
//                if (currentLayoutState == "basics")
//                {
//                    doneButton.Position = new Vector2(startingPositionX + (100f * (fVisibleColumns - 3)), startingPositionY);
//                    return;
//                }
//                doneButton.Position = new Vector2(startingPositionX, startingPositionY);
//            }
//            catch (Exception ex)
//            {
//                ExceptionHandler.HandleException(ex, "MoveDoneButton");
//            }
//        }
//        public static void OnDoneClick(WindowBase sender, UIMouseEventArgs args)
//        {
//            if (args.MouseKey == MouseKeys.kMouseRight)
//            {
//                Simulator.AddObject(new OneShotFunctionTask(Configure.Face, StopWatch.TickStyles.Milliseconds, 1f));
//                return;
//            }
//            CASTopState topState = CASTopState.CreateASim;
//            if (Responder.Instance.CASModel.CASMode == CASMode.Mirror)
//            {
//                topState = CASTopState.Mirror;
//            }
//            else if (Responder.Instance.CASModel.CASMode == CASMode.Tattoo)
//            {
//                topState = CASTopState.Tattoo;
//            }
//            else if (Responder.Instance.CASModel.CASMode == CASMode.Stylist)
//            {
//                topState = CASTopState.Stylist;
//            }
//            CASController.Singleton.SetCurrentState(new CASState(topState, CASMidState.Summary, CASPhysicalState.None, CASClothingState.None));
//            args.Handled = true;
//        }
//    }
//}
////Vector2 donebuttonposition = CASFacialDetails.gSingleton.mDoneButton.Position;
////// create a message with the position
////string message = string.Format("donebutton position: x = {0}, y = {1}", donebuttonposition.x, donebuttonposition.y);
////// show the notification
////Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(message, StyledNotification.NotificationStyle.kGameMessageNegative));