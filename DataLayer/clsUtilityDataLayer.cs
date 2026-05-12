using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataLayer
{
    
    internal class clsUtilityDataLayer
    {

        private static string SourceName = "DVLD_Project";
        public static void LogError(Exception ex)
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            EventLog.WriteEntry(SourceName, $"Error: {ex.Message}\nStack Trace: {ex.StackTrace}", EventLogEntryType.Error);
        }

       
    }
}
