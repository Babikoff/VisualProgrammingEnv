using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Activities.Presentation;

namespace SimpleActivities.ActivityDesigners
{
    public partial class InputExpressionNumberDesigner
    {
        public InputExpressionNumberDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(InputExpressionNumber);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(InputExpressionNumberDesigner)));
        }
    }
}