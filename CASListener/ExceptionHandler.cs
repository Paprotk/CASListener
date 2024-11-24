using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;

namespace Arro.MCR
{
    public class ExceptionHandler
    {
        public static string functionErrorName;
        public static Exception exception;

        public static void WriteErrorXMLFile(string fileName, Exception errorToPrint)
        {
            uint num = 0u;
            string s = Simulator.CreateExportFile(ref num, fileName);

            if (num != 0)
            {
                CustomXmlWriter customXmlWriter = new CustomXmlWriter(num);
                customXmlWriter.WriteToBuffer(errorToPrint.ToString());
                customXmlWriter.WriteEndDocument();
            }
        }
        public static void HandleException(Exception ex, string functionName)
        {
            functionErrorName = functionName;
            exception = ex;
            ExceptionHandler buttonNotification = new ExceptionHandler();
            buttonNotification.ShowButtonNotification(); // This will invoke the notification
        }
        public void ShowButtonNotification()
        {
            string titleText = "Error occurred while executing " + functionErrorName + ". Click button below to save exception info to The Sims 3 folder.";
            StyledNotification.Format format = new StyledNotification.Format(
                titleText, // Notification text
                "=^..^=", // Button text
                ButtonCallback,
                StyledNotification.NotificationStyle.kSystemMessage
            );
            format.mCloseOnCallback = true;
            StyledNotification.Show(format, "arro_error_icon");
        }
        public void ButtonCallback()
        {
            ExceptionHandler.WriteErrorXMLFile(functionErrorName + "_error", exception);
            return;
        }
    }
}
