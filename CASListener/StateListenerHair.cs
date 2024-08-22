using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using static Arro.CASListener;

namespace Arro
{
    public class Hair
    {
        [Tunable]
        public static float fVisibleRows;

        [Tunable]
        public static float fVisibleColumns;

        public static void StateListenerHair()
        {
            try
            {
                var HairLayout = CASHair.sHairLayout;
                var EyebrowsLayout = CASEyebrows.sEyesLayout;
                var BeardLayout = CASBeard.sBeardLayout;
                var BodyHairLayout = CASBodyHair.sBodyHairLayout;

                if (HairLayout != null && EyebrowsLayout == null && BeardLayout == null && BodyHairLayout == null)
                {
                    SetHairItemgrid();
                    SetHairSizeLong();
                    SetHairSizeShort();
                }
                if (HairLayout == null && EyebrowsLayout != null && BeardLayout == null && BodyHairLayout == null)
                {
                    //Eyebrows
                }
                if (HairLayout == null && EyebrowsLayout == null && BeardLayout != null && BodyHairLayout == null)
                {
                    //Beard
                }
                if (HairLayout == null && EyebrowsLayout == null && BeardLayout == null && BodyHairLayout != null)
                {
                    //Bodyhair
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerHair");
            }


        }

        public static void SetHairItemgrid()
        {
            try
            {
                var VisibleRows = CASHair.gSingleton.mHairTypesGrid.VisibleRows;
                var VisibleColumns = CASHair.gSingleton.mHairTypesGrid.VisibleColumns;
                var mGrid = CASHair.gSingleton.mHairTypesGrid.mGrid;
                Rect GridArea = CASHair.gSingleton.mHairTypesGrid.Area;
                if (fVisibleRows > 3)
                {
                    VisibleRows = (uint)fVisibleRows;
                    GridArea.Height = CASHair.gSingleton.mHairTypesGrid.Area.Height + 65f * fVisibleRows;
                    CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, CASHair.gSingleton.mHairTypesGrid.Position.x + (35f * fVisibleRows) + 65f * TinyUIFixForTS3Integration.getUIScale());
                }
                if (fVisibleColumns > 1)
                {
                    VisibleColumns = (uint)fVisibleColumns;
                }
                CASHair.gSingleton.mHairTypesGrid.VisibleRows = VisibleRows;
                CASHair.gSingleton.mHairTypesGrid.Area = GridArea;
                CASHair.gSingleton.mHairTypesGrid.mGrid = mGrid;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetHairItemgrid");
            }
        }

        public static void SetHairSizeLong()
        {
            try
            {
                Rect area = CASPhysical.gSingleton.mLongPanel.Area;
                area.Height = CASListener.fHairWindowSize * TinyUIFixForTS3Integration.getUIScale();
                if (CASHair.sHairLayout is null)
                {
                    //Do nothing
                }
                else
                {
                    //CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, CASListener.fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
                }
                CASPhysical.gSingleton.mLongPanel.Area = area;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetHairSizeLong");
            }

        }
        public static void SetHairSizeShort()
        {
            try
            {

                Rect area = CASPhysical.gSingleton.mShortPanel.Area;
                area.Height = CASListener.fHairWindowSize * TinyUIFixForTS3Integration.getUIScale();
                if (CASHair.sHairLayout is null)
                {
                    //Do nothing
                }
                else
                {
                    //CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, CASListener.fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
                }
                CASPhysical.gSingleton.mShortPanel.Area = area;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetHairSizeLong");
            }

        }
    }
}