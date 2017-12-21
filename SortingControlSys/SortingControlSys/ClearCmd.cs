using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortingControlSys.Model;

namespace SortingControlSys
{
    public class ClearCmd
    {

        public ClearModel obj { get; set; }
        public event Action<ClearModel> SwitchHandler;

        public void OnSwitch(Group group)
        {
            group.Write(obj.value, obj.index);
            if (SwitchHandler != null)
            {
                SwitchHandler(obj);
            }
        }
    }

    public class ClearModel
    {
        public string text { get; set; }
        public int line { get; set; }
        public int index { get; set; }
        public bool value { get; set; }
    }
}
