using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using static Arro.CASListener;

namespace Arro
{
    public class Hair
    {
        public static void StateListenerHair()
        {
            try
            {
                var HairLayout = CASHair.sHairLayout;
                var EyebrowsLayout = CASEyebrows.sEyesLayout;
                var BeardLayout = CASBeard.sBeardLayout;
                var BodyHairLayout = CASBodyHair.sBodyHairLayout;

                if (HairLayout == null && EyebrowsLayout == null && BeardLayout == null && BodyHairLayout == null)
                {
                    //Do nothing
                }
                else
                {
                    SetHairSizeLong();
                    SetHairSizeShort();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerHair");
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
                    CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, CASListener.fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
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
                    CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, CASListener.fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
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