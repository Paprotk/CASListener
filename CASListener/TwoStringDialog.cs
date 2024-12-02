using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System.Collections.Generic;

namespace Arro.MCR
{
    public class TwoStringDialogMCR : TwoStringInputDialog
    {
        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText)
        {
            return TwoStringDialogMCR.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), verifyFilename: false);
        }

        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, bool verifyFilename)
        {
            return TwoStringDialogMCR.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), verifyFilename);
        }

        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, Vector2 position, bool verifyFilename)
        {
            if (ModalDialog.EnableModalDialogs)
            {
                using (TwoStringDialogMCR mcrTwoStringInputDialog = new TwoStringDialogMCR(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, position, verifyFilename))
                {
                    Audio.StartSound("ui_window_drop");
                    mcrTwoStringInputDialog.StartModal();
                    return mcrTwoStringInputDialog.Result;
                }
            }
            return null;
        }

        public TwoStringDialogMCR(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, Vector2 position, bool verifyFilename)
            : base("CustomInputDialog", 4096, "", "", "", defaultEntryText, defaultSecondEntryText, oKText, cancelText, position, verifyFilename)
        {
            if (mModalDialogWindow != null)
            {
                Text text = mModalDialogWindow.GetChildByID(7U, true) as Text;
                if (text != null)
                {
                    text.Caption = Localization.LocalizeString("Arro/MCR/Local:ConfigureGrid", new object[0]);
                }
                Text text2 = this.mModalDialogWindow.GetChildByID(1U, true) as Text;
                if (text2 != null)
                {
                    text2.Caption = Localization.LocalizeString("Arro/MCR/Local:RowCount", new object[0]);
                }
                Text text3 = mModalDialogWindow.GetChildByID(6U, true) as Text;
                if (text3 != null)
                {
                    text3.Caption = Localization.LocalizeString("Arro/MCR/Local:ColumnCount", new object[0]);
                }
            }
            mSecondEntryTextEdit.MaxTextLength = 2U;
            mEntryTextEdit.MaxTextLength = 2U;
            mEntryTextEdit.TextValidate += TextValidateNumeric;
            mSecondEntryTextEdit.TextValidate += TextValidateNumeric;
            mModalDialogWindow.TriggerDown += OnTriggerDown;
        }

        private void TextValidateNumeric(WindowBase sender, UITextValidateEventArgs eventArgs)
        {
            int num;
            if (eventArgs.TextChange.Length != 0 && !int.TryParse(eventArgs.TextChange, out num))
            {
                eventArgs.TextValidated = false;
            }
        }
        private void OnTriggerDown(WindowBase sender, UITriggerEventArgs eventArgs)
        {
            switch ((ModalDialog.Triggers)eventArgs.TriggerCode)
            {
                case ModalDialog.Triggers.kOKTrigger:
                    Audio.StartSound("ui_secondary_button");
                    OnTriggerOk();
                    eventArgs.Handled = true;
                    break;
                case ModalDialog.Triggers.kCancelTrigger:
                    Audio.StartSound("ui_secondary_button");
                    OnTriggerCancel();
                    eventArgs.Handled = true;
                    break;
            }
        }

        public new const string kLayoutName = "CustomInputDialog";

        public new const int kWinExportID = 4096;
    }
}
