using System;
using Sims3.Gameplay;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Sims3.UI.CAS.CAP;
using Arro.MCR;

namespace Arro.MCR
{
    public class Clothes : Task
    {
        [PersistableStatic(true)]
        public static float fVisibleRows;

        [PersistableStatic(true)]
        public static float fVisibleColumns;

        public override void Simulate()
        {
            try
            {
                var ClothesLayout = CASClothingCategory.sClothingCategoryLayout;

                if (ClothesLayout != null)
                {
                    Main.CanMCRClothes = true;
                    SetClothesItemgrid();
                }
                else
                {
                    Main.CanMCRClothes = false;
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
                if (fVisibleRows > 3)
                {
                    VisibleRows = (uint)fVisibleRows;
                    GridArea.Height = (139f * fVisibleRows) * TinyUIFixForTS3Integration.getUIScale();
                }
                if (fVisibleColumns > 1)
                {VisibleColumns = (uint)fVisibleColumns;
                    GridArea.Width = (305f * fVisibleColumns + 15f) * TinyUIFixForTS3Integration.getUIScale();
                }
                CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows = VisibleRows;
                CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns = VisibleColumns;
                CASClothingCategory.gSingleton.mClothingTypesGrid.Area = GridArea;
                SetCASClothingSize();
                SetButtonState();
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
        public static void  SetCASClothingSize() //This is responsible for setting window background size. 
        {   
            try
            {
                if (CASClothing.sClothingLayout != null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout == null)
                {
                    Rect CASClothingHeight = CASClothing.gSingleton.Area;
                    if (fVisibleRows > 3)
                    {
                        CASClothingHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns > 1)
                    {
                        CASClothingHeight.Width = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    CASClothing.gSingleton.Area = CASClothingHeight;
                }
                if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout != null && CAPAccessories.sCAPAccessoriesLayout == null)
                {
                    Rect CASDresserClothingHeight = CASDresserClothing.gSingleton.Area;
                    if (fVisibleRows > 3)
                    {
                        CASDresserClothingHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns > 1)
                    {
                        CASDresserClothingHeight.Width = (300f * fVisibleColumns + 109f) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    CASDresserClothing.gSingleton.Area = CASDresserClothingHeight;
                }
                if (CASClothing.sClothingLayout == null && CASDresserClothing.sClothingLayout == null && CAPAccessories.sCAPAccessoriesLayout != null)
                {
                    Rect CAPAccessoriesHeight = CAPAccessories.gSingleton.Area;
                    if (fVisibleRows > 3)
                    {
                        CAPAccessoriesHeight.Height = (534f + (139f * (fVisibleRows - 3))) * TinyUIFixForTS3Integration.getUIScale();
                    }
                    if (fVisibleColumns > 1)
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
    }
}