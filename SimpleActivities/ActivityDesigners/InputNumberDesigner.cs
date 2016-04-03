using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Activities.Presentation;

namespace SimpleActivities.ActivityDesigners
{
    public partial class InputNumberDesigner
    {
        public InputNumberDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(InputNumber);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(InputNumberDesigner)));
            //builder.AddCustomAttributes(type, new ActivityDesignerOptionsAttribute { AlwaysCollapseChildren = true, AllowDrillIn = false });
            //builder.AddCustomAttributes(type, type.GetProperty("To"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("From"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Subject"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Host"), BrowsableAttribute.No);
        }
    }
}