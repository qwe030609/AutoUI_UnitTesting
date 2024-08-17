using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PP5AutoUITests
{
    public class KeysConverter
    {
        ////
        //// 摘要:
        ////     Represents the NUL keystroke.
        //public static readonly string Null = Convert.ToString(Convert.ToChar(57344, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Cancel keystroke.
        //public static readonly string Cancel = Convert.ToString(Convert.ToChar(57345, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Help keystroke.
        //public static readonly string Help = Convert.ToString(Convert.ToChar(57346, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Backspace key.
        //public static readonly string Backspace = Convert.ToString(Convert.ToChar(57347, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Tab key.
        //public static readonly string Tab = Convert.ToString(Convert.ToChar(57348, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Clear keystroke.
        //public static readonly string Clear = Convert.ToString(Convert.ToChar(57349, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Return key.
        //public static readonly string Return = Convert.ToString(Convert.ToChar(57350, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Enter key.
        //public static readonly string Enter = Convert.ToString(Convert.ToChar(57351, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Shift key.
        //public static readonly string Shift = Convert.ToString(Convert.ToChar(57352, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Shift key.
        //public static readonly string LeftShift = Convert.ToString(Convert.ToChar(57352, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Control key.
        //public static readonly string Control = Convert.ToString(Convert.ToChar(57353, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Control key.
        //public static readonly string LeftControl = Convert.ToString(Convert.ToChar(57353, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Alt key.
        //public static readonly string Alt = Convert.ToString(Convert.ToChar(57354, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Alt key.
        //public static readonly string LeftAlt = Convert.ToString(Convert.ToChar(57354, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Pause key.
        //public static readonly string Pause = Convert.ToString(Convert.ToChar(57355, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Escape key.
        //public static readonly string Escape = Convert.ToString(Convert.ToChar(57356, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Spacebar key.
        //public static readonly string Space = Convert.ToString(Convert.ToChar(57357, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Page Up key.
        //public static readonly string PageUp = Convert.ToString(Convert.ToChar(57358, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Page Down key.
        //public static readonly string PageDown = Convert.ToString(Convert.ToChar(57359, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the End key.
        //public static readonly string End = Convert.ToString(Convert.ToChar(57360, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Home key.
        //public static readonly string Home = Convert.ToString(Convert.ToChar(57361, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the left arrow key.
        //public static readonly string Left = Convert.ToString(Convert.ToChar(57362, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the left arrow key.
        //public static readonly string ArrowLeft = Convert.ToString(Convert.ToChar(57362, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the up arrow key.
        //public static readonly string Up = Convert.ToString(Convert.ToChar(57363, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the up arrow key.
        //public static readonly string ArrowUp = Convert.ToString(Convert.ToChar(57363, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the right arrow key.
        //public static readonly string Right = Convert.ToString(Convert.ToChar(57364, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the right arrow key.
        //public static readonly string ArrowRight = Convert.ToString(Convert.ToChar(57364, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Left arrow key.
        //public static readonly string Down = Convert.ToString(Convert.ToChar(57365, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Left arrow key.
        //public static readonly string ArrowDown = Convert.ToString(Convert.ToChar(57365, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Insert key.
        //public static readonly string Insert = Convert.ToString(Convert.ToChar(57366, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the Delete key.
        //public static readonly string Delete = Convert.ToString(Convert.ToChar(57367, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the semi-colon key.
        //public static readonly string Semicolon = Convert.ToString(Convert.ToChar(57368, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the equal sign key.
        //public static readonly string Equal = Convert.ToString(Convert.ToChar(57369, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 0 key.
        //public static readonly string NumberPad0 = Convert.ToString(Convert.ToChar(57370, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 1 key.
        //public static readonly string NumberPad1 = Convert.ToString(Convert.ToChar(57371, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 2 key.
        //public static readonly string NumberPad2 = Convert.ToString(Convert.ToChar(57372, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 3 key.
        //public static readonly string NumberPad3 = Convert.ToString(Convert.ToChar(57373, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 4 key.
        //public static readonly string NumberPad4 = Convert.ToString(Convert.ToChar(57374, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 5 key.
        //public static readonly string NumberPad5 = Convert.ToString(Convert.ToChar(57375, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 6 key.
        //public static readonly string NumberPad6 = Convert.ToString(Convert.ToChar(57376, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 7 key.
        //public static readonly string NumberPad7 = Convert.ToString(Convert.ToChar(57377, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 8 key.
        //public static readonly string NumberPad8 = Convert.ToString(Convert.ToChar(57378, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad 9 key.
        //public static readonly string NumberPad9 = Convert.ToString(Convert.ToChar(57379, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad multiplication key.
        //public static readonly string Multiply = Convert.ToString(Convert.ToChar(57380, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad addition key.
        //public static readonly string Add = Convert.ToString(Convert.ToChar(57381, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad thousands separator key.
        //public static readonly string Separator = Convert.ToString(Convert.ToChar(57382, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad subtraction key.
        //public static readonly string Subtract = Convert.ToString(Convert.ToChar(57383, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad decimal separator key.
        //public static readonly string Decimal = Convert.ToString(Convert.ToChar(57384, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the number pad division key.
        //public static readonly string Divide = Convert.ToString(Convert.ToChar(57385, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F1.
        //public static readonly string F1 = Convert.ToString(Convert.ToChar(57393, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F2.
        //public static readonly string F2 = Convert.ToString(Convert.ToChar(57394, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F3.
        //public static readonly string F3 = Convert.ToString(Convert.ToChar(57395, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F4.
        //public static readonly string F4 = Convert.ToString(Convert.ToChar(57396, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F5.
        //public static readonly string F5 = Convert.ToString(Convert.ToChar(57397, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F6.
        //public static readonly string F6 = Convert.ToString(Convert.ToChar(57398, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F7.
        //public static readonly string F7 = Convert.ToString(Convert.ToChar(57399, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F8.
        //public static readonly string F8 = Convert.ToString(Convert.ToChar(57400, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F9.
        //public static readonly string F9 = Convert.ToString(Convert.ToChar(57401, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F10.
        //public static readonly string F10 = Convert.ToString(Convert.ToChar(57402, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F11.
        //public static readonly string F11 = Convert.ToString(Convert.ToChar(57403, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key F12.
        //public static readonly string F12 = Convert.ToString(Convert.ToChar(57404, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key META.
        //public static readonly string Meta = Convert.ToString(Convert.ToChar(57405, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ////
        //// 摘要:
        ////     Represents the function key COMMAND.
        //public static readonly string Command = Convert.ToString(Convert.ToChar(57405, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        static KeysConverter Instance;

        public KeysConverter() { }

        public static KeysConverter GetInstance()
        {
            Instance ??= new KeysConverter();
            return Instance;
        }

        private ConcurrentDictionary<string, string> Descriptions;

        //
        // 摘要:
        //     Gets the description of a specific key.
        //
        // 參數:
        //   value:
        //     The key value for which to get the description.
        //
        // 傳回:
        //     The description of the key.
        public string GetDescription(string keystroke)
        {
            if (Descriptions == null)
            {
                Descriptions = new ConcurrentDictionary<string, string>();
                Descriptions.TryAdd(Keys.Null, "Null");
                Descriptions.TryAdd(Keys.Cancel, "Cancel");
                Descriptions.TryAdd(Keys.Help, "Help");
                Descriptions.TryAdd(Keys.Backspace, "Backspace");
                Descriptions.TryAdd(Keys.Tab, "Tab");
                Descriptions.TryAdd(Keys.Clear, "Clear");
                Descriptions.TryAdd(Keys.Return, "Return");
                Descriptions.TryAdd(Keys.Enter, "Enter");
                Descriptions.TryAdd(Keys.Shift, "Shift");
                Descriptions.TryAdd(Keys.Control, "Control");
                Descriptions.TryAdd(Keys.Alt, "Alt");
                Descriptions.TryAdd(Keys.Pause, "Pause");
                Descriptions.TryAdd(Keys.Escape, "Escape");
                Descriptions.TryAdd(Keys.Space, "Space");
                Descriptions.TryAdd(Keys.PageUp, "Page Up");
                Descriptions.TryAdd(Keys.PageDown, "PageDown");
                Descriptions.TryAdd(Keys.End, "End");
                Descriptions.TryAdd(Keys.Home, "Home");
                Descriptions.TryAdd(Keys.Left, "Left");
                Descriptions.TryAdd(Keys.Up, "Up");
                Descriptions.TryAdd(Keys.Right, "Right");
                Descriptions.TryAdd(Keys.Down, "Down");
                Descriptions.TryAdd(Keys.Insert, "Insert");
                Descriptions.TryAdd(Keys.Delete, "Delete");
                Descriptions.TryAdd(Keys.Semicolon, "Semicolon");
                Descriptions.TryAdd(Keys.Equal, "Equal");
                Descriptions.TryAdd(Keys.NumberPad0, "Number Pad 0");
                Descriptions.TryAdd(Keys.NumberPad1, "Number Pad 1");
                Descriptions.TryAdd(Keys.NumberPad2, "Number Pad 2");
                Descriptions.TryAdd(Keys.NumberPad3, "Number Pad 3");
                Descriptions.TryAdd(Keys.NumberPad4, "Number Pad 4");
                Descriptions.TryAdd(Keys.NumberPad5, "Number Pad 5");
                Descriptions.TryAdd(Keys.NumberPad6, "Number Pad 6");
                Descriptions.TryAdd(Keys.NumberPad7, "Number Pad 7");
                Descriptions.TryAdd(Keys.NumberPad8, "Number Pad 8");
                Descriptions.TryAdd(Keys.NumberPad9, "Number Pad 9");
                Descriptions.TryAdd(Keys.Multiply, "Multiply");
                Descriptions.TryAdd(Keys.Add, "Add");
                Descriptions.TryAdd(Keys.Separator, "Separator");
                Descriptions.TryAdd(Keys.Subtract, "Subtract");
                Descriptions.TryAdd(Keys.Decimal, "Decimal");
                Descriptions.TryAdd(Keys.Divide, "Divide");
                Descriptions.TryAdd(Keys.F1, "F1");
                Descriptions.TryAdd(Keys.F2, "F2");
                Descriptions.TryAdd(Keys.F3, "F3");
                Descriptions.TryAdd(Keys.F4, "F4");
                Descriptions.TryAdd(Keys.F5, "F5");
                Descriptions.TryAdd(Keys.F6, "F6");
                Descriptions.TryAdd(Keys.F7, "F7");
                Descriptions.TryAdd(Keys.F8, "F8");
                Descriptions.TryAdd(Keys.F9, "F9");
                Descriptions.TryAdd(Keys.F10, "F10");
                Descriptions.TryAdd(Keys.F11, "F11");
                Descriptions.TryAdd(Keys.F12, "F12");
                Descriptions.TryAdd(Keys.Meta, "Meta");
                Descriptions.TryAdd(Keys.Command, "Command");
            }

            if (Descriptions.ContainsKey(keystroke))
            {
                return Descriptions[keystroke];
            }
            return "Unknown Keystroke";
        }
    }
}
