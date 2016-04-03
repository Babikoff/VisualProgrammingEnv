using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace SimpleActivities.ActivityDesigners
{
    public partial class ExpressionMessageDesigner
    {
        public ExpressionMessageDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(ExpressionMessage);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(ExpressionMessageDesigner)));
        }
    }
}