﻿using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime;
//using System.Windows.Markup;
using System.Workflow.ComponentModel.Serialization;

namespace SimpleActivities
{
    [ContentProperty("Напечатать")]
    [Designer(typeof(ActivityDesigners.НапечататьDesigner))]
    public sealed class Напечатать : CodeActivity
    {
        [DefaultValue(null)]
        public InArgument<object> Печатать
        { 
            get; 
            set; 
        }

        public bool Перевод_строки
        {
            get;
            set;
        }

        public Напечатать()
        {
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            RuntimeArgument runtimeArgument1 = new RuntimeArgument("Печатать", typeof(object), ArgumentDirection.In);
            metadata.Bind((Argument)this.Печатать, runtimeArgument1);
            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>()
              {
                runtimeArgument1,
              });
        }

        protected override void Execute(CodeActivityContext context)
        {
            var text = Печатать.Get((ActivityContext)context).ToString();

            if (Перевод_строки)
                WorkflowExecutionLog.Write(this, text);
            else
                WorkflowExecutionLog.Write(this, text);

            WorkflowExecutionLog.Write(this, string.Empty);
        }
    }
}
