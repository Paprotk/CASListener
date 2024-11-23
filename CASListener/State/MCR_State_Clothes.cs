using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using System;
using System.Reflection;

namespace Arro.MCR
{
    public class Clothes : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 3;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 1;

        public static bool shouldMoveDoneButton = true;

        public static uint previousVisibleColumns;

        public static bool spDisabled = false;

        public static string currentLayout;

        public override void Simulate()
        {
            try
            {
                var ClothesLayout = CASClothingCategory.sClothingCategoryLayout;

                if (ClothesLayout != null)
                {
                    Main.canMCRClothes = true;
                    OnTick();
                    previousVisibleColumns = (uint)fVisibleColumns;
                }
                else
                {
                    Main.canMCRClothes = false;
                    shouldMoveDoneButton = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerClothes");
            }
        }
        public static void OnTick()
        {
            try
            {
                GetCurrentLayout();
                SetClothesItemgrid();
                RefreshGrid();
                SetCASClothingBackgroundSize();
                SetButtonState();
                if (shouldMoveDoneButton)
                {
                    MoveDoneButton();
                }
                shouldMoveDoneButton = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnTick_Clothes");
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
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetClothesItemgrid");
            }
        }
        public static void RefreshGrid()
        {
            if (Configure.shouldUpdate) //This refreshes grid
            {
                if (Main.isNraasMCInstalled)
                {
                    new Clothes().InvokeNraasPopulateGrid(); //If NRaasMC is installed then use reflection to invoke method without referencing it in project
                    Configure.shouldUpdate = false;
                    return;
                }
                CASClothingCategory.gSingleton.PopulateGrid();
                Configure.shouldUpdate = false;
            }
        }
        public static void SetButtonState() //Disables buttons that are not needed.
        {
            try
            {
                var buttons = new[]
                {
                     CASClothingCategory.gSingleton.mTrashButton,
                     CASClothingCategory.gSingleton.mShareButton,
                     CASClothingCategory.gSingleton.mSaveButton,
                     CASClothingCategory.gSingleton.mDesignButton
                };
                foreach (var button in buttons)
                {
                    button.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetButtonState");
            }
        }
        public static Button DoneButton;
        public static void MoveDoneButton()
        {
            try
            {
                float startingPositionX;
                float startingPositionY;
                float fVisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;

                switch (currentLayout)
                {
                    case "CASClothing":
                        DoneButton = CASClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        DoneButton.Click -= CASClothing.gSingleton.OnDoneButtonClick;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CASDresserClothing":
                        DoneButton = CASDresserClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                        DoneButton.Click -= CASDresserClothing.gSingleton.OnDoneButtonClick;
                        startingPositionX = -8f;
                        startingPositionY = 6f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                    case "CAPAccessories":
                        DoneButton = CAPAccessories.gSingleton.GetChildByID(2095900161U, true) as Button;
                        DoneButton.Click -= CAPAccessories.gSingleton.OnDoneButtonClick;
                        startingPositionX = 353f;
                        startingPositionY = 35f;
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                        break;
                }
                DoneButton.MouseUp += Clothes.OnDoneClick;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "MoveDoneButton");
            }
        }
        public static void OnDoneClick(WindowBase sender, UIMouseEventArgs args)
        {
            if (args.MouseKey == MouseKeys.kMouseRight)
            {
                Simulator.AddObject(new OneShotFunctionTask(Configure.Clothes, StopWatch.TickStyles.Seconds, 0.1f));
                return;
            }
            switch (currentLayout)
            {
                case "CASClothing":
                    CASController.Singleton.SetCurrentState(
                        new CASState(CASTopState.CreateASim, CASMidState.Summary, CASPhysicalState.None, CASClothingState.None)
                    );
                    args.Handled = true;
                    break;

                case "CASDresserClothing":
                    CASController.Singleton.SetCurrentState(
                        new CASState(CASTopState.Dresser, CASMidState.Summary, CASPhysicalState.None, CASClothingState.None)
                    );
                    args.Handled = true;
                    break;

                case "CAPAccessories":
                    CASMode casmode = Responder.Instance.CASModel.CASMode;
                    if (casmode != CASMode.Full)
                    {
                        switch (casmode)
                        {
                            case CASMode.Collar:
                                CASController.Singleton.SetCurrentState(CASState.CollarSummary);
                                break;
                            case CASMode.Tack:
                                CASController.Singleton.SetCurrentState(CASState.TackSummary);
                                break;
                        }
                    }
                    else
                    {
                        CASController.Singleton.SetCurrentState(CASState.PetSummary);
                    }
                    args.Handled = true;
                    break;
            }
        }

        public static void SetCASClothingBackgroundSize()
        {
            try
            {
                Rect rect;
                float backgroundHeight = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                float backgroundWidth = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
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
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetCASClothingSize");
            }
        }
        public void InvokeNraasPopulateGrid()
        {
            if (!Main.isNraasMCInstalled || Main.nraasAssembly == null)
                return;

            try
            {
                // Find the target type within the assembly
                Type targetType = Main.nraasAssembly.GetType("NRaas.MasterControllerSpace.CAS.CASClothingCategoryEx");
                if (targetType == null)
                {
                    return;
                }

                // Find and invoke the PopulateGrid method
                MethodInfo populateGridMethod = targetType.GetMethod("PopulateGrid", BindingFlags.NonPublic | BindingFlags.Static);
                if (populateGridMethod != null)
                {
                    populateGridMethod.Invoke(null, null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InvokeNraasPopulateGrid");
            }
        }
        public void InvokeDisableSp()
        {
            if (!Main.isSmoothPatchInstalled || Main.smoothpatchAssembly == null)
                return;

            try
            {
                // Find the target type within the assembly
                Type targetType = Main.smoothpatchAssembly.GetType("LazyDuchess.SmoothPatch.ClothingPerformance");
                if (targetType == null)
                {
                    return;
                }

                // Find and invoke the OnWorldQuit method
                MethodInfo unhookMethod = targetType.GetMethod("OnWorldQuit", BindingFlags.NonPublic | BindingFlags.Static);
                if (unhookMethod != null)
                {
                    unhookMethod.Invoke(null, new object[] { null, null });
                    spDisabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InvokeDisableSp");
            }
        }
        public void InvokeEnableSp()
        {
            try
            {
                // Find the target type within the assembly
                Type targetType = Main.smoothpatchAssembly.GetType("LazyDuchess.SmoothPatch.ClothingPerformance");
                if (targetType == null)
                {
                    return;
                }

                // Find and invoke the OnWorldLoad method
                MethodInfo hookMethod = targetType.GetMethod("OnWorldLoad", BindingFlags.NonPublic | BindingFlags.Static);
                if (hookMethod != null)
                {
                    hookMethod.Invoke(null, new object[] { null, null });
                    spDisabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InvokeEnableSp");
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
