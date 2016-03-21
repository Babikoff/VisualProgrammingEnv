using System;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime;
//using System.Windows.Markup;
using System.Workflow.ComponentModel.Serialization;
using System.Windows.Forms;

namespace SimpleActivities
{
    [ContentProperty("Сообщение")]
    [Designer(typeof(ActivityDesigners.СообщениеDesigner))]
    public sealed class Сообщение : CodeActivity
    {
        [DefaultValue(null)]
        public InArgument<object> Заголовок
        { 
            get; 
            set; 
        }

        public InArgument<object> Текст
        { 
            get; 
            set; 
        }

        public Сообщение()
        {
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            RuntimeArgument runtimeArgument1 = new RuntimeArgument("Заголовок", typeof(object), ArgumentDirection.In);
            metadata.Bind((Argument)this.Заголовок, runtimeArgument1);
            RuntimeArgument runtimeArgument2 = new RuntimeArgument("Текст", typeof(object), ArgumentDirection.In);
            metadata.Bind((Argument)this.Текст, runtimeArgument2);
            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>()
              {
                runtimeArgument1,
                runtimeArgument2
              });
        }

        protected override void Execute(CodeActivityContext context)
        {
            var caption = Заголовок.Get((ActivityContext)context).ToString();
            var text = Текст.Get((ActivityContext)context).ToString();
            MessageBox.Show(text, caption);
            Console.WriteLine(caption);
            Console.WriteLine(text);
        }
    }
}
