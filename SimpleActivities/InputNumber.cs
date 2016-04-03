using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Markup;
using System.Workflow.ComponentModel.Serialization;

namespace SimpleActivities
{
    [ContentProperty("Число")]
    [Designer(typeof(ActivityDesigners.InputNumberDesigner))]
    public sealed class InputNumber : CodeActivity
    {
        public string Надпись
        {
            get;
            set;
        }

        [DefaultValue(0)]
        [RequiredArgument]
        public InOutArgument<Int32> Число
        { 
            get; 
            set; 
        }

        public InputNumber()
        {
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            RuntimeArgument runtimeArgument1 = new RuntimeArgument("Число", typeof(Int32), ArgumentDirection.InOut);
            metadata.Bind((Argument)this.Число, runtimeArgument1);
            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>()
              {
                runtimeArgument1,
              });
        }

        protected override void Execute(CodeActivityContext context)
        {
            var inputIntForm = new InputIntForm();
            inputIntForm.Number = Число.Get((ActivityContext)context);
            inputIntForm.Message = Надпись;
            string logOutput;

            var res = inputIntForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                Число.Set(context, inputIntForm.Number);
                logOutput = inputIntForm.Message + ": " + inputIntForm.Number;
            }
            else
            {
                logOutput = "Число не было введено";
            }

            WorkflowExecutionLog.Write(this, logOutput);
            WorkflowExecutionLog.Write(this, string.Empty);
        }
    }
}
