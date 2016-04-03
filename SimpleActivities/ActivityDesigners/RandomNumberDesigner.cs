using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Activities.Presentation;

namespace SimpleActivities.ActivityDesigners
{
    public partial class RandomNumberDesigner
    {
        public RandomNumberDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(RandomNumber);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(RandomNumberDesigner)));
            //builder.AddCustomAttributes(type, new ActivityDesignerOptionsAttribute { AlwaysCollapseChildren = true, AllowDrillIn = false });
            builder.AddCustomAttributes(type, type.GetProperty("DisplayName"), BrowsableAttribute.No);
        }
    }
}