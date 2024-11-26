using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;

namespace Arro.MCR
{
    public class Configure
    {
        public static void Clothes()
        {
            try
            {
                if (isDialogActive) return;

                isDialogActive = true;
                string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                string promptText = Localization.LocalizeString("Arro/MCR/Local:2", new object[0]);
                string secondPromptText = Localization.LocalizeString("Arro/MCR/Local:3", new object[0]);
                string defaultEntryText = MCR.Clothes.fVisibleRows.ToString();
                string defaultSecondEntryText = MCR.Clothes.fVisibleColumns.ToString();
                string oKText = "Ok";
                string cancelText = Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]);

                List<string> result = TwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
                // Check if the dialog was canceled
                if (result != null && result.Count == 2)
                {
                    float.TryParse(result[0], out float rows);
                    if (rows < 3)
                    {
                        rows = 3;
                    }
                    else if (rows > 16)
                    {
                        rows = 16;
                    }

                    float.TryParse(result[1], out float columns);
                    if (columns < 1)
                    {
                        columns = 1;
                    }
                    else if (columns > 16)
                    {
                        columns = 16;
                    }
                    if (rows != MCR.Clothes.fVisibleRows || columns != MCR.Clothes.fVisibleColumns)
                    {
                        CASHook.SetBool(false, false, false);
                        MCR.Clothes.fVisibleRows = rows;
                        MCR.Clothes.fVisibleColumns = columns;
                    }
                }
                isDialogActive = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ConfigureClothes");
            }
        }
        public static void Face()
        {
            try
            {
                if (isDialogActive)
                {
                    return;
                }
                isDialogActive = true;
                string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                string[] promptText = new string[]
                {
                    Localization.LocalizeString("Arro/MCR/Local:2", new object[0]),
                    Localization.LocalizeString("Arro/MCR/Local:3", new object[0]),
                    "Set slider count",
                };
                string[] defaultEntryText = new string[]
                {
                    MCR.Face.fVisibleRows.ToString(),
                    MCR.Face.fVisibleColumns.ToString(),
                    MCR.Face.fVisibleSliders.ToString(),
                };
                bool numbersOnly = true;

                string[] result = ThreeStringInputDialog.Show(titleText, promptText, defaultEntryText, numbersOnly);

                int visibleRows = string.IsNullOrEmpty(result[0])
                    ? 0
                    : int.TryParse(result[0], out visibleRows)
                        ? Math.Max(visibleRows, 3)
                        : 0;
                MCR.Face.fVisibleRows = Math.Max(visibleRows, 3);

                int visibleColumns = string.IsNullOrEmpty(result[1])
                    ? 0
                    : int.TryParse(result[1], out visibleColumns)
                        ? Math.Max(visibleColumns, 3)
                        : 0;
                MCR.Face.fVisibleColumns = Math.Max(visibleColumns, 3);

                int visibleSliders = string.IsNullOrEmpty(result[2])
                    ? 0
                    : int.TryParse(result[2], out visibleSliders)
                        ? Math.Max(visibleSliders, 3)
                        : 0;
                MCR.Face.fVisibleSliders = Math.Max(visibleSliders, 3);
                isDialogActive = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ConfigureFace");
            }
        }
        public static bool shouldUpdate = false;
        public static bool isDialogActive = false;
    }
}