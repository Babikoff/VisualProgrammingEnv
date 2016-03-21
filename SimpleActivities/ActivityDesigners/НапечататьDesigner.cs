using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace SimpleActivities.ActivityDesigners
{
    public partial class НапечататьDesigner
    {
        public НапечататьDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(Напечатать);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(НапечататьDesigner)));
            //builder.AddCustomAttributes(type, type.GetProperty("To"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("From"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Subject"), BrowsableAttribute.No);
            //builder.AddCustomAttributes(type, type.GetProperty("Host"), BrowsableAttribute.No);
        }
    }
}