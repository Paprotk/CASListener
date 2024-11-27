using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using System;
using System.Reflection;

namespace Arro.MCR
{

    public class Clothes
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 3;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 1;

        public static string currentLayout;

        public static void Hook()
        {
            try
            {
                GetCurrentLayout();
                SetClothesItemgrid();
                RefreshGrid();
                SetClothingBackgroundSize();
                SetButtonVisibility();
                MoveDoneButton();
                EnableButton();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Clothes.Hook");
            }
        }
        public static void GetCurrentLayout()
        {
            if (CASClothing.sClothingLayout != null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout == null)
            {
                currentLayout = "CASClothing";
            }
            else if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout != null && CAPAccessories.sCAPAccessoriesLayout == null)
            {
                currentLayout = "CASDresserClothing";
            }
            else if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout != null)
            {
                currentLayout = "CAPAccessories";
            }
        }
        public static void SetClothesItemgrid()
        {
            try
            {
                if (CASClothingCategory.gSingleton != null)
                {
                    var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                    var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
                    Rect GridArea = CASClothingCategory.gSingleton.mClothingTypesGrid.Area;
                    if (fVisibleRows >= 3) //Integration with smoothpatch is done via ArroMCRSP
                    {
                        VisibleRows = (uint)fVisibleRows;
                        GridArea.Height = (139f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    VisibleColumns = (uint)fVisibleColumns;
                    GridArea.Width = (305f * fVisibleColumns + 20f) * TinyUIFixForTS3Integration.getUIScale();
                    CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns = VisibleColumns;
                    CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows = VisibleRows;
                    CASClothingCategory.gSingleton.mClothingTypesGrid.Area = GridArea;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetClothesItemgrid");
            }
        }
        public static void RefreshGrid()
        {
            if (Main.isNraasMCInstalled)
            {
                new Clothes().InvokeNraasPopulateGrid(); //If NRaasMC is installed then use reflection to invoke method without referencing it in project
                return;
            }
            CASClothingCategory.gSingleton.PopulateGrid();
        }
        public static void SetButtonVisibility() //Disables buttons that are not needed.
        {
            try
            {
                var buttons = new[]
                {
                  CASClothingCategory.gSingleton.mTrashButton,
                  CASClothingCategory.gSingleton.mSaveButton,
                  CASClothingCategory.gSingleton.mDesignButton
                };
                foreach (var button in buttons)
                {
                    button.Visible = false;
                }
                ShareButtonHook();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetButtonVisibility");
            }

        }
        public static Button DoneButton;
        public static void MoveDoneButton()
        {
            float startingPositionX;
            float startingPositionY;
            if (currentLayout != null)
            {
                switch (currentLayout)
                {
                    case "CASClothing":
                        DoneButton = CASClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CASDresserClothing":
                        DoneButton = CASDresserClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CAPAccessories":
                        DoneButton = CAPAccessories.gSingleton.GetChildByID(2095900161U, true) as Button;
                        startingPositionX = 353f;
                        startingPositionY = 35f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                }
            }
        }
        public static Button mConfigureButton;
        public static void ShareButtonHook()
        {
            mConfigureButton = CASClothingCategory.gSingleton.mShareButton;
            mConfigureButton.Position = new Vector2(CASClothingCategory.gSingleton.mTrashButton.Position.x + 10f, CASClothingCategory.gSingleton.mSortButton.Position.y - 13f);
            mConfigureButton.TooltipText = Localization.LocalizeString("Arro/MCR/Local:ConfigureGrid", new object[0]);
            mConfigureButton.Click -= CASClothingCategory.gSingleton.OnShareButtonClick;
            mConfigureButton.MouseUp -= OnGridClick;
            mConfigureButton.MouseUp += OnGridClick;
        }

        public static void OnGridClick(WindowBase sender, UIMouseEventArgs args)
        {
            try
            {
                if (args.MouseKey == MouseKeys.kMouseLeft)
                {
                    Simulator.AddObject(new OneShotFunctionTask(Configure.Clothes, StopWatch.TickStyles.Milliseconds, 1f));
                    return;
                }
                if (args.MouseKey == MouseKeys.kMouseRight)
                {
                    Simulator.AddObject(new OneShotFunctionTask(() =>
                    {
                        bool Continue = TwoButtonDialog.Show(
                            Localization.LocalizeString("Arro/MCR/Local:DoYouWantToResetGrid", new object[0]),
                            Localization.LocalizeString("Ui/Caption/Global:Yes", new object[0]),
                            Localization.LocalizeString("Ui/Caption/Global:No", new object[0])
                        );

                        if (Continue)
                        {
                            fVisibleRows = 3;
                            fVisibleColumns = 1;
                            CASHook.SetBool(false, false, false);
                        }
                    }, StopWatch.TickStyles.Milliseconds, 1f));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnGridClick");
            }
        }
        public static void EnableButton()
        {
            mConfigureButton.Enabled = true;
            Simulator.AddObject(new OneShotFunctionTask(EnableButton, StopWatch.TickStyles.Seconds, 1f));
        }

        public static void SetClothingBackgroundSize()
        {
            Rect rect;
            float backgroundHeight = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
            float backgroundWidth = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
            if (currentLayout != null)
            {
                switch (currentLayout)
                {
                    case "CASClothing":
                        rect = CASClothing.gSingleton.Area;
                        rect.Height = backgroundHeight;
                        rect.Width = backgroundWidth;
                        CASClothing.gSingleton.Area = rect;
                        break;

                    case "CASDresserClothing":
                        rect = CASDresserClothing.gSingleton.Area;
                        rect.Height = backgroundHeight;
                        rect.Width = backgroundWidth;
                        CASDresserClothing.gSingleton.Area = rect;
                        break;

                    case "CAPAccessories":
                        rect = CAPAccessories.gSingleton.Area;
                        rect.Height = backgroundHeight;
                        rect.Width = backgroundWidth;
                        CAPAccessories.gSingleton.Area = rect;
                        break;
                }
            }
        }
        public void InvokeNraasPopulateGrid()
        {
            try
            {
                Type targetType = Main.nraasAssembly.GetType("NRaas.MasterControllerSpace.CAS.CASClothingCategoryEx");
                if (targetType != null)
                {
                    MethodInfo populateGridMethod = targetType.GetMethod("PopulateGrid", BindingFlags.NonPublic | BindingFlags.Static);
                    populateGridMethod?.Invoke(null, null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InvokeNraasPopulateGrid");
            }
        }
    }
}

//Vector2 doneButtonPosition = DoneButton.Position;
//// Create a message with the position
//string message = string.Format("DoneButton Position: X = {0}, Y = {1}", doneButtonPosition.x, doneButtonPosition.y);
//// Show the notification
//Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(message, StyledNotification.NotificationStyle.kGameMessageNegative));

//This is for vector2 position
