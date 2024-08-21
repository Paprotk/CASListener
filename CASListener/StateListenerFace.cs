using System;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using static Arro.CASListener;

namespace Arro
{
    public class Face
    {
        public static void StateListenerFace()
        {
            try
            {
                var HeadEarsLayout = CASHeadEars.sHeadEarsLayout;
                var EyesLayout = CASEyes.sEyesLayout;
                var NoseLayout = CASNose.sNoseLayout;
                var MouthLayout = CASMouth.sMouthLayout;
                var MolesLayout = CASMoles.sMolesLayout;
                var MakeupLayout = CASMakeup.sMakeupLayout;

                if (HeadEarsLayout == null && EyesLayout == null && NoseLayout == null && MouthLayout == null && MolesLayout == null && MakeupLayout == null)
                {
                    //Do nothing
                }
                else
                {
                    SetFaceSizeLong();
                    SetFaceSizeShort();
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
                Rect area = CASFacialDetails.gSingleton.mLongPanel.Area;
                area.Height = CASListener.fFaceWindowSize * TinyUIFixForTS3Integration.getUIScale(); ;
                CASFacialDetails.gSingleton.mLongPanel.Area = area;
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
                Rect area = CASFacialDetails.gSingleton.mShortPanel.Area;
                area.Height = CASListener.fFaceWindowSize * TinyUIFixForTS3Integration.getUIScale();
                CASFacialDetails.gSingleton.mShortPanel.Area = area;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetFaceSizeShort");
            }
        }
    }
}