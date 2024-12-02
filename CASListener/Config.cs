using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
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
    }
}