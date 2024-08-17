using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public enum ElementControlType : int
    {
        // Below elements has stationary class name
        [System.ComponentModel.Description("Button")]
        Button = 0,
        [System.ComponentModel.Description("CheckBox")]
        CheckBox = 1,
        [System.ComponentModel.Description("ComboBox")]
        ComboBox = 2,
        [System.ComponentModel.Description("Edit")]
        TextBox = 3,
        [System.ComponentModel.Description("Image")]
        Image = 4,
        [System.ComponentModel.Description("ListItem")]
        ListBoxItem = 5,
        [System.ComponentModel.Description("List")]
        ListBox = 6,
        [System.ComponentModel.Description("Menu")]
        Menu = 7,
        [System.ComponentModel.Description("MenuItem")]
        MenuItem = 8,
        [System.ComponentModel.Description("ProgressBar")]
        ProgressBar = 9,
        [System.ComponentModel.Description("RadioButton")]
        RadioButton = 10,
        [System.ComponentModel.Description("ScrollBar")]
        ScrollBar = 11,
        [System.ComponentModel.Description("Tab")]
        TabControl = 12,
        [System.ComponentModel.Description("TabItem")]
        TabItem = 13,
        [System.ComponentModel.Description("Text")]
        TextBlock = 14,
        [System.ComponentModel.Description("ToolBar")]
        ToolBar = 15,
        [System.ComponentModel.Description("ToolTip")]
        ToolTip = 16,
        [System.ComponentModel.Description("Tree")]
        TreeView = 17,
        [System.ComponentModel.Description("TreeItem")]
        TreeViewItem = 18,
        [System.ComponentModel.Description("Window")]
        Window = 19,
        [System.ComponentModel.Description("Pane")]
        ScrollViewer = 20,
        [System.ComponentModel.Description("DatePicker")]
        DatePicker = 21,
        [System.ComponentModel.Description("Calendar")]
        Calendar = 22,
        [System.ComponentModel.Description("DayButton")]
        CalendarDayButton = 23,
        [System.ComponentModel.Description("CalendarButton")]
        CalendarButton = 24,
        [System.ComponentModel.Description("Separator")]
        Separator = 25,

        // Below element has varying or no class name
        [System.ComponentModel.Description("Header")]
        Header = 26,
        [System.ComponentModel.Description("HeaderItem")]
        HeaderItem = 27,
        [System.ComponentModel.Description("DataGrid")]
        DataGrid = 28,
        [System.ComponentModel.Description("DataItem")]
        DataItem = 29,
        [System.ComponentModel.Description("Thumb")]
        Thumb = 30,
        [System.ComponentModel.Description("Group")]
        Group = 31,
        [System.ComponentModel.Description("Custom")]
        Custom = 32,
    }
}
