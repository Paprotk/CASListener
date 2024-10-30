using System;
using System.Reflection;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;

namespace Arro.MCR
{
    public class Clothes : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows = 3;

        [PersistableStatic(true)]
        public static float fVisibleColumns = 1;

        public static bool ShouldMoveDoneButton = true;

        public static uint PreviousVisibleColumns; 

        public override void Simulate()
        {
            try
            {
                var ClothesLayout = CASClothingCategory.sClothingCategoryLayout;

                if (ClothesLayout != null)
                {
                    Main.CanMCRClothes = true;
                    SetClothesItemgrid();
                    PreviousVisibleColumns = (uint)fVisibleColumns;
    }
                else
                {
                    Main.CanMCRClothes = false;
                    ShouldMoveDoneButton = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerClothes");
            }
        }
        public static void SetClothesItemgrid() //This is responsible for itemgrid, not background size.
        {
            try
            {
                var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
                Rect GridArea = CASClothingCategory.gSingleton.mClothingTypesGrid.Area;
                if (fVisibleRows >= 3)
                {
                    VisibleRows = (uint)fVisibleRows;
                    GridArea.Height = (139f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                }
                if (fVisibleColumns >= 1)
                {
                    VisibleColumns = (uint)fVisibleColumns;
                    GridArea.Width = (305f * fVisibleColumns + 20f) * TinyUIFixForTS3Integration.getUIScale();
                }
                CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns = VisibleColumns;
                CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows = VisibleRows;
                CASClothingCategory.gSingleton.mClothingTypesGrid.Area = GridArea;
                if (RCConfigure.ShouldUpdate) //This refreshes grid
                {
                    if (Main.IsNraasMCInstalled)
                    {
                        new Clothes().InvokeNraasPopulateGrid(); //If NRaasMC is installed then use reflection to invoke method without referencing it in script
                    }
                    else
                    {
                        CASClothingCategory.gSingleton.PopulateGrid();
                    }
                    RCConfigure.ShouldUpdate = false;
                }
                SetCASClothingSize();
                SetButtonState();
                if (ShouldMoveDoneButton)
                {
                    MoveDoneButton();
                }
                ShouldMoveDoneButton = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetClothesItemgrid");
            }
        }
        public static void SetButtonState() //Disables buttons that are not needed.
        {
            try
            {
                var DeleteButton = CASClothingCategory.gSingleton.mTrashButton;
                var ShareButton = CASClothingCategory.gSingleton.mShareButton;
                var SaveButton = CASClothingCategory.gSingleton.mSaveButton;
                var DesignButton = CASClothingCategory.gSingleton.mDesignButton;
                DeleteButton.Visible = false;
                ShareButton.Visible = false;
                SaveButton.Visible = false;
                DesignButton.Visible = false;
                CASClothingCategory.gSingleton.mTrashButton = DeleteButton;
                CASClothingCategory.gSingleton.mShareButton = ShareButton;
                CASClothingCategory.gSingleton.mSaveButton = SaveButton;
                CASClothingCategory.gSingleton.mDesignButton = DesignButton;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetButtonState");
            }
        }

        public static void MoveDoneButton()
        {
            try
            {
                if (CASClothing.gSingleton != null)
                {
                    Button DoneButton = CASClothing.gSingleton.GetChildByID(98278400U, true) as Button;

                    if (DoneButton != null && ShouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = -8f;
                        float startingPositionY = 6f;

                        // Calculate the number of visible columns (replace with your actual logic)
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
                }

                if (CASDresserClothing.sClothingLayout != null)
                {
                    Button DoneButton = CASDresserClothing.gSingleton.GetChildByID(98278400U, true) as Button;

                    if (DoneButton != null && ShouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = -8f;
                        float startingPositionY = 6f;

                        // Calculate the number of visible columns (replace with your actual logic)
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
                }

                if (CAPAccessories.gSingleton != null)
                {
                    Button DoneButton = CAPAccessories.gSingleton.GetChildByID(2095900161U, true) as Button;

                    if (DoneButton != null && ShouldMoveDoneButton)
                    {
                        // Define the starting position
                        float startingPositionX = 353f;
                        float startingPositionY = 35f;

                        // Calculate the number of visible columns (replace with your actual logic)
                        int fVisibleColumns = GetVisibleColumns(); // This method should return the number of visible columns

                        // Update the position of the DoneButton
                        DoneButton.Position = new Vector2(startingPositionX + (300 * (fVisibleColumns - 1)), startingPositionY);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "MoveDoneButton");
            }
        }

        // Example method to determine the number of visible columns
        public static int GetVisibleColumns()
        {
            var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
            return (int)VisibleColumns;
        }

        public static void  SetCASClothingSize() //This is responsible for setting window background size. 
        {   
            try
            {
                if (CASClothing.sClothingLayout != null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout == null)
                {
                    Rect CASClothingHeight = CASClothing.gSingleton.Area;
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
            if (!Main.IsNraasMCInstalled || Main.nraasAssembly == null)
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

    }
}

//Vector2 doneButtonPosition = DoneButton.Position;
//// Create a message with the position
//string message = string.Format("DoneButton Position: X = {0}, Y = {1}", doneButtonPosition.x, doneButtonPosition.y);
//// Show the notification
//Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(message, StyledNotification.NotificationStyle.kGameMessageNegative));

//This is for vector2 position
