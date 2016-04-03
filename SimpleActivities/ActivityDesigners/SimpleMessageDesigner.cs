using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace SimpleActivities.ActivityDesigners
{
    public partial class SimpleMessageDesigner
    {
        public SimpleMessageDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(SimpleMessage);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(SimpleMessageDesigner)));
        }
    }
}