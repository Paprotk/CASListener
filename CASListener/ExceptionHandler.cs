using System;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;

namespace Arro
{
    public class ExceptionHandler
    {
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
            string functionErrorName = functionName;
            ExceptionHandler.WriteErrorXMLFile(functionErrorName + "_error", ex);
            string errorOccurred = "Error occurred while executing " + functionErrorName + ". Saved exception info to The Sims 3 folder.";
            StyledNotification.Format format = new StyledNotification.Format(errorOccurred, StyledNotification.NotificationStyle.kGameMessagePositive);
            StyledNotification.Show(format, "arro_error_icon");
        }
    }
}