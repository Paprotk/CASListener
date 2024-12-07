using Sims3.Gameplay.Objects.Decorations;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;


namespace Arro.MCR
{
    public class Configure
    {
        public static bool isDialogActive;
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static void Clothes()
        {
            try
            {
                if (isDialogActive) return;

                isDialogActive = true;

                List<string> result = TwoStringDialogMCR.Show(
                Localization.LocalizeString("Arro/MCR/Local:ConfigureGrid", new object[0]),
                Localization.LocalizeString("Arro/MCR/Local:RowCount", new object[0]),
                Localization.LocalizeString("Arro/MCR/Local:ColumnCount", new object[0]),
                MCR.Clothes.fVisibleRows.ToString(),
                MCR.Clothes.fVisibleColumns.ToString(),
                Localization.LocalizeString("Ui/Caption/Global:Accept", new object[0]),
                Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]),
                new Vector2(-1f, -1f), false
                );

                if (result != null && result.Count == 2)
                {
                    float.TryParse(result[0], out float rows);
                    rows = Clamp(rows, 3, 16);

                    float.TryParse(result[1], out float columns);
                    columns = Clamp(columns, 1, 16);

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
                string titleText = Localization.LocalizeString("Arro/MCR/Local:ConfigureGrid", new object[0]);
                string[] promptText = new string[]
                {
                    Localization.LocalizeString("Arro/MCR/Local:RowCount", new object[0]),
                    Localization.LocalizeString("Arro/MCR/Local:ColumnCount", new object[0]),
                    Localization.LocalizeString("Arro/MCR/Local:SliderCount", new object[0]),
                };
                string[] defaultEntryText = new string[]
                {
                    Arro.MCR.Face.fVisibleRows.ToString(),
                    Arro.MCR.Face.fVisibleColumns.ToString(),
                    Arro.MCR.Face.fVisibleSliders.ToString()
                };

                string[] result = ThreeStringInputDialog.Show(titleText, promptText, defaultEntryText, true);

                Arro.MCR.Face.fVisibleRows = ParseInput(result[0]);
                Arro.MCR.Face.fVisibleColumns = ParseInput(result[1]);
                Arro.MCR.Face.fVisibleSliders = ParseInput(result[2]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ConfigureFace");
            }
        }

        private static int ParseInput(string input)
        {
            return string.IsNullOrEmpty(input) ? 0 : int.TryParse(input, out int result) ? result : 0;
        }
    }
}