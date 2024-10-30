using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using Arro.MCR;
//Currently unused
namespace Arro.MCR
{
    public class Face : Task
    {
        [Tunable]
        public static float fFaceWindowSize;

        public override void Simulate()
        {
            try
            {
                if (CASFacialDetails.gSingleton != null)
                {
                    Main.CanMCRFace = true;

                    var HeadEarsLayout = CASHeadEars.sHeadEarsLayout;
                    var EyesLayout = CASEyes.sEyesLayout;
                    var NoseLayout = CASNose.sNoseLayout;
                    var MouthLayout = CASMouth.sMouthLayout;
                    var MolesLayout = CASMoles.sMolesLayout;
                    var MakeupLayout = CASMakeup.sMakeupLayout;

                    if (HeadEarsLayout == null && EyesLayout == null && NoseLayout == null && MouthLayout == null && MakeupLayout == null)
                    {
                    }
                    else
                    {
                        SetFaceSizeLong();
                        SetFaceSizeShort();
                    }
                }
                else
                {
                    Main.CanMCRFace = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StateListenerFace");
            }
        }

        public static void SetFaceSizeLong()
        {
            try
            {
                if (CASFacialDetails.gSingleton.mLongPanel != null)
                {
                    Rect area = CASFacialDetails.gSingleton.mLongPanel.Area;
                    area.Height = fFaceWindowSize * TinyUIFixForTS3Integration.getUIScale();
                    CASFacialDetails.gSingleton.mLongPanel.Area = area;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetFaceSizeLong");
            }

        }
        public static void SetFaceSizeShort()
        {
            try
            {
                if (CASFacialDetails.gSingleton.mShortPanel != null)
                {
                    Rect area = CASFacialDetails.gSingleton.mShortPanel.Area;
                    area.Height = fFaceWindowSize * TinyUIFixForTS3Integration.getUIScale();
                    CASFacialDetails.gSingleton.mShortPanel.Area = area;
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetFaceSizeShort");
            }
        }
    }
}