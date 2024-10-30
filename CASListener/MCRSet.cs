using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Objects;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using System;
using System.Collections.Generic;

namespace Arro.MCR
{
    public static class RCConfigure
    {
        public static void Configure()
        {
            try
            {
                if (Main.CanMCRClothes)
                {
                    var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                    var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
                    string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                    string promptText = Localization.LocalizeString("Arro/MCR/Local:2", new object[0]);
                    string secondPromptText = Localization.LocalizeString("Arro/MCR/Local:3", new object[0]);
                    string defaultEntryText = VisibleRows.ToString();
                    string defaultSecondEntryText = VisibleColumns.ToString();
                    string oKText = "OK";
                    string cancelText = Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]);

                    List<string> result = TwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
                    // Check if the dialog was canceled
                    if (result != null && result.Count == 2)
                    {
                        float r;
                        float.TryParse(result[0], out r);
                        if (r < 3 || r == 3)
                        {
                            r = 3;
                        }

                        float c;
                        float.TryParse(result[1], out c);
                        if (c < 1 || c == 1)
                        {
                            c = 1;
                        }
                        ShouldUpdate = true;
                        Clothes.ShouldMoveDoneButton = true;
                        Clothes.fVisibleRows = r;
                        Clothes.fVisibleColumns = c;
                    }
                }
                else
                {
                    string NotificationInfo = Localization.LocalizeString("Arro/MCR/Local:4", new object[0]);
                    Sims3.UI.StyledNotification.Show(new Sims3.UI.StyledNotification.Format(NotificationInfo, StyledNotification.NotificationStyle.kGameMessageNegative));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Configure");
            }
        }
        public static bool ShouldUpdate = false;
    }
}
//Trying to make modal window with smaller text area size
