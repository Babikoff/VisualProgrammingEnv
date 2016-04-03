using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleActivities
{
    public delegate void WorkflowOutputDelegate(object sender, string text);

    public static class WorkflowExecutionLog
    {
        public static event WorkflowOutputDelegate WorkflowOutputEvent;

        public static void Write(object sender, string text)
        {
            if (WorkflowOutputEvent != null)
            {
                WorkflowOutputEvent(sender, text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}
