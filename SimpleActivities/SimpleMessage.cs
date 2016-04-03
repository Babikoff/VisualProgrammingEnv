using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime;
using System.Workflow.ComponentModel.Serialization;
using System.Windows.Forms;

namespace SimpleActivities
{
    [ContentProperty("Текст")]
    [Designer(typeof(ActivityDesigners.SimpleMessageDesigner))]
    public sealed class SimpleMessage : CodeActivity
    {
        [DefaultValue(null)]
        public string Заголовок
        { 
            get; 
            set; 
        }

        public string Текст
        { 
            get; 
            set; 
        }

        public SimpleMessage()
        {
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
        }

        protected override void Execute(CodeActivityContext context)
        {
            MessageBox.Show(Текст, Заголовок);
            WorkflowExecutionLog.Write(this, Заголовок + ": " + Текст);
            WorkflowExecutionLog.Write(this, string.Empty);
        }
    }
}
