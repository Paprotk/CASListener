using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;

namespace Arro.MCR
{
    public class ExceptionHandler
    {
        public static string functionErrorName;
        public static Exception exception;
        private static List<string> reportedErrors = new List<string>();

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

            // Generate a unique signature for the exception
            string errorSignature = $"{functionName}|{ex.Message}|{ex.StackTrace}";

            // Check if this error has already been reported
            if (reportedErrors.Contains(errorSignature))
            {
                return; // Suppress duplicate notification
            }

            // Add the error signature to the list of reported errors
            reportedErrors.Add(errorSignature);

            // Show the notification
            ExceptionHandler buttonNotification = new ExceptionHandler();
            buttonNotification.ShowButtonNotification();
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
            // Save the error details to file
            ExceptionHandler.WriteErrorXMLFile(functionErrorName + "_error", exception);

            // Clear the List to allow re-notification for the same error
            reportedErrors.Clear();
        }
    }
}
