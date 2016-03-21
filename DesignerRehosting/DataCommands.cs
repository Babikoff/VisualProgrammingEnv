using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignerRehosting
{
    public class DataCommands
    {
        private static RoutedUICommand _commandRun;
        public static RoutedUICommand CommandRun { get { return _commandRun; } }

        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F5, ModifierKeys.Control, "F5"));
            inputs.Add(new KeyGesture(Key.F9, ModifierKeys.Control, "F9"));

            _commandRun = new RoutedUICommand("Run", "Run", typeof(DataCommands), inputs);
        }
    }
}
