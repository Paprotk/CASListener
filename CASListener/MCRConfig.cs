using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.CAS;
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
                if (Main.canMCRClothes)
                {
                    if (isDialogActive)
                    {
                        return;
                    }
                    isDialogActive = true;
                    var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                    var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
                    string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                    string promptText = Localization.LocalizeString("Arro/MCR/Local:2", new object[0]);
                    string secondPromptText = Localization.LocalizeString("Arro/MCR/Local:3", new object[0]);
                    string defaultEntryText = VisibleRows.ToString();
                    string defaultSecondEntryText = VisibleColumns.ToString();
                    string oKText = "Ok";
                    string cancelText = Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]);

                    List<string> result = TwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
                    // Check if the dialog was canceled
                    if (result != null && result.Count == 2)
                    {
                        float rows;
                        float.TryParse(result[0], out rows);
                        if (rows < 3)
                        {
                            rows = 3;
                        }

                        float columns;
                        float.TryParse(result[1], out columns);
                        if (columns < 1)
                        {
                            columns = 1;
                        }
                        if (rows != MCR.Clothes.fVisibleRows || columns != MCR.Clothes.fVisibleColumns)
                        {
                            shouldUpdate = true;
                            MCR.Clothes.shouldMoveDoneButton = true;
                            MCR.Clothes.fVisibleRows = rows;
                            MCR.Clothes.fVisibleColumns = columns;
                        }
                    }
                    isDialogActive = false;
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
        public static void Face()
        {
            try
            {
                if (Main.canMCRFace)
                {
                    if (isDialogActive)
                    {
                        return;
                    }
                    isDialogActive = true;
                    var VisibleRows = Arro.MCR.Face.fVisibleRows;
                    var VisibleColumns = Arro.MCR.Face.fVisibleColumns;
                    string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                    string promptText = Localization.LocalizeString("Arro/MCR/Local:2", new object[0]);
                    string secondPromptText = Localization.LocalizeString("Arro/MCR/Local:3", new object[0]);
                    string defaultEntryText = VisibleRows.ToString();
                    string defaultSecondEntryText = VisibleColumns.ToString();
                    string oKText = "Ok";
                    string cancelText = Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]);

                    List<string> result = TwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
                    // Check if the dialog was canceled
                    if (result != null && result.Count == 2)
                    {
                        float rows;
                        float.TryParse(result[0], out rows);
                        if (rows < 3)
                        {
                            rows = 3;
                        }

                        float columns;
                        float.TryParse(result[1], out columns);
                        if (columns < 3)
                        {
                            columns = 3;
                        }
                        if (rows != MCR.Face.fVisibleRows || columns != MCR.Face.fVisibleColumns)
                        {
                            Arro.MCR.Face.shouldupdate = true;
                            //MCR.Clothes.shouldMoveDoneButton = true;
                            MCR.Face.fVisibleRows = rows;
                            MCR.Face.fVisibleColumns = columns;
                        }
                    }
                    isDialogActive = false;
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
        public static void Hair()
        {
            try
            {
                if (Main.canMCRHair)
                {
                    if (isDialogActive)
                    {
                        return;
                    }
                    isDialogActive = true;
                    var VisibleRows = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleRows;
                    var VisibleColumns = CASClothingCategory.gSingleton.mClothingTypesGrid.VisibleColumns;
                    string titleText = Localization.LocalizeString("Arro/MCR/Local:1", new object[0]);
                    string promptText = Localization.LocalizeString("Arro/MCR/Local:2", new object[0]);
                    string secondPromptText = Localization.LocalizeString("Arro/MCR/Local:3", new object[0]);
                    string defaultEntryText = VisibleRows.ToString();
                    string defaultSecondEntryText = VisibleColumns.ToString();
                    string oKText = "Ok";
                    string cancelText = Localization.LocalizeString("Ui/Caption/QuitDialog:Cancel", new object[0]);

                    List<string> result = TwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
                    // Check if the dialog was canceled
                    if (result != null && result.Count == 2)
                    {
                        float rows;
                        float.TryParse(result[0], out rows);
                        if (rows < 3)
                        {
                            rows = 3;
                        }

                        float columns;
                        float.TryParse(result[1], out columns);
                        if (columns < 1)
                        {
                            columns = 1;
                        }
                        if (rows != MCR.Clothes.fVisibleRows || columns != MCR.Clothes.fVisibleColumns)
                        {
                            shouldUpdate = true;
                            MCR.Clothes.shouldMoveDoneButton = true;
                            MCR.Clothes.fVisibleRows = rows;
                            MCR.Clothes.fVisibleColumns = columns;
                        }
                    }
                    isDialogActive = false;
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
        public static bool shouldUpdate = false;
        public static bool isDialogActive = false;
    }
}