using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using static Arro.CASListener;

namespace Arro
{
    public class Clothes
    {
        [Tunable]
        public static float fVisibleRows;

        public static void StateListenerClothes()
        {
            try
            {
                var ClothesLayout = CASClothingCategory.sClothingCategoryLayout;

                if (ClothesLayout == null)
                {
                    //Do nothing
                }
                else
                {
                    SetClothesItemgrid();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerClothes");
            }
        }
        public static void SetClothesItemgrid()
        {
            try
            {
                var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                Rect GridArea = CASClothingCategory.gSingleton.mClothingTypesGrid.Area;
                Rect CASClothingHeight = CASClothing.gSingleton.Area;
                var DeleteButton = CASClothingCategory.gSingleton.mTrashButton;
                var ShareButton = CASClothingCategory.gSingleton.mShareButton;
                var SaveButton = CASClothingCategory.gSingleton.mSaveButton;
                var DesignButton = CASClothingCategory.gSingleton.mDesignButton;
                VisibleRows = (uint)fVisibleRows;
                GridArea.Height = 814;
                CASClothingHeight.Height = 951f;
                DeleteButton.Visible = false;
                ShareButton.Visible = false;
                SaveButton.Visible = false;
                DesignButton.Visible = false;
                CASClothingCategory.gSingleton.mTrashButton = DeleteButton;
                CASClothingCategory.gSingleton.mShareButton = ShareButton;
                CASClothingCategory.gSingleton.mSaveButton = SaveButton;
                CASClothingCategory.gSingleton.mDesignButton = DesignButton;
                CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows = VisibleRows;
                CASClothingCategory.gSingleton.mClothingTypesGrid.Area = GridArea;
                CASClothing.gSingleton.Area = CASClothingHeight;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetClothesItemgrid");
            }
        }
    }
}