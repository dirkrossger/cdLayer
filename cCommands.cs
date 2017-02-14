using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Autocad usings
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
#endregion

namespace cdLAYER
{
    public class cCommands
    {
        [CommandMethod("xx")]
        public void LayerOnOff()
        {
            Form1 _Form = new Form1();
            _Form.ShowDialog();

        }
    }
}
