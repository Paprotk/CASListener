using System;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;
using Sims3.UI;

namespace Arro.MCR

{
    public class ArroGridButton : Window
    {
        public ArroGridButton(uint winHandle) : base(winHandle)
        {
        }

        public override void Init()
        {
            mConfigureButton = (base.GetChildByID(98291479U, true) as Button);
            GridButtonHook();
        }

        public static bool Load()
        {
            ResourceKey resKey = ResourceKey.CreateUILayoutKey("CASPetCreationDEBUG", 0U);
            ArroGridButton.sLayout = UIManager.LoadLayoutAndAddToWindow(resKey, UICategory.CAS);
            if (ArroGridButton.sLayout != null)
            {
                ArroGridButton.sInstance = (ArroGridButton.sLayout.GetWindowByExportID(1) as ArroGridButton);
                if (ArroGridButton.sInstance != null)
                {
                    return true;
                }
                ArroGridButton.sLayout.Shutdown();
                ArroGridButton.sLayout.Dispose();
                ArroGridButton.sLayout = null;
            }
            return false;
        }

        public static void Unload()
        {
            if (ArroGridButton.sInstance != null)
            {
                ArroGridButton.sInstance.Visible = false;
                ArroGridButton.sInstance = null;
            }
            if (ArroGridButton.sLayout != null)
            {
                ArroGridButton.sLayout.Shutdown();
                ArroGridButton.sLayout.Dispose();
                ArroGridButton.sLayout = null;
            }
        }

        public static void Show()
        {
            if (ArroGridButton.sInstance != null)
            {
                ArroGridButton.sInstance.Visible = true;
            }
        }

        public static void Hide()
        {
            if (ArroGridButton.sInstance != null)
            {
                ArroGridButton.sInstance.Visible = false;
            }
        }
        public static void GridButtonHook()
        {
            string tooltipText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
            mConfigureButton.TooltipText = tooltipText;
            mConfigureButton.MouseUp += OnGridClick;
        }
        public static void OnGridClick(WindowBase sender, UIMouseEventArgs args)
        {
            try
            {
                if (args.MouseKey == MouseKeys.kMouseLeft)
                {
                    Simulator.AddObject(new OneShotFunctionTask(Configure.Clothes, StopWatch.TickStyles.Milliseconds, 1f));
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnSortClick");
            }
        }

        public static Button mConfigureButton;

        public static Layout sLayout;

        public static ArroGridButton sInstance;
    }
}