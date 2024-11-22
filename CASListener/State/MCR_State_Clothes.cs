using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using Sims3.UI.Hud;
using System;
using System.Reflection;

namespace Arro.MCR
{
    public class Clothes : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 6;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 2;

        public static bool shouldMoveDoneButton = true;

        public static uint previousVisibleColumns;

        public static bool spDisabled = false;

        public static string clothesState;

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
                SetClothesItemgrid();
                RefreshGrid();
                SetCASClothingSize();
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
        public static void SetClothesItemgrid() //This is responsible for itemgrid, not background size.
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
            if (Config.shouldUpdate) //This refreshes grid
            {
                if (Main.isNraasMCInstalled)
                {
                    new Clothes().InvokeNraasPopulateGrid(); //If NRaasMC is installed then use reflection to invoke method without referencing it in project
                }
                else
                {
                    CASClothingCategory.gSingleton.PopulateGrid();
                }
                Config.shouldUpdate = false;
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
                if (CASClothing.gSingleton != null)
                {
                    DoneButton = CASClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                    DoneButton.Click -= CASClothing.gSingleton.OnDoneButtonClick;

                    if (DoneButton != null && shouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = -8f;
                        float startingPositionY = 6f;

                        // Calculate the number of visible columns
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
                }

                if (CASDresserClothing.sClothingLayout != null)
                {
                    DoneButton = CASDresserClothing.gSingleton.GetChildByID(98278400U, true) as Button;
                    DoneButton.Click -= CASDresserClothing.gSingleton.OnDoneButtonClick;

                    if (DoneButton != null && shouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = -8f;
                        float startingPositionY = 6f;

                        // Calculate the number of visible columns
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
                }

                if (CAPAccessories.gSingleton != null)
                {
                    DoneButton = CAPAccessories.gSingleton.GetChildByID(2095900161U, true) as Button;
                    DoneButton.Click -= CAPAccessories.gSingleton.OnDoneButtonClick;

                    if (DoneButton != null && shouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = 353f;
                        float startingPositionY = 35f;

                        // Calculate the number of visible columns
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
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
                Simulator.AddObject(new OneShotFunctionTask(Config.Configure, StopWatch.TickStyles.Seconds, 0.1f));
                return;
            }
            switch (clothesState)
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

        public static int GetVisibleColumns()
        {
            var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
            return (int)VisibleColumns;
        }

        public static void SetCASClothingSize() //This is responsible for setting window background size. 
        {
            try
            {
                if (CASClothing.sClothingLayout != null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout == null)
                {
                    Rect CASClothingHeight = CASClothing.gSingleton.Area;
                    clothesState = "CASClothing";
                    if (fVisibleRows >= 3)
                    {
                        CASClothingHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns >= 1)
                    {
                        CASClothingHeight.Width = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    CASClothing.gSingleton.Area = CASClothingHeight;
                }
                if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout != null && CAPAccessories.sCAPAccessoriesLayout == null)
                {
                    clothesState = "CASDresserClothing";
                    Rect CASDresserClothingHeight = CASDresserClothing.gSingleton.Area;
                    if (fVisibleRows >= 3)
                    {
                        CASDresserClothingHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns >= 1)
                    {
                        CASDresserClothingHeight.Width = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    CASDresserClothing.gSingleton.Area = CASDresserClothingHeight;
                }
                if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout != null)
                {
                    clothesState = "CAPAccessories";
                    Rect CAPAccessoriesHeight = CAPAccessories.gSingleton.Area;
                    if (fVisibleRows >= 3)
                    {
                        CAPAccessoriesHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns >= 1)
                    {
                        CAPAccessoriesHeight.Width = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    CAPAccessories.gSingleton.Area = CAPAccessoriesHeight;
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
