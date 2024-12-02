using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Arro.MCR
{
    // Token: 0x020000AB RID: 171
    public class CustomTwoStringInputDialog : ModalDialog
    {
        // Token: 0x0600053C RID: 1340 RVA: 0x00026D10 File Offset: 0x00024F10
        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText)
        {
            return CustomTwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), false);
        }

        // Token: 0x0600053D RID: 1341 RVA: 0x00026D3C File Offset: 0x00024F3C
        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, bool verifyFilename)
        {
            return CustomTwoStringInputDialog.Show(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, new Vector2(-1f, -1f), verifyFilename);
        }

        // Token: 0x0600053E RID: 1342 RVA: 0x00026D6C File Offset: 0x00024F6C
        public static List<string> Show(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, Vector2 position, bool verifyFilename)
        {
            if (ModalDialog.EnableModalDialogs)
            {
                using (CustomTwoStringInputDialog twoStringInputDialog = new CustomTwoStringInputDialog(titleText, promptText, secondPromptText, defaultEntryText, defaultSecondEntryText, oKText, cancelText, position, verifyFilename))
                {
                    twoStringInputDialog.StartModal();
                    return twoStringInputDialog.Result;
                }
            }
            return null;
        }

        // Token: 0x0600053F RID: 1343 RVA: 0x00026DC0 File Offset: 0x00024FC0
        public CustomTwoStringInputDialog(string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, Vector2 position, bool verifyFilename) : base("CustomInputDialog", 4096, true, ModalDialog.PauseMode.PauseSimulator, null, "ui_window_drop", "ui_hardwindow_close")
        {
            if (this.mModalDialogWindow != null)
            {
                Text text = this.mModalDialogWindow.GetChildByID(7U, true) as Text;
                if (text != null)
                {
                    text.Caption = titleText;
                }
                Text text2 = this.mModalDialogWindow.GetChildByID(1U, true) as Text;
                if (text2 != null)
                {
                    text2.Caption = promptText;
                }
                Text text3 = this.mModalDialogWindow.GetChildByID(6U, true) as Text;
                if (text3 != null)
                {
                    text3.Caption = secondPromptText;
                }
                this.mEntryTextEdit = (this.mModalDialogWindow.GetChildByID(2U, true) as TextEdit);
                this.mEntryTextEdit.MaxTextLength = 2U;
                if (defaultEntryText != null && defaultEntryText != "")
                {
                    this.mEntryTextEdit.Caption = defaultEntryText;
                    this.mEntryTextEdit.Enabled = true;
                    this.mEntryTextEdit.TextStyle = 103722498U;
                }
                else
                {
                    this.mEntryTextEdit.MouseDown += CustomTwoStringInputDialog.OnTextEditFirstMouseDown;
                    this.mEntryTextEdit.Enabled = false;
                }
                this.mEntryTextEdit.TextChange += this.OnTextEditChange;
                this.mEntryTextEdit.TextAccept += this.OnTextEditAccept;
                this.mEntryTextEdit.TextValidate += this.OnTextEditValidate;
                this.mEntryTextEdit.TextEditTab += this.OnTextEditTab;
                this.mSecondEntryTextEdit = (this.mModalDialogWindow.GetChildByID(5U, true) as TextEdit);
                this.mSecondEntryTextEdit.MaxTextLength = 2U;
                if (defaultSecondEntryText != null && defaultSecondEntryText != "")
                {
                    this.mSecondEntryTextEdit.Caption = defaultSecondEntryText;
                    this.mSecondEntryTextEdit.Enabled = true;
                    this.mSecondEntryTextEdit.TextStyle = 103722498U;
                }
                else
                {
                    this.mSecondEntryTextEdit.MouseDown += CustomTwoStringInputDialog.OnTextEditFirstMouseDown;
                    this.mSecondEntryTextEdit.Enabled = false;
                }
                this.mSecondEntryTextEdit.TextEditTab += this.OnTextEditTab;
                Rect area = this.mModalDialogWindow.Area;
                float num = area.BottomRight.x - area.TopLeft.x;
                float num2 = area.BottomRight.y - area.TopLeft.y;
                float num3 = position.x;
                float num4 = position.y;
                if (num3 < 0f && num4 < 0f)
                {
                    Rect area2 = this.mModalDialogWindow.Parent.Area;
                    float num5 = area2.BottomRight.x - area2.TopLeft.x;
                    float num6 = area2.BottomRight.y - area2.TopLeft.y;
                    num3 = (float)Math.Round((double)((num5 - num) / 2f));
                    num4 = (float)Math.Round((double)((num6 - num2) / 2f));
                }
                area.Set(num3, num4, num3 + num, num4 + num2);
                this.mModalDialogWindow.Area = area;
                this.mVerifyFilename = verifyFilename;
                this.mInvalidCharacterWindow = this.mModalDialogWindow.GetChildByID(16U, true);
                this.mAcceptButton = (this.mModalDialogWindow.GetChildByID(3U, true) as Button);
                if (this.mAcceptButton != null)
                {
                    this.mAcceptButton.Click += this.OnButtonClick;
                    this.mAcceptButton.TooltipText = oKText;
                    this.mAcceptButton.Enabled = !this.mVerifyFilename;
                    if (this.mVerifyFilename && this.mEntryTextEdit.Enabled)
                    {
                        this.mAcceptButton.Enabled = GameUtils.IsValidFilename(this.mEntryTextEdit.Caption);
                    }
                }
                Button button = this.mModalDialogWindow.GetChildByID(4U, true) as Button;
                if (button != null)
                {
                    button.Click += this.OnButtonClick;
                    button.TooltipText = cancelText;
                }
                this.mModalDialogWindow.CloseButtonPressed += this.OnCloseButtonClick;
                this.mModalDialogWindow.Tick += this.OnTick;
                base.OkayID = 3U;
                base.CancelID = 4U;
                base.SelectedID = 2U;
            }
        }

        // Token: 0x06000540 RID: 1344 RVA: 0x00027204 File Offset: 0x00025404
        public CustomTwoStringInputDialog(string layoutOverride, int exportIDOverride, string titleText, string promptText, string secondPromptText, string defaultEntryText, string defaultSecondEntryText, string oKText, string cancelText, Vector2 position, bool verifyFilename) : base(layoutOverride, exportIDOverride, true, ModalDialog.PauseMode.PauseSimulator, null, "ui_window_drop", "ui_hardwindow_close")
        {
            if (this.mModalDialogWindow != null)
            {
                Text text = this.mModalDialogWindow.GetChildByID(7U, true) as Text;
                if (text != null)
                {
                    text.Caption = titleText;
                }
                Text text2 = this.mModalDialogWindow.GetChildByID(1U, true) as Text;
                if (text2 != null)
                {
                    text2.Caption = promptText;
                }
                Text text3 = this.mModalDialogWindow.GetChildByID(6U, true) as Text;
                if (text3 != null)
                {
                    text3.Caption = secondPromptText;
                }
                this.mEntryTextEdit = (this.mModalDialogWindow.GetChildByID(2U, true) as TextEdit);
                this.mEntryTextEdit.MaxTextLength = 2U;
                if (defaultEntryText != null && defaultEntryText != "")
                {
                    this.mEntryTextEdit.Caption = defaultEntryText;
                    this.mEntryTextEdit.Enabled = true;
                    this.mEntryTextEdit.TextStyle = 103722498U;
                }
                else
                {
                    this.mEntryTextEdit.MouseDown += CustomTwoStringInputDialog.OnTextEditFirstMouseDown;
                    this.mEntryTextEdit.Enabled = false;
                }
                this.mEntryTextEdit.TextChange += this.OnTextEditChange;
                this.mEntryTextEdit.TextAccept += this.OnTextEditAccept;
                this.mEntryTextEdit.TextValidate += this.OnTextEditValidate;
                this.mEntryTextEdit.TextEditTab += this.OnTextEditTab;
                this.mSecondEntryTextEdit = (this.mModalDialogWindow.GetChildByID(5U, true) as TextEdit);
                this.mSecondEntryTextEdit.MaxTextLength = 2U;
                if (defaultSecondEntryText != null && defaultSecondEntryText != "")
                {
                    this.mSecondEntryTextEdit.Caption = defaultSecondEntryText;
                    this.mSecondEntryTextEdit.Enabled = true;
                    this.mSecondEntryTextEdit.TextStyle = 103722498U;
                }
                else
                {
                    this.mSecondEntryTextEdit.MouseDown += CustomTwoStringInputDialog.OnTextEditFirstMouseDown;
                    this.mSecondEntryTextEdit.Enabled = false;
                }
                this.mSecondEntryTextEdit.TextEditTab += this.OnTextEditTab;
                Rect area = this.mModalDialogWindow.Area;
                float num = area.BottomRight.x - area.TopLeft.x;
                float num2 = area.BottomRight.y - area.TopLeft.y;
                float num3 = position.x;
                float num4 = position.y;
                if (num3 < 0f && num4 < 0f)
                {
                    Rect area2 = this.mModalDialogWindow.Parent.Area;
                    float num5 = area2.BottomRight.x - area2.TopLeft.x;
                    float num6 = area2.BottomRight.y - area2.TopLeft.y;
                    num3 = (float)Math.Round((double)((num5 - num) / 2f));
                    num4 = (float)Math.Round((double)((num6 - num2) / 2f));
                }
                area.Set(num3, num4, num3 + num, num4 + num2);
                this.mModalDialogWindow.Area = area;
                this.mVerifyFilename = verifyFilename;
                this.mInvalidCharacterWindow = this.mModalDialogWindow.GetChildByID(16U, true);
                this.mAcceptButton = (this.mModalDialogWindow.GetChildByID(3U, true) as Button);
                if (this.mAcceptButton != null)
                {
                    this.mAcceptButton.Click += this.OnButtonClick;
                    this.mAcceptButton.TooltipText = oKText;
                    this.mAcceptButton.Enabled = !this.mVerifyFilename;
                    if (this.mVerifyFilename && this.mEntryTextEdit.Enabled)
                    {
                        this.mAcceptButton.Enabled = GameUtils.IsValidFilename(this.mEntryTextEdit.Caption);
                    }
                }
                Button button = this.mModalDialogWindow.GetChildByID(4U, true) as Button;
                if (button != null)
                {
                    button.Click += this.OnButtonClick;
                    button.TooltipText = cancelText;
                }
                this.mModalDialogWindow.CloseButtonPressed += this.OnCloseButtonClick;
                this.mModalDialogWindow.Tick += this.OnTick;
                base.OkayID = 3U;
                base.CancelID = 4U;
                base.SelectedID = 2U;
            }
        }

        // Token: 0x06000541 RID: 1345 RVA: 0x00027640 File Offset: 0x00025840
        public void OnTextEditTab(WindowBase sender, UIEventArgs args)
        {
            if (sender == this.mEntryTextEdit)
            {
                if (!this.mSecondEntryTextEdit.Enabled)
                {
                    CustomTwoStringInputDialog.OnTextEditFirstMouseDown(this.mSecondEntryTextEdit, null);
                }
                UIManager.SetFocus(InputContext.kICKeyboard, this.mSecondEntryTextEdit);
                this.mSecondEntryTextEdit.CursorIndex = (uint)this.mSecondEntryTextEdit.Caption.Length;
                return;
            }
            if (sender == this.mSecondEntryTextEdit)
            {
                if (!this.mEntryTextEdit.Enabled)
                {
                    CustomTwoStringInputDialog.OnTextEditFirstMouseDown(this.mEntryTextEdit, null);
                }
                UIManager.SetFocus(InputContext.kICKeyboard, this.mEntryTextEdit);
                this.mEntryTextEdit.CursorIndex = (uint)this.mEntryTextEdit.Caption.Length;
            }
        }

        // Token: 0x06000542 RID: 1346 RVA: 0x000276EC File Offset: 0x000258EC
        public void OnTextEditValidate(WindowBase sender, UITextValidateEventArgs args)
        {
            if (!this.mVerifyFilename)
            {
                args.TextValidated = true;
                return;
            }
            Regex regex = new Regex("&|;|\\{|\\}");
            bool flag = regex.IsMatch(sender.Caption) || !GameUtils.HasValidFilenameChars(sender.Caption);
            args.TextValidated = !flag;
            this.mInvalidCharacterWindow.Visible = !args.TextValidated;
            if (args.TextValidated)
            {
                this.mAcceptButton.Enabled = (GameUtils.IsValidFilename(sender.Caption) || sender.Caption.Trim() == "");
            }
        }

        // Token: 0x06000543 RID: 1347 RVA: 0x000052C8 File Offset: 0x000034C8
        public void OnTextEditChange(WindowBase sender, UITextChangeEventArgs args)
        {
            if (!this.mVerifyFilename)
            {
                this.mAcceptButton.Enabled = true;
                return;
            }
            this.mAcceptButton.Enabled = GameUtils.IsValidFilename(sender.Caption);
        }

        // Token: 0x06000544 RID: 1348 RVA: 0x000052F5 File Offset: 0x000034F5
        public void OnTextEditAccept(WindowBase sender, UITextEditAcceptEventArgs args)
        {
            this.mInvalidCharacterWindow.Visible = false;
        }

        // Token: 0x06000545 RID: 1349 RVA: 0x0002778C File Offset: 0x0002598C
        public void OnTick(WindowBase sender, UIEventArgs eventArgs)
        {
            TextEdit textEdit = this.mModalDialogWindow.GetChildByID(2U, true) as TextEdit;
            if (textEdit != null)
            {
                uint length = (uint)textEdit.Caption.Length;
                textEdit.AnchorIndex = length;
                textEdit.CursorIndex = length;
                UIManager.SetFocus(InputContext.kICKeyboard, textEdit);
                textEdit.HideCaret = false;
            }
            this.mModalDialogWindow.Tick -= this.OnTick;
        }

        // Token: 0x06000546 RID: 1350 RVA: 0x000277F4 File Offset: 0x000259F4
        public static void OnTextEditFirstMouseDown(WindowBase sender, UIMouseEventArgs eventArgs)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit != null)
            {
                textEdit.Enabled = true;
                textEdit.Caption = string.Empty;
                textEdit.TextStyle = 103722498U;
                if (eventArgs != null)
                {
                    UIManager.SetFocus(InputContext.kICKeyboard, textEdit);
                }
                textEdit.MouseDown -= CustomTwoStringInputDialog.OnTextEditFirstMouseDown;
            }
        }

        // Token: 0x17000113 RID: 275
        // (get) Token: 0x06000547 RID: 1351 RVA: 0x00005303 File Offset: 0x00003503
        public List<string> Result
        {
            get
            {
                return this.mResult;
            }
        }

        // Token: 0x06000548 RID: 1352 RVA: 0x0000501F File Offset: 0x0000321F
        public void OnCloseButtonClick(WindowBase sender, UIDialogFinishedEventArgs eventArgs)
        {
            eventArgs.Handled = true;
            this.EndDialog(4U);
        }

        // Token: 0x06000549 RID: 1353 RVA: 0x0000502F File Offset: 0x0000322F
        public void OnButtonClick(WindowBase sender, UIButtonClickEventArgs eventArgs)
        {
            eventArgs.Handled = true;
            this.EndDialog(sender.ID);
        }

        // Token: 0x0600054A RID: 1354 RVA: 0x0002784C File Offset: 0x00025A4C
        public override void OnTriggerForward()
        {
            switch (base.SelectedID)
            {
                case 2U:
                    base.SelectedID = 5U;
                    return;
                case 3U:
                    base.SelectedID = 4U;
                    return;
                case 4U:
                    base.SelectedID = 2U;
                    return;
                case 5U:
                    base.SelectedID = 3U;
                    return;
                default:
                    return;
            }
        }

        // Token: 0x0600054B RID: 1355 RVA: 0x00027898 File Offset: 0x00025A98
        public override void OnTriggerBackward()
        {
            switch (base.SelectedID)
            {
                case 2U:
                    base.SelectedID = 4U;
                    return;
                case 3U:
                    base.SelectedID = 5U;
                    return;
                case 4U:
                    base.SelectedID = 3U;
                    return;
                case 5U:
                    base.SelectedID = 2U;
                    return;
                default:
                    return;
            }
        }

        // Token: 0x0600054C RID: 1356 RVA: 0x000278E4 File Offset: 0x00025AE4
        public override void OnTriggerControl(uint controlID)
        {
            if (controlID == 2U)
            {
                WindowBase childByID = this.mModalDialogWindow.GetChildByID(2U, true);
                if (childByID != null)
                {
                    ConsoleHost.DisplaySoftKeyboard(this.mModalDialogWindow.WinHandle, childByID.WinHandle);
                }
            }
        }

        // Token: 0x0600054D RID: 1357 RVA: 0x00027924 File Offset: 0x00025B24
        public override bool OnEnd(uint buttonID)
        {
            if (buttonID == 3U)
            {
                this.mResult = new List<string>();
                TextEdit textEdit = this.mModalDialogWindow.GetChildByID(2U, true) as TextEdit;
                if (textEdit.Enabled)
                {
                    this.mResult.Add(textEdit.Caption);
                }
                else
                {
                    this.mResult.Add("");
                }
                textEdit = (this.mModalDialogWindow.GetChildByID(5U, true) as TextEdit);
                if (textEdit.Enabled)
                {
                    this.mResult.Add(textEdit.Caption);
                }
                else
                {
                    this.mResult.Add("");
                }
            }
            return true;
        }

        // Token: 0x04000298 RID: 664
        public const string kLayoutName = "CustomInputDialog";

        // Token: 0x04000299 RID: 665
        public const int kWinExportID = 4096;

        // Token: 0x0400029A RID: 666
        public const uint kMaxFilenameLength = 2U;

        // Token: 0x0400029B RID: 667
        public const uint kMaxDescriptionLength = 2U;

        // Token: 0x0400029C RID: 668
        public TextEdit mEntryTextEdit;

        // Token: 0x0400029D RID: 669
        public TextEdit mSecondEntryTextEdit;

        // Token: 0x0400029E RID: 670
        public Button mAcceptButton;

        // Token: 0x0400029F RID: 671
        public bool mVerifyFilename;

        // Token: 0x040002A0 RID: 672
        public WindowBase mInvalidCharacterWindow;

        // Token: 0x040002A1 RID: 673
        public List<string> mResult;

        // Token: 0x020000AC RID: 172
        public enum ControlID : uint
        {
            // Token: 0x040002A3 RID: 675
            kPromptTextID = 1U,
            // Token: 0x040002A4 RID: 676
            kInputTextEditID,
            // Token: 0x040002A5 RID: 677
            kOKButtonID,
            // Token: 0x040002A6 RID: 678
            kCancelButtonID,
            // Token: 0x040002A7 RID: 679
            kSecondInputTextEditID,
            // Token: 0x040002A8 RID: 680
            kSecondPromptTextID,
            // Token: 0x040002A9 RID: 681
            kTitleTextID,
            // Token: 0x040002AA RID: 682
            kInvalidCharacterWindowID = 16U
        }
    }
}
