using Sims3.Gameplay.Objects.Decorations;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
using System;

namespace Arro.MCR
{
    public static class RCConfigure
    {
        public static void Configure()
        {
            try
            {
                if (Clothes.CanMCRClothes)
                {
                    string rows = StringInputDialog.Show("Rows", "Enter a number of rows:", "", true);
                    float r;
                    float.TryParse(rows, out r);
                    if (r < 3)
                    {
                        r = 3;
                    }
                    string cols = StringInputDialog.Show("Columns", "Enter a number of colums:", "", true);
                    float c;
                    float.TryParse(cols, out c);
                    if (c < 1)
                    {
                        c = 1;
                    }
                    Clothes.fVisibleRows = r;
                    Clothes.fVisibleColumns = c;
                    Clothes.SetClothesItemgrid();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Configure");
            }
        }
    }
}
