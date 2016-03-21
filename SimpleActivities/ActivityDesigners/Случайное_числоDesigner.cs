using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Activities.Presentation;

namespace SimpleActivities.ActivityDesigners
{
    public partial class Случайное_числоDesigner
    {
        public Случайное_числоDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(Случайное_число);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(Случайное_числоDesigner)));
            //builder.AddCustomAttributes(type, new ActivityDesignerOptionsAttribute { AlwaysCollapseChildren = true, AllowDrillIn = false });
            //builder.AddCustomAttributes(type, type.GetProperty("To"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("From"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Subject"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Host"), BrowsableAttribute.No);
        }
    }
}