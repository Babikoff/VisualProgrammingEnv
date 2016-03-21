using System;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace SimpleActivities.ActivityDesigners
{
    public partial class СообщениеDesigner
    {
        public СообщениеDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            Type type = typeof(Сообщение);
            builder.AddCustomAttributes(type, new DesignerAttribute(typeof(НапечататьDesigner)));
        }
    }
}