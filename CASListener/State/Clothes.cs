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
                    Rect GridArea = CASClothingCategory.gSingleton.mClothingTypesGrid.Area;
                    var VisibleRows = (uint)fVisibleRows;
                    GridArea.Height = (139f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                    var VisibleColumns = (uint)fVisibleColumns;
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
            if (Main.isNraasMcInstalled)
            {
                new Clothes().InvokeNraasPopulateGrid();
                return;
            }
            CASClothingCategory.gSingleton.PopulateGrid();
        }
        public static void SetButtonVisibility()
        {
            try
            {
                var buttons = new[]
                {
                  CASClothingCategory.gSingleton.mTrashButton,
                  CASClothingCategory.gSingleton.mSaveButton,
                  CASClothingCategory.gSingleton.mDesignButton,
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
            if (currentLayout != null)
            {
                float startingPositionX;
                float startingPositionY;
                switch (currentLayout)
                {
                    case "CASClothing":
                        DoneButton = CASClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * TinyUIFixForTS3Integration.getUIScale() * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CASDresserClothing":
                        DoneButton = CASDresserClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * TinyUIFixForTS3Integration.getUIScale() * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CAPAccessories":
                        DoneButton = CAPAccessories.gSingleton.GetChildByID(2095900161U, true) as Button;
                        startingPositionX = 353f;
                        startingPositionY = 35f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * TinyUIFixForTS3Integration.getUIScale() * (fVisibleColumns - 1)), startingPositionY);
                        break;
                }
            }
        }
        public static Button mConfigureButton;
        public static void ShareButtonHook()
        {
            mConfigureButton = CASClothingCategory.gSingleton.mShareButton;
            mConfigureButton.Position = new Vector2(CASClothingCategory.gSingleton.mTrashButton.Position.x + 10f * TinyUIFixForTS3Integration.getUIScale(), CASClothingCategory.gSingleton.mSortButton.Position.y - 13f * TinyUIFixForTS3Integration.getUIScale());
            mConfigureButton.TooltipText = Localization.LocalizeString("Arro/MCR/Local:ConfigureGrid", new object[0]);
            mConfigureButton.Click -= CASClothingCategory.gSingleton.OnShareButtonClick;
            mConfigureButton.MouseUp += (sender, args) => OnConfigureButtonClick(sender, args);
            mConfigureButton.Tick += (sender, args) => EnableConfigureButton(sender, args);
        }
        public static void OnConfigureButtonClick(WindowBase sender, UIMouseEventArgs args)
        {
            try
            {
                if (args.MouseKey == MouseKeys.kMouseLeft)
                {
                    Simulator.AddObject(new OneShotFunctionTask(Configure.Clothes, StopWatch.TickStyles.Milliseconds, 1f));
                }
                else if (args.MouseKey == MouseKeys.kMouseRight)
                {
                    Simulator.AddObject(new OneShotFunctionTask(ResetGrid, StopWatch.TickStyles.Milliseconds, 1f));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnGridClick");
            }
        }
        private static bool isResetDialogShown;
        public static void ResetGrid()
        {
            if (!isResetDialogShown)
            {
                isResetDialogShown = true;
                var Continue = TwoButtonDialog.Show(
                    Localization.LocalizeString("Arro/MCR/Local:DoYouWantToResetGrid", new object[0]),
                    Localization.LocalizeString("Ui/Caption/Global:Yes", new object[0]),
                    Localization.LocalizeString("Ui/Caption/Global:No", new object[0])
                );
                isResetDialogShown = false;
                if (!Continue) return;
                fVisibleRows = 3;
                fVisibleColumns = 1;
                CASHook.SetBool(false, false, false);
            }
        }
        private static void EnableConfigureButton(WindowBase sender, UIEventArgs eventArgs)
        {
            mConfigureButton.Enabled = true;
        }

        public static void SetClothingBackgroundSize()
        {
            var backgroundHeight = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
            var backgroundWidth = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
            if (currentLayout != null)
            {
                Rect rect;
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
                if (targetType == null) return;
                MethodInfo populateGridMethod = targetType.GetMethod("PopulateGrid", BindingFlags.NonPublic | BindingFlags.Static);
                populateGridMethod?.Invoke(null, null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InvokeNraasPopulateGrid");
            }
        }
    }
}
