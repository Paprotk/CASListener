using System;
using Arro.CASListener;
using Sims3.SimIFace;
using Sims3.UI.CAS;

namespace Arro
{
    public class Hair : Task
    {
        [Tunable]
        public static float fHairWindowSize;
        [Tunable]
        public static float fHairColorsPosition;

        public override void Simulate()
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
                if (CASPhysical.gSingleton.mLongPanel != null)
                {
                    Rect area = CASPhysical.gSingleton.mLongPanel.Area;
                    area.Height = fHairWindowSize * TinyUIFixForTS3Integration.getUIScale();
                    if (CASHair.sHairLayout is null)
                    {
                        //Do nothing
                    }
                    else
                    {
                        CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
                    }
                    CASPhysical.gSingleton.mLongPanel.Area = area;
                }

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
                if (CASPhysical.gSingleton.mShortPanel != null)
                {
                    Rect area = CASPhysical.gSingleton.mShortPanel.Area;
                    area.Height = fHairWindowSize * TinyUIFixForTS3Integration.getUIScale();
                    if (CASHair.sHairLayout is null)
                    {
                        //Do nothing
                    }
                    else
                    {
                        CASHair.gSingleton.mHairColorPanel.Position = new Vector2(CASHair.gSingleton.mHairColorPanel.Position.x, fHairColorsPosition * TinyUIFixForTS3Integration.getUIScale());
                    }
                    CASPhysical.gSingleton.mShortPanel.Area = area;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetHairSizeLong");
            }

        }
    }
}