using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime;
using System.Windows;
using System.Windows.Forms;
using System.Workflow.ComponentModel.Serialization;

namespace SimpleActivities
{
    [Designer(typeof(ActivityDesigners.RandomNumberDesigner))]
    public sealed class RandomNumber : CodeActivity
    {
        [RequiredArgument]
        public OutArgument<Int32> Число
        {
            get;
            set;
        }

        [RequiredArgument]
        public InArgument<Int32> От
        {
            get;
            set;
        }

        [RequiredArgument]
        public InArgument<Int32> До
        {
            get;
            set;
        }

        public RandomNumber()
        {
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            RuntimeArgument runtimeArgument1 = new RuntimeArgument("Число", typeof(Int32), ArgumentDirection.Out);
            metadata.Bind((Argument)this.Число, runtimeArgument1);
            RuntimeArgument runtimeArgument2 = new RuntimeArgument("От", typeof(Int32), ArgumentDirection.In);
            metadata.Bind((Argument)this.От, runtimeArgument2);
            RuntimeArgument runtimeArgument3 = new RuntimeArgument("До", typeof(Int32), ArgumentDirection.In);
            metadata.Bind((Argument)this.До, runtimeArgument3);
            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>()
              {
                runtimeArgument1,
                runtimeArgument2,
                runtimeArgument3,
              });
        }

        protected override void Execute(CodeActivityContext context)
        {
            var random = new Random();
            var from = От.Get((ActivityContext)context);
            var to = До.Get((ActivityContext)context);
            var randomNumber = random.Next(from, to);
            Число.Set(context, randomNumber);
            
            WorkflowExecutionLog.Write(this, "Сгенерировано случайное число: " + randomNumber);
            WorkflowExecutionLog.Write(this, string.Empty);
        }
    }
}
